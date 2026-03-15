namespace MedicalCentreApp.GCommon
{
    public static class ViewModelValidation
    {
        public static class DoctorViewModels
        {
            /// <summary>
            /// Doctor FullName should be at least 2 characters.
            /// </summary>
            public const int DoctorFullNameMinLength = 2;

            /// <summary>
            /// Doctor FullName should be able to store text with length up to 100 characters.
            /// </summary>
            public const int DoctorFullNameMaxLength = 100;

            /// <summary>
            /// Doctor Specialty should be at least 3 characters.
            /// </summary>
            public const int DoctorSpecialtyMinLength = 3;

            /// <summary>
            /// Doctor Specialty should be able to store text with length up to 50 characters.
            /// </summary>
            public const int DoctorSpecialtyMaxLength = 50;

            /// <summary>
            /// Doctor MaxImageSizeInBytes should be up to 2048 MB.
            /// </summary>
            public const int MaxImageSizeInBytes = 2048;
        }

        public static class PatientViewModels
        {
            /// <summary>
            /// Patient first name must be at least 2 characters.
            /// </summary>
            public const int PatientFirstNameMinLength = 2;

            /// <summary>
            /// Patient first name can contain up to 50 characters.
            /// </summary>
            public const int PatientFirstNameMaxLength = 50;

            /// <summary>
            /// Patient middle name must be at least 2 characters.
            /// </summary>
            public const int PatientMiddleNameMinLength = 2;

            /// <summary>
            /// Patient middle name can contain up to 50 characters.
            /// </summary>
            public const int PatientMiddleNameMaxLength = 50;

            /// <summary>
            /// Patient last name must be at least 2 characters.
            /// </summary>
            public const int PatientLastNameMinLength = 2;

            /// <summary>
            /// Patient last name can contain up to 50 characters.
            /// </summary>
            public const int PatientLastNameMaxLength = 50;

            /// <summary>
            /// Patient EGN must contain exactly 10 digits.
            /// </summary>
            public const int PatientEGNMinLength = 10;

            /// <summary>
            /// Patient EGN must contain exactly 10 digits.
            /// </summary>
            public const int PatientEGNMaxLength = 10;

            /// <summary>
            /// Patient phone number must be between 10 and 15 characters.
            /// </summary>
            public const int PatientPhoneNumberMinLength = 10;

            /// <summary>
            /// Patient phone number can contain up to 15 characters.
            /// </summary>
            public const int PatientPhoneNumberMaxLength = 15;

            /// <summary>
            /// Patient email must be at least 5 characters.
            /// </summary>
            public const int PatientEmailMinLength = 5;

            /// <summary>
            /// Patient email can contain up to 100 characters.
            /// </summary>
            public const int PatientEmailMaxLength = 100;

            /// <summary>
            /// Patient address must be at least 10 characters.
            /// </summary>
            public const int PatientAddressMinLength = 10;

            /// <summary>
            /// Patient address can contain up to 250 characters.
            /// </summary>
            public const int PatientAddressMaxLength = 250;
        }

        public static class AppointmentViewModels
        {
            /// <summary>
            /// Appointment reason must be at least 5 characters.
            /// </summary>
            public const int AppointmentReasonMinLength = 5;

            /// <summary>
            /// Appointment reason can contain up to 200 characters.
            /// </summary>
            public const int AppointmentReasonMaxLength = 200;
        }

        public static class MedicalRecordViewModels
        {
            /// <summary>
            /// Diagnosis must be at least 10 characters.
            /// </summary>
            public const int MedicalRecordDiagnosisMinLength = 10;

            /// <summary>
            /// Diagnosis can contain up to 5000 characters.
            /// </summary>
            public const int MedicalRecordDiagnosisMaxLength = 5000;

            /// <summary>
            /// Prescription description must be at least 10 characters.
            /// </summary>
            public const int MedicalRecordPrescriptionMinLength = 10;

            /// <summary>
            /// Prescription description can contain up to 500 characters.
            /// </summary>
            public const int MedicalRecordPrescriptionMaxLength = 500;
        }

        public static class DepartmentViewModels
        {
            /// <summary>
            /// Department Name should be at least 3 characters.
            /// </summary>
            public const int DepartmentNameMinLength = 3;

            /// <summary>
            /// Department name can contain up to 100 characters.
            /// </summary>
            public const int DepartmentNameMaxLength = 100;

            /// <summary>
            /// Department Description should be at least 10 characters.
            /// </summary>
            public const int DepartmentDescriptionMinLength = 10;

            /// <summary>
            /// Department description can contain up to 250 characters.
            /// </summary>
            public const int DepartmentDescriptionMaxLength = 250;
        }

        public static class PrescriptionViewModels
        {
            /// <summary>
            /// Medication Name should be at least 10 characters.
            /// </summary>
            public const int PrescriptionMedicationNameMinLength = 10;

            /// <summary>
            /// Medication Name can contain up to 100 characters.
            /// </summary>
            public const int PrescriptionMedicationNameMaxLength = 100;

            /// <summary>
            /// Dosage information should be at least 5 characters.
            /// </summary>
            public const int PrescriptionDosageMinLength = 5;

            /// <summary>
            /// Dosage information can contain up to 100 characters.
            /// </summary>
            public const int PrescriptionDosageMaxLength = 100;
        }

        public static class InvoiceViewModels
        {
            /// <summary>
            /// SQL column type used to store invoice amount.
            /// </summary>
            public const string InvoiceAmountColumnType = "decimal(18,2)";

            /// <summary>
            /// Minimum allowed invoice amount.
            /// </summary>
            public const double InvoiceAmountMinValue = 0;

            /// <summary>
            /// Maximum allowed invoice amount.
            /// </summary>
            public const double InvoiceAmountMaxValue = 10000;
        }
    }
}
