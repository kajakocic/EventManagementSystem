using projekat_kaja.DTOs;
using projekat_kaja.Models;

namespace projekat_kaja.Services;

public interface IRegistrationService
{
    Registration MakeReservation(int eventId, int UserId, int brMesta);

    Registration AddReservation(AddRegDTO reservationDto);
    RegistrationDTO GetReservation(int id);
    IEnumerable<RegistrationDTO> GetReservations(int userId);
    void DeleteReservation(int id);

}