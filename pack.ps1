#Requires -Version 5.1
param(
    [Parameter(Mandatory)]
    [string]$ApiKey
)

$ErrorActionPreference = 'Stop'

$OutDir = './nupkgs'

# Wipe and recreate output directory so stale packages never accumulate
if (Test-Path $OutDir) {
    Remove-Item $OutDir -Recurse -Force
}
New-Item $OutDir -ItemType Directory | Out-Null

# Pack Cerebellum.NetBlocks (core, no intra-solution dependencies)
Write-Host 'Packing Cerebellum.NetBlocks...'
dotnet pack NetBlocks.csproj -c Release -o $OutDir
if ($LASTEXITCODE -ne 0) { throw "dotnet pack failed for NetBlocks.csproj (exit code $LASTEXITCODE)" }

# Pack Cerebellum.NetBlocks.Models
Write-Host 'Packing Cerebellum.NetBlocks.Models...'
dotnet pack Models.Shared/Models.Shared.csproj -c Release -o $OutDir
if ($LASTEXITCODE -ne 0) { throw "dotnet pack failed for Models.Shared.csproj (exit code $LASTEXITCODE)" }

# Read version from NetBlocks.csproj
$csproj = [xml](Get-Content 'NetBlocks.csproj')
$Version = $csproj.Project.PropertyGroup | Where-Object { $_.Version } | Select-Object -ExpandProperty Version

# Push main packages
Write-Host 'Pushing .nupkg files to nuget.org...'
dotnet nuget push "nupkgs/*.nupkg" `
    --api-key $ApiKey `
    --source https://api.nuget.org/v3/index.json
if ($LASTEXITCODE -ne 0) { throw "dotnet nuget push failed (exit code $LASTEXITCODE)" }

# Push symbol packages
Write-Host 'Pushing .snupkg files to nuget.org...'
dotnet nuget push "nupkgs/*.snupkg" `
    --api-key $ApiKey `
    --source https://api.nuget.org/v3/index.json
if ($LASTEXITCODE -ne 0) { throw "dotnet nuget push failed (exit code $LASTEXITCODE)" }

Write-Host ''
Write-Host 'Done. Published to nuget.org:'
Write-Host "  Cerebellum.NetBlocks $Version"
Write-Host "  Cerebellum.NetBlocks.Models $Version"
