namespace MedicalCentreApp.Common
{
    public static class EntityValidation
    {
        // Doctor
        public const int DoctorFullNameMinLength = 2;
        public const int DoctorFullNameMaxLength = 100;

        public const int DoctorSpecialtyMinLength = 3;
        public const int DoctorSpecialtyMaxLength = 50;

        public const int MaxImageSizeInBytes = 2 * 1024 * 1024; 


        // Patient
        public const int PatientFirstNameMinLength = 2;
        public const int PatientFirstNameMaxLength = 50;

        public const int PatientLastNameMinLength = 2;
        public const int PatientLastNameMaxLength = 50;

        public const int PatientEGNMinLength = 10;
        public const int PatientEGNMaxLength = 10;

        public const int PatientPhoneNumberMinLength = 10;
        public const int PatientPhoneNumberMaxLength = 15; // for international numbers

        // Appointment
        public const int AppointmentReasonMinLength = 5;
        public const int AppointmentReasonMaxLength = 200;

        // MedicalRecord
        public const int MedicalRecordDiagnosisMinLength = 10;
        public const int MedicalRecordDiagnosisMaxLength = 5000;

        public const int MedicalRecordPrescriptionMinLength = 10;
        public const int MedicalRecordPrescriptionMaxLength = 500;
    }
}
