

//object result = null;

//switch (ParameterSetName)
//{
//    case PathParameterSet:
//    case LiteralPathParameterSet:
//        try
//        {
//            // Change the current working directory
//            if (string.IsNullOrEmpty(Path))
//            {
//                // If user just typed 'cd', go to FileSystem provider home directory
//                Path = SessionState.Internal.GetSingleProvider(Commands.FileSystemProvider.ProviderName).Home;
//            }

//            result = SessionState.Path.SetLocation(Path, CmdletProviderContext, ParameterSetName == LiteralPathParameterSet);
//        }
//        catch (PSNotSupportedException notSupported)
//        {
//            WriteError(
//                new ErrorRecord(
//                    notSupported.ErrorRecord,
//                    notSupported));
//        }
//        catch (DriveNotFoundException driveNotFound)
//        {
//            WriteError(
//                new ErrorRecord(
//                    driveNotFound.ErrorRecord,
//                    driveNotFound));
//        }
//        catch (ProviderNotFoundException providerNotFound)
//        {
//            WriteError(
//                new ErrorRecord(
//                    providerNotFound.ErrorRecord,
//                    providerNotFound));
//        }
//        catch (ItemNotFoundException pathNotFound)
//        {
//            WriteError(
//                new ErrorRecord(
//                    pathNotFound.ErrorRecord,
//                    pathNotFound));
//        }
//        catch (PSArgumentException argException)
//        {
//            WriteError(
//                new ErrorRecord(
//                    argException.ErrorRecord,
//                    argException));
//        }

//        break;

//    case StackParameterSet:

//        try
//        {
//            // Change the default location stack
//            result = SessionState.Path.SetDefaultLocationStack(StackName);
//        }
//        catch (ItemNotFoundException itemNotFound)
//        {
//            WriteError(
//                new ErrorRecord(
//                    itemNotFound.ErrorRecord,
//                    itemNotFound));
//        }

//        break;

//    default:
//        Dbg.Diagnostics.Assert(
//            false,
//            "One of the specified parameter sets should have been called");
//        break;
//}

//if (_passThrough && result != null)
//{
//    WriteObject(result);
//}




