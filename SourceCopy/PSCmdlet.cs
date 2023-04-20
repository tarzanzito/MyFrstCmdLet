
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Host;

namespace My.Management.Automation
{
    //
    // Summary:
    //     Defines members and overrides used by Cmdlets. All Cmdlets must derive from System.Management.Automation.Cmdlet.
    //
    // Remarks:
    //     There are two ways to create a Cmdlet: by deriving from the Cmdlet base class,
    //     and by deriving from the PSCmdlet base class. The Cmdlet base class is the primary
    //     means by which users create their own Cmdlets. Extending this class provides
    //     support for the most common functionality, including object output and record
    //     processing. If your Cmdlet requires access to the MSH Runtime (for example, variables
    //     in the session state, access to the host, or information about the current Cmdlet
    //     Providers,) then you should instead derive from the PSCmdlet base class. The
    //     public members defined by the PSCmdlet class are not designed to be overridden;
    //     instead, they provided access to different aspects of the MSH runtime. In both
    //     cases, users should first develop and implement an object model to accomplish
    //     their task, extending the Cmdlet or PSCmdlet classes only as a thin management
    //     layer.
    public abstract class PSCmdlet : My.Management.Automation.Cmdlet
    {
        //
        // Summary:
        //     Gets the event manager for the current runspace.
        public PSEventManager Events => null;

        //
        // Summary:
        //     Gets the host interaction APIs.
        public PSHost Host => null;

        //
        // Summary:
        //     Provides access to utility routines for executing scripts and creating script
        //     blocks.
        //
        // Value:
        //     Returns an object exposing the utility routines.
        public CommandInvocationIntrinsics InvokeCommand => null;

        //
        // Summary:
        //     Gets the instance of the provider interface APIs for the current runspace.
        public ProviderIntrinsics InvokeProvider => null;

        //
        // Summary:
        //     Manager for JobSourceAdapters registered.
        public JobManager JobManager => null;

        //
        // Summary:
        //     Repository for jobs.
        public JobRepository JobRepository => null;

        //
        // Summary:
        //     Contains information about the identity of this cmdlet and how it was invoked.
        public InvocationInfo MyInvocation => null;

        //
        // Summary:
        //     If the cmdlet declares paging support (via System.Management.Automation.CmdletCommonMetadataAttribute.SupportsPaging),
        //     then System.Management.Automation.PSCmdlet.PagingParameters property contains
        //     arguments of the paging parameters. Otherwise System.Management.Automation.PSCmdlet.PagingParameters
        //     property is null.
        public PagingParameters PagingParameters => null;

        //
        // Summary:
        //     The name of the parameter set in effect.
        //
        // Value:
        //     the parameter set name
        public string ParameterSetName => null;

        //
        // Summary:
        //     Gets the instance of session state for the current runspace.
        public SessionState SessionState => null;

        //
        // Summary:
        //     Initializes the new instance of PSCmdlet class.
        //
        // Remarks:
        //     Only subclasses of System.Management.Automation.Cmdlet can be created.
        protected PSCmdlet()
        {
        }

        public PathInfo CurrentProviderLocation(string providerId)
        {
            return null;
        }

        public Collection<string> GetResolvedProviderPathFromPSPath(string path, out ProviderInfo provider)
        {
            provider = null;
            return null;
        }

        public string GetUnresolvedProviderPathFromPSPath(string path)
        {
            return null;
        }

        public object GetVariableValue(string name)
        {
            return null;
        }

        public object GetVariableValue(string name, object defaultValue)
        {
            return null;
        }
    }
}
