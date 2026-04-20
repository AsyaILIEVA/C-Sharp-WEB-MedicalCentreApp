### MedicalCentreApp





###### MedicalCentreApp is an ASP.NET Core MVC web application for managing patients, doctors, appointments, and medical records in a medical centre environment.

###### This project was developed as part of the SoftUni C# Web ASP.NET Fundamentals course - January 2026 and improved and completed during C# Web ASP.NET Advanced course - February 2026. It follows modern ASP.NET Core architectural practices, including layered structure and separation of concerns.



#### 🚀 **Technologies \& Tools**



* ASP.NET Core MVC (.NET 8)
* Entity Framework Core
* SQL Server
* ASP.NET Core Identity (Authentication \& Roles)
* Razor Views
* Data Annotations (Validation)
* Visual Studio 2022



#### ✨ **Application Features**



* Patient management (Create, Read (Index, Details), Update (Edit), Delete)
* Doctor management (Create, Read (Index, Details), Update (Edit), Delete)
* Appointment scheduling system
* Automatic appointment status handling
* Medical record creation per appointment
* Role-based access (Admin, Doctor, Patient) was improved to Administrator(new Area), User, Doctor and Patient Roles
* Server-side validation using Data Annotations
* Seeded test data for easier demonstration
* Add Pagination
* Custom error pages



#### 🗄 **Database Design**



**Entities**

* Doctor
* Patient
* Appointment
* MedicalRecord

Added:

* Department
* Prescription
* Invoice



**Enum**

* AppointmentStatus



**Relationships**

* One Patient → Many Appointments
* One Doctor → Many Appointments
* One Appointment → One MedicalRecord
* One MedicalRecord → One Appointment



*The database design follows:*

Data validation through Data Annotations

One-to-many and one-to-one relationships



#### 🏗 **Architecture \& Project Structure**

The solution follows a layered architecture pattern:



###### 🌐 **Web Layer**

*MedicalCentreApp*

* Controllers - Add Controllers
* Razor Views
* Areas - Add Area (Administrator and Identity) with specific logic from Controllers and Views
* wwwroot
* Program.cs
* appsettings.json



###### 📦 **ViewModels Layer**

*MedicalCentreApp.ViewModels*



###### 🗄 **Data Layer**

*MedicalCentreApp.Data*

* Configuration - Excluded from project, Use RoleSeeder instead
* Migrations
* MedicalCentreAppDbContext

Add:

* Repositories, so the application flow is:

User Request → Controller → Service → Repository → DbContext → Database

Database Response → Repository → Service → Controller → View → User



*MedicalCentreApp.Data.Models*

* Entities
* Enums



###### ⚙️ **Services Layer**

*MedicalCentreApp.Services.Core*

* AppointmentService
* DoctorService
* PatientService

Add:

* MedicalRecordService
* InvoiceService
* DepartmentService
* PrescriptionService



*MedicalCentreApp.Services.Core.Interfaces*

* IAppointmentService
* IDoctorService
* IPatientService

Add:

* IMedicalRecordService
* IInvoiceService
* IDepartmentService
* IPrescriptionService



Add:

*MedicalCentreApp.Services.Tests*



###### 🔎 **Common Utilities**

*MedicalCentreApp.GCommon*

* EntityValidation - Moved to Common Folder
* ViewModelValidation - Added in GCommon



###### ***This structure ensures:***

###### Separation of concerns

###### Clean service abstraction

###### Maintainable and scalable codebase





#### ▶️ **How to Run the Project**



1️⃣ **Clone the Repository**



###### git clone *https://github.com/AsyaILIEVA/C-Sharp-WEB-MedicalCentreApp*

###### 

2️⃣ **Restore NuGet Packages**
dotnet restore



3️⃣ **Configure Database Connection**

Update your connection string in:



###### appsettings.Development.json

"ConnectionStrings": {
"DevConnection": *"Server=.;Database=MedicalCentreApp2026;Trusted\_Connection=True;Encrypted=False;"*
}
---

4️⃣ **Apply Migrations \& Update Database**

Using Package Manager Console:



###### Update-Database



5️⃣ **Run the Application**
dotnet run



The application will be available at:

https://localhost:7172



#### 🌱 **Seed Data**

On application startup, the database is seeded with:



* 12 sample doctors
* 20 sample patients 
* User roles - Administrator can manage Doctors, Patients, Appointments, Prescriptions and Invoices through Admin Panel/Dashboard.

 	Seeded Administrator - can be logged in with Email = admin@medicalcentre.com, Password = Admin123!

This allows quick testing and demonstration of the system.



#### 👤 Author

Asya Ilieva – SoftUni Student

