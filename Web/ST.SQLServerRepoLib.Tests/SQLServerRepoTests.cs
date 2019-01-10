using System;
using System.Collections.Generic;
using System.Linq;
using DeepEqual.Syntax;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using ST.SharedEntitiesLib;
using Xunit;

namespace ST.SQLServerRepoLib.Tests
{
    /// <summary>
    /// In each case, I could mock the context and assert the calls on the mock
    /// I choose a more pragmatic approach instead
    /// (which is to test the context at the same time as the repo)
    /// i.e. you could do A.CallTo(() ==> context // etc)).
    /// The might become very unwieldy
    /// </summary>
    public class SQLServerRepoTests
    {
        [Fact]
        public void GetActiveTicketsReturnsTickets()
        {
            SupportTicketDbContext ctx = GetTestDbContextScenario0();

            try
            {
                var repo = new SQLRepo(ctx);
                var result = repo.GetActiveTickets();
                Assert.True(result.Count == 3);
                Assert.IsType<List<Ticket>>(result);
                result.FirstOrDefault().ShouldDeepEqual(ctx.Tickets.FirstOrDefault());
            }
            finally
            {
                ctx.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void GetTicketsReturnsTicket()
        {
            SupportTicketDbContext ctx = GetTestDbContextScenario0();
            try
            {
                var repo = new SQLRepo(ctx);
                var result = repo.GetTicket(1);
                Assert.IsType<Ticket>(result);
                result.ShouldDeepEqual(ctx.Tickets.FirstOrDefault());
            }
            finally
            {
                ctx.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void UpdateTicketUpdatesSuccessfully()
        {
            SupportTicketDbContext ctx = GetTestDbContextScenario0();
            var ticket = ctx.Tickets.First();
            const string updatedValue = "Problem Updated";
            
            try
            {
                var repo = new SQLRepo(ctx);
                ticket.Problem = updatedValue;
                var result = repo.UpdateTicket(ticket);
                Assert.IsType<Ticket>(result);
                result.ShouldDeepEqual(ctx.Tickets.FirstOrDefault());
            }
            finally
            {
                ctx.Database.EnsureDeleted();
            }
        }

        /// <summary>
        /// Scenario0: Base Scenario InMemory Database
        /// Should work for a wide range of unit tests
        /// </summary>
        /// <returns></returns>
        private static SupportTicketDbContext GetTestDbContextScenario0()
        {
            var options = new DbContextOptionsBuilder<SupportTicketDbContext>()
                .UseInMemoryDatabase(databaseName: "GetActiveTicketsReturnsTickets")
                .Options;
            var ctx = new SupportTicketDbContext(options);
            ctx.Tickets.Add(GetNewTicketStubEntity(1));
            ctx.Tickets.Add(GetNewTicketStubEntity(2));
            ctx.Tickets.Add(GetNewTicketStubEntity(3));
            ctx.SaveChanges();
            return ctx;
        }

        private static Ticket GetNewTicketStubEntity(int id)
        {
            return new Ticket(){
                TicketId = id, Active = true, ProductId = id,
                SeverityId = id, Problem = $"Pr{id}", Description = $"D{id}",
                Product = new Product(){ ProductId = id, Description = $"Product{id}" },
                Severity = new Severity() { SeverityId = id, DisplayName = $"Severity{id}" }
            };
        }
    }
}
