using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Rocky.Application.Utilities.MailJet;
using System.Threading.Tasks;

namespace Rocky.Application.Utilities
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public MailJetSettings MailJetSettings { get; set; }

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string body)
        {
            MailJetSettings = _configuration.GetSection("MailJet").Get<MailJetSettings>();

            var client = new MailjetClient(MailJetSettings.ApiKey, MailJetSettings.SecretKey);
            var request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
                .Property(Send.Messages, new JArray
                {
                    new JObject
                    {
                        {
                            "From",
                            new JObject
                            {
                                {"Email", "dotnetmastery@protonmail.com"},
                                {"Name", "Ben"}
                            }
                        },
                        {
                            "To",
                            new JArray
                            {
                                new JObject
                                {
                                    {
                                        "Email",
                                        email
                                    },
                                    {
                                        "Name",
                                        "DotNetMastery"
                                    }
                                }
                            }
                        },
                        {
                            "Subject",
                            subject
                        },
                        {
                            "HTMLPart",
                            body
                        }
                    }
                });

            await client.PostAsync(request);
        }
    }
}
