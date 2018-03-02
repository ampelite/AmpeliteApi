using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AmpeliteApi.Models
{
    public partial class db_AmpeliteContext : DbContext
    {
        public db_AmpeliteContext(DbContextOptions<db_AmpeliteContext> options) : base(options) { }
        
        public virtual DbSet<AuthDevices> AuthDevices { get; set; }
        public virtual DbSet<AuthPermissions> AuthPermissions { get; set; }
        public virtual DbSet<AuthTransactions> AuthTransactions { get; set; }
        public virtual DbSet<DailypoGroupReport> DailypoGroupReport { get; set; }
        public virtual DbSet<DailypoGroupUnit> DailypoGroupUnit { get; set; }
        public virtual DbSet<DailypoProductGroup> DailypoProductGroup { get; set; }
        public virtual DbSet<DailypoProductTeam> DailypoProductTeam { get; set; }
<<<<<<< HEAD
        public virtual DbSet<HrEmployee> HrEmployee { get; set; }

        public virtual DbSet<DailypoGraphProduct> DailypoGraphProduct { get; set; }

        // Unable to generate entity type for table 'dbo.GET_TransactionSO'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.GET_TransactionInv'. Please see the warning messages.

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Server=AMPELITE-001\SQLEXPRESS01;Database=db_Ampelite;Trusted_Connection=True;");
        //            }
        //        }

        public db_AmpeliteContext(DbContextOptions<db_AmpeliteContext> options) : base(options)
        {

        }

=======
        public virtual DbSet<DailypoGraphProduct> DailypoGraphProduct { get; set; }
        public virtual DbSet<GetTransactionInv> GetTransactionInv { get; set; }
        public virtual DbSet<GetTransactionSo> GetTransactionSo { get; set; }
        public virtual DbSet<HrEmployee> HrEmployee { get; set; }

>>>>>>> 2eea142fdf1253aaf2b0901d691c70b4932ba6fa
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthDevices>(entity =>
            {
                entity.HasKey(e => e.AuthDId);

                entity.ToTable("AUTH_Devices");

                entity.HasIndex(e => e.SEmpId)
                    .HasName("IX_AUTH_Devices");

                entity.Property(e => e.AuthDId).HasColumnName("auth_dID");

                entity.Property(e => e.AuthDDevice)
                    .IsRequired()
                    .HasColumnName("auth_dDevice")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AuthDMacAddress)
                    .IsRequired()
                    .HasColumnName("auth_dMacAddress")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpId).HasColumnName("sEmpID");

                entity.HasOne(d => d.SEmp)
                    .WithMany(p => p.AuthDevices)
                    .HasForeignKey(d => d.SEmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AUTH_Devi__sEmpI__398D8EEE");
            });

            modelBuilder.Entity<AuthPermissions>(entity =>
            {
                entity.HasKey(e => e.AuthPId);

                entity.ToTable("AUTH_Permissions");

                entity.Property(e => e.AuthPId).HasColumnName("auth_pID");

                entity.Property(e => e.AuthPRole)
                    .IsRequired()
                    .HasColumnName("auth_pRole")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AuthPStatus).HasColumnName("auth_pStatus");

                entity.Property(e => e.AuthPUpdateAt)
                    .HasColumnName("auth_pUpdateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthPUpdateBy)
                    .IsRequired()
                    .HasColumnName("auth_pUpdateBy")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpId).HasColumnName("sEmpID");

                entity.HasOne(d => d.SEmp)
                    .WithMany(p => p.AuthPermissions)
                    .HasForeignKey(d => d.SEmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AUTH_Perm__sEmpI__412EB0B6");
            });

            modelBuilder.Entity<AuthTransactions>(entity =>
            {
                entity.HasKey(e => e.AuthTId);

                entity.ToTable("AUTH_Transactions");

                entity.Property(e => e.AuthTId).HasColumnName("auth_tID");

                entity.Property(e => e.AuthDMacAddress)
                    .IsRequired()
                    .HasColumnName("auth_dMacAddress")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AuthTLastLoginDate)
                    .HasColumnName("auth_tLastLoginDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.SEmpId).HasColumnName("sEmpID");

                entity.HasOne(d => d.SEmp)
                    .WithMany(p => p.AuthTransactions)
                    .HasForeignKey(d => d.SEmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AUTH_Tran__sEmpI__46E78A0C");
            });

            modelBuilder.Entity<DailypoGroupReport>(entity =>
            {
                entity.HasKey(e => e.GroupCode);

                entity.ToTable("DAILYPO_GroupReport");

                entity.Property(e => e.GroupCode)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DailypoGroupUnit>(entity =>
            {
                entity.HasKey(e => e.UnitId);

                entity.ToTable("DAILYPO_GroupUnit");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.GroupCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UnitCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UnitTitle).HasMaxLength(50);
            });

            modelBuilder.Entity<DailypoProductGroup>(entity =>
            {
                entity.ToTable("DAILYPO_ProductGroup");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GoodBrandCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GoodBrandName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GroupBrandCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GroupBrandName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Product)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DailypoProductTeam>(entity =>
            {
                entity.HasKey(e => e.GptId);

                entity.ToTable("DAILYPO_ProductTeam");

                entity.Property(e => e.GptId).HasColumnName("GPT_ID");

                entity.Property(e => e.Product)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ReCateProduct)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.TeamCode)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.TeamName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HrEmployee>(entity =>
            {
                entity.HasKey(e => e.SEmpId);

                entity.ToTable("HR_Employee");

                entity.Property(e => e.SEmpId)
                    .HasColumnName("sEmpID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DEmpLeaveQuota1).HasColumnName("dEmpLeaveQuota1");

                entity.Property(e => e.DEmpLeaveQuota10).HasColumnName("dEmpLeaveQuota10");

                entity.Property(e => e.DEmpLeaveQuota2).HasColumnName("dEmpLeaveQuota2");

                entity.Property(e => e.DEmpLeaveQuota3).HasColumnName("dEmpLeaveQuota3");

                entity.Property(e => e.DEmpLeaveQuota4).HasColumnName("dEmpLeaveQuota4");

                entity.Property(e => e.DEmpLeaveQuota5).HasColumnName("dEmpLeaveQuota5");

                entity.Property(e => e.DEmpLeaveQuota6).HasColumnName("dEmpLeaveQuota6");

                entity.Property(e => e.DEmpLeaveQuota7).HasColumnName("dEmpLeaveQuota7");

                entity.Property(e => e.DEmpLeaveQuota8).HasColumnName("dEmpLeaveQuota8");

                entity.Property(e => e.DEmpLeaveQuota9).HasColumnName("dEmpLeaveQuota9");

                entity.Property(e => e.IEmpQuitWorkType).HasColumnName("iEmpQuitWorkType");

                entity.Property(e => e.IEmpWorkStatus).HasColumnName("iEmpWorkStatus");

                entity.Property(e => e.IYtdlastClosePeriod).HasColumnName("iYTDLastClosePeriod");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NEmpHeight).HasColumnName("nEmpHeight");

                entity.Property(e => e.NEmpPayPeriod).HasColumnName("nEmpPayPeriod");

                entity.Property(e => e.NEmpWeight).HasColumnName("nEmpWeight");

                entity.Property(e => e.SEmpDepartment)
                    .HasColumnName("sEmpDepartment")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpEmail)
                    .HasColumnName("sEmpEmail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpEngFirstName)
                    .HasColumnName("sEmpEngFirstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpEngLastName)
                    .HasColumnName("sEmpEngLastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpEngNamePrefix)
                    .HasColumnName("sEmpEngNamePrefix")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpFirstName)
                    .IsRequired()
                    .HasColumnName("sEmpFirstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpGender)
                    .HasColumnName("sEmpGender")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpIdcardIssuePlace)
                    .HasColumnName("sEmpIDCardIssuePlace")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpIdcardNumber).HasColumnName("sEmpIDCardNumber");

                entity.Property(e => e.SEmpLastName)
                    .IsRequired()
                    .HasColumnName("sEmpLastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpMobilePhone)
                    .HasColumnName("sEmpMobilePhone")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpNamePrefix)
                    .IsRequired()
                    .HasColumnName("sEmpNamePrefix")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpNationality)
                    .HasColumnName("sEmpNationality")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpNickName)
                    .HasColumnName("sEmpNickName")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpOrgLevel2)
                    .IsRequired()
                    .HasColumnName("sEmpOrgLevel2")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpPassword)
                    .HasColumnName("sEmpPassword")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpPresentAddr1)
                    .HasColumnName("sEmpPresentAddr1")
                    .IsUnicode(false);

                entity.Property(e => e.SEmpPresentPhone)
                    .HasColumnName("sEmpPresentPhone")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpPresentZip).HasColumnName("sEmpPresentZip");

                entity.Property(e => e.SEmpRace)
                    .HasColumnName("sEmpRace")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpRegisterAddr1)
                    .HasColumnName("sEmpRegisterAddr1")
                    .IsUnicode(false);

                entity.Property(e => e.SEmpRegisterPhone)
                    .HasColumnName("sEmpRegisterPhone")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpRegisterZip).HasColumnName("sEmpRegisterZip");

                entity.Property(e => e.SEmpReligion)
                    .HasColumnName("sEmpReligion")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SEmpTimeCardId).HasColumnName("sEmpTimeCardID");

                entity.Property(e => e.SEmpUserName)
                    .HasColumnName("sEmpUserName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TEmpBirthDate)
                    .HasColumnName("tEmpBirthDate")
                    .HasColumnType("date");

                entity.Property(e => e.TEmpIdcardExpireDate)
                    .HasColumnName("tEmpIDCardExpireDate")
                    .HasColumnType("date");

                entity.Property(e => e.TEmpIdcardIssueDate)
                    .HasColumnName("tEmpIDCardIssueDate")
                    .HasColumnType("date");

                entity.Property(e => e.TEmpQuitWorkDate)
                    .HasColumnName("tEmpQuitWorkDate")
                    .HasColumnType("date");

                entity.Property(e => e.TEmpStartPermanentWorkDate)
                    .HasColumnName("tEmpStartPermanentWorkDate")
                    .HasColumnType("date");

                entity.Property(e => e.TEmpStartWorkDate)
                    .HasColumnName("tEmpStartWorkDate")
                    .HasColumnType("date");

                entity.Property(e => e.TYtdlastCloseDate)
                    .HasColumnName("tYTDLastCloseDate")
                    .HasColumnType("date");

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("update_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<DailypoGraphProduct>(entity =>
            {
                entity.HasKey(e => e.ID);

                //entity.ToTable("sp_DAILYPO_GraphProduct");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.EDate);

                entity.Property(e => e._Day);

                entity.Property(e => e.TeamCode);

                entity.Property(e => e.TeamName);

                entity.Property(e => e.Unit);

            });
        }

      
    }
}
