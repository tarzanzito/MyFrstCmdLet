Function Set-Location2
{
	[CmdletBinding()]
	Param
	(
		[Parameter(Mandatory=$false, HelpMessage="Literal Path.")] 
		[string]$LiteralPath
	)
	Begin
	{ 
		Write-Host "Estou no: Set-Location2 : Begin"
	}
	Process
	{  
		Write-Host "Estou no: Set-Location2 : Process"
		
		if ($LiteralPath)
		{
			Write-Host "With Content"
			Set-Location -LiteralPath $LiteralPath
		}
		else
		{
			Write-Host "Empty"
			Set-Location
		}	
		Write-Host "Process zz"
	}
	End
	{ 
		Write-Host "Estou no: Set-Location2 : End"
	}
}

#nao me parece ser preciso o export
#Export-ModuleMember -Function "Set-Location2"

#Export-ModuleMember [[-Function] <string[]>] [-Cmdlet <string[]>] [-Variable <string[]>] [-Alias <string[]>] [<CommonParameters>]

Write-Host "Estou no: My-Demo.psm1"


#Export-ModuleMember -CmdLet "Set-Location2"

#PS C:\Users\Paulo\Desktop> Set-Location2
#My-Demo.psm1
#Set-Location2: The term 'Set-Location2' is not recognized as a name of a cmdlet, function, script file, or executable program.
#Check the spelling of the name, or if a path was included, verify that the path is correct and try again.

