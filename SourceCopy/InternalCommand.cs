
using System.Diagnostics;
using System.Management.Automation;

namespace My.Management.Automation.Internal
{
    //
    // Summary:
    //     Defines members used by Cmdlets. All Cmdlets must derive from System.Management.Automation.Cmdlet.
    //
    // Remarks:
    //     Only use System.Management.Automation.Internal.InternalCommand as a subclass
    //     of System.Management.Automation.Cmdlet. Do not attempt to create instances of
    //     System.Management.Automation.Internal.InternalCommand independently, or to derive
    //     other classes than System.Management.Automation.Cmdlet from System.Management.Automation.Internal.InternalCommand.
    [DebuggerDisplay("Command = {_commandInfo}")]
    public abstract class InternalCommand
    {
        //
        // Summary:
        //     This property tells you if you were being invoked inside the runspace or if it
        //     was an external request.
        public CommandOrigin CommandOrigin => CommandOrigin.Runspace;

        //
        // Summary:
        //     Initializes the new instance of Cmdlet class.
        //
        // Remarks:
        //     The only constructor is internal, so outside users cannot create an instance
        //     of this class.
        internal InternalCommand()
        {
        }
    }
}

