namespace MedicalCentreApp.Data.Common
{
    public static class EntityValidation
    {
        public static class Doctor
        {
            public const int DoctorFullNameMaxLength = 100;           
            public const int DoctorSpecialtyMaxLength = 50;
            public const int MaxImageSizeInBytes = 2048;
        }
        public static class Patient
        {
            public const int PatientFirstNameMaxLength = 50;            
            public const int PatientMiddleNameMaxLength = 50;            
            public const int PatientLastNameMaxLength = 50;            
            public const int PatientEGNMaxLength = 10;            
            public const int PatientPhoneNumberMaxLength = 15;            
            public const int PatientEmailMaxLength = 100;            
            public const int PatientAddressMaxLength = 250;
        }
        public static class Appointment
        {            
            public const int AppointmentReasonMaxLength = 200;
        }
        public static class MedicalRecord
        {            
            public const int MedicalRecordDiagnosisMaxLength = 5000;            
            public const int MedicalRecordPrescriptionMaxLength = 500;
        }
        public static class Department
        {
            public const int DepartmentNameMaxLength = 100;
            public const int DepartmentDescriptionMaxLength = 250;
        }
        public static class Prescription
        {
            public const int PrescriptionMedicationNameMaxLength = 100;
            public const int PrescriptionDosageMaxLength = 100;
        }
        public static class Invoice
        {
            public const string InvoiceAmountColumnType = "decimal(18,2)";            
            public const double InvoiceAmountMaxValue = 10000;
        }
    }
}
