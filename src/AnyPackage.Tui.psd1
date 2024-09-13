@{
    RootModule = 'AnyPackage.Tui.dll'
    ModuleVersion = '0.1.0'
    CompatiblePSEditions = @('Desktop', 'Core')
    GUID = '5dce6aa1-5e01-4c16-8d55-41060f3041ee'
    Author = 'Thomas Nieto'
    Copyright = '2024 (c) Thomas Nieto. All rights reserved.'
    Description = 'Text-based user interface for AnyPackage.'
    PowerShellVersion = '5.1'
    RequiredModules = @('AnyPackage')
    FunctionsToExport = @()
    CmdletsToExport = @('Show-AnyPackage')
    AliasesToExport = @()
    PrivateData = @{
        PSData = @{
            Tags = @('AnyPackage', 'TUI')
            LicenseUri = 'https://github.com/anypackage/tui/blob/main/LICENSE'
            ProjectUri = 'https://github.com/anypackage/tui'
        }
    }
    HelpInfoUri = 'https://go.anypackage.dev/help'
}
