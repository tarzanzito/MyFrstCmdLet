Function My-Something
{
	[CmdletBinding()]
	Param
	(
		$Msg
#		[Parameter(Mandatory=$false,
#                HelpMessage="Msg from computer.")] 
#		[string]$Msg,
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