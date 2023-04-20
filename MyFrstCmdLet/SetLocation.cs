
using System.Collections.Generic;
using System.Management.Automation;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;


namespace MyFrstCmdLet
{
    [Cmdlet(VerbsCommon.Set,
            "LocationX",
            DefaultParameterSetName = PathParameterSet,
            SupportsTransactions = true,
            HelpUri = "https://go.microsoft.com/fwlink/?LinkID=2097049")]
    [OutputType(typeof(PathInfo), typeof(PathInfoStack))]
    public class SetLocationXCommand : PSCmdlet
    {
        #region Command parameters

        private const string PathParameterSet = "Path";
        private const string LiteralPathParameterSet = "LiteralPath";
        private const string StackParameterSet = "Stack";

        [Parameter(Position = 0,
                   ParameterSetName = PathParameterSet,
                   ValueFromPipeline = true,
                   ValueFromPipelineByPropertyName = true)]
        public string Path
        {
            get => _path;
            set => _path = value;
        }

        [Parameter(ParameterSetName = LiteralPathParameterSet,
                   Mandatory = true,
                   ValueFromPipeline = false,
                   ValueFromPipelineByPropertyName = true)]
        [Alias("PSPath", "LP")]
        public string LiteralPath
        {
            get => _path;
            set
            {
                _path = value;
                //base.SuppressWildcardExpansion = true;
            }
        }

        [Parameter] 
        public SwitchParameter PassThru //parameter sem valor !!!
        {
            get => _passThrough;
            set => _passThrough = value;
        }

        [Parameter(ParameterSetName = StackParameterSet,
                   ValueFromPipelineByPropertyName = true)]
        public string StackName { get; set; }

        #endregion Command parameters

        #region Command data

        /// <summary>
        /// The filter used when doing a dir.
        /// </summary>
        private string _path = string.Empty;

        /// <summary>
        /// Determines if output should be passed through for
        /// set-location.
        /// </summary>
        private bool _passThrough;

        #endregion Command data

        #region Command code

        /// <summary>
        /// The functional part of the code that does the changing of the current
        /// working directory.
        /// </summary>
        protected override void ProcessRecord()
        {
            object result = null;

            // WriteObject("Parameter:" + this.ParameterSetName);

            //Set-LocationX [[-Path] <string>] [-PassThru] [<CommonParameters>]
            //Set-LocationX -LiteralPath<string>[-PassThru] [< CommonParameters >]
            //Set-LocationX [-PassThru] [-StackName<string>] [< CommonParameters >]

            switch (this.ParameterSetName)
            {
                case PathParameterSet:
                case LiteralPathParameterSet:
                    try
                    {
                        // Change the current working directory
                        if (string.IsNullOrEmpty(Path))
                        {
                            // If user just typed 'cd', go to FileSystem provider home directory
                            //Path = SessionState.Internal.GetSingleProvider(Commands.FileSystemProvider.ProviderName).Home;
                        }

                        //result = SessionState.Path.SetLocation(Path, CmdletProviderContext, ParameterSetName == LiteralPathParameterSet);
                    }
                    catch (PSNotSupportedException notSupported)
                    {
                        WriteError(
                            new ErrorRecord(
                                notSupported.ErrorRecord,
                                notSupported));
                    }
                    catch (DriveNotFoundException driveNotFound)
                    {
                        WriteError(
                            new ErrorRecord(
                                driveNotFound.ErrorRecord,
                                driveNotFound));
                    }
                    catch (ProviderNotFoundException providerNotFound)
                    {
                        WriteError(
                            new ErrorRecord(
                                providerNotFound.ErrorRecord,
                                providerNotFound));
                    }
                    catch (ItemNotFoundException pathNotFound)
                    {
                        WriteError(
                            new ErrorRecord(
                                pathNotFound.ErrorRecord,
                                pathNotFound));
                    }
                    catch (PSArgumentException argException)
                    {
                        WriteError(
                            new ErrorRecord(
                                argException.ErrorRecord,
                                argException));
                    }

                    break;

                case StackParameterSet:

                    try
                    {
                        // Change the default location stack
                        //result = SessionState.Path.SetDefaultLocationStack(StackName);
                    }
                    catch (ItemNotFoundException itemNotFound)
                    {
                        WriteError(
                            new ErrorRecord(
                                itemNotFound.ErrorRecord,
                                itemNotFound));
                    }

                    break;

                default:
                    //Dbg.Diagnostics.Assert(
                    //    false,
                    //    "One of the specified parameter sets should have been called");
                    break;
            }

            if (_passThrough && result != null)
            {
                WriteObject(result);
            }
        }

        #endregion Command code

    }
}