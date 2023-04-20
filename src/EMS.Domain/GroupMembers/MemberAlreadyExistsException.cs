using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EMS.GroupMembers
{
    internal class MemberAlreadyExistsException : BusinessException
    {
        public MemberAlreadyExistsException(Guid userId)
            : base(EMSDomainErrorCodes.MemberAlreadyExists)
        {
            WithData("userId", userId);
        }
    }
}
