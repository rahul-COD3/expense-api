using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EMS.Groups
{
    public class GroupAlreadyExistsException : BusinessException
    {
        public GroupAlreadyExistsException(string name)
            : base(EMSDomainErrorCodes.GroupAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
