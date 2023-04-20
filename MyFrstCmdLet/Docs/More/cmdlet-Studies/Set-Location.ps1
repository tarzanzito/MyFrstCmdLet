Function Set-Location2
{
	[CmdletBinding()]
	Param
	(
		[Parameter(Mandatory=$true, HelpMessage="Literal Path.")] 
		[string]$LiteralPath
	)
	Begin
	{ 
		#Write-Host "Begin"
	}
	Process
	{  
		#Write-Host "Process1"
		Set-Location -LiteralPath $LiteralPath
		#Write-Host "Process2"
	}
	End
	{ 
		#Write-Host "End"
	}
}


Set-Location2 "C:\Users\Paulo\Desktop\EnchantedByte"
