
function Get-ListOfFiles {
	#[OutputType([System.IO.FileInfo])]
	#[OutputType('System.IO.FileInfo')]
    [CmdletBinding()]
    param(
        [string]$Path
    )
    Get-ChildItem -Path $Path
}

function Get-RandomDate()
{
    [OutputType([System.DateTime])]
    Param (
        [parameter(Mandatory=$true)]
        [System.Int32]
        $DaysAgo
    )
    [System.DateTime]$ret = [System.DateTime]::Today
    $randomDays = Get-Random -Minimum 0 -Maximum $DaysAgo
    $ret = [System.DateTime]::Today.AddDays($randomDays * -1)

    "Hello world!"
    42
    
    return $ret
}

function Write-Message
{
  [CmdletBinding()]
    param(
        [string]$Msg
    )

	Write-Host 'Message:' $Msg
}
