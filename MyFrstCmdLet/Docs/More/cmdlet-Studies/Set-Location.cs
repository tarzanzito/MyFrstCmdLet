// Set-Location -LiteralPath <System.String> [-PassThru] [-UseTransaction] [<CommonParameters>]

//Set-Location [[-Path] <System.String>] [-PassThru] [-UseTransaction] [<CommonParameters>]

//Set-Location [-PassThru] [-StackName <System.String>] [-UseTransaction] [<CommonParameters>]

//-Path
//-LiteralPath
//-PassThru
//-UseTransaction
//-StackName

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
                            Path = SessionState.Internal.GetSingleProvider(Commands.FileSystemProvider.ProviderName).Home;
                        }

                        result = SessionState.Path.SetLocation(Path, CmdletProviderContext, ParameterSetName == LiteralPathParameterSet);
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
                    Dbg.Diagnostics.Assert(
                        false,
                        "One of the specified parameter sets should have been called");
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