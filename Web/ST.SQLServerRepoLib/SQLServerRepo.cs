﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ST.SharedEntitiesLib;
using ST.SharedHelpersLib.EntityFramework;
using ST.SharedInterfacesLib;
using ST.SharedHelpersLib.Extensions;

namespace ST.SQLServerRepoLib
{
    public class SQLRepo : ISTRepo
    {
        private readonly SupportTicketDbContext _context;
        private static string _connectionString;
        public SQLRepo(SupportTicketDbContext context)
        {
            _context = context;
        }
        public void Initialise(string connectionString)
        {
            _connectionString = connectionString;

            using (var context = new SupportTicketDbContext(connectionString))
            {
                context.Database.Migrate();
            }

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            using (var context = new SupportTicketDbContext(_connectionString))
            {
                var products = new List<Product>()
                {
                    new Product() {Description = "P1"},
                    new Product() {Description = "P2"},
                    new Product() {Description = "P3"}
                };

                foreach (var newProduct in products)
                {
                    var dbProduct =
                        context.Product.ToList().FirstOrDefault(p => p.Description.Equals(newProduct.Description));
                    if (dbProduct == null)
                    {
                        context.Product.AddOrUpdate(newProduct);
                    }
                }

                var severities = new List<Severity>()
                {
                    new Severity() {DisplayName = "Mild"},
                    new Severity() {DisplayName = "Medium"},
                    new Severity() {DisplayName = "Critical"}
                };

                foreach (var newSeverity in severities)
                {
                    var dbSeverity =
                        context.Severity.ToList().FirstOrDefault(s => s.DisplayName.Equals(newSeverity.DisplayName));
                    if (dbSeverity == null)
                    {
                        context.Severity.AddOrUpdate(newSeverity);
                    }
                }

                context.SaveChanges(true);
            }
        }

        public virtual Ticket AddTicket(Ticket ticket)
        {
            var result = EfHelpers.Execute(
                _context, 
                $"Could not add ticketId: {ticket.TicketId} ",
                ctx =>
                {
                    ticket.Product = null;
                    ticket.Severity = null;
                    ctx.Tickets.Add(ticket);

                    ctx.SaveChanges();

                    return ticket;
                }
            );
            return result;
        }

        public ICollection<Ticket> GetActiveTickets()
        {
            var result = EfHelpers.Execute(
                _context,
                "Could not get active tickets from the database",
                ctx =>
                {
                    var tickets = ctx.Tickets
                        .Include("Severity")
                        .Include("Product")
                        .Where(t => t.Active)
                        .ToList();
                    return tickets;
                });

            return result;
        }

        private ICollection<Ticket> GetActiveTicketsMatching(List<int> ticketIds)
        {
            var result = EfHelpers.Execute(
                _context, 
                $"Could not get active tickets matching ticketIds:{string.Join(',', ticketIds)}",
                ctx =>
                {
                    var tickets = ctx.Tickets
                        .Include("Severity")
                        .Include("Product")
                        .Where(t => t.Active && ticketIds.Any(tid => tid == t.TicketId))
                        .ToList();
                    return tickets;
                }
            );

            return result;
        }

        public ICollection<Severity> GetSeverities()
        {
            using (var ctx = new SupportTicketDbContext(_connectionString))
            {
                var result = ctx.Severity.ToList();
                return result;
            }
        }

        public ICollection<Product> GetProducts()
        {
            using (var ctx = new SupportTicketDbContext(_connectionString))
            {
                var result = ctx.Product.ToList();
                return result;
            }

            //return new List<Product>();
        }

        public Ticket GetTicket(int ticketId)
        {
            var result = EfHelpers.Execute(
                _context,
                $"Could not GetTicket({ticketId})",
                ctx =>
                {
                    var ticket = ctx.Tickets
                        .Include("Severity")
                        .Include("Product")
                        .FirstOrDefault(t => t.TicketId.Equals(ticketId));
                    return ticket;
                }
            );
            return result;
        }

        public Ticket UpdateTicket(Ticket ticket)
        {
            var result = EfHelpers.Execute(
                _context,
                $"Could not update ticket {ticket.TicketId}",
                ctx =>
                {
                    var innerResult = ctx.Tickets
                        .First(t => t.TicketId == ticket.TicketId);

                    innerResult.Active = ticket.Active;
                    innerResult.Description = ticket.Description;
                    innerResult.Problem = ticket.Problem;
                    innerResult.ProductId = ticket.ProductId;
                    innerResult.SeverityId = ticket.SeverityId;

                    ctx.SaveChanges();

                    // Refetch it from the database (if you want an extra layer of surety)
                    // a waste of resources though!
                    // innerResult = ctx.Tickets.FirstOrDefault(t => t.TicketId.Equals(ticket.TicketId));
                    return innerResult; 
                }
            );
            return result;
        }

        public bool DeleteTicket(int ticketId)
        {
            using (var ctx = new SupportTicketDbContext(_connectionString))
            {

                var ticket = GetTicket(ticketId);
                if (ticket == null) return false;

                ctx.Remove(ticket);
                ctx.SaveChanges();
                return true;
            }

        }
    }
}
