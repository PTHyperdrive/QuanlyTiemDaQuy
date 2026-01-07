# Build Script for QuanLyTiemDaQuy
# Creates all 3 editions: Mainline, Mobile APK, POS Embedded

param(
    [switch]$Mainline,
    [switch]$Mobile,
    [switch]$POSEmbedded,
    [switch]$All,
    [switch]$CreateInstallers
)

$ErrorActionPreference = "Stop"
$DistDir = ".\dist"

# Create dist directory if not exists
if (-not (Test-Path $DistDir)) {
    New-Item -ItemType Directory -Path $DistDir | Out-Null
}

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  QuanLyTiemDaQuy Build Script" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Build Mainline (.NET Framework 4.8)
function Build-Mainline {
    Write-Host "[1/3] Building Mainline Edition (.NET 4.8)..." -ForegroundColor Yellow
    
    dotnet build QuanlyTiemDaQuy.csproj -c Release
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "  ✅ Mainline build successful!" -ForegroundColor Green
        Write-Host "  Output: bin\Release\QuanLyTiemDaQuy.exe" -ForegroundColor Gray
    }
    else {
        Write-Host "  ❌ Mainline build failed!" -ForegroundColor Red
        exit 1
    }
}

# Build Mobile APK (.NET MAUI Android)
function Build-Mobile {
    Write-Host "[2/3] Building Mobile Edition (Android APK)..." -ForegroundColor Yellow
    
    Push-Location "QuanLyTiemDaQuy.Maui"
    
    dotnet publish -f net10.0-android -c Release
    
    if ($LASTEXITCODE -eq 0) {
        # Copy APK to dist
        $apkPath = Get-ChildItem -Path "bin\Release\net10.0-android\publish\*.apk" -Recurse | Select-Object -First 1
        if ($apkPath) {
            Copy-Item $apkPath.FullName -Destination "..\$DistDir\QuanLyTiemDaQuy-Mobile-v1.0.apk"
            Write-Host "  ✅ Mobile build successful!" -ForegroundColor Green
            Write-Host "  Output: $DistDir\QuanLyTiemDaQuy-Mobile-v1.0.apk" -ForegroundColor Gray
        }
    }
    else {
        Write-Host "  ❌ Mobile build failed!" -ForegroundColor Red
    }
    
    Pop-Location
}

# Build POS Embedded (.NET MAUI WinUI for Windows Embedded 10)
function Build-POSEmbedded {
    Write-Host "[3/3] Building POS Embedded Edition (Windows Embedded)..." -ForegroundColor Yellow
    
    Push-Location "QuanLyTiemDaQuy.Maui"
    # Use framework-dependent deployment (requires .NET runtime installed on target)
    dotnet publish -f net10.0-windows10.0.19041.0 -c Release
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "  ✅ POS Embedded build successful!" -ForegroundColor Green
        Write-Host "  Output: QuanLyTiemDaQuy.Maui\bin\Release\net9.0-windows10.0.19041.0\win10-x64\publish\" -ForegroundColor Gray
    }
    else {
        Write-Host "  ❌ POS Embedded build failed!" -ForegroundColor Red
    }
    
    Pop-Location
}

# Create Installers using Inno Setup
function Create-Installers {
    Write-Host ""
    Write-Host "[INSTALLER] Creating installers with Inno Setup..." -ForegroundColor Magenta
    
    $InnoSetup = "C:\Program Files (x86)\Inno Setup 6\ISCC.exe"
    
    if (-not (Test-Path $InnoSetup)) {
        Write-Host "  ⚠️ Inno Setup not found at: $InnoSetup" -ForegroundColor Yellow
        Write-Host "  Download from: https://jrsoftware.org/isinfo.php" -ForegroundColor Gray
        return
    }
    
    # Mainline installer
    Write-Host "  Creating Mainline installer..." -ForegroundColor Cyan
    & $InnoSetup "installer\mainline.iss"
    
    # POS Embedded installer
    Write-Host "  Creating POS Embedded installer..." -ForegroundColor Cyan
    & $InnoSetup "installer\pos-embedded.iss"
    
    Write-Host "  ✅ Installers created in $DistDir\" -ForegroundColor Green
}

# Main execution
if ($All -or (-not $Mainline -and -not $Mobile -and -not $POSEmbedded)) {
    Build-Mainline
    Build-Mobile
    Build-POSEmbedded
}
else {
    if ($Mainline) { Build-Mainline }
    if ($Mobile) { Build-Mobile }
    if ($POSEmbedded) { Build-POSEmbedded }
}

if ($CreateInstallers -or $All) {
    Create-Installers
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Build Complete!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Outputs:" -ForegroundColor White
Write-Host "  - Mainline:     bin\Release\QuanLyTiemDaQuy.exe"
Write-Host "  - Mobile APK:   $DistDir\QuanLyTiemDaQuy-Mobile-v1.0.apk"
Write-Host "  - POS Embedded: QuanLyTiemDaQuy.Maui\bin\Release\...\publish\"
Write-Host ""
