


using System.IO;
using System.Management.Automation;  // Windows PowerShell assembly.
using System.Xml.Linq;
//using Microsoft.PowerShell.Commands; // PowerShell cmdlets assembly you want to call.

namespace MyFrstCmdLet
{
    //cmdlet vs pscmdlet
    //https://weblogs.asp.net/shahar/cmdlet-vs-pscmslet-windows-powershell

    //all possible attributes for Cmdlet
    [Cmdlet(
        VerbsCommunications.Send, //VerbsCommon.Get, VerbsCommunications.*, VerbsCommon.* , VerbsLifecycle.*
        "MyFrstCmdLet",
        DefaultParameterSetName = MyNameParameter,
        SupportsTransactions = false,
        HelpUri = "https://go.microsoft.com/fwlink/?LinkID=2096495",
        ConfirmImpact = ConfirmImpact.None,
        RemotingCapability = RemotingCapability.None,
        SupportsPaging = false,
        SupportsShouldProcess = false
        )
    ]

    //[OutputType(  ?!?!??!?!??!?!
    //    typeof(MyOutput), //typeof(MyOutput), //PSTypeName[] Type
    //    ParameterSetName = new string[] { MyNameParameter, MyAgeParameter },
    //    ProviderCmdlet = null
    //    )
    //]

    //[Alias("xpto")] ?!???!?!??!

    public class MyFrstCmdLetCommand :  PSCmdlet //PSCmdlet //Cmdlet
    {
        #region Command parameters

        private const string MyNameParameter = "Name";
        private const string MyAgeParameter = "Age";

        //all possible attributes for Parameter
        [Parameter(//1Position = 0,
                   ParameterSetName = MyNameParameter,
                   Mandatory = false,
                   ValueFromPipeline = true,
                   ValueFromPipelineByPropertyName = true,
                   ValueFromRemainingArguments = false,
                   DontShow = false,
                   //ExperimentAction = ExperimentAction.None // its only get
                   //ExperimentName = null                    // its only get
                   HelpMessage = "this is my message help !!!!!"
                   //HelpMessageBaseName = null  //error : OperationStopped: 'HelpMessageBaseName' property specified was not found.
                   //HelpMessageResourceId = null  //error : OperationStopped: 'HelpMessageResourceId' property specified was not found.
                  )
        ]        
        public string Name { get; set; } = "PAULO";

        //

        [Parameter(ParameterSetName = MyAgeParameter,
                   Mandatory = false,
                   ValueFromPipeline = true,
                   ValueFromPipelineByPropertyName = true
                   )
        ]

        //[Alias("PSPath", "LP")] ?!?!??!?!?
        //https://poshcode.gitbook.io/powershell-practice-and-style/style-guide/function-structure
        
        //Function attributes for parameter
        //[AllowNull()]
        //[AllowEmptyString()]
        //[AllowEmptyCollection()]
        //[ValidateCount(1, 5)]
        public int Age{ get; set; } = 18;

        #endregion

        #region Command code

        protected override void BeginProcessing()
        {
            WriteObject("MyFrstCmdLetCommand... BeginProcessing!");
        }

        protected override void ProcessRecord()
        {
            //call other comdlet
            using var ps = PowerShell.Create(RunspaceMode.CurrentRunspace);

            ps.Commands.Clear();
            if (!string.IsNullOrEmpty(Name))
                ps.AddCommand("Set-Location").AddParameter("LiteralPath", Name);
            else
                ps.AddCommand("Set-Location");

            ////

            //using var ps = PowerShell.Create(RunspaceMode.CurrentRunspace);

            if (!string.IsNullOrEmpty(Name))
                ps.AddCommand("Set-Location").AddParameter("LiteralPath");
            var output = ps.Invoke<string>();
            if (ps.HadErrors)
            {
                WriteObject("ERROR na CHAMADA: Get-Clipboard");
                WriteError(new ErrorRecord(ps.Streams.Error[0].Exception,
                           "Get-Clipboard Error", ErrorCategory.NotSpecified, null));
            }
            else
            {
                WriteObject("OK na CHAMADA: Get-Clipboard");
                var sb = new System.Text.StringBuilder();
                foreach (var text in output)
                {
                    sb.Append(text);
                }

                var reversed = sb.ToString().ToCharArray();
                WriteObject(new string(reversed));
            }




        }
        protected override void EndProcessing()
        {
            WriteObject("Hello... EndProcessing!");
        }
        protected override void StopProcessing()
        {
            WriteObject("Hello... StopProcessing!");
        }

        #endregion

    }
}




//try
//{

