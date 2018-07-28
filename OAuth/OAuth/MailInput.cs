using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthService {
  public class MailInput {
    // string[] to, string subject, string body,  string attachmentPath = null, string contentMediaType = null, string contentSubType = null
    public string EmailAddress { get; set; }
    public string Name { get; set; }
    public string OAuthToken { get; set; }
    public string[] ToEmailAdresses { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string AttachmentFilePath { get; set; }
    public string ContentType { get; set; }
    public string ContentSubType { get; set; }

  }
}
