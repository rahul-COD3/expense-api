using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace EMS.Friends
{
    public class FriendAppService : ApplicationService, IFriendAppService
    {
        private readonly IRepository<Friend, Guid> _friendRepository;

        public FriendAppService(IRepository<Friend, Guid> friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task CreateFriendAsync(CreateUpdateFriendDto input)
        {
            // Validate input
            if (input.UserId == input.FriendId)
            {
                throw new BusinessException("A user cannot add themselves as a friend.");
            }

            else
            {
                //check if the user exists in User table or not

                // Create new friend entity

                var friend = new Friend
                {
                    UserId = (Guid)input.UserId,
                    FriendId = input.FriendId,
                    IsDeleted = input.IsDeleted
                };


                await _friendRepository.InsertAsync(friend);

            }
        }
        public async Task<List<FriendDto>> GetAllFriendsAsync()
        {
            var friends = await _friendRepository.GetListAsync();

            return ObjectMapper.Map<List<Friend>, List<FriendDto>>(friends);
        }



        public async Task DeleteFriendAsync(Guid id)
        {
            // Get friend entity by id
            var friend = await _friendRepository.GetAsync(id);

            // Soft delete friend
            friend.IsDeleted = true;

            // Save to repository
            await _friendRepository.UpdateAsync(friend);
        }
    }
}
