using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AmpeliteApi.Models;
using AmpeliteApi.Models.Ampelite;

namespace AmpeliteApi.Data
{
    public partial class db_AmpeliteContext : DbContext
    {
        public db_AmpeliteContext(DbContextOptions<db_AmpeliteContext> options) : base(options) { }

        public virtual DbSet<AuthDevices> AuthDevices { get; set; }
        public virtual DbSet<AuthPermissions> AuthPermissions { get; set; }
        public virtual DbSet<AuthTransactions> AuthTransactions { get; set; }
        public virtual DbSet<CodePromotion> CodePromotion { get; set; }
        public virtual DbSet<CostRf> CostRf { get; set; }
        public virtual DbSet<CreditCustomerGroup> CreditCustomerGroup { get; set; }
        public virtual DbSet<DailypoGroupReport> DailypoGroupReport { get; set; }
        public virtual DbSet<DailypoGroupUnit> DailypoGroupUnit { get; set; }
        public virtual DbSet<DailypoProductGroup> DailypoProductGroup { get; set; }
        public virtual DbSet<DailypoProductTeam> DailypoProductTeam { get; set; }
        public virtual DbSet<GetTransactionInv> GetTransactionInv { get; set; }
        public virtual DbSet<GetTransactionSo> GetTransactionSo { get; set; }
        public virtual DbSet<GoodBrandCode> GoodBrandCode { get; set; }
        public virtual DbSet<GoodCateCode> GoodCateCode { get; set; }
        public virtual DbSet<HrEmployee> HrEmployee { get; set; }
        public virtual DbSet<ListCustPromotion> ListCustPromotion { get; set; }
        public virtual DbSet<ListProductPromotion> ListProductPromotion { get; set; }
        public virtual DbSet<ProductPromotion> ProductPromotion { get; set; }
        public virtual DbSet<SaleCode> SaleCode { get; set; }
        public virtual DbSet<SaleproFrpcostRf> SaleproFrpcostRf { get; set; }
        public virtual DbSet<SaleproGoodPattn> SaleproGoodPattn { get; set; }
        public virtual DbSet<SalePromotionReport> SalePromotionReport { get; set; }
        public virtual DbSet<SaleproTrussScw> SaleproTrussScw { get; set; }
        public virtual DbSet<SaleProPromotionTarget> SaleProPromotionTargets { get; set; }
        public virtual DbSet<SaleProBalanceHD> SaleProBalanceHDs { get; set; }
        public virtual DbSet<SaleProBalanceDT> SaleProBalanceDTs { get; set; }

        // Store procedure
        public virtual DbSet<DailypoGraphProduct> DailypoGraphProduct { get; set; }
        public virtual DbSet<DailypoDetailDaily> DailypoDetailDaily { get; set; }
        public virtual DbSet<SpSaleproFrpLoyalty> SpSaleproFrpLoyalty { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthDevices>(entity =>
            {
                entity.HasKey(e => e.AuthDId);

                entity.ToTable("AUTH_Devices");

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
            });

            modelBuilder.Entity<CodePromotion>(entity =>
            {
                entity.HasKey(e => e.SubId);
                entity.ToTable("Code_Promotion");
                entity.Property(e => e.SubId)
                    .HasColumnName("SUB_ID")
                    .HasColumnType("nvarchar(4)")
                    .ValueGeneratedNever();
                entity.Property(e => e.CodeMainPro)
                    .HasColumnName("Code_MainPro");
                entity.Property(e => e.MainPro)
                    .HasColumnType("nvarchar(20)");
                entity.Property(e => e.SubCodePro)
                    .HasColumnName("SUB_CodePro")
                    .HasColumnType("nvarchar(50)")
                    .HasMaxLength(50);
                entity.Property(e => e.SubPromotion)
                    .HasColumnType("nvarchar(max)")
                    .HasColumnName("SUB_Promotion");
                entity.Property(e => e.StartDate).HasColumnType("datetime");
                entity.Property(e => e.EndDate).HasColumnType("datetime");
                entity.Property(e => e.Status).HasColumnType("bit");
                entity.Property(e => e.CreateBy).HasMaxLength(50);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.UpateBy).HasColumnType("datetime");
                entity.Property(e => e.UpateDate).HasMaxLength(50);
            });

