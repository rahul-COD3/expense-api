using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace EMS.Friends
{
    public class AddFriendAppService : ApplicationService, IAddFriendAppService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<Friend, Guid> _friendRepository;

        public AddFriendAppService(UserManager<IdentityUser> userManager, IGuidGenerator guidGenerator, ICurrentUser currentUser, IRepository<Friend, Guid> friendRepository)
        {
            _userManager = userManager;
            _guidGenerator = guidGenerator;
            _currentUser = currentUser;
            _friendRepository = friendRepository;
        }

        public async Task<String> AddFriendAsync(string name, string emailId)
        {
            var existingUser = await _userManager.FindByEmailAsync(emailId);
            if (!Regex.IsMatch(emailId, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new BusinessException("Invalid email format");
            }
            if (existingUser != null)
            {
                //check if friendship of existingUser and current user exist

                // throw new BusinessException("User already exists");
                var existingFriendship = await _friendRepository.FirstOrDefaultAsync(f =>
                f.UserId == _currentUser.Id && f.FriendId == existingUser.Id && !f.IsDeleted);

                if (existingFriendship != null)
                {
                    return "Friend exist";
                }

                var friend = new Friend
                {
                    UserId = (Guid)_currentUser.Id,
                    FriendId = existingUser.Id,
                    IsDeleted = false
                };

                await _friendRepository.InsertAsync(friend);


                return "Friend added";
            }
            else
            {
                await SendEmailAsync(emailId);
                var user = new IdentityUser(_guidGenerator.Create(), name, emailId);
                var resultUser = await _userManager.CreateAsync(user);

                if (resultUser.Succeeded)
                {
                    //var currentUser = await _currentUser.Id();
                    var friend = new Friend
                    {
                        UserId = (Guid)_currentUser.Id,
                        FriendId = user.Id,
                        IsDeleted = false
                    };
                    await _friendRepository.InsertAsync(friend);
                  
                    return "Friend invited";
                }
                
                throw new ApplicationException($"Could not add user: {resultUser.Errors.FirstOrDefault()?.Description}");
            }
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
                smtpClient.Credentials = new System.Net.NetworkCredential("test1403email@gmail.com", "honxqgcfvhfksxfo");
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
