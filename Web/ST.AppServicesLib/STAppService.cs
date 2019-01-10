using System.Collections.Generic;
using ST.SharedEntitiesLib;
using ST.SharedInterfacesLib;

namespace ST.AppServicesLib
{
    public class STAppService<TAppRepo> :
        ISTAppService<ISTRepo> where TAppRepo: ISTRepo
    {
        private readonly TAppRepo _appRepo;

        public STAppService(TAppRepo appRepo)
        {
            _appRepo = appRepo;
        }

        public Ticket CreateTicket(Ticket ticket)
        {
            var result = _appRepo.AddTicket(ticket);
            return result;
        }

        public ICollection<Ticket> GetActiveTickets()
        {
            var result = _appRepo.GetActiveTickets();
            return result;
        }

        public ICollection<Severity> GetSeverities()
        {
            var result = _appRepo.GetSeverities();
            return result;
        }

        public StaticData GetStaticData()
        {
            var result = new StaticData();
            result.Severities = GetSeverities();
            result.Products = GetProducts();
            return result;
        }

        public ICollection<Product> GetProducts()
        {
            var result = _appRepo.GetProducts();
            return result;
        }

        public Ticket GetTicket(int id)
        {
            var result = _appRepo.GetTicket(id);
            return result;
        }

        public Ticket AddTicket(Ticket ticket)
        {
            var result = _appRepo.AddTicket(ticket);

            return result;
        }

        public Ticket UpdateTicket(Ticket ticket)
        {
            Ticket result = _appRepo.UpdateTicket(ticket);
            return result;
        }

        public Ticket GetActiveTicket(int ticketId)
        {
            Ticket result = _appRepo.GetTicket(ticketId);
            return result;
        }

        public bool DeleteTicket(int ticketId)
        {
            var result = _appRepo.DeleteTicket(ticketId);
            return result;
        }

        // TODO: All callers to use this instead of the above methods
        public ISTRepo AppRepo => _appRepo;
    }
}