            modelBuilder.Entity<CostRf>(entity =>
            {
                entity.HasKey(e => e.GoodBrandcode);

                entity.ToTable("CostRF");

                entity.Property(e => e.GoodBrandcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.PerRfMax)
                    .HasColumnName("PerRF_Max")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubCodePro)
                    .HasColumnName("SUB_CodePro")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubId)
                    .HasColumnName("SUB_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CreditCustomerGroup>(entity =>
            {
                entity.HasKey(e => e.CustomerCode);

                entity.Property(e => e.CustomerCode)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.ColAmount).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.ColAmount2).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.ColAmount3).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.ColAmount4).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.ColDay).HasColumnType("date");

                entity.Property(e => e.ColDay2).HasColumnType("date");

                entity.Property(e => e.ColDay3).HasColumnType("date");

                entity.Property(e => e.ColDay4).HasColumnType("date");

                entity.Property(e => e.CreditTerm1Frpday).HasColumnName("CreditTerm1FRPDay");

                entity.Property(e => e.CustCodeId).HasColumnName("CustCode_ID");

                entity.Property(e => e.CustomerHardCode)
                    .HasColumnName("Customer_HardCode")
                    .HasMaxLength(20);

                entity.Property(e => e.CustomerMainCode)
                    .HasColumnName("Customer_MainCode")
                    .HasMaxLength(20);

                entity.Property(e => e.CustomerName).HasMaxLength(150);

                entity.Property(e => e.CustomerType)
                    .HasColumnName("Customer_Type")
                    .HasMaxLength(255);

                entity.Property(e => e.Pdcamount)
                    .HasColumnName("PDCAmount")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Pdcday).HasColumnName("PDCDay");

                entity.Property(e => e.Pdcterm1FrpdayType).HasColumnName("PDCTerm1FRPDayType");

                entity.Property(e => e.Pdcterm1ScrewDayType).HasColumnName("PDCTerm1ScrewDayType");

                entity.Property(e => e.TempAmount).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.TempTypePdc).HasColumnName("TempTypePDC");

                entity.Property(e => e.TypePdc).HasColumnName("TypePDC");

                entity.Property(e => e.WinspeedTypePdc).HasColumnName("WinspeedTypePDC");
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

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

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

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

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

                entity.Property(e => e.GoodPattnCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GoodPattnId).HasColumnName("GoodPattnID");

                entity.Property(e => e.GoodPattnName)
                    .HasMaxLength(100)
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

            modelBuilder.Entity<GoodBrandCode>(entity =>
            {
                entity.HasKey(e => e.GoodBrandcode);

                entity.Property(e => e.GoodBrandcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.GoodBrandName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubCodePro)
                    .HasColumnName("SUB_CodePro")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubId)
                    .HasColumnName("SUB_ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GoodCateCode>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GoodCatecode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.SubCodePro)
                    .HasColumnName("SUB_CodePro")
                    .HasMaxLength(50);

                entity.Property(e => e.SubId)
                    .IsRequired()
                    .HasColumnName("SUB_ID")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<HrEmployee>(entity =>
            {
                entity.ToTable("HR_Employee");

                entity.Property(e => e.Id).HasColumnName("id");

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

                entity.Property(e => e.SEmpId).HasColumnName("sEmpID");

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

            modelBuilder.Entity<ListCustPromotion>(entity =>
            {
                entity.HasKey(e => e.CustCodeId);

                entity.ToTable("List_CustPromotion");

                entity.Property(e => e.CustCodeId)
                    .HasColumnName("CustCode_ID")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.CustCode).HasMaxLength(50);

                entity.Property(e => e.SubCodePro)
                    .HasColumnName("SUB_CodePro")
                    .HasMaxLength(50);

                entity.Property(e => e.SubId)
                    .HasColumnName("SUB_ID")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ListProductPromotion>(entity =>
            {
                entity.HasKey(e => e.GoodId);

                entity.ToTable("List_ProductPromotion");

                entity.Property(e => e.GoodId)
                    .HasColumnName("GoodID")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.SubCodePro)
                    .HasColumnName("SUB_CodePro")
                    .HasMaxLength(50);

                entity.Property(e => e.SubId)
                    .HasColumnName("SUB_ID")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProductPromotion>(entity =>
            {
                entity.ToTable("Product_Promotion");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GoodCateCode).HasMaxLength(50);

                entity.Property(e => e.GoodCateName).HasMaxLength(50);

                entity.Property(e => e.GoodCode).HasMaxLength(50);

                entity.Property(e => e.GoodId)
                    .HasColumnName("GoodID")
                    .HasMaxLength(50);

                entity.Property(e => e.GoodName).HasMaxLength(50);

                entity.Property(e => e.GoodType).HasMaxLength(50);

                entity.Property(e => e.GoodTypeCode)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SaleCode>(entity =>
            {
                entity.HasKey(e => e.SaleCode1);

                entity.Property(e => e.SaleCode1)
                    .HasColumnName("SaleCode")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.SaleName).IsUnicode(false);

                entity.Property(e => e.SubCodePro)
                    .HasColumnName("SUB_CodePro")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubId)
                    .HasColumnName("SUB_ID")
                    .HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<SaleproFrpcostRf>(entity =>
            {
                entity.ToTable("SALEPRO_FRPCostRF");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GoodCateCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GoodCateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaxLength).HasColumnName("Max_Length");

                entity.Property(e => e.Rf).HasColumnName("RF");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SubCodePro)
                    .IsRequired()
                    .HasColumnName("SUB_CodePro")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SubId)
                    .IsRequired()
                    .HasColumnName("SUB_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TrsP).HasColumnName("Trs_P");
            });

            modelBuilder.Entity<SaleproGoodPattn>(entity =>
            {
                entity.ToTable("SALEPRO_GoodPattn");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FactorCp).HasColumnName("Factor_CP");

                entity.Property(e => e.GoodClassCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GoodClassName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GoodPattnCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GoodPattnName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SubCodePro)
                    .IsRequired()
                    .HasColumnName("SUB_CodePro")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SubId)
                    .IsRequired()
                    .HasColumnName("SUB_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalePromotionReport>(entity =>
            {
                entity.ToTable("Sale_Promotion_Report");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustCode).HasMaxLength(50);

                entity.Property(e => e.CustCodeId)
                    .IsRequired()
                    .HasColumnName("CustCode_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductCode).HasMaxLength(50);

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.SubId).HasColumnName("SUB_ID");
            });

            modelBuilder.Entity<SaleproTrussScw>(entity =>
            {
                entity.ToTable("SALEPRO_TrussSCW");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustCode).HasMaxLength(50);

                entity.Property(e => e.CustCodeId)
                    .IsRequired()
                    .HasColumnName("CustCode_ID")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductCode).HasMaxLength(50);

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.SubId)
                    .IsRequired()
                    .HasColumnName("SUB_ID")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SaleProPromotionTarget>(entity =>
            {
                entity.ToTable("SALEPRO_PromotionTarget");
                entity.HasKey(e => e.TargetID);
                entity.Property(e => e.TargetID);
                entity.Property(e => e.Target).HasColumnType("decimal(8,2)");
                entity.Property(e => e.Unit);
                entity.Property(e => e.UnitDesc).HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Reward);
                entity.Property(e => e.Discount);
                entity.Property(e => e.GiftVoucher);
                entity.Property(e => e.Bonus);
                entity.Property(e => e.IsBonus).HasColumnType("bit").HasDefaultValue(0);
                entity.Property(e => e.CostPromotion);
                entity.Property(e => e.Status).HasColumnType("bit").HasDefaultValue(1);
                entity.Property(e => e.Month);
                entity.Property(e => e.Year);
                entity.Property(e => e.SubID).HasColumnName("SUB_ID").HasMaxLength(50);
                entity.Property(e => e.CreateBy).HasMaxLength(50);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.UpdateBy).HasMaxLength(50);
                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SaleProBalanceHD>(entity =>
            {
                entity.ToTable("SALEPRO_BalanceHD");
                entity.HasKey(e => e.BHDID);
                entity.Property(e => e.BHDID);
                entity.Property(e => e.SUBID).HasColumnName("SUB_ID").HasMaxLength(20);
                entity.Property(e => e.IsConfirm).HasColumnType("bit").HasDefaultValue(0);
                entity.Property(e => e.CustCode).HasMaxLength(20);
                entity.Property(e => e.CustName).HasMaxLength(255);
                entity.Property(e => e.EmpCode).HasMaxLength(20);
                entity.Property(e => e.EmpName).HasMaxLength(255);
                entity.Property(e => e.Month);
                entity.Property(e => e.Year);
                entity.Property(e => e.GoodAmnt).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.GoodQty2).HasColumnType("decimal(8, 2)");
                entity.Property(e => e.TotalReward).HasColumnType("decimal(8,2)");
                entity.Property(e => e.TotalGiftVoucher).HasColumnType("decimal(8,2)");
                entity.Property(e => e.TotalDiscount).HasColumnType("decimal(8,2)");
                entity.Property(e => e.TotalBonus).HasColumnType("decimal(8,2)");
                entity.HasMany(e => e.BalancesDT);
            });

            modelBuilder.Entity<SaleProBalanceDT>(entity =>
            {
                entity.ToTable("SALEPRO_BalanceDT");
                entity.HasKey(e => e.BDTID);
                entity.Property(e => e.BDTID);
                entity.Property(e => e.BHDID);
                entity.Property(e => e.TargetID);
                entity.Property(e => e.Target).HasColumnType("decimal(8,2)");
                entity.Property(e => e.Reward).HasColumnType("decimal(8,2)");
                entity.Property(e => e.GiftVoucher).HasColumnType("decimal(8,2)");
                entity.Property(e => e.Discount).HasColumnType("decimal(8,2)");
                entity.Property(e => e.Bonus).HasColumnType("decimal(8,2)");
                entity.Property(e => e.IsBonus).HasColumnType("bit").HasDefaultValue(0);
                entity.Property(e => e.Unit);

                entity.Property(e => e.RewardSelect).HasColumnType("bit").HasDefaultValue(0);
                entity.Property(e => e.GiftSelect).HasColumnType("bit").HasDefaultValue(0);
                entity.Property(e => e.DiscountSelect).HasColumnType("bit").HasDefaultValue(0);
                entity.Property(e => e.BonusSelect).HasColumnType("bit").HasDefaultValue(0);

                entity.HasOne(e => e.BalanceHD).WithMany(e => e.BalancesDT).HasForeignKey("BHDID");
                

            });
        }
    }
}
