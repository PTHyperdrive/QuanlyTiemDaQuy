# 💎 QuanLyTiemDaQuy - Jewelry Store Management System

A comprehensive jewelry store management system for gemstone trading, supporting desktop (Windows) and mobile (Android) platforms.

---

## 📦 Editions

| Edition | Platform | Framework | Use Case |
|---------|----------|-----------|----------|
| **Mainline** | Windows Desktop | .NET Framework 4.8 | Full-featured desktop application |
| **Mobile** | Android | .NET MAUI | Sales staff mobile app |
| **POS Embedded** | Windows Embedded 10 | .NET MAUI (WinUI) | Point-of-sale terminals |

---

## ✨ Features

### � Dashboard
- Real-time sales statistics
- Revenue tracking (daily/monthly)
- Low stock alerts
- Quick access to all modules

### � Product Management
- Complete gemstone catalog (Diamond, Ruby, Sapphire, Emerald, etc.)
- 4C grading system (Carat, Color, Clarity, Cut)
- Certificate management (GIA, IGI, HRD, AGS, Gübelin)
- Automatic product code generation (KC-XXX, RB-XXX, etc.)
- Display location tracking

### � Sales & Invoicing
- Quick sales processing
- Customer lookup
- Discount management (VIP tiers)
- Invoice printing
- Payment tracking

### � Import Management
- Purchase from suppliers
- Auto-create products during import
- Certificate validation
- Market price integration
- Import cost tracking

### 👥 Customer Management
- Customer database
- VIP/VVIP tier system
- Purchase history
- Loyalty discounts

### 📊 Reports
- Invoice reports (by date, status)
- Import stock reports
- Revenue analytics
- Export to various formats

### 🔐 Security & Access Control
- Role-based access (Admin, Manager, Sales)
- Password management
- Activity logging
- Module visibility per role

---

## 🛠️ Technology Stack

| Component | Technology |
|-----------|------------|
| Desktop App | Windows Forms (.NET 4.8) |
| Mobile App | .NET MAUI (Android) |
| Database | SQL Server |
| Architecture | 3-Layer (DAL → BLL → UI) |
| ORM | ADO.NET with stored procedures |

---

## 📁 Project Structure

```
QuanlyTiemDaQuy/
├── Forms/                      # WinForms UI (Mainline)
├── QuanLyTiemDaQuy.BLL/        # Business Logic Layer
├── QuanLyTiemDaQuy.DAL/        # Data Access Layer
├── QuanLyTiemDaQuy.Models/     # Shared Models
├── QuanLyTiemDaQuy.Maui/       # Mobile & POS WinUI App
├── QuanLyTiemDaQuy.Core/       # Shared Core (MAUI)
├── QuanLyTiemDaQuy.Core.BLL/   # Core Business Logic
├── QuanLyTiemDaQuy.Core.DAL/   # Core Data Access
└── Database/                   # SQL Scripts
```

---

## � Installation

### Prerequisites

- **Windows 10/11** (for Desktop & POS Embedded)
- **SQL Server 2019+** or Azure SQL
- **.NET Framework 4.8** (Mainline)
- **.NET 8.0 SDK** (for development)
- **Android 8.0+** (Mobile)

### Quick Start

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-org/QuanlyTiemDaQuy.git
   ```

2. **Set up the database**
   ```bash
   # Run the database scripts in order
   sqlcmd -S localhost -i Database/01_CreateTables.sql
   sqlcmd -S localhost -i Database/02_SeedData.sql
   ```

3. **Configure connection string**
   Edit `App.config` and update the connection string.

4. **Build and run**
   ```bash
   # Mainline (Desktop)
   dotnet build QuanlyTiemDaQuy.csproj
   
   # Mobile (APK)
   dotnet build QuanLyTiemDaQuy.Maui -f net9.0-android
   
   # POS Embedded (Windows)
   dotnet build QuanLyTiemDaQuy.Maui -f net9.0-windows10.0.19041.0
   ```

---

## 🔑 License Keys

The application supports 3 license types:

| License | Code | Features |
|---------|------|----------|
| **Full** | `QLTDQ-FULL-XXXX-XXXX` | All features, unlimited devices |
| **POS** | `QLTDQ-POS-XXXX-XXXX` | Sales, Products, Customers only |
| **POS Embedded** | `QLTDQ-POSE-XXXX-XXXX` | Optimized for embedded devices |

### Default Test Keys (Development Only)
```
Full:         QLTDQ-FULL-DEV1-2026
POS:          QLTDQ-POS-DEV1-2026
POS Embedded: QLTDQ-POSE-DEV1-2026
```

---

## � Building Installers

We recommend **Inno Setup** for creating Windows installers.

### Install Inno Setup
Download from: https://jrsoftware.org/isinfo.php

### Build Installers

```powershell
# 1. Build all editions
.\build-all.ps1

# 2. Create installers
iscc installer\mainline.iss    # Desktop installer
iscc installer\pos-embedded.iss # POS Embedded installer
```

### Installer Output
```
dist/
├── QuanLyTiemDaQuy-Setup-Mainline-v1.0.exe
├── QuanLyTiemDaQuy-Setup-POSEmbedded-v1.0.exe
└── QuanLyTiemDaQuy-Mobile-v1.0.apk
```

---

## 👥 Default Accounts

| Role | Username | Password |
|------|----------|----------|
| Admin | `admin` | `Admin@123` |
| Manager | `manager` | `Manager@123` |
| Sales | `sales` | `Sales@123` |

---

## 📞 Support

- **Email**: support@jewelry-pos.vn
- **Hotline**: 1900-XXX-XXX
- **Documentation**: [Wiki](./docs/wiki.md)

---

## 📄 License

Copyright © 2026 Jewelry POS Solutions. All rights reserved.

See [LICENSE](./LICENSE) for details.
