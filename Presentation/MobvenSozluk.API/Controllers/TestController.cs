using Amazon.Auth.AccessControlPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Infrastructure.Exceptions;
using Serilog;

namespace MobvenSozluk.API.Controllers
{
    public class TestController : CustomBaseController
    {

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("getadmin")]
        public ActionResult GetAdmin()
        {
           
            return Ok("Admins can see it");     
        }
        [Authorize(Policy = "RequireEditorRole")]
        [HttpGet("geteditor")]
        public ActionResult GetEditor()
        {
            return Ok("Editors can see it");
        }
        [Authorize(Policy = "RequireUserRole")]
        [HttpGet("getuser")]
        public ActionResult GetUser()
        {
            return Ok("Users can see it");
        }
    }
}
