using System;
using System.Collections.Generic;
using FakeItEasy;
using ST.SharedEntitiesLib;
using ST.SharedInterfacesLib;
using Xunit;

namespace ST.AppServicesLib.Tests
{
    public class STAppServiceTests
    {
        [Fact]
        public void GetActiveTicketsReturnsResult()
        {
            var appRepo = A.Fake<ISTRepo>();
            A.CallTo(() => appRepo.GetActiveTickets()).Returns(new List<Ticket>()
            {
                new Ticket() {TicketId = 1, ProductId = 1, SeverityId = 1, Problem = "Pr1", Description = "D1"},
                new Ticket() {TicketId = 2, ProductId = 2, SeverityId = 2, Problem = "Pr2", Description = "D2"}
            });
            var stappService = new STAppService<ISTRepo>(appRepo);
            var result = stappService.GetActiveTickets();
            A.CallTo(() => appRepo.GetActiveTickets()).MustHaveHappened();
            Assert.IsType<List<Ticket>>(result);
            Assert.True(result.Count == 2);
        }
    }
}
