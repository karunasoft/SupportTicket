using System.Collections.Generic;
using ST.SharedEntitiesLib;
using ST.SharedUserEntitiesLib;

namespace ST.SharedInterfacesLib
{
    public interface ISTAppService<out TAppRepo>  
        where TAppRepo : ISTRepo
    {
        ICollection<Ticket> GetActiveTickets();

        ICollection<Severity> GetSeverities();

        StaticData GetStaticData();

        ICollection<Product> GetProducts();

        Ticket GetTicket(int id);

        Ticket AddTicket(Ticket ticket);
        Ticket UpdateTicket(Ticket ticket);
        Ticket GetActiveTicket(int id);
        bool DeleteTicket(int id);
    }
}