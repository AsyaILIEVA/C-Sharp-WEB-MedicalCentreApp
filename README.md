# C-Sharp-WEB-MedicalCentreApp



\# MedicalCentreApp



MedicalCentreApp is an ASP.NET Core MVC web application for managing patients, doctors, and medical appointments in a medical centre.



---



\## 🚀 Technologies Used



\- ASP.NET Core MVC (.NET 8)

\- Entity Framework Core

\- SQL Server

\- Razor Views



\- Visual Studio 2022



---


---

## 🗄 Database Structure

### Entities

The entities follow MVC standards: they use DataAnnotations for validation, enums for fixed values, explicit foreign keys, and clear one-to-many relationships. 
All constraints ensure data integrity.

- Doctor
- Patient
- Appointment
- MedicalRecord

### Enums
- AppointmentStatus

### Relationships
- One patient can have many appointments
- One doctor can have many appointments
- One appointment can have medicalrecord
- One medicalrecord has only one appointment

---

