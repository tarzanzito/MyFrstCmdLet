using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace MyFrstCmdLet
{
    [Cmdlet(VerbsCommon.Get, "RepeatedPhrase")]
    [OutputType(typeof(string))]
    public class GetRepeatedPhraseCommand : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Word")]
        [ValidateNotNullOrEmpty()]
        public string Phrase { get; set; }

        [Parameter(Position = 1, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Repeat")]
        public int NumberOfTimesToRepeatPhrase { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var result = new StringBuilder();
            for (int i = 0; i < NumberOfTimesToRepeatPhrase; i++)
            {
                result.Append(Phrase);
            }

            WriteObject(result.ToString()); // This is what actually "returns" output.
        }
    }
}