//    WriteObject($"MyFrstCmdLetCommand... ProcessRecord V12: {Name} - {Age} !");

//    var _runSpace = System.Management.Automation.Runspaces.RunspaceFactory.CreateRunspace();
//    _runSpace.Open();
//    var pipeLine = _runSpace.CreatePipeline();
//    System.Management.Automation.Runspaces.Command cmd =
//        new System.Management.Automation.Runspaces.Command("Get-Command");
//    cmd.Parameters.Add("Url", "");
//    if (!string.IsNullOrEmpty(CredentialManagerEntry))
//    {
//        cmd.Parameters.Add("Credentials", CredentialManagerEntry);
//    }
//    pipeLine.Commands.Add(cmd);
//    var res = pipeLine.Invoke();
//}
//catch (Exception ex)
//{
//    WriteObject("Error... ProcessRecord : " + ex.Message + "!");
//}

////try
////{
////var setLocation = new Microsoft.PowerShell.Commands.s.set
////setLocation.Path = Name;
////setLocation.Invoke();
////GetProcessCommand gp = new GetProcessCommand();
////object result = SessionState..Path.SetLocation(Path, CmdletProviderContext, ParameterSetName == LiteralPathParameterSet);

//WriteObject($"Hello... ProcessRecord V12: {Name} - {Age} !");

//////V1

////var _runSpace = RunspaceFactory.CreateRunspace();
////_runSpace.Open();
////var pipeLine = _runSpace.CreatePipeline();
////Command cmd = new Command("Get-Command");
////cmd.Parameters.Add("Url", "");
//////if (!string.IsNullOrEmpty(CredentialManagerEntry))
//////{
//////    cmd.Parameters.Add("Credentials", CredentialManagerEntry);
//////}
////pipeLine.Commands.Add(cmd);
////var res = pipeLine.Invoke();

//////V2 

//using var ps = PowerShell.Create(RunspaceMode.CurrentRunspace);

//ps.AddCommand("Get-Clipboard").AddParameter("Raw");
//var output = ps.Invoke<string>();
//if (ps.HadErrors)
//{
//    WriteObject("ERROR na CHAMADA: Get-Clipboard");
//    WriteError(new ErrorRecord(ps.Streams.Error[0].Exception,
//               "Get-Clipboard Error", ErrorCategory.NotSpecified, null));
//}
//else
//{
//    WriteObject("OK na CHAMADA: Get-Clipboard");
//    var sb = new System.Text.StringBuilder();
//    foreach (var text in output)
//    {
//        sb.Append(text);
//    }

//    var reversed = sb.ToString().ToCharArray();
//    //   Array.Reverse(reversed);
//    WriteObject(new string(reversed));
//}


//ps.Commands.Clear();
//ps.AddCommand("Set-Location").AddParameter("LiteralPath", @"c:\Yes [Drama]");
//var output2 = ps.Invoke<string>();
//WriteObject(output2);


//////V3

////Runspace runspace = RunspaceFactory.CreateRunspace();
////runspace.Open();
////Pipeline pipeline = runspace.CreatePipeline();

////Command dir = new Command("Get-Item");
////pipeline.Commands.Add(dir);
////Command select = new Command("Select");
////select.Parameters.Add(null, "Name");
////pipeline.Commands.Add(select);

////Collection<PSObject> out1 = pipeline.Invoke();

////runspace.Close();

//////V4

////PowerShell ps0 = PowerShell.Create(RunspaceMode.CurrentRunspace);
////ps0.AddCommand("Get-ChildItem");
////ps0.AddParameter("Path", @"c:\Windows");
////ps0.AddParameter("Filter", "*.exe");
////ps0.Invoke();

////////

////System.Collections.IDictionary parameters = new Dictionary<String, String>();
////parameters.Add("Path", @"c:\Windows");
////parameters.Add("Filter", "*.exe");

////PowerShell ps1 = PowerShell.Create();
////ps1.AddCommand("Get-Process");
////ps1.AddParameters(parameters);
////ps1.Invoke();


////PowerShell ps2 = PowerShell.Create();
////ps2.AddScript(File.ReadAllText(@"C:\Scripts\script01.ps1"));
////Collection<PSObject> results = ps2.Invoke();
////foreach (PSObject result in results)
////{
////    PSMemberInfoCollection<PSMemberInfo> memberInfos = result.Members;
////    Console.WriteLine(memberInfos["Id"].Value);
////}

///////////////////////////////////////////////////

////}
////catch (Exception ex)
////{
////    WriteObject("Error... ProcessRecord : " + ex.Message + "!");
////}