using System;
using System.Collections.Generic;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using ST.SharedEntitiesLib;
using ST.SharedInterfacesLib;
using ST.Web.Controllers.ApiControllers;
using Xunit;

namespace ST.Web.Tests
{
    public class TicketsControllerTests
    {
        [Fact]
        public void GetTicketsReturnsTickets()
        {
            var tickets = new List<Ticket>()
            {
                new Ticket(){TicketId = 1, ProductId = 1, SeverityId = 1, Description = "D1", Problem = "Pr1"},
                new Ticket(){TicketId = 2, ProductId = 2, SeverityId = 2, Description = "D2", Problem = "Pr2"}
            };

            var fakeStService = A.Fake<ISTAppService<ISTRepo>>();
            A.CallTo(() => fakeStService.GetActiveTickets()).Returns(tickets);
            var ticketsController = new TicketsController(fakeStService);
            var result = ticketsController.GetTickets();
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = ((OkObjectResult) result).Value;
            Assert.IsType<List<Ticket>>(okObjectResult);
            var listResult = (List<Ticket>) okObjectResult;
            Assert.True(listResult.Count == 2);
        }
    }
}
