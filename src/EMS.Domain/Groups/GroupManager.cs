using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace EMS.Groups;

public class GroupManager : DomainService
{
    private readonly IGroupRepository _groupRepository;

    public GroupManager(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    public async Task<Group> CreateAsync(
        [NotNull] string name,
        [CanBeNull] string about,
        [NotNull] Guid createdBy,
        [NotNull] bool isDeleted = false
        )
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        var existingGroup = await _groupRepository.FindByNameAsync(name);
        if (existingGroup != null) {
            throw new GroupAlreadyExistsException(name);
        }
        return new Group(
            GuidGenerator.Create(),
            name,
            about,
            createdBy,
            isDeleted
        );
    }
    public async Task ChangeNameAsync(
        [NotNull] Group group,
        [NotNull] string newName)
    {
        Check.NotNull(group, nameof(group));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingAuthor = await _groupRepository.FindByNameAsync(newName);
        if (existingAuthor != null && existingAuthor.Id != group.Id)
        {
            throw new GroupAlreadyExistsException(newName);
        }

        group.ChangeName(newName);
    }
}
