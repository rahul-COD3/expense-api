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
                    UserId = input.UserId,
                    FriendId = input.FriendId,
                    IsDeleted = input.IsDeleted
                };

                //false
                // Create a friend when the friend (or User) is not regiostered in splitwise
                //Invite the friend using invite API service and get user id of newly registered user where his idRegistered is set to false
                //this will not be present in here, need a trigger that will do this// when user accepts invitation , need to set its isRegistered to true and make an entry of its user id to friends table

                //true
                // Create a friend when the friend (or User ) is registered in Splitwise
                // enter the user id of that friend in friend id in friends table , and the user id of one who is adding the friend would be entered in user id of friend table





                // Save to repository
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
