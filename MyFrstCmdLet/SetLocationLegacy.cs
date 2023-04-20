
using System.Management.Automation;  // Windows PowerShell assembly.


namespace MyFrstCmdLet
{
    //all possible attributes for Cmdlet
    [Cmdlet(
        VerbsCommon.Set,
        "LocationLegacy"//,
       // DefaultParameterSetName = "Path"
        )
    ]
    public class SetLocationLegacyCommand :  PSCmdlet //PSCmdlet //Cmdlet
    {
        #region Command parameters

        [Parameter(Position = 0,
                   ParameterSetName = "Path",
                   Mandatory = false,
                   ValueFromPipeline = true,
                   //ValueFromPipelineByPropertyName = true,
                   //ValueFromRemainingArguments = true,
                   HelpMessage = "this is my message help !!!!!"
                  )
        ]        
        public string Path { get; set; } = "";

        #endregion

        #region Command code

        protected override void BeginProcessing()
        {
            WriteObject("BeginProcessing!");
        }
        protected override void ProcessRecord()
        {
            WriteObject($"ProcessRecord Ini 3: {Path}");

            using var ps = PowerShell.Create(RunspaceMode.CurrentRunspace);

            ps.Commands.Clear();
            if (!string.IsNullOrEmpty(Path))
                ps.AddCommand("Set-Location").AddParameter("LiteralPath", Path);
            else
                ps.AddCommand("Set-Location");

            var output = ps.Invoke();

            if (ps.HadErrors)
            {
                WriteObject("ERROR calling: Set-Location");
                WriteError(new ErrorRecord(ps.Streams.Error[0].Exception,
                           "Set-Location Error", ErrorCategory.NotSpecified, null));
            }
            else
            {
                WriteObject("OK na CHAMADA: Set-Location");
                var sb = new System.Text.StringBuilder();
                foreach (var text in output)
                    sb.Append(text);

                string res = sb.ToString();
                WriteObject(res);
            }

            WriteObject($"ProcessRecord End!");
        }

        protected override void EndProcessing()
        {
            WriteObject("EndProcessing!");
        }

        protected override void StopProcessing()
        {
            WriteObject("StopProcessing!");
        }

        #endregion

    }
}
////V1

//var _runSpace = RunspaceFactory.CreateRunspace();
//_runSpace.Open();
//var pipeLine = _runSpace.CreatePipeline();
//Command cmd = new Command("Get-Command");
//cmd.Parameters.Add("Url", "");
////if (!string.IsNullOrEmpty(CredentialManagerEntry))
////{
////    cmd.Parameters.Add("Credentials", CredentialManagerEntry);
////}
//pipeLine.Commands.Add(cmd);
//var res = pipeLine.Invoke();

////V2 

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


////V3

//Runspace runspace = RunspaceFactory.CreateRunspace();
//runspace.Open();
//Pipeline pipeline = runspace.CreatePipeline();

//Command dir = new Command("Get-Item");
//pipeline.Commands.Add(dir);
//Command select = new Command("Select");
//select.Parameters.Add(null, "Name");
//pipeline.Commands.Add(select);

//Collection<PSObject> out1 = pipeline.Invoke();

//runspace.Close();

////V4

//PowerShell ps0 = PowerShell.Create(RunspaceMode.CurrentRunspace);
//ps0.AddCommand("Get-ChildItem");
//ps0.AddParameter("Path", @"c:\Windows");
//ps0.AddParameter("Filter", "*.exe");
//ps0.Invoke();

//////

//System.Collections.IDictionary parameters = new Dictionary<String, String>();
//parameters.Add("Path", @"c:\Windows");
//parameters.Add("Filter", "*.exe");

//PowerShell ps1 = PowerShell.Create();
//ps1.AddCommand("Get-Process");
//ps1.AddParameters(parameters);
//ps1.Invoke();


//PowerShell ps2 = PowerShell.Create();
//ps2.AddScript(File.ReadAllText(@"C:\Scripts\script01.ps1"));
//Collection<PSObject> results = ps2.Invoke();
//foreach (PSObject result in results)
//{
//    PSMemberInfoCollection<PSMemberInfo> memberInfos = result.Members;
//    Console.WriteLine(memberInfos["Id"].Value);
//}

/////////////////////////////////////////////////

//}
//catch (Exception ex)
//{
//    WriteObject("Error... ProcessRecord : " + ex.Message + "!");
//}
