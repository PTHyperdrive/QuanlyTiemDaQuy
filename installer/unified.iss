; Inno Setup Script for QuanLyTiemDaQuy - Unified Installer
; All 3 editions in one installer with license key validation
; Version 3.0.1

#define MyAppName "QuanLyTiemDaQuy"
#define MyAppVersion "3.0.1"
#define MyAppPublisher "Jewelry POS Solutions"
#define MyAppURL "https://jewelry-pos.vn"

[Setup]
AppId={{C3D4E5F6-A7B8-9012-CDEF-345678901234}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
DefaultDirName={autopf}\QuanLyTiemDaQuy
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=..\LICENSE
OutputDir=..\dist
OutputBaseFilename=QuanLyTiemDaQuy-Setup-v{#MyAppVersion}
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64compatible

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Types]
Name: "full"; Description: "Full Installation (Desktop + POS)"
Name: "mainline"; Description: "Mainline Desktop Only (.NET 4.8)"
Name: "pos"; Description: "POS Embedded Only (.NET 10)"
Name: "custom"; Description: "Custom Installation"; Flags: iscustom

[Components]
Name: "mainline"; Description: "Mainline Desktop Application (.NET 4.8)"; Types: full mainline custom
Name: "pos"; Description: "POS Embedded Application (Windows 10)"; Types: full pos custom
Name: "mobile"; Description: "Mobile APK (Copy to dist folder)"; Types: full custom

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"
Name: "posdesktopicon"; Description: "Create POS desktop icon"; GroupDescription: "{cm:AdditionalIcons}"; Components: pos

[Files]
; Mainline files (.NET 4.8)
Source: "..\bin\Release\QuanLyTiemDaQuy.exe"; DestDir: "{app}\Mainline"; Components: mainline; Flags: ignoreversion
Source: "..\bin\Release\*.dll"; DestDir: "{app}\Mainline"; Components: mainline; Flags: ignoreversion
Source: "..\bin\Release\*.config"; DestDir: "{app}\Mainline"; Components: mainline; Flags: ignoreversion

; POS Embedded files (.NET 10 MAUI)
Source: "..\QuanLyTiemDaQuy.Maui\bin\Release\net10.0-windows10.0.19041.0\win-x64\*"; DestDir: "{app}\POS"; Components: pos; Flags: ignoreversion recursesubdirs

; Mobile APK
Source: "..\dist\QuanLyTiemDaQuy-Mobile-v1.0.apk"; DestDir: "{app}\Mobile"; Components: mobile; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName} Desktop"; Filename: "{app}\Mainline\QuanLyTiemDaQuy.exe"; Components: mainline
Name: "{group}\{#MyAppName} POS"; Filename: "{app}\POS\QuanLyTiemDaQuy.Maui.exe"; Components: pos
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName} Desktop"; Filename: "{app}\Mainline\QuanLyTiemDaQuy.exe"; Tasks: desktopicon; Components: mainline
Name: "{autodesktop}\{#MyAppName} POS"; Filename: "{app}\POS\QuanLyTiemDaQuy.Maui.exe"; Tasks: posdesktopicon; Components: pos

[Run]
Filename: "{app}\Mainline\QuanLyTiemDaQuy.exe"; Description: "Launch Mainline Desktop"; Flags: nowait postinstall skipifsilent; Components: mainline

[Registry]
Root: HKLM; Subkey: "Software\JewelryPOS\QuanLyTiemDaQuy"; ValueType: string; ValueName: "Version"; ValueData: "{#MyAppVersion}"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\JewelryPOS\QuanLyTiemDaQuy"; ValueType: string; ValueName: "InstallPath"; ValueData: "{app}"

[Code]
var
  LicenseKeyPage: TInputQueryWizardPage;
  EditionPage: TInputOptionWizardPage;
  LicenseKey: String;
  SelectedEdition: Integer;

const
  // License key validation - format: QLTDQ-[FULL/POS/POSE]-2505-2004
  KEY_SUFFIX = '2505-2004';

function IsValidLicenseKey(Key: String): Boolean;
var
  UpperKey: String;
begin
  Result := False;
  UpperKey := UpperCase(Trim(Key));
  
  // Check valid formats
  if (Pos('QLTDQ-FULL-' + KEY_SUFFIX, UpperKey) = 1) then
    Result := True
  else if (Pos('QLTDQ-POS-' + KEY_SUFFIX, UpperKey) = 1) then
    Result := True
  else if (Pos('QLTDQ-POSE-' + KEY_SUFFIX, UpperKey) = 1) then
    Result := True
  // Development keys
  else if (Pos('QLTDQ-DEV', UpperKey) > 0) then
    Result := True;
end;

function GetLicenseType(Key: String): String;
var
  UpperKey: String;
begin
  UpperKey := UpperCase(Trim(Key));
  
  if Pos('FULL', UpperKey) > 0 then
    Result := 'Full'
  else if Pos('POSE', UpperKey) > 0 then
    Result := 'POS Embedded'
  else if Pos('POS', UpperKey) > 0 then
    Result := 'POS'
  else
    Result := 'Unknown';
end;

procedure InitializeWizard;
begin
  // License Key Page
  LicenseKeyPage := CreateInputQueryPage(wpLicense,
    'License Key', 'Enter your license key',
    'Please enter your QuanLyTiemDaQuy license key:' + #13#10 + #13#10 +
    'Valid formats:' + #13#10 +
    '  • QLTDQ-FULL-2505-2004 (All features)' + #13#10 +
    '  • QLTDQ-POS-2505-2004 (POS features)' + #13#10 +
    '  • QLTDQ-POSE-2505-2004 (POS Embedded)');
  LicenseKeyPage.Add('License Key:', False);
end;

function NextButtonClick(CurPageID: Integer): Boolean;
var
  LicType: String;
begin
  Result := True;
  
  if CurPageID = LicenseKeyPage.ID then
  begin
    LicenseKey := LicenseKeyPage.Values[0];
    
    if not IsValidLicenseKey(LicenseKey) then
    begin
      MsgBox('Invalid license key!' + #13#10 + #13#10 +
             'Please enter a valid key in format:' + #13#10 +
             'QLTDQ-FULL-2505-2004' + #13#10 +
             'QLTDQ-POS-2505-2004' + #13#10 +
             'QLTDQ-POSE-2505-2004', mbError, MB_OK);
      Result := False;
    end
    else
    begin
      LicType := GetLicenseType(LicenseKey);
      MsgBox('License validated successfully!' + #13#10 + #13#10 +
             'License Type: ' + LicType, mbInformation, MB_OK);
    end;
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    // Save license key
    RegWriteStringValue(HKEY_LOCAL_MACHINE, 'Software\JewelryPOS\QuanLyTiemDaQuy', 
      'LicenseKey', LicenseKey);
    RegWriteStringValue(HKEY_LOCAL_MACHINE, 'Software\JewelryPOS\QuanLyTiemDaQuy', 
      'LicenseType', GetLicenseType(LicenseKey));
  end;
end;
