using AutoMapper;
using EMS.Friends;

namespace EMS;

public class EMSApplicationAutoMapperProfile : Profile
{
    public EMSApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Friend, FriendDto>();
        CreateMap<CreateUpdateFriendDto,Friend>();
    }
}
