param([string]$RevitVersion="2024",[string]$ProjectName="CreateCube")
$ErrorActionPreference="Stop"
$root = Split-Path -Parent $MyInvocation.MyCommand.Path | Split-Path -Parent
$projDir = Join-Path $root "src\$ProjectName"
$bin = Join-Path $projDir "bin\Release\$ProjectName.dll"
$addinTemplate = Join-Path $projDir "$ProjectName.addin"
$dstDir = Join-Path $env:APPDATA "Autodesk\Revit\Addins\$RevitVersion\$ProjectName"
New-Item -ItemType Directory -Path $dstDir -Force | Out-Null
Copy-Item $bin $dstDir -Force
(Get-Content $addinTemplate) -replace "__ASSEMBLY__", (Join-Path $dstDir "$ProjectName.dll") | Set-Content (Join-Path $dstDir "$ProjectName.addin")
Write-Host "Deployed to $dstDir"
