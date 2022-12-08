using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERSModelsLayer;
using ERSBusinessLayer;

namespace ERSApiLayer.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ERSController : ControllerBase
    {

        private IBusinessLayer? bus;
        public ERSController(IBusinessLayer iBus)
        {
            bus = iBus;
        }

        [HttpPost("Login")]
        public ActionResult UserLogin(string email, string password)
        {
            bus.UserLogin(email, password);
            return Ok();
        }

        [HttpPut("Register")]
        public ActionResult<Employee> RegisterUser(string email, string password)
        {
            Employee temp = bus.RegisterUser(email, password);
            return Created("uri/path", temp); //#TODO
            // if(bus.RegisterUser(loginD))
            // {
            //    return Created("we made it boys", loginD);
            // }
            // else
            // {
            //     return BadRequest();
            // }
            
        }

        [HttpPost("Submit New Ticket")]
        public ActionResult SubmitNewTicket(Reimbursement ticket)
        {
            return Ok();
        }
    }
}