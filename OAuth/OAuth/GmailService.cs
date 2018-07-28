using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace OAuthService {
  public class GmailService {
      public  void SendGmail(MailInput mailInput )
      {
        var message = CreateMessage(mailInput);
        Send(message,mailInput.EmailAddress,mailInput.OAuthToken);
       
      }

    public MimeMessage CreateMessage(MailInput mailInput)
    {
      MimeMessage message = new MimeMessage();
      message.From.Add( new MailboxAddress( mailInput.Name, mailInput.EmailAddress ) );
      foreach ( string str in mailInput.ToEmailAdresses ) {
        message.To.Add( new MailboxAddress( "", str ) );
      }
      message.Subject = mailInput.Subject;
      var attachment = new MimePart( mailInput.ContentType, mailInput.ContentSubType ) {
        ContentDisposition = new ContentDisposition( ContentDisposition.Attachment ),
        ContentTransferEncoding = ContentEncoding.Base64,
        FileName = Path.GetFileName( mailInput.AttachmentFilePath ),
        ContentObject = new ContentObject( File.OpenRead( mailInput.AttachmentFilePath ), ContentEncoding.Default )
      };
      if ( mailInput.ContentType != null ) {
        var mailBody = new TextPart( "plain" ) {
          Text = mailInput.Body
        };
        var multipart = new Multipart( "mixed" ) { mailBody, attachment };
        message.Body = multipart;
      }
      else {
        message.Body = new TextPart( "plain" ) {
          Text = mailInput.Body
        };
      }

      return message;
    }
    public void Send(MimeMessage message,string emailAddress,string accessToken)
    {
      using ( var client = new SmtpClient() ) {
        client.ServerCertificateValidationCallback = ( s, c, h, e ) => true;
        client.Connect( "smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls );
        client.Authenticate( emailAddress, accessToken);
        client.Send( message );
        client.Disconnect( true );
      }
    }
    
  }

}
