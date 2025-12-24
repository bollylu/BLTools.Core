<#
.SYNOPSIS
Extracts and formats semantic version from a version string.

.DESCRIPTION
Parses a semantic version string following the pattern: major.minor.patch[-prerelease][+build]
Returns a formatted version string suitable for .NET assemblies.

.PARAMETER VersionString
The version string to parse (typically from git ref).

.EXAMPLE
GetBuildVersion -VersionString "refs/heads/release/1.2.3-beta+42"
Returns: "1.2.3-beta.42"

.NOTES
Supports .NET 8, 9, and 10 targeting.
#>
function GetBuildVersion {
    param (
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$VersionString
    )

    # Define semantic version regex pattern
    $VersionPattern = '(?<major>\d+)(\.(?<minor>\d+))?(\.(?<patch>\d+))?(\-(?<pre>[0-9A-Za-z\-\.]+))?(\+(?<build>\d+))?'
    
    if ($VersionString -notmatch $VersionPattern) {
        Write-Warning "Version string '$VersionString' does not match semantic version pattern"
        return "1.0.0-build"
    }

    # Extract version components
    [uint64]$Major = [uint64]::Parse($matches['major'] ?? '0')
    [uint64]$Minor = [uint64]::Parse($matches['minor'] ?? '0')
    [uint64]$Patch = [uint64]::Parse($matches['patch'] ?? '0')
    [string]$PreReleaseTag = $matches['pre'] ?? [string]::Empty
    [uint64]$BuildRevision = [uint64]::Parse($matches['build'] ?? '0')

    # Build version string
    [string]$Version = "$Major.$Minor.$Patch"
    
    if ($PreReleaseTag) {
        $Version += "-$PreReleaseTag"
    }

    if ($BuildRevision -gt 0) {
        $Version += ".$BuildRevision"
    }

    return $Version
}

# Export function for module
Export-ModuleMember -Function GetBuildVersion