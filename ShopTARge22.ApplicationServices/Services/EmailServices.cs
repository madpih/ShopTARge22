﻿using MimeKit.Text;
using MimeKit;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.Core.Dto;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace ShopTARge22.ApplicationServices.Services
{
    public class EmailServices : IEmailServices

    {

        private readonly IConfiguration _config;
        public EmailServices(IConfiguration config)
        {
            _config = config;
        }
        public void SendEmail(EmailDto request)
        {

            var email = new MimeMessage();

            //email.From.Add(MailboxAddress.Parse("german.sawayn@ethereal.email"));
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body};

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
			//smtp.Connect(_config.GetSection("EmailHost").Value, 587, false);
            //smtp.CheckCertificateRevocation = false;

			smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}
