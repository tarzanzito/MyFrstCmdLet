Function My-Something
{
	[CmdletBinding(
	            ConfirmImpact=<string>,
				# Default parameter set name you want PowerShell to use 
				# if there is no parameter set.
                DefaultParameterSetName=<string>, 
				# uri to online help, must begin with http or https
                HelpURI=<uri>, 
				# Used when you’re trying to return data from a large database suchas MySQL.
                SupportsPaging=$false, 
				# Adds three parameters – First, Skip, and IncludeTotalCount to the function.
                SupportsShouldProcess=$true,
				ConfirmImpact='High',
				# positional binding binds positions to parameters 
                PositionalBinding=$false

		) as defined

	
	)]
	Param
	(
#		$Msg
		[Alias("UN","Writer","Editor")]
		[Parameter(
				   Mandatory=$false,
				   Position = 0,
				   ParameterSetName=<String>,
				   ValueFromPipeline = $true,
				   ValueFromPipelineByPropertyName = $true,
				   ValueFromRemainingArguments=$true,
				   HelpMessage="Msg from computer."
		)]
#		[switch]		
		[string]$Msg
#
#		[Parameter(Mandatory=$false,
#                HelpMessage="Source computer.")]
#		[string]$Source
	)
	Begin
	{ 
#		$errorlogfile = "$home\Documents\PSlogs\Error_Log.txt"	
	}
	Process
	{  
#		Write-Host $errorlogfile 
		Write-Host $Msg
#		Write-Host $Source
	}
	End
	{ 
	}
}

My-Something -Msg aaaaaaaa