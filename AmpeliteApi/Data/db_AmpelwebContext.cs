using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AmpeliteApi.Models.Ampelweb;

namespace AmpeliteApi.Data
{
    public partial class db_AmpelwebContext : DbContext
    {
        public db_AmpelwebContext(DbContextOptions<db_AmpelwebContext> options) : base(options) { }

        public virtual DbSet<AmpelTeam> AmpelTeam { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AmpelTeam>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AmpelTeam");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SaleCode)
                .HasMaxLength(25)
                .IsUnicode(false);

                entity.Property(e => e.SaleName)
                .HasMaxLength(150)
                .IsUnicode(false);

                entity.Property(e => e.ZoneCode).HasMaxLength(10).IsUnicode(false);

                entity.Property(e => e.ZoneName).HasMaxLength(150).IsUnicode(false);

                entity.Property(e => e.ZoneGroupCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ZoneGroup_Code");

                entity.Property(e => e.ZoneGroup).HasMaxLength(150).IsUnicode(false);

                entity.Property(e => e.TeamCode).HasMaxLength(10).IsUnicode(false);

                entity.Property(e => e.TeamName).HasMaxLength(100).IsUnicode(false);

                entity.Property(e => e.TeamGroupCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TeamGroup_Code");

                entity.Property(e => e.TeamGroup).HasMaxLength(100).IsUnicode(false);

                entity.Property(e => e.Traget);

                entity.Property(e => e.AddSaleTotal).HasMaxLength(20).IsUnicode(false);

                entity.Property(e => e.SaleCodeRpt)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("SaleCode_RPT");

                entity.Property(e => e.SaleNameRpt)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("SaleName_RPT");

                entity.Property(e => e.UpdateNow)
                .HasColumnName("Update_Now")
                .HasColumnType("datetime");

                entity.Property(e => e.UserUpdate)
                .HasColumnName("User_Update");
            });
        }
    }
}
