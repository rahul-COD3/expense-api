using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMS.Groups
{
    public class Group :  AuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string About { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

        private Group()
        {
//             this is a private constructor
        }

        internal Group(Guid id, [NotNull] string name, [NotNull] string about, [NotNull] Guid createdBy, [NotNull] bool isDeleted = false) : base(id)
        {
            SetName(name);
            About = about;
            CreatedBy = createdBy;
            IsDeleted = isDeleted;
        }
        internal Group ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }
        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: GroupConsts.MaxNameLength
            );
        }
    }
}
