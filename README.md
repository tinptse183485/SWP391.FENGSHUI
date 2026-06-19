# 🐟 Koi Feng Shui Consulting System

> A full-stack web application that provides personalized Feng Shui consulting for Koi fish ponds — recommending fish types, colors, quantities, pond shapes, and directions based on the user's birth date and personal element.

---

## 📖 About the Project

**Koi Feng Shui Consulting System** is a team project built for the SWP391 course (Semester 5) at FPT University by a team of 5, with the author serving as Team Leader and Back-End Developer.

The system combines traditional Vietnamese Feng Shui principles with a modern web stack. Users enter their date of birth, and the system calculates their personal element (Metal, Wood, Water, Fire, Earth), life palace, and compatible directions — then recommends the most auspicious Koi fish varieties, colors, quantities, and pond configurations.

Beyond consulting, the platform supports an advertisement marketplace where Koi-related businesses can post ads, purchase visibility packages, and reach users browsing the site.

---

## ✨ Features

### Feng Shui Consulting
- Birth date → personal element calculation (with lunar calendar conversion)
- Life palace and auspicious direction determination
- Koi fish type, color, and quantity recommendations based on personal element
- Pond shape and directional alignment guidance
- Element compatibility and mutualism analysis

### Advertisement Marketplace
- Users can post Koi-related advertisements
- Tiered visibility packages (Rank-based) with expiration management
- Automated ad expiration via Hangfire background job
- Feedback and rating system per advertisement

### User Management
- Registration, login, and password reset with email verification
- Google OAuth login
- Role-based access control (Admin / Member)
- User profile management

### Admin Dashboard
- Revenue analytics by package
- User demographics by age group

### Payment
- VNPay integration for package purchases

### Blog
- Blog listing and detail pages for Feng Shui content

---

## 🛠️ Tech Stack

### Back-End

| Technology | Details |
|---|---|
| Framework | ASP.NET Core 6.0 Web API |
| Language | C# (.NET 6) |
| Database | SQL Server |
| ORM | Entity Framework Core 6.0 (SqlServer) |
| Authentication | JWT Bearer (HS256) |
| Background Jobs | Hangfire (SqlServer storage) |
| Password Hashing | BCrypt.Net-Next 4.0.3 |
| Payment | VNPay |
| Email | SMTP (Gmail) |
| API Docs | Swagger / Swashbuckle 6.5.0 |

### Front-End

| Technology | Details |
|---|---|
| Framework | React 18.3 + Vite 5.4 |
| UI Libraries | Ant Design 5.21, Material-UI 6.1, Framer Motion |
| HTTP Client | Axios 1.7 |
| Routing | React Router DOM 6.26 |
| Charts | Chart.js 4.4, Recharts 2.13 |
| Auth | JWT Decode, Google OAuth (@react-oauth/google) |
| Rich Text | TinyMCE React |
| Notifications | React Toastify |
| Testing | CodeceptJS 3.6 + Playwright 1.47 (E2E) |

---

## 🏛️ Project Structure

```
Web-Application-Koi-Feng-Shui-Consulting-System/
├── CreateDB/
│   └── Feng_Shui_Koi_Consulting_System_DB.sql   # Database creation script
├── KoiFengShui.BE/                               # Back-End solution
│   ├── KoiFengShui.BE/                           # API Layer
│   │   ├── Controllers/                          # 20 REST API controllers
│   │   ├── Attributes/                           # Custom authorize attributes
│   │   ├── Middleware/                           # JWT middleware
│   │   └── Program.cs                            # DI registration, middleware
│   ├── FengShuiKoi_Services/                     # Business Logic Layer
│   ├── FengShuiKoi_Repository/                   # Repository Layer
│   ├── FengShuiKoi_DAO/                          # Data Access Layer (EF Core)
│   └── FengShuiKoi_BO/                           # Business Objects (Models & DTOs)
└── KoiFengShui.FE/                               # Front-End (React + Vite)
    └── src/
        ├── page/                                 # 19 page components
        ├── components/                           # Shared UI components
        ├── config/                               # API config / axios setup
        └── utils/                               # Utility helpers
```

---

## 📡 API Modules

