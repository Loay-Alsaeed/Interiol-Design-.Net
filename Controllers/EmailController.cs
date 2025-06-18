using Microsoft.AspNetCore.Mvc;
using Azure.Communication.Email;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend_.Net.DTO;
using Azure;

namespace Backend_.Net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailClient _emailClient;
        private const string SenderAddress = "DoNotReply@f4529acb-1cad-405d-8f38-4e762c0bf692.azurecomm.net"; // ← عدّلها حسب الدومين الحقيقي

        public EmailController()
        {
            string connectionString = "endpoint=https://interialdesign.uae.communication.azure.com/;accesskey=Ar1QAKA6733Hlkq2C1Wg2nDMeRuNsSGGsKYhQ5VRUQ7ErdTUIqcnJQQJ99BFACULyCpgQMyCAAAAAZCSmLUR";
            _emailClient = new EmailClient(connectionString);
        }

        [HttpPost("contact")]
        public async Task<IActionResult> contact([FromBody] ContactDTO contact)
        {
            if (contact == null || string.IsNullOrEmpty(contact.email))
                return BadRequest("Invalid contact data.");

            var emailContent = new EmailContent($"New Contact from {contact.name}")
            {
                PlainText = contact.message,
                Html = $@"
                    <html>
                        <body>
                            <h2>New Message From {contact.name}</h2>
                            <p><strong>Email: </strong> {contact.email}</p>
                            <p><strong>Phone: </strong> {contact.phone}</p>
                            <p><strong>Subject: </strong> {contact.subject}</p>
                            <p><strong>Messsage: </strong><br>{contact.message}</p>
                        </body>
                    </html>"
            };

            var recipients = new EmailRecipients(new List<EmailAddress>
            {
                new EmailAddress("loayalsaeed1234@gmail.com") // ← عدّل البريد المستلم هنا
            });

            var emailMessage = new EmailMessage(SenderAddress, recipients, emailContent);

            try
            {
                EmailSendOperation operation = await _emailClient.SendAsync(WaitUntil.Completed, emailMessage);
                return Ok(new { status = "Email sent", operationId = operation.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to send email", details = ex.Message });
            }
        }

        [HttpPost("email")]
        public async Task<IActionResult> SendEmail([FromBody] RequestDesignDTO contact)
        {
            if (contact == null || string.IsNullOrEmpty(contact.email))
                return BadRequest("Invalid contact data.");

            var emailContent = new EmailContent($"New Design request from {contact.name}")
            {
                PlainText = contact.message,
                Html = $@"
                    <html>
                        <body>
                            <h2>New Message From {contact.name}</h2>
                            <p><strong>Email: </strong> {contact.email}</p>
                            <p><strong>Phone: </strong> {contact.phone}</p>
                            <p><strong>Messsage: </strong><br>{contact.message}</p>
                        </body>
                    </html>"
            };

            var recipients = new EmailRecipients(new List<EmailAddress>
            {
                new EmailAddress("loayalsaeed1234@gmail.com") // ← عدّل البريد المستلم هنا
            });

            var emailMessage = new EmailMessage(SenderAddress, recipients, emailContent);

            try
            {
                EmailSendOperation operation = await _emailClient.SendAsync(WaitUntil.Completed, emailMessage);
                return Ok(new { status = "Email sent", operationId = operation.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to send email", details = ex.Message });
            }
        }
    }
}
