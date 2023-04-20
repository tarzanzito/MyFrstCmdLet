using Microsoft.VisualBasic;
using System;
using System.Management.Automation;   // PowerShell assembly.
using System.Management.Automation.Runspaces;
using System.Text;

namespace MyFrstCmdLet
{
    //https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/how-to-invoke-a-pscmdlet-from-within-a-pscmdlet?view=powershell-7.3
    
    //Note
    //The[PSCmdlet] class differs from the[Cmdlet] class.
    //[PSCmdlet] implementations use runspace context information so you must invoke another cmdlet using
    //the PowerShell pipeline API.In[Cmdlet] implementations you can call the cmdlet's .NET API directly.
    //For an example, see How to invoke a Cmdlet from within a Cmdlet.

    [Cmdlet(VerbsCommon.Get, "ClipboardReverse")]
    [OutputType(typeof(string))]
    public class ClipboardReverseCommand : PSCmdlet
    {
        protected override void EndProcessing()
        {
            using var ps = PowerShell.Create(RunspaceMode.CurrentRunspace);
            ps.AddCommand("Get-Clipboard").AddParameter("Raw");
            var output = ps.Invoke<string>();
            if (ps.HadErrors)
            {
                WriteError(new ErrorRecord(ps.Streams.Error[0].Exception,
                           "Get-Clipboard Error", ErrorCategory.NotSpecified, null));
            }
            else
            {
                var sb = new StringBuilder();
                foreach (var text in output)
                {
                    sb.Append(text);
                }

                var reversed = sb.ToString().ToCharArray();
                Array.Reverse(reversed);
                WriteObject(new string(reversed));
            }
        }
    }
}

