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

        [HttpPut("Register")]
        public ActionResult<Employee> RegisterUser(string email, string password)
        {
            Employee? newEmp = bus.RegisterUser(email, password);
            if (newEmp == null)
            {
                return BadRequest();
            }
            else
            {
                return Created("uri/path", newEmp);
            }
        }

        [HttpPost("Login")]
        public ActionResult<int> UserLogin(string email, string password)
        {
            int loginResult = bus.UserLogin(email, password);
            switch (loginResult)
            {
                case -2:
                    {
                        return BadRequest();
                    }
                case -1:
                    {
                        return NotFound();
                    }
                default:
                    {
                        return Ok(loginResult); //returns UserID
                    }
            }
        }


        [HttpPost("Submit New Ticket")]
        public ActionResult<Reimbursement> SubmitNewTicket(Reimbursement? ticket)
        {
            ticket = bus!.SubmitNewTicket(ticket);

            if(ticket != null)
            {
                return Created("uri/ticketPath", ticket);
            }
            else
            {
                return NotFound();   
            }
        }

        [HttpPut("Manager:Change Ticket Status")]
        public ActionResult ChangeTicketStatus(int userID, int reimbursmentID)
        {

            bus.ChangeTicketStatus(userID, reimbursmentID);
            return Ok();
        }
    }
}