

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;

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
    //     Providers,) then you should instead derive from the PSCmdlet base class. In both
    //     cases, users should first develop and implement an object model to accomplish
    //     their task, extending the Cmdlet or PSCmdlet classes only as a thin management
    //     layer.
    public abstract class Cmdlet : My.Management.Automation.Internal.InternalCommand
    {
        //
        // Summary:
        //     Holds the command runtime object for this command. This object controls what
        //     actually happens when a write is called.
        public ICommandRuntime CommandRuntime
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        //
        // Summary:
        //     Lists the common parameters that are added by the PowerShell engine to any cmdlet
        //     that derives from PSCmdlet.
        public static HashSet<string> CommonParameters => null;

        //
        // Summary:
        //     Gets an object that surfaces the current PowerShell transaction. When this object
        //     is disposed, PowerShell resets the active transaction.
        public PSTransactionContext CurrentPSTransaction => null;

        //
        // Summary:
        //     Lists the common parameters that are added by the PowerShell engine when a cmdlet
        //     defines additional capabilities (SupportsShouldProcess, SupportsTransactions)
        public static HashSet<string> OptionalCommonParameters => null;

        //
        // Summary:
        //     Is this command stopping?
        //
        // Remarks:
        //     If Stopping is true, many Cmdlet methods will throw System.Management.Automation.PipelineStoppedException.
        //     In general, if a Cmdlet's override implementation of ProcessRecord etc. throws
        //     System.Management.Automation.PipelineStoppedException, the best thing to do is
        //     to shut down the operation and return to the caller. It is acceptable to not
        //     catch System.Management.Automation.PipelineStoppedException and allow the exception
        //     to reach ProcessRecord.
        public bool Stopping => false;

        //
        // Summary:
        //     Initializes the new instance of Cmdlet class.
        //
        // Remarks:
        //     Only subclasses of System.Management.Automation.Cmdlet can be created.
        protected Cmdlet()
        {
        }

        //
        // Summary:
        //     When overridden in the derived class, performs initialization of command execution.
        //     Default implementation in the base class just returns.
        //
        // Exceptions:
        //   T:System.Exception:
        //     This method is overridden in the implementation of individual Cmdlets, and can
        //     throw literally any exception.
        protected virtual void BeginProcessing()
        {
        }

        //
        // Summary:
        //     When overridden in the derived class, performs clean-up after the command execution.
        //     Default implementation in the base class just returns.
        //
        // Exceptions:
        //   T:System.Exception:
        //     This method is overridden in the implementation of individual Cmdlets, and can
        //     throw literally any exception.
        protected virtual void EndProcessing()
        {
        }

        //
        // Summary:
        //     Gets the resource string corresponding to baseName and resourceId from the current
        //     assembly. You should override this if you require a different behavior.
        //
        // Parameters:
        //   baseName:
        //     The base resource name.
        //
        //   resourceId:
        //     The resource id.
        //
        // Returns:
        //     The resource string corresponding to baseName and resourceId.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     Invalid baseName or resourceId, or string not found in resources
        //
        // Remarks:
        //     This behavior may be used when the Cmdlet specifies HelpMessageBaseName and HelpMessageResourceId
        //     when defining System.Management.Automation.ParameterAttribute, or when it uses
        //     the System.Management.Automation.ErrorDetails constructor variants which take
        //     baseName and resourceId.
        public virtual string GetResourceString(string baseName, string resourceId)
        {
            return null;
        }

        //
        // Summary:
        //     Invoke this cmdlet object returning a collection of results.
        //
        // Returns:
        //     The results that were produced by this class.
        public IEnumerable Invoke()
        {
            return null;
        }

        //
        // Summary:
        //     Returns a strongly-typed enumerator for the results of this cmdlet.
        //
        // Type parameters:
        //   T:
        //     The type returned by the enumerator
        //
        // Returns:
        //     An instance of the appropriate enumerator.
        //
        // Exceptions:
        //   T:System.InvalidCastException:
        //     Thrown when the object returned by the cmdlet cannot be converted to the target
        //     type.
        public IEnumerable<T> Invoke<T>()
        {
            return null;
        }

        //
        // Summary:
        //     When overridden in the derived class, performs execution of the command.
        //
        // Exceptions:
        //   T:System.Exception:
        //     This method is overridden in the implementation of individual Cmdlets, and can
        //     throw literally any exception.
        protected virtual void ProcessRecord()
        {
        }

        //
        // Summary:
        //     Confirm an operation or grouping of operations with the user. This differs from
        //     ShouldProcess in that it is not affected by preference settings or command-line
        //     parameters, it always does the query. This variant only offers Yes/No, not YesToAll/NoToAll.
        //
        // Parameters:
        //   query:
        //     Textual query of whether the action should be performed, usually in the form
        //     of a question.
        //
        //   caption:
        //     Caption of the window which may be displayed when the user is prompted whether
        //     or not to perform the action. It may be displayed by some hosts, but not all.
        //
        // Returns:
        //     If ShouldContinue returns true, the operation should be performed. If ShouldContinue
        //     returns false, the operation should not be performed, and the Cmdlet should move
        //     on to the next target resource.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. ShouldContinue may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     Cmdlets using ShouldContinue should also offer a "bool Force" parameter which
        //     bypasses the calls to ShouldContinue and ShouldProcess. If this is not done,
        //     it will be difficult to use the Cmdlet from scripts and non-interactive hosts.
        //     Cmdlets using ShouldContinue must still verify operations which will make changes
        //     using ShouldProcess. This will assure that settings such as -WhatIf work properly.
        //     You may call ShouldContinue either before or after ShouldProcess. ShouldContinue
        //     may only be called during a call to this Cmdlet's implementation of ProcessRecord,
        //     BeginProcessing or EndProcessing, and only from that thread. Cmdlets may have
        //     different "classes" of confirmations. For example, "del" confirms whether files
        //     in a particular directory should be deleted, whether read-only files should be
        //     deleted, etc. Cmdlets can use ShouldContinue to store YesToAll/NoToAll members
        //     for each such "class" to keep track of whether the user has confirmed "delete
        //     all read-only files" etc. ShouldProcess offers YesToAll/NoToAll automatically,
        //     but answering YesToAll or NoToAll applies to all subsequent calls to ShouldProcess
        //     for the Cmdlet instance.
        public bool ShouldContinue(string query, string caption)
        {
            return false;
        }

        //
        // Summary:
        //     Confirm an operation or grouping of operations with the user. This differs from
        //     ShouldProcess in that it is not affected by preference settings or command-line
        //     parameters, it always does the query. This variant offers Yes, No, YesToAll and
        //     NoToAll.
        //
        // Parameters:
        //   query:
        //     Textual query of whether the action should be performed, usually in the form
        //     of a question.
        //
        //   caption:
        //     Caption of the window which may be displayed when the user is prompted whether
        //     or not to perform the action. It may be displayed by some hosts, but not all.
        //
        //   hasSecurityImpact:
        //     true if the operation being confirmed has a security impact. If specified, the
        //     default option selected in the selection menu is 'No'.
        //
        //   yesToAll:
        //     true iff user selects YesToAll. If this is already true, ShouldContinue will
        //     bypass the prompt and return true.
        //
        //   noToAll:
        //     true iff user selects NoToAll. If this is already true, ShouldContinue will bypass
        //     the prompt and return false.
        //
        // Returns:
        //     If ShouldContinue returns true, the operation should be performed. If ShouldContinue
        //     returns false, the operation should not be performed, and the Cmdlet should move
        //     on to the next target resource.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. ShouldContinue may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     Cmdlets using ShouldContinue should also offer a "bool Force" parameter which
        //     bypasses the calls to ShouldContinue and ShouldProcess. If this is not done,
        //     it will be difficult to use the Cmdlet from scripts and non-interactive hosts.
        //     Cmdlets using ShouldContinue must still verify operations which will make changes
        //     using ShouldProcess. This will assure that settings such as -WhatIf work properly.
        //     You may call ShouldContinue either before or after ShouldProcess. ShouldContinue
        //     may only be called during a call to this Cmdlet's implementation of ProcessRecord,
        //     BeginProcessing or EndProcessing, and only from that thread. Cmdlets may have
        //     different "classes" of confirmations. For example, "del" confirms whether files
        //     in a particular directory should be deleted, whether read-only files should be
        //     deleted, etc. Cmdlets can use ShouldContinue to store YesToAll/NoToAll members
        //     for each such "class" to keep track of whether the user has confirmed "delete
        //     all read-only files" etc. ShouldProcess offers YesToAll/NoToAll automatically,
        //     but answering YesToAll or NoToAll applies to all subsequent calls to ShouldProcess
        //     for the Cmdlet instance.
        public bool ShouldContinue(string query, string caption, bool hasSecurityImpact, ref bool yesToAll, ref bool noToAll)
        {
            return false;
        }

        //
        // Summary:
        //     Confirm an operation or grouping of operations with the user. This differs from
        //     ShouldProcess in that it is not affected by preference settings or command-line
        //     parameters, it always does the query. This variant offers Yes, No, YesToAll and
        //     NoToAll.
        //
        // Parameters:
        //   query:
        //     Textual query of whether the action should be performed, usually in the form
        //     of a question.
        //
        //   caption:
        //     Caption of the window which may be displayed when the user is prompted whether
        //     or not to perform the action. It may be displayed by some hosts, but not all.
        //
        //   yesToAll:
        //     true iff user selects YesToAll. If this is already true, ShouldContinue will
        //     bypass the prompt and return true.
        //
        //   noToAll:
        //     true iff user selects NoToAll. If this is already true, ShouldContinue will bypass
        //     the prompt and return false.
        //
        // Returns:
        //     If ShouldContinue returns true, the operation should be performed. If ShouldContinue
        //     returns false, the operation should not be performed, and the Cmdlet should move
        //     on to the next target resource.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. ShouldContinue may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     Cmdlets using ShouldContinue should also offer a "bool Force" parameter which
        //     bypasses the calls to ShouldContinue and ShouldProcess. If this is not done,
        //     it will be difficult to use the Cmdlet from scripts and non-interactive hosts.
        //     Cmdlets using ShouldContinue must still verify operations which will make changes
        //     using ShouldProcess. This will assure that settings such as -WhatIf work properly.
        //     You may call ShouldContinue either before or after ShouldProcess. ShouldContinue
        //     may only be called during a call to this Cmdlet's implementation of ProcessRecord,
        //     BeginProcessing or EndProcessing, and only from that thread. Cmdlets may have
        //     different "classes" of confirmations. For example, "del" confirms whether files
        //     in a particular directory should be deleted, whether read-only files should be
        //     deleted, etc. Cmdlets can use ShouldContinue to store YesToAll/NoToAll members
        //     for each such "class" to keep track of whether the user has confirmed "delete
        //     all read-only files" etc. ShouldProcess offers YesToAll/NoToAll automatically,
        //     but answering YesToAll or NoToAll applies to all subsequent calls to ShouldProcess
        //     for the Cmdlet instance.
        public bool ShouldContinue(string query, string caption, ref bool yesToAll, ref bool noToAll)
        {
            return false;
        }

        //
        // Summary:
        //     Confirm the operation with the user. Cmdlets which make changes (e.g. delete
        //     files, stop services etc.) should call ShouldProcess to give the user the opportunity
        //     to confirm that the operation should actually be performed.
        //
        // Parameters:
        //   target:
        //     Name of the target resource being acted upon. This will potentially be displayed
        //     to the user.
        //
        // Returns:
        //     If ShouldProcess returns true, the operation should be performed. If ShouldProcess
        //     returns false, the operation should not be performed, and the Cmdlet should move
        //     on to the next target resource.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. ShouldProcess may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     A Cmdlet should declare [Cmdlet( SupportsShouldProcess = true )] if-and-only-if
        //     it calls ShouldProcess before making changes. ShouldProcess may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread. ShouldProcess will take into account
        //     command-line settings and preference variables in determining what it should
        //     return and whether it should prompt the user.
        public bool ShouldProcess(string target)
        {
            return false;
        }

        //
        // Summary:
        //     Confirm the operation with the user. Cmdlets which make changes (e.g. delete
        //     files, stop services etc.) should call ShouldProcess to give the user the opportunity
        //     to confirm that the operation should actually be performed. This variant allows
        //     the caller to specify text for both the target resource and the action.
        //
        // Parameters:
        //   target:
        //     Name of the target resource being acted upon. This will potentially be displayed
        //     to the user.
        //
        //   action:
        //     Name of the action which is being performed. This will potentially be displayed
        //     to the user. (default is Cmdlet name)
        //
        // Returns:
        //     If ShouldProcess returns true, the operation should be performed. If ShouldProcess
        //     returns false, the operation should not be performed, and the Cmdlet should move
        //     on to the next target resource.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. ShouldProcess may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     A Cmdlet should declare [Cmdlet( SupportsShouldProcess = true )] if-and-only-if
        //     it calls ShouldProcess before making changes. ShouldProcess may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread. ShouldProcess will take into account
        //     command-line settings and preference variables in determining what it should
        //     return and whether it should prompt the user.
        public bool ShouldProcess(string target, string action)
        {
            return false;
        }

        //
        // Summary:
        //     Confirm the operation with the user. Cmdlets which make changes (e.g. delete
        //     files, stop services etc.) should call ShouldProcess to give the user the opportunity
        //     to confirm that the operation should actually be performed. This variant allows
        //     the caller to specify the complete text describing the operation, rather than
        //     just the name and action.
        //
        // Parameters:
        //   verboseDescription:
        //     Textual description of the action to be performed. This is what will be displayed
        //     to the user for ActionPreference.Continue.
        //
        //   verboseWarning:
        //     Textual query of whether the action should be performed, usually in the form
        //     of a question. This is what will be displayed to the user for ActionPreference.Inquire.
        //
        //   caption:
        //     Caption of the window which may be displayed if the user is prompted whether
        //     or not to perform the action. caption may be displayed by some hosts, but not
        //     all.
        //
        // Returns:
        //     If ShouldProcess returns true, the operation should be performed. If ShouldProcess
        //     returns false, the operation should not be performed, and the Cmdlet should move
        //     on to the next target resource.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. ShouldProcess may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     A Cmdlet should declare [Cmdlet( SupportsShouldProcess = true )] if-and-only-if
        //     it calls ShouldProcess before making changes. ShouldProcess may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread. ShouldProcess will take into account
        //     command-line settings and preference variables in determining what it should
        //     return and whether it should prompt the user.
        public bool ShouldProcess(string verboseDescription, string verboseWarning, string caption)
        {
            return false;
        }

        //
        // Summary:
        //     Confirm the operation with the user. Cmdlets which make changes (e.g. delete
        //     files, stop services etc.) should call ShouldProcess to give the user the opportunity
        //     to confirm that the operation should actually be performed. This variant allows
        //     the caller to specify the complete text describing the operation, rather than
        //     just the name and action.
        //
        // Parameters:
        //   verboseDescription:
        //     Textual description of the action to be performed. This is what will be displayed
        //     to the user for ActionPreference.Continue.
        //
        //   verboseWarning:
        //     Textual query of whether the action should be performed, usually in the form
        //     of a question. This is what will be displayed to the user for ActionPreference.Inquire.
        //
        //   caption:
        //     Caption of the window which may be displayed if the user is prompted whether
        //     or not to perform the action. caption may be displayed by some hosts, but not
        //     all.
        //
        //   shouldProcessReason:
        //     Indicates the reason(s) why ShouldProcess returned what it returned. Only the
        //     reasons enumerated in System.Management.Automation.ShouldProcessReason are returned.
        //
        // Returns:
        //     If ShouldProcess returns true, the operation should be performed. If ShouldProcess
        //     returns false, the operation should not be performed, and the Cmdlet should move
        //     on to the next target resource.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. ShouldProcess may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     A Cmdlet should declare [Cmdlet( SupportsShouldProcess = true )] if-and-only-if
        //     it calls ShouldProcess before making changes. ShouldProcess may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread. ShouldProcess will take into account
        //     command-line settings and preference variables in determining what it should
        //     return and whether it should prompt the user.
        public bool ShouldProcess(string verboseDescription, string verboseWarning, string caption, out ShouldProcessReason shouldProcessReason)
        {
            shouldProcessReason = ShouldProcessReason.None;
            return false;
        }

        //
        // Summary:
        //     When overridden in the derived class, interrupts currently running code within
        //     the command. It should interrupt BeginProcessing, ProcessRecord, and EndProcessing.
        //     Default implementation in the base class just returns.
        //
        // Exceptions:
        //   T:System.Exception:
        //     This method is overridden in the implementation of individual Cmdlets, and can
        //     throw literally any exception.
        protected virtual void StopProcessing()
        {
        }

        //
        // Summary:
        //     Terminate the command and report an error.
        //
        // Parameters:
        //   errorRecord:
        //     The error which caused the command to be terminated
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     always
        //
        // Remarks:
        //     System.Management.Automation.Cmdlet.ThrowTerminatingError(System.Management.Automation.ErrorRecord)
        //     terminates the command, where System.Management.Automation.ICommandRuntime.WriteError(System.Management.Automation.ErrorRecord)
        //     allows the command to continue. The cmdlet can also terminate the command by
        //     simply throwing any exception. When the cmdlet's implementation of System.Management.Automation.Cmdlet.ProcessRecord,
        //     System.Management.Automation.Cmdlet.BeginProcessing or System.Management.Automation.Cmdlet.EndProcessing
        //     throws an exception, the Engine will always catch the exception and report it
        //     as a terminating error. However, it is preferred for the cmdlet to call System.Management.Automation.Cmdlet.ThrowTerminatingError(System.Management.Automation.ErrorRecord),
        //     so that the additional information in System.Management.Automation.ErrorRecord
        //     is available. System.Management.Automation.Cmdlet.ThrowTerminatingError(System.Management.Automation.ErrorRecord)
        //     always throws System.Management.Automation.PipelineStoppedException, regardless
        //     of what error was specified in errorRecord. The Cmdlet should generally just
        //     allow System.Management.Automation.PipelineStoppedException. to percolate up
        //     to the caller of System.Management.Automation.Cmdlet.ProcessRecord. etc.
        [DoesNotReturn]
        public void ThrowTerminatingError(ErrorRecord errorRecord)
        {
        }

        //
        // Summary:
        //     Returns true if a transaction is available and active.
        public bool TransactionAvailable()
        {
            return false;
        }

        //
        // Summary:
        //     Write text into pipeline execution log.
        //
        // Parameters:
        //   text:
        //     Text to be written to log.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. WriteWarning may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     Use WriteCommandDetail to write important information about cmdlet execution
        //     to pipeline execution log. If LogPipelineExecutionDetail is turned on, this information
        //     will be written to monad log under log category "Pipeline execution detail"
        public void WriteCommandDetail(string text)
        {
        }

        //
        // Summary:
        //     Display debug information.
        //
        // Parameters:
        //   text:
        //     Debug output.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. WriteDebug may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     Use WriteDebug to display debug information on the inner workings of your Cmdlet.
        //     By default, debug output will not be displayed, although this can be configured
        //     with the DebugPreference shell variable or the -Debug command-line option.
        public void WriteDebug(string text)
        {
        }

        //
        // Summary:
        //     Internal variant: Writes the specified error to the error pipe.
        //
        // Parameters:
        //   errorRecord:
        //     Error.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread
        //
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        // Remarks:
        //     Do not call WriteError(e.ErrorRecord). The ErrorRecord contained in the ErrorRecord
        //     property of an exception which implements IContainsErrorRecord should not be
        //     passed directly to WriteError, since it contains a System.Management.Automation.ParentContainsErrorRecordException
        //     rather than the real exception.
        public void WriteError(ErrorRecord errorRecord)
        {
        }

        //
        // Summary:
        //     Route information to the user or host.
        //
        // Parameters:
        //   informationRecord:
        //     The information record to write.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. WriteInformation may only be
        //     called during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     Use WriteInformation to transmit information to the user about the activity of
        //     your Cmdlet. By default, informational output will be displayed, although this
        //     can be configured with the InformationPreference shell variable or the -InformationPreference
        //     command-line option.
        public void WriteInformation(InformationRecord informationRecord)
        {
        }

        //
        // Summary:
        //     Route information to the user or host.
        //
        // Parameters:
        //   messageData:
        //     The object / message data to transmit to the hosting application.
        //
        //   tags:
        //     Any tags to be associated with the message data. These can later be used to filter
        //     or separate objects being sent to the host.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. WriteInformation may only be
        //     called during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     Use WriteInformation to transmit information to the user about the activity of
        //     your Cmdlet. By default, informational output will be displayed, although this
        //     can be configured with the InformationPreference shell variable or the -InformationPreference
        //     command-line option.
        public void WriteInformation(object messageData, string[] tags)
        {
        }

        //
        // Summary:
        //     Writes the object to the output pipe.
        //
        // Parameters:
        //   sendToPipeline:
        //     The object that needs to be written. This will be written as a single object,
        //     even if it is an enumeration.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. WriteObject may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        public void WriteObject(object sendToPipeline)
        {
        }

        //
        // Summary:
        //     Writes one or more objects to the output pipe. If the object is a collection
        //     and the enumerateCollection flag is true, the objects in the collection will
        //     be written individually.
        //
        // Parameters:
        //   sendToPipeline:
        //     The object that needs to be written to the pipeline.
        //
        //   enumerateCollection:
        //     true if the collection should be enumerated
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. WriteObject may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        public void WriteObject(object sendToPipeline, bool enumerateCollection)
        {
        }

        //
        // Summary:
        //     Display progress information.
        //
        // Parameters:
        //   progressRecord:
        //     Progress information.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. WriteProgress may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     Use WriteProgress to display progress information about the activity of your
        //     Cmdlet, when the operation of your Cmdlet could potentially take a long time.
        //     By default, progress output will be displayed, although this can be configured
        //     with the ProgressPreference shell variable.
        public void WriteProgress(ProgressRecord progressRecord)
        {
        }

        //
        // Summary:
        //     Display verbose information.
        //
        // Parameters:
        //   text:
        //     Verbose output.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. WriteVerbose may only be called
        //     during a call to this Cmdlets's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     Use WriteVerbose to display more detailed information about the activity of your
        //     Cmdlet. By default, verbose output will not be displayed, although this can be
        //     configured with the VerbosePreference shell variable or the -Verbose and -Debug
        //     command-line options.
        public void WriteVerbose(string text)
        {
        }

        //
        // Summary:
        //     Display warning information.
        //
        // Parameters:
        //   text:
        //     Warning output.
        //
        // Exceptions:
        //   T:System.Management.Automation.PipelineStoppedException:
        //     The pipeline has already been terminated, or was terminated during the execution
        //     of this method. The Cmdlet should generally just allow PipelineStoppedException
        //     to percolate up to the caller of ProcessRecord etc.
        //
        //   T:System.InvalidOperationException:
        //     Not permitted at this time or from this thread. WriteWarning may only be called
        //     during a call to this Cmdlet's implementation of ProcessRecord, BeginProcessing
        //     or EndProcessing, and only from that thread.
        //
        // Remarks:
        //     Use WriteWarning to display warnings about the activity of your Cmdlet. By default,
        //     warning output will be displayed, although this can be configured with the WarningPreference
        //     shell variable or the -Verbose and -Debug command-line options.
        public void WriteWarning(string text)
        {
        }
    }
}
