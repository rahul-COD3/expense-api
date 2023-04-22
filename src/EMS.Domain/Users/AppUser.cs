using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace EMS.Users
{
    public class AppUser : IdentityUser
    {
        public double amountOwed { get; protected set; }

        public double amountOwes { get; protected set; }
        public double totalAmount { get; protected set; }
        public bool isRegistered { get; protected set; }
    }
}
