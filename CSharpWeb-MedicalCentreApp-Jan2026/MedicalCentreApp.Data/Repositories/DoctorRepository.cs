using MedicalCentreApp.Data.Models;
using MedicalCentreApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicalCentreApp.Data.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(MedicalCentreAppDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Doctor?> GetDoctorWithAppointmentsAsync(int id)
        {
            return await dbSet
                .Include(d => d.Appointments)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}