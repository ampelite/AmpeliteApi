using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AmpeliteApi.Models
{
    public partial class db_AmpeliteContext : DbContext
    {
        public virtual DbSet<AuthDevices> AuthDevices { get; set; }
        public virtual DbSet<AuthPermissions> AuthPermissions { get; set; }
        public virtual DbSet<AuthTransactions> AuthTransactions { get; set; }
        public virtual DbSet<DailypoGroupReport> DailypoGroupReport { get; set; }
        public virtual DbSet<DailypoGroupUnit> DailypoGroupUnit { get; set; }
        public virtual DbSet<DailypoProductGroup> DailypoProductGroup { get; set; }
        public virtual DbSet<DailypoProductTeam> DailypoProductTeam { get; set; }
        public virtual DbSet<GetTransactionInv> GetTransactionInv { get; set; }
        public virtual DbSet<GetTransactionSo> GetTransactionSo { get; set; }
        public virtual DbSet<HrEmployee> HrEmployee { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //        optionsBuilder.UseSqlServer(@"Server=AMPELITE-001\SQLEXPRESS01;Database=db_Ampelite;Trusted_Connection=True;user id=sa;password=Amp7896321;");
        //    }
        //}
        public db_AmpeliteContext(DbContextOptions<db_AmpeliteContext> options) : base(options) { }

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

            modelBuilder.Entity<GetTransactionInv>(entity =>
            {
                entity.ToTable("GET_TransactionInv");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrchId).HasColumnName("BrchID");

                entity.Property(e => e.CustCode).HasMaxLength(50);

                entity.Property(e => e.CustName).HasMaxLength(255);

                entity.Property(e => e.CustPono)
                    .HasColumnName("CustPONo")
                    .HasMaxLength(50);

                entity.Property(e => e.CustTypeCode).HasMaxLength(10);

                entity.Property(e => e.CustTypeName).HasMaxLength(50);

                entity.Property(e => e.DataFrom).HasMaxLength(5);

                entity.Property(e => e.DocuDate).HasColumnType("datetime");

                entity.Property(e => e.DocuNo).HasMaxLength(50);

                entity.Property(e => e.DocuStatus).HasMaxLength(10);

                entity.Property(e => e.EmpCode).HasMaxLength(50);

                entity.Property(e => e.GoodBrandCode).HasMaxLength(50);

                entity.Property(e => e.GoodBrandId).HasColumnName("GoodBrandID");

                entity.Property(e => e.GoodBrandName).HasMaxLength(50);

                entity.Property(e => e.GoodCateId).HasColumnName("GoodCateID");

                entity.Property(e => e.GoodClassCode).HasMaxLength(50);

                entity.Property(e => e.GoodClassName).HasMaxLength(50);

                entity.Property(e => e.GoodCode).HasMaxLength(100);

                entity.Property(e => e.GoodColorCode).HasMaxLength(10);

                entity.Property(e => e.GoodColorId).HasColumnName("GoodColorID");

                entity.Property(e => e.GoodColorName).HasMaxLength(50);

                entity.Property(e => e.GoodColorNameEng)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoodGroupCode).HasMaxLength(50);

                entity.Property(e => e.GoodGroupId).HasColumnName("GoodGroupID");

                entity.Property(e => e.GoodGroupName).HasMaxLength(50);

                entity.Property(e => e.GoodId)
                    .HasColumnName("GoodID")
                    .HasMaxLength(100);

                entity.Property(e => e.GoodMarketName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoodName).HasMaxLength(255);

                entity.Property(e => e.GoodPattnCode).HasMaxLength(10);

                entity.Property(e => e.GoodPattnId).HasColumnName("GoodPattnID");

                entity.Property(e => e.GoodPattnName).HasMaxLength(100);

                entity.Property(e => e.GoodPrice1).HasColumnType("money");

                entity.Property(e => e.GoodTypeCode).HasMaxLength(10);

                entity.Property(e => e.GoodTypeId).HasColumnName("GoodTypeID");

                entity.Property(e => e.GoodTypeName).HasMaxLength(50);

                entity.Property(e => e.GoodUnitName).HasMaxLength(50);

                entity.Property(e => e.InvNo).HasMaxLength(50);

                entity.Property(e => e.JobCode).HasMaxLength(50);

                entity.Property(e => e.JobName).HasMaxLength(255);

                entity.Property(e => e.MainGoodUnitId).HasColumnName("MainGoodUnitID");

                entity.Property(e => e.Model).HasMaxLength(255);

                entity.Property(e => e.Product).HasMaxLength(255);

                entity.Property(e => e.ProductCode).HasMaxLength(200);

                entity.Property(e => e.SaleAreaCode).HasMaxLength(10);

                entity.Property(e => e.SaleAreaName).HasMaxLength(100);

                entity.Property(e => e.SaleName).HasMaxLength(255);
            });

            modelBuilder.Entity<GetTransactionSo>(entity =>
            {
                entity.ToTable("GET_TransactionSO");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BillDiscFormula)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BrchId).HasColumnName("BrchID");

                entity.Property(e => e.ClearSo)
                    .HasColumnName("ClearSO")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CustCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustId).HasColumnName("CustID");

                entity.Property(e => e.CustName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CustPono)
                    .HasColumnName("CustPONo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DataFrom)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DeptId).HasColumnName("DeptID");

                entity.Property(e => e.DeptName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocuDate).HasColumnType("datetime");

                entity.Property(e => e.DocuNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.EmpName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmpNameEng)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GoodBrandCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GoodBrandId).HasColumnName("GoodBrandID");

                entity.Property(e => e.GoodBrandName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GoodCateId).HasColumnName("GoodCateID");

                entity.Property(e => e.GoodClassCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GoodClassId).HasColumnName("GoodClassID");

                entity.Property(e => e.GoodClassName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GoodCode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoodColorCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GoodColorId).HasColumnName("GoodColorID");

                entity.Property(e => e.GoodColorName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GoodColorNameEng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GoodId).HasColumnName("GoodID");

                entity.Property(e => e.GoodName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoodTypeCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GoodTypeId).HasColumnName("GoodTypeID");

                entity.Property(e => e.GoodTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GoodUnitId2).HasColumnName("GoodUnitID2");

                entity.Property(e => e.GoodUnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InveCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InveId).HasColumnName("InveID");

                entity.Property(e => e.InveName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InveNameEng)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.JobCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.JobName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.JobNameEng)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.OnHold)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Product)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SaleAreaCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SaleAreaId).HasColumnName("SaleAreaID");

                entity.Property(e => e.SaleAreaName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.ShipToAddr1).IsUnicode(false);

                entity.Property(e => e.Soid).HasColumnName("SOID");

                entity.Property(e => e.TranspId).HasColumnName("TranspID");

                entity.Property(e => e.TranspName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Vatamnt).HasColumnName("VATAmnt");
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
        }
    }
}
