﻿using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMS.Groups
{
    public class Group :  AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string About { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        private Group()
        {
        }

        internal Group(Guid id, [NotNull] string name, [NotNull] string about, [NotNull] Guid createdBy, [NotNull] bool isDeleted) : base(id)
        {
            this.Name = name;
            this.About = about;
            this.CreatedBy = createdBy;
            this.IsDeleted = isDeleted;
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