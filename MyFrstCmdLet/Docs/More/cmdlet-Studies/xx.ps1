Function Set-Location3
{
	[CmdletBinding()]
	Param
	(
		[Parameter(Mandatory=$false, HelpMessage="Literal Path.")] 
		[string]$LiteralPath
	)
	Begin
	{ 
		Write-Host "Begin"
	}
	Process
	{  
		Write-Host "Process1"
		
		if ($LiteralPath)
		{
			Write-Host "with literal"
			Set-Location -LiteralPath $LiteralPath
		}
		else
		{
				Write-Host "empty literal"
			Set-Location
		}
			
		Write-Host "Process2"
	}
	End
	{ 
		Write-Host "End"
	}
}


Set-Location3 "c:\"
dir