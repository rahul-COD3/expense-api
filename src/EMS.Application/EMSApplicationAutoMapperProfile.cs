using AutoMapper;
using EMS.Expenses;
using EMS.GroupMembers;
using EMS.Payments;

namespace EMS;

public class EMSApplicationAutoMapperProfile : Profile
{
    public EMSApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Expense, ExpenseDto>();
        CreateMap<CreateUpdateExpenseDto, Expense>();
        CreateMap<GroupMember, GroupMemberDto>();
        CreateMap<Payment, PaymentDto>();

    }
}
