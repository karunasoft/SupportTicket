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
            var fakeStService = A.Fake<ISTAppService<ISTRepo>>();
            var ticketsController = new TicketsController(fakeStService);
            var result = ticketsController.GetTickets();
            Assert.IsType<OkObjectResult>(result);
            A.CallTo(() => fakeStService.GetActiveTickets()).MustHaveHappened();
        }
    }
}
