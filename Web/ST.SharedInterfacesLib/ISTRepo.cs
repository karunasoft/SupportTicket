using System.Collections.Generic;
using ST.SharedEntitiesLib;

namespace ST.SharedInterfacesLib
{
    public interface ISTRepo
    {
        void Initialise(string connectionString);

        Ticket AddTicket(Ticket info);

        ICollection<Ticket> GetActiveTickets();

        ICollection<Severity> GetSeverities();

        ICollection<Product> GetProducts();

        Ticket GetTicket(int id);
        Ticket UpdateTicket(Ticket ticket);
        bool DeleteTicket(int ticketId);
    }
}
