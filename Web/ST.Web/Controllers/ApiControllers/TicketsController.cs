using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ST.SharedEntitiesLib;
using ST.SharedInterfacesLib;

namespace ST.Web.Controllers.ApiControllers
{
    [EnableCors("AllowAnyOrigin")]
    public class TicketsController : ControllerBase
    {
        private readonly ISTAppService<ISTRepo> _stService;

        public TicketsController(ISTAppService<ISTRepo> stService)
        {
            _stService = stService;
        }

        // GET: api/Tickets
        [Route("api/Tickets")]
        public IActionResult GetTickets()
        {
            try
            {
                var result = _stService.GetActiveTickets();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [Route("api/Products")]
        public IActionResult GetProducts()
        {
            var result = _stService.GetProducts();
            return Ok(result);
        }

        [Route("api/Severities")]
        public IActionResult GetSeverities()
        {
            var result = _stService.GetSeverities();
            return Ok(result);
        }

        [Route("api/StaticData")]
        public IActionResult GetStaticData()
        {
            var result = _stService.GetStaticData();
            return Ok(result);
        }

        // GET: api/Tickets/5
        [Route("api/Tickets/{id}")]
        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult GetTicket(int id)
        {
            Ticket ticket = _stService.GetTicket(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        
        //[Route("api/Tickets/{id}"), EnableCors("AllowAnyOrigin")]
        //[HttpDelete("{id}", Name = "DeleteTicket")]
        //public IActionResult DeleteTicket(int id)
        //{
        //    var result = _stService.DeleteTicket(id);


        //    if (!result)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok();
        //    }
        //}

        [Route("api/tickets/add")]
        [HttpPost]
        public IActionResult PostTicket([FromBody]Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _stService.AddTicket(ticket);
            return CreatedAtRoute("GetTicket", new { id = result.TicketId }, result);
        }

        [Route("api/tickets")]
        [HttpPut]
        public IActionResult PutTicket([FromBody]Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = _stService.UpdateTicket(ticket);
            
            return Ok(result); // can return NoContent too (204)
        }
    }
}
