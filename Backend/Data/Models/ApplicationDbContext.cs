using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Approval> Approvals { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpenseApproval> ExpenseApprovals { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Approval>(entity =>
        {
            entity.HasKey(e => e.ApprovalId).HasName("PK__Approval__328477F44C1B59F5");

            entity.HasOne(d => d.Approver).WithMany(p => p.Approvals)
                .HasForeignKey(d => d.ApproverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Approvals__Appro__398D8EEE");

            entity.HasOne(d => d.Expense).WithMany(p => p.Approvals)
                .HasForeignKey(d => d.ExpenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Approvals__Expen__38996AB5");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("PK__Expenses__1445CFD3F1AC6A96");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ApprovalsReceived).HasDefaultValueSql("((0))");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.ExpenseDate).HasColumnType("datetime");

            entity.HasOne(d => d.Group).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Expenses__GroupI__30F848ED");

            entity.HasOne(d => d.Payer).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.PayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Expenses__PayerI__31EC6D26");

            entity.HasMany(d => d.Users).WithMany(p => p.ExpensesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "ExpenseUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ExpenseUs__UserI__35BCFE0A"),
                    l => l.HasOne<Expense>().WithMany()
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ExpenseUs__Expen__34C8D9D1"),
                    j =>
                    {
                        j.HasKey("ExpenseId", "UserId").HasName("PK__ExpenseU__C53D4317EF5012CE");
                        j.ToTable("ExpenseUsers");
                    });
        });

        modelBuilder.Entity<ExpenseApproval>(entity =>
        {
            entity.HasKey(e => new { e.ExpenseId, e.UserId }).HasName("PK__ExpenseA__C53D43179E336BE8");

            entity.HasOne(d => d.Expense).WithMany(p => p.ExpenseApprovals)
                .HasForeignKey(d => d.ExpenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExpenseAp__Expen__3D5E1FD2");

            entity.HasOne(d => d.User).WithMany(p => p.ExpenseApprovals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExpenseAp__UserI__3E52440B");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF36AAF5D0961");

            entity.Property(e => e.GroupName).HasMaxLength(50);

            entity.HasOne(d => d.Creator).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Groups__CreatorI__29572725");

            entity.HasMany(d => d.Users).WithMany(p => p.GroupsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupMember",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GroupMemb__UserI__2D27B809"),
                    l => l.HasOne<Group>().WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__GroupMemb__Group__2C3393D0"),
                    j =>
                    {
                        j.HasKey("GroupId", "UserId").HasName("PK__GroupMem__C5E27FAE0AD2BEA0");
                        j.ToTable("GroupMembers");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C195933AB");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
