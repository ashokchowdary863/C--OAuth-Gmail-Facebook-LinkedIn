using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mail;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace OAuthService {
  public class GmailService {
      public  bool SendGmail(MailInput mailInput )
      {
        try
        {
          var message = CreateMessage(mailInput);
          Send(message, mailInput.EmailAddress, mailInput.OAuthToken);
          return true;
        }
        catch (Exception e)
        {
          return false;
        }

      }

    public MimeMessage CreateMessage(MailInput mailInput)
    {
      MimeMessage message = new MimeMessage();
      message.From.Add( new MailboxAddress( mailInput.Name, mailInput.EmailAddress ) );
      foreach ( string str in mailInput.ToEmailAdresses ) {
        message.To.Add( new MailboxAddress( "", str ) );
      }
      message.Subject = mailInput.Subject;

      message.Body = new TextPart("plain")
      {
        Text = mailInput.Body
      };

      return message;
    }
    public void Send(MimeMessage message,string emailAddress,string accessToken)
    {
      using ( var client = new SmtpClient( new ProtocolLogger( Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath,"imap.log") ) ) ) {
        client.Connect( "smtp.gmail.com", 587 );

        // use the OAuth2.0 access token obtained above
        var oauth2 = new SaslMechanismOAuth2( emailAddress, accessToken );
        client.Authenticate( oauth2 );
        client.Send(message);
        client.Disconnect( true );
      }
    }
    
  }

}
