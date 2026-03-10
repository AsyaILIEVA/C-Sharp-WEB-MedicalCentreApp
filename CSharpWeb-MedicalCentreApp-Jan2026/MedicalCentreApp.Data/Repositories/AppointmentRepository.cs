using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;

namespace MedicalCentreApp.Data.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(MedicalCentreAppDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}