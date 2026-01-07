; Inno Setup Script for QuanLyTiemDaQuy - POS Embedded Edition
; .NET MAUI WinUI Application for Windows Embedded 10

#define MyAppName "QuanLyTiemDaQuy - POS Embedded"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Jewelry POS Solutions"
#define MyAppURL "https://jewelry-pos.vn"
#define MyAppExeName "QuanLyTiemDaQuy.Maui.exe"

[Setup]
AppId={{B2C3D4E5-F6A7-8901-BCDE-F23456789012}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
DefaultDirName={autopf}\QuanLyTiemDaQuy-POS
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=..\LICENSE
OutputDir=..\dist
OutputBaseFilename=QuanLyTiemDaQuy-Setup-POSEmbedded-v{#MyAppVersion}
; SetupIconFile=..\Properties\app.ico  ; TODO: Add icon file if needed
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64
; Windows Embedded 10 specific
MinVersion=10.0

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"
Name: "autostart"; Description: "Start automatically with Windows"; GroupDescription: "Startup Options:"; Flags: unchecked

[Files]
; MAUI WinUI output files
Source: "..\QuanLyTiemDaQuy.Maui\bin\Release\net10.0-windows10.0.19041.0\win-x64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Registry]
Root: HKLM; Subkey: "Software\JewelryPOS\QuanLyTiemDaQuy"; ValueType: string; ValueName: "Edition"; ValueData: "POSEmbedded"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\JewelryPOS\QuanLyTiemDaQuy"; ValueType: string; ValueName: "Version"; ValueData: "{#MyAppVersion}"
Root: HKLM; Subkey: "Software\JewelryPOS\QuanLyTiemDaQuy"; ValueType: string; ValueName: "InstallPath"; ValueData: "{app}"
; Auto-start if selected
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "QuanLyTiemDaQuy-POS"; ValueData: """{app}\{#MyAppExeName}"""; Flags: uninsdeletevalue; Tasks: autostart

[Code]
var
  LicenseKeyPage: TInputQueryWizardPage;
  LicenseKey: String;

function IsValidLicenseKey(Key: String): Boolean;
begin
  // Accept POS Embedded or Full license keys
  Result := (Pos('QLTDQ-POSE-', Key) = 1) or 
            (Pos('QLTDQ-FULL-', Key) = 1) or 
            (Pos('QLTDQ-DEV', Key) > 0);
end;

procedure InitializeWizard;
begin
  LicenseKeyPage := CreateInputQueryPage(wpLicense,
    'License Key', 'Enter your license key',
    'Please enter your QuanLyTiemDaQuy POS Embedded license key:' + #13#10 +
    'Format: QLTDQ-POSE-XXXX-XXXX' + #13#10 + #13#10 +
    'Note: Full Edition keys (QLTDQ-FULL-*) are also accepted.');
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
      MsgBox('Invalid license key. Please enter a valid POS Embedded or Full Edition key.', mbError, MB_OK);
      Result := False;
    end;
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    RegWriteStringValue(HKEY_LOCAL_MACHINE, 'Software\JewelryPOS\QuanLyTiemDaQuy', 
      'LicenseKey', LicenseKey);
  end;
end;

// Disable unnecessary Windows features for POS kiosk mode
procedure EnableKioskMode;
begin
  // Set shell to our app for kiosk mode (optional, requires admin)
  // RegWriteStringValue(HKEY_LOCAL_MACHINE, 'Software\Microsoft\Windows NT\CurrentVersion\Winlogon',
  //   'Shell', ExpandConstant('{app}\{#MyAppExeName}'));
end;
