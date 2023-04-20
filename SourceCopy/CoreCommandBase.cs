using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;
using System.Threading.Tasks;

//using System.Management.Automation;

namespace MyFrstCmdLet
{
    #region CoreCommandBase

    /// <summary>
    /// The base command for the core commands.
    /// </summary>
    public abstract class CoreCommandBase : PSCmdlet //, IDynamicParameters
    {
        #region Protected members

        //internal virtual CmdletProviderContext CmdletProviderContext
        //{
        //    get
        //    {
        //        CmdletProviderContext coreCommandContext = new(this);

        //        coreCommandContext.Force = Force;

        //        Collection<string> includeFilter =
        //            SessionStateUtilities.ConvertArrayToCollection<string>(Include);

        //        Collection<string> excludeFilter =
        //            SessionStateUtilities.ConvertArrayToCollection<string>(Exclude);

        //        coreCommandContext.SetFilters(includeFilter, excludeFilter, Filter);
        //        coreCommandContext.SuppressWildcardExpansion = SuppressWildcardExpansion;
        //        coreCommandContext.DynamicParameters = RetrievedDynamicParameters;
        //        stopContextCollection.Add(coreCommandContext);

        //        return coreCommandContext;
        //    }
        //}

        internal virtual SwitchParameter SuppressWildcardExpansion
        {
            get => _suppressWildcardExpansion;
            set => _suppressWildcardExpansion = value;
        }

        private bool _suppressWildcardExpansion;

        //internal virtual object GetDynamicParameters(CmdletProviderContext context) => null;

        protected virtual bool ProviderSupportsShouldProcess => true;

        protected bool DoesProviderSupportShouldProcess(string[] paths)
        {
            bool result = true;

            //if (paths != null)
            //{
            //    foreach (string path in paths)
            //    {
            //        ProviderInfo provider = null;
            //        PSDriveInfo drive = null;

            //        // I don't really care about the returned path, just the provider name
            //        SessionState.Path.GetUnresolvedProviderPathFromPSPath(
            //            path,
            //            this.CmdletProviderContext,
            //            out provider,
            //            out drive);

            //        // Check the provider's capabilities

            //        if (!CmdletProviderManagementIntrinsics.CheckProviderCapabilities(
            //                ProviderCapabilities.ShouldProcess,
            //                provider))
            //        {
            //            result = false;
            //            break;
            //        }
            //    }
            //}

            return result;
        }

        protected internal object RetrievedDynamicParameters => _dynamicParameters;

        private object _dynamicParameters;

        #endregion Protected members

        #region Public members

        protected override void StopProcessing()
        {
            //foreach (CmdletProviderContext stopContext in stopContextCollection)
            //{
            //    stopContext.StopProcessing();
            //}
        }

        //internal Collection<CmdletProviderContext> stopContextCollection = new();

        public virtual string Filter { get; set; }

        public virtual string[] Include
        {
            get;
            set;
        } = Array.Empty<string>();

        public virtual string[] Exclude
        {
            get;
            set;
        } = Array.Empty<string>();

        public virtual SwitchParameter Force
        {
            get => _force;
            set => _force = value;
        }

        private bool _force;

        public object GetDynamicParameters()
        {
            //try
            //{
            //    _dynamicParameters = GetDynamicParameters(context);
            //}
            //catch (ItemNotFoundException)
            //{
            //    _dynamicParameters = null;
            //}
            //catch (ProviderNotFoundException)
            //{
            //    _dynamicParameters = null;
            //}
            //catch (DriveNotFoundException)
            //{
            //    _dynamicParameters = null;
            //}

            return null; // _dynamicParameters;
        }

        public bool SupportsShouldProcess => ProviderSupportsShouldProcess;

        #endregion Public members
    }

    #endregion CoreCommandBase


    #region SetLocationCommand

    [Cmdlet(VerbsCommon.Set, "Location", DefaultParameterSetName = PathParameterSet, SupportsTransactions = true, HelpUri = "https://go.microsoft.com/fwlink/?LinkID=2097049")]
    [OutputType(typeof(PathInfo), typeof(PathInfoStack))]
    public class SetLocationCommand : CoreCommandBase
    {
        #region Command parameters
        private const string PathParameterSet = "Path";
        private const string LiteralPathParameterSet = "LiteralPath";
        private const string StackParameterSet = "Stack";

        /// <summary>
        /// Gets or sets the path property.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = PathParameterSet,
                   ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string Path
        {
            get => _path;
            set => _path = value;
        }

        /// <summary>
        /// Gets or sets the path property, when bound from the pipeline.
        /// </summary>
        [Parameter(ParameterSetName = LiteralPathParameterSet,
                   Mandatory = true, ValueFromPipeline = false, ValueFromPipelineByPropertyName = true)]
        [Alias("PSPath", "LP")]
        public string LiteralPath
        {
            get => _path;
            set
            {
                _path = value;
                base.SuppressWildcardExpansion = true;
            }
        }


        /// <summary>
        /// Gets or sets the parameter -passThru which states output from
        /// the command should be placed in the pipeline.
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru
        {
            get => _passThrough;
            set => _passThrough = value;
        }

        /// <summary>
        /// Gets or sets the StackName parameter which determines which location stack
        /// to use for the push. If the parameter is missing or empty the default
        /// location stack is used.
        /// </summary>
        [Parameter(ParameterSetName = StackParameterSet, ValueFromPipelineByPropertyName = true)]
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

            switch (ParameterSetName)
            {
                case PathParameterSet:
                case LiteralPathParameterSet:
                    try
                    {
                        // Change the current working directory
                        if (string.IsNullOrEmpty(Path))
                        {
                            // If user just typed 'cd', go to FileSystem provider home directory
                           // Path = SessionState.Internal.GetSingleProvider(Commands.FileSystemProvider.ProviderName).Home;
                        }

                       // result = SessionState.Path.SetLocation(Path, CmdletProviderContext, ParameterSetName == LiteralPathParameterSet);
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
                        result = SessionState.Path.SetDefaultLocationStack(StackName);
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

#endregion SetLocationCommand

}
