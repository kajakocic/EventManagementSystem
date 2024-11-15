using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public class RegistrationRepository : GenericRepository<Registration>, IRegistrationRepository
{
    public RegistrationRepository(EMSContext context) : base(context)
    {
    }
}