| Controller | Route | Description |
|---|---|---|
| AccountController | `/api/Account` | Login, register, update profile |
| FateController | `/api/Fate` | Element calculation, lunar conversion, life palace |
| ElementController | `/api/Element` | Feng Shui elements reference data |
| ElementMutualismController | `/api/ElementMutualism` | Element compatibility and mutualism |
| KoiVarietyController | `/api/KoiVariety` | Koi recommendations by DOB, color, element |
| ColorController | `/api/Color` | Color reference data |
| TypeColorController | `/api/TypeColor` | Color type mappings |
| QuantityOfFishController | `/api/QuantityOfFish` | Fish quantity recommendations by DOB |
| CompatibilityController | `/api/Compatibility` | Koi type compatibility scoring |
| PondController | `/api/Pond` | Pond shape and direction recommendations |
| ShapeController | `/api/Shape` | Pond shape reference data |
| DirectionController | `/api/Direction` | Direction reference data |
| LifePalaceDirectionController | `/api/LifePalaceDirection` | Life palace and direction mappings |
| AdvertisementController | `/api/Advertisement` | Ad listing, detail, sorted view |
| AdsPackageController | `/api/AdsPackage` | Ad package management |
| PackageController | `/api/Package` | Visibility package CRUD |
| FeedbackController | `/api/Feedback` | Ad feedback and ratings |
| BlogController | `/api/Blog` | Blog listing and detail |
| DashboardController | `/api/Dashboard` | Admin revenue and user analytics |
| VNPayController | `/api/VNPay` | Payment creation and callback |

---

## ⚙️ Background Jobs (Hangfire)

| Job | Description |
|---|---|
| `AdvertisementExpirationService` | Automatically expires advertisements when their package end date is reached |

Hangfire dashboard available at `/hangfire`.

---

## 🖥️ Front-End Pages

| Page | Description |
|---|---|
| Home | Landing page |
| Login / Register | Authentication pages |
| Forgot Password / Reset Password | Password recovery flow |
| Consulting | Main Feng Shui consulting entry point |
| Calculation | Personal element and life palace calculation |
| Calculate Compatibility | Koi type compatibility checker |
| Ads List | Browse all advertisements |
| Advertisement Detail | Single ad detail with feedback |
| Create Ads | Post a new advertisement |
| User Ads | Manage own advertisements |
| Choose Package | Select visibility package |
| Payment / Payment Success | VNPay payment flow |
| Blogs List | Browse Feng Shui blog posts |
| User Profile | View and edit profile |
| Admin Page | Admin dashboard with analytics |
| Policies | Terms and policies |

---

## 🚀 Getting Started

### Prerequisites

- .NET 6.0 SDK
- SQL Server
- Node.js 18+
- npm

### Back-End Setup

1. Clone the repository
2. Create the database using the script in `CreateDB/Feng_Shui_Koi_Consulting_System_DB.sql`
3. Update `appsettings.json` with your SQL Server connection string
4. Set the required environment variables:

```bash
JWT_SIGNING_KEY=your_jwt_secret_key
JWT_ISSUER=your_issuer
JWT_AUDIENCE=your_audience
```

5. Run the back-end:

```bash
cd KoiFengShui.BE
dotnet restore KoiFengShui.BE.sln
dotnet run --project KoiFengShui.BE/KoiFengShui.BE.csproj
```

API runs on `https://localhost:7xxx`
Swagger UI: `https://localhost:7xxx/swagger`
Hangfire Dashboard: `https://localhost:7xxx/hangfire`

### Front-End Setup

1. Navigate to the front-end directory:

```bash
cd KoiFengShui.FE
```

2. Install dependencies:

```bash
npm install
```

3. Start the development server:

```bash
npm run dev
```

Front-end runs on `http://localhost:5173`

---

## 🧪 E2E Testing

The project includes end-to-end tests using CodeceptJS and Playwright:

```bash
cd KoiFengShui.FE
npx codeceptjs run
```

---

## 📚 Course Information

- **Course**: SWP391 — Software Development Project
- **Semester**: Semester 5
- **University**: FPT University
- **Team size**: 5 members
- **Role**: Team Leader & Back-End Developer

---

## 📝 License

This project is developed for educational purposes as part of the FPT University SWP391 course.

