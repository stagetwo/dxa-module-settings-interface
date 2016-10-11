param($installPath, $toolsPath, $package, $project)

Add-Type -AssemblyName 'Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'

$project = Get-Project
$buildProject = [Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection.GetLoadedProjects($project.FullName) | Select-Object -First 1

$alchemyPropertyGroup = $null

Foreach ($propertyGroup in $buildProject.Xml.PropertyGroups) 
{
    Foreach ($property in $propertyGroup.Properties)
    {
        if ($property.Name -eq "AlchemyUploadDialog") {
            $alchemyPropertyGroup = $propertyGroup;
            break
        }
    }
}
if ($alchemyPropertyGroup -eq $null) {
    $alchemyPropertyGroup = $buildProject.Xml.AddPropertyGroup()
}

$hasAlchemyRemoveBuildFiles = $false
$hasAlchemyUploadDialog = $false

Foreach ($property in $alchemyPropertyGroup.Properties)
{
    if ($property.Name -eq "AlchemyRemoveBuildFiles")
    {
        $hasAlchemyRemoveBuildFiles = $true
    }
    if ($property.Name -eq "AlchemyUploadDialog")
    {
        $hasAlchemyUploadDialog = $true
    }
}
if ($hasAlchemyRemoveBuildFiles -eq $false) {
    $alchemyPropertyGroup.AddProperty("AlchemyRemoveBuildFiles", "NonProjectAssembly")
}
if ($hasAlchemyUploadDialog -eq $false) {
    $alchemyPropertyGroup.AddProperty("AlchemyUploadDialog", "DEBUG|RELEASE")
}

$project.Save()