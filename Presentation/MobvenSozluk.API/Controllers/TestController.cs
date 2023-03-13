using Amazon.Auth.AccessControlPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MobvenSozluk.API.Controllers
{
    #region CODE EXPLANATION SECTION
    /*
     In this controller you can easliy test role management.
     "[Authorize(Policy = "RequireUserRole")]" This policy is comes from "IdentityServiceExtensions" file.
     If you want to add new policy please go "IdentityServiceExtensions" file.
     */
    #endregion
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
