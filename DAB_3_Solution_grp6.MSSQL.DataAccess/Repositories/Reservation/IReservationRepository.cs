namespace DAB_3_Solution_grp6.MSSQL.DataAccess.Repositories.Reservation
{
    public interface IReservationRepository
    {
        Task<List<Entities.Reservation>> GetReservationsById(string auId);
    }
}
