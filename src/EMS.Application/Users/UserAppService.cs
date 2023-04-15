using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Domain.Entities;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.TextTemplating;
using System.Net.Mail;


namespace EMS.Users
{
    public interface IUserAppService
    {
        Task<bool> CheckEmailExists(string email);
    }

    public class UserAppService : ApplicationService, IUserAppService, ITransientDependency
    {
        private readonly IIdentityUserRepository _userRepository;

        public UserAppService(IIdentityUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            var users = await _userRepository.GetListAsync();
            bool emailExists = users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (!emailExists)
            {
                await SendEmailAsync(email);
            }

            return emailExists;
        }

        private async Task SendEmailAsync(string targetEmail)
        {
            var mailMessage = new MailMessage();
            mailMessage.To.Add(targetEmail);
            mailMessage.From = new MailAddress("test1403email@gmail.com");
            mailMessage.Subject = "Join Splitwise";
            mailMessage.Body = "Please join to Splitwise using this link https://secure.splitwise.com/login";

            using (var smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("test1403email@gmail.com", "fohmxxrccvyyzcsl");
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
