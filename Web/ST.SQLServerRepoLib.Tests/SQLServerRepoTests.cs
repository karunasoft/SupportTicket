using System;
using System.Collections.Generic;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using ST.SharedEntitiesLib;
using Xunit;

namespace ST.SQLServerRepoLib.Tests
{
    public class SQLServerRepoTests
    {
        [Fact]
        public void GetActiveTicketsReturnsTickets()
        {
            // Could mock the context and assert the calls on the mock
            // I choose the simpler approach
            SupportTicketDbContext ctx = GetTestDbContextScenario0();

            var repo = new SQLRepo(ctx);
            var result = repo.GetActiveTickets();
            Assert.True(result.Count ==3);
            Assert.IsType<List<Ticket>>(result);
            Assert.True(((List<Ticket>)result)[0].Product.Description == "Product1");

        }

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
