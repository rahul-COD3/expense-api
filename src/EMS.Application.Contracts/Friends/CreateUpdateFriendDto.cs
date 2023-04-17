using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMS.Friends
{
    public class CreateUpdateFriendDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid FriendId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
