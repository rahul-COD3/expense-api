using EMS.Expenses;
using EMS.Friends;
using EMS.GroupMembers;
using EMS.Groups;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using EMS.Payments;

namespace EMS.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class EMSDbContext :
    AbpDbContext<EMSDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Expense> Expenses { get; set; }

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    public DbSet<Friend> Friends { get; set; }

    public DbSet<GroupMember> GroupMembers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public EMSDbContext(DbContextOptions<EMSDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.Entity<Expense>(b =>
        {
            b.ToTable(EMSConsts.DbTablePrefix + "Expenses",
               EMSConsts.DbSchema);
            b.ConfigureByConvention(); 
            b.Property(x => x.ExpenseTitle).IsRequired().HasMaxLength(128);
            b.HasOne<IdentityUser>()
             .WithMany()
             .HasForeignKey(x => x.PaidBy)
             .IsRequired();
            b.HasOne<Group>()
            .WithMany()
            .HasForeignKey(x => x.GroupId)
            .IsRequired();
        });

        builder.Entity<GroupMember>(b =>
        {
            b.ToTable(EMSConsts.DbTablePrefix + "GroupMembers",
                EMSConsts.DbSchema);
            b.ConfigureByConvention(); 
            b.Property(x => x.groupId).IsRequired();
            b.Property(x => x.isRemoved).HasDefaultValue(false);
            b.HasOne<IdentityUser>()
               .WithMany()
               .HasForeignKey(gm => gm.userId)
               .IsRequired();
            b.HasOne<Group>()
            .WithMany()
            .HasForeignKey(g => g.groupId)
            .IsRequired();
        });
        builder.Entity<Group>(b =>
        {
            b.ToTable(EMSConsts.DbTablePrefix + "Groups",
                               EMSConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.About).IsRequired();
            b.Property(x => x.IsDeleted).HasDefaultValue(false);
        });

        builder.Entity<Friend>(b =>
        {
            b.ToTable(EMSConsts.DbTablePrefix + "Friends", EMSConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(b => b.UserId).IsRequired();
            b.Property(b => b.FriendId).IsRequired();
            b.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            b.HasOne<IdentityUser>()
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        });

        builder.Entity<Payment>(b =>
        {
            b.ToTable(EMSConsts.DbTablePrefix + "Payments",
                EMSConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Amount).IsRequired().HasMaxLength(128);
        });

        
            
            
    }
}
