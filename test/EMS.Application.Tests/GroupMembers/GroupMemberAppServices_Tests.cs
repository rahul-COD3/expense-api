using EMS.Groups;
using Shouldly;
using System.Threading.Tasks;

using Xunit;

namespace EMS.GroupMembers
{
    public class GroupMemberAppServices_Tests : EMSApplicationTestBase
    {
        private readonly IGroupMemberAppService _groupMemberAppService;
        private readonly IGroupAppService _groupAppService;

        public GroupMemberAppServices_Tests()
        {
            _groupMemberAppService = GetRequiredService<IGroupMemberAppService>();
            _groupAppService = GetRequiredService<IGroupAppService>();

        }
        [Fact]
        public async Task Should_Get_List_Of_GroupMembers()
        {
            var result = await _groupMemberAppService.GetListAsync(
                    new GetGroupMemberListDto()
               );
            result.TotalCount.ShouldBeGreaterThan(1);
        }

    }
}
