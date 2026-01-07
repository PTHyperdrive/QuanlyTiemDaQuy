; Inno Setup Script for QuanLyTiemDaQuy - Mainline Edition
; .NET Framework 4.8 Desktop Application

#define MyAppName "QuanLyTiemDaQuy - Mainline"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Jewelry POS Solutions"
#define MyAppURL "https://jewelry-pos.vn"
#define MyAppExeName "QuanLyTiemDaQuy.exe"

[Setup]
AppId={{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\QuanLyTiemDaQuy
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=..\LICENSE
OutputDir=..\dist
OutputBaseFilename=QuanLyTiemDaQuy-Setup-Mainline-v{#MyAppVersion}
; SetupIconFile=..\Properties\app.ico  ; TODO: Add icon file if needed
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode

[Files]
Source: "..\bin\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\*.config"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Registry]
Root: HKLM; Subkey: "Software\JewelryPOS\QuanLyTiemDaQuy"; ValueType: string; ValueName: "Edition"; ValueData: "Mainline"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\JewelryPOS\QuanLyTiemDaQuy"; ValueType: string; ValueName: "Version"; ValueData: "{#MyAppVersion}"
Root: HKLM; Subkey: "Software\JewelryPOS\QuanLyTiemDaQuy"; ValueType: string; ValueName: "InstallPath"; ValueData: "{app}"

[Code]
var
  LicenseKeyPage: TInputQueryWizardPage;
  LicenseKey: String;

function IsValidLicenseKey(Key: String): Boolean;
begin
  // Accept Full license keys for Mainline edition
  Result := (Pos('QLTDQ-FULL-', Key) = 1) or (Pos('QLTDQ-DEV', Key) > 0);
end;

procedure InitializeWizard;
begin
  LicenseKeyPage := CreateInputQueryPage(wpLicense,
    'License Key', 'Enter your license key',
    'Please enter your QuanLyTiemDaQuy Full Edition license key:' + #13#10 +
    'Format: QLTDQ-FULL-XXXX-XXXX');
  LicenseKeyPage.Add('License Key:', False);
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
  Result := True;
  if CurPageID = LicenseKeyPage.ID then
  begin
    LicenseKey := LicenseKeyPage.Values[0];
    if not IsValidLicenseKey(LicenseKey) then
    begin
      MsgBox('Invalid license key. Please enter a valid Full Edition key.', mbError, MB_OK);
      Result := False;
    end;
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    // Save license key to registry
    RegWriteStringValue(HKEY_LOCAL_MACHINE, 'Software\JewelryPOS\QuanLyTiemDaQuy', 
      'LicenseKey', LicenseKey);
  end;
end;
