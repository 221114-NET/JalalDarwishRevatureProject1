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
        public ActionResult UserLogin(LoginData loginD)
        {
            bus.UserLoginRequest(loginD);
            return Ok();
        }

        [HttpPut("Register")]
        public ActionResult RegisterUser(LoginData loginD)
        {
            bus.RegisterUser(loginD);
            return Ok();
        }

        [HttpPost("Submit New Ticket")]
        public ActionResult SubmitNewTicket(Reimbursement ticket)
        {
            return Ok();
        }
    }
}