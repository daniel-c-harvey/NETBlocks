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

# Pack Cerebellum.NetBlocks.Models
Write-Host 'Packing Cerebellum.NetBlocks.Models...'
dotnet pack Models.Shared/Models.Shared.csproj -c Release -o $OutDir

# Push main packages
Write-Host 'Pushing .nupkg files to nuget.org...'
dotnet nuget push "nupkgs/*.nupkg" `
    --api-key $ApiKey `
    --source https://api.nuget.org/v3/index.json `
    --skip-duplicate

# Push symbol packages
Write-Host 'Pushing .snupkg files to nuget.org...'
dotnet nuget push "nupkgs/*.snupkg" `
    --api-key $ApiKey `
    --source https://api.nuget.org/v3/index.json `
    --skip-duplicate

Write-Host ''
Write-Host 'Done. Published to nuget.org:'
Write-Host '  Cerebellum.NetBlocks 10.3.0'
Write-Host '  Cerebellum.NetBlocks.Models 10.3.0'
