C-Sharp-WEB-MedicalCentreApp
MedicalCentreApp








MedicalCentreApp is an ASP.NET Core MVC web application for managing patients, doctors, appointments, and medical records in a medical centre environment.

This project was developed as part of the SoftUni Web Fundamentals course and follows modern ASP.NET Core architectural practices, including layered structure and separation of concerns.

🚀 Technologies & Tools

ASP.NET Core MVC (.NET 8)

Entity Framework Core

SQL Server

ASP.NET Core Identity (Authentication & Roles)

Razor Views

Data Annotations (Validation)

Visual Studio 2022

✨ Application Features

🔹 Patient management (Create, Read, Update, Delete)

🔹 Doctor management (Create, Read, Update, Delete)

🔹 Appointment scheduling system

🔹 Automatic appointment status handling

🔹 Medical record creation per appointment

🔹 Role-based access (Admin, Doctor, Patient)

🔹 Server-side validation using Data Annotations

🔹 Seeded test data for easier demonstration

🗄 Database Design
Entities

Doctor

Patient

Appointment

MedicalRecord

Enum

AppointmentStatus

Relationships

One Patient → Many Appointments

One Doctor → Many Appointments

One Appointment → One MedicalRecord

One MedicalRecord → One Appointment

The database design follows:

Explicit foreign keys

One-to-many and one-to-one relationships

Data validation through Data Annotations

Clear separation between entities and view models

🏗 Architecture & Project Structure

The solution follows a layered architecture pattern:

🌐 Web Layer

MedicalCentreApp

Controllers

Razor Views

Areas

wwwroot

Program.cs

appsettings.json

📦 ViewModels Layer

MedicalCentreApp.ViewModels

🗄 Data Layer

MedicalCentreApp.Data

Configuration

Migrations

MedicalCentreDbContext

MedicalCentreApp.Data.Models

Entities

Enums

⚙️ Services Layer

MedicalCentreApp.Services.Core

AppointmentService

DoctorService

PatientService

MedicalCentreApp.Services.Core.Interfaces

IAppointmentService

IDoctorService

IPatientService

🔎 Common Utilities

MedicalCentreApp.GCommon

EntityValidation

This structure ensures:

Separation of concerns

Clean service abstraction

Maintainable and scalable codebase

▶️ How to Run the Project
1️⃣ Clone the Repository
git clone https://github.com/AsyaILIEVA/C-Sharp-WEB-MedicalCentreApp
cd MedicalCentreApp

2️⃣ Restore NuGet Packages
dotnet restore

3️⃣ Configure Database Connection

Update your connection string in:

appsettings.Development.json

"ConnectionStrings": {
  "DevConnection": "Server=.;Database=MedicalCentreApp2026;Trusted_Connection=True;Encrypted=False;"
}

4️⃣ Apply Migrations & Update Database

Using Package Manager Console:

Update-Database


Or using CLI:

dotnet ef database update

5️⃣ Run the Application
dotnet run


The application will be available at:

https://localhost:7172

🌱 Seed Data

On application startup, the database is seeded with:

5 sample doctors

10 sample patients

Default user roles (Admin, Doctor, Patient)

This allows quick testing and demonstration of the system.

👤 Author

Asya Ilieva – SoftUni Student
