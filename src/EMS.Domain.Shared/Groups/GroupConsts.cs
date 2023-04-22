using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Groups
{
    public class GroupConsts
    {
        public const int MaxNameLength = 60;
        public const string GroupNameRegex = @"^[a-zA-Z0-9 ]+$";
        public const string GroupNameRegexErrorMessage = "Group name can only contain letters, numbers and space";
    }
}
