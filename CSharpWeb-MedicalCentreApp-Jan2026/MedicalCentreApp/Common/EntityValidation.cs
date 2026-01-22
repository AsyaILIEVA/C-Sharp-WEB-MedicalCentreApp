namespace MedicalCentreApp.Common
{
    public static class EntityValidation
    {
        // Doctor
        public const int DoctorFullNameMaxLength = 100;
        public const int DoctorSpecialtyMaxLength = 50;

        // Patient
        public const int PatientFirstNameMaxLength = 50;
        public const int PatientLastNameMaxLength = 50;
        public const int PatientEGNMinLength = 10;
        public const int PatientEGNMaxLength = 10;
        public const int PatientPhoneNumberMinLength = 10;
        public const int PatientPhoneNumberMaxLength = 15; // for international numbers

        // Appointment
        public const int AppointmentReasonMaxLength = 200;

        // MedicalRecord
        public const int MedicalRecordDiagnosisMaxLength = 5000;
        public const int MedicalRecordPrescriptionMaxLength = 500;
    }
}
