using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OAuthService;

namespace OAuth.Web.Controllers
{
    public class GmailApiController : ApiController
    {
      [HttpPost]
      [Route("api/gmail/sendMail")]
      public bool SendMail([FromBody]MailInput mailInput)
      {
         return new GmailService().SendGmail(mailInput);
      }
    }
}
