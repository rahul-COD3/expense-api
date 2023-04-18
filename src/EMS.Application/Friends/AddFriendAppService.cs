using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using static Volo.Abp.UI.Navigation.DefaultMenuNames.Application;

namespace EMS.Friends
{
    public class AddFriendAppService : ApplicationService, IAddFriendAppService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly UserManager<IdentityUser> _userManager;
        

        public AddFriendAppService(UserManager<IdentityUser> userManager, IGuidGenerator guidGenerator)
        {
            _userManager = userManager;
            _guidGenerator = guidGenerator;
        }

        public async Task AddFriendAsync(string name, string email)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                throw new BusinessException("User already exist");

            }
            else
            await SendEmailAsync(email);
            var user = new IdentityUser(_guidGenerator.Create(), name, email);

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return;
            }

            throw new ApplicationException($"Could not add user: {result.Errors.FirstOrDefault()?.Description}");
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
