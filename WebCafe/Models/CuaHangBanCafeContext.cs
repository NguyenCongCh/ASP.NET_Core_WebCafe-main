using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebCafe.Models;

public partial class CuaHangBanCafeContext : DbContext
{
    public CuaHangBanCafeContext()
    {
    }

    public CuaHangBanCafeContext(DbContextOptions<CuaHangBanCafeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<DanhMucSp> DanhMucSps { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<QuanLyShipper> QuanLyShippers { get; set; }

    public virtual DbSet<RoleAccount> RoleAccounts { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TinTuc> TinTucs { get; set; }

    public virtual DbSet<TrangThaiDh> TrangThaiDhs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-9I3J6GPT\\SQLEXPRESS;Initial Catalog=CuaHangBanCafe;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA58614E9D175");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.TaiKhoan)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account__RoleID__5AEE82B9");
        });

        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaCtdh).HasName("PK__ChiTietD__1E4E40F011B1B8E2");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.MaCtdh).HasColumnName("MaCTDH");
            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDon__MaDH__5BE2A6F2");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDon__MaSP__5CD6CB2B");
        });

        modelBuilder.Entity<DanhMucSp>(entity =>
        {
            entity.HasKey(e => e.MaDm).HasName("PK__DanhMucS__2725866ED7EA2C7E");

            entity.ToTable("DanhMucSP");

            entity.Property(e => e.MaDm).HasColumnName("MaDM");
            entity.Property(e => e.AnhDm).HasColumnName("AnhDM");
            entity.Property(e => e.MoTaDm)
                .HasMaxLength(500)
                .HasColumnName("MoTaDM");
            entity.Property(e => e.TenDm)
                .HasMaxLength(200)
                .HasColumnName("TenDM");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DonHang__27258661634EA785");

            entity.ToTable("DonHang");

            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.MaKm).HasColumnName("MaKM");
            entity.Property(e => e.NgayTao).HasColumnType("date");
            entity.Property(e => e.NgayThanhToan).HasColumnType("date");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonHang__MaKH__5DCAEF64");

            entity.HasOne(d => d.MaKmNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaKm)
                .HasConstraintName("FK_DonHang_KhuyenMai");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1EC09B3E21");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AvatarKh).HasColumnName("AvatarKH");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.Diachi).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.Ngaysinh).HasColumnType("date");
            entity.Property(e => e.Password)
                .HasMaxLength(26)
                .IsUnicode(false);
            entity.Property(e => e.TenKh)
                .HasMaxLength(200)
                .HasColumnName("TenKH");

            entity.HasOne(d => d.Account).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("fk_Account_KhachHang");
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKm);

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.MaKm)
                .ValueGeneratedNever()
                .HasColumnName("MaKM");
            entity.Property(e => e.ChiTiet)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.LoaiKm).HasColumnName("LoaiKM");
            entity.Property(e => e.NgayBd)
                .HasColumnType("datetime")
                .HasColumnName("NgayBD");
            entity.Property(e => e.NgayKt)
                .HasColumnType("datetime")
                .HasColumnName("NgayKT");
            entity.Property(e => e.TenKm)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("TenKM");
        });

        modelBuilder.Entity<QuanLyShipper>(entity =>
        {
            entity.HasKey(e => e.MaShipper).HasName("PK__QuanLySh__5C944AF6FE52BBCC");

            entity.ToTable("QuanLyShipper");

            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.NgayLayHang).HasColumnType("date");
            entity.Property(e => e.TenCongty).HasMaxLength(100);
            entity.Property(e => e.TenShipper).HasMaxLength(100);

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.QuanLyShippers)
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuanLyShip__MaDH__5FB337D6");
        });

        modelBuilder.Entity<RoleAccount>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__RoleAcco__8AFACE3A3BDCA96D");

            entity.ToTable("RoleAccount");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Mota).HasMaxLength(200);
            entity.Property(e => e.RoleName)
                .HasMaxLength(12)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725081C111B2812");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.AnhSp).HasColumnName("AnhSP");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.GiaSp).HasColumnName("GiaSP");
            entity.Property(e => e.MaDm).HasColumnName("MaDM");
            entity.Property(e => e.MaKm).HasColumnName("MaKM");
            entity.Property(e => e.MotaSp).HasColumnName("MotaSP");
            entity.Property(e => e.NgaySua).HasColumnType("date");
            entity.Property(e => e.TenSp)
                .HasMaxLength(200)
                .HasColumnName("TenSP");
            entity.Property(e => e.VideoSp).HasColumnName("VideoSP");

            entity.HasOne(d => d.MaDmNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDm)
                .HasConstraintName("FK__SanPham__MaDM__60A75C0F");

            entity.HasOne(d => d.MaKmNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaKm)
                .HasConstraintName("FK_SanPham_KhuyenMai");
        });

        modelBuilder.Entity<TinTuc>(entity =>
        {
            entity.HasKey(e => e.MaTt).HasName("PK__TinTuc__272500792030A31B");

            entity.ToTable("TinTuc");

            entity.Property(e => e.MaTt).HasColumnName("MaTT");
            entity.Property(e => e.AnhTt).HasColumnName("AnhTT");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.Motangan).HasMaxLength(200);
            entity.Property(e => e.Tacgia).HasMaxLength(100);
            entity.Property(e => e.TenTt)
                .HasMaxLength(200)
                .HasColumnName("TenTT");
        });

        modelBuilder.Entity<TrangThaiDh>(entity =>
        {
            entity.HasKey(e => e.MaTtdh).HasName("PK__TrangTha__4484B855D98999B9");

            entity.ToTable("TrangThaiDH");

            entity.Property(e => e.MaTtdh).HasColumnName("MaTTDH");
            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.TrangThai).HasMaxLength(200);

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.TrangThaiDhs)
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TrangThaiD__MaDH__619B8048");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
