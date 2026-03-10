using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;

namespace MedicalCentreApp.Data.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(MedicalCentreAppDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}