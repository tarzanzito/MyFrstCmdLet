
using System;
using System.Diagnostics;
using System.Management.Automation;   // PowerShell assembly.
using Microsoft.PowerShell.Commands;  // PowerShell cmdlets assembly you want to call.
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
namespace MyFrstCmdLet
{
 
    //https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/how-to-invoke-a-cmdlet-from-within-a-cmdlet?view=powershell-7.3#example

    //Important
    //You can invoke only those cmdlets that derive directly from the System.Management.Automation.Cmdlet class.
    //You can't invoke a cmdlet that derives from the System.Management.Automation.PSCmdlet class.
    //For an example, see How to invoke a PSCmdlet from within a PSCmdlet.


    // Declare the class as a cmdlet and specify an
    // appropriate verb and noun for the cmdlet name.
    [Cmdlet(VerbsCommunications.Send, "GreetingInvoke")]
    public class SendGreetingInvokeCommand : Cmdlet
    {
        // Declare the parameters for the cmdlet.
        [Parameter(Mandatory = true)]
        public string Name { get; set; }

        // Override the BeginProcessing method to invoke
        // the Get-Process cmdlet.
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            //GetProcessCommand gp = new GetProcessCommand(); //ERROR  IN LINE !!!!!!!!!
            //gp.Name = new string[] { "[a-t]*" };
            //foreach (Process p in gp.Invoke<Process>())
            //{
            //    WriteVerbose(p.ToString());
            //}
        }

        // Override the ProcessRecord method to process
        // the supplied user name and write out a
        // greeting to the user by calling the WriteObject
        // method.
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            WriteObject("Hello " + Name + "!");
        }
    }
}

