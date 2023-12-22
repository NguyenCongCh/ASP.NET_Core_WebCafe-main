using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCafe.Migrations
{
    /// <inheritdoc />
    public partial class AddRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMucSP",
                columns: table => new
                {
                    MaDM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDM = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AnhDM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTaDM = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DanhMucS__2725866EE256EC61", x => x.MaDM);
                });

            migrationBuilder.CreateTable(
                name: "RoleAccount",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RoleAcco__8AFACE3AAD449DBE", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "TinTuc",
                columns: table => new
                {
                    MaTT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTT = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AnhTT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Motangan = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Motadai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tacgia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    LoaiTin = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TinTuc__27250079137A8879", x => x.MaTT);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    MaSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDM = table.Column<int>(type: "int", nullable: false),
                    TenSP = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AnhSP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaSP = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    BestSeller = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    NgaySua = table.Column<DateTime>(type: "date", nullable: false),
                    MotaSP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SanPham__2725081CBC3461AC", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK__SanPham__MaDM__60A75C0F",
                        column: x => x.MaDM,
                        principalTable: "DanhMucSP",
                        principalColumn: "MaDM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiKhoan = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__349DA586DBEEEBCF", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK__Account__RoleID__5AEE82B9",
                        column: x => x.RoleID,
                        principalTable: "RoleAccount",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKH = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AvatarKH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diachi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Ngaysinh = table.Column<DateTime>(type: "date", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "varchar(26)", unicode: false, maxLength: 26, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhachHan__2725CF1E6183654A", x => x.MaKH);
                    table.ForeignKey(
                        name: "fk_Account_KhachHang",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    MaDH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "date", nullable: false),
                    TrangThaiHuyDon = table.Column<bool>(type: "bit", nullable: false),
                    ThanhToan = table.Column<bool>(type: "bit", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "date", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongTien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DonHang__27258661039BADEE", x => x.MaDH);
                    table.ForeignKey(
                        name: "FK__DonHang__MaKH__5DCAEF64",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    MaCTDH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDH = table.Column<int>(type: "int", nullable: false),
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<int>(type: "int", nullable: false),
                    Ngaygiao = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietD__1E4E40F03AFB7E71", x => x.MaCTDH);
                    table.ForeignKey(
                        name: "FK__ChiTietDon__MaDH__5BE2A6F2",
                        column: x => x.MaDH,
                        principalTable: "DonHang",
                        principalColumn: "MaDH");
                    table.ForeignKey(
                        name: "FK__ChiTietDon__MaSP__5CD6CB2B",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP");
                });

            migrationBuilder.CreateTable(
                name: "QuanLyShipper",
                columns: table => new
                {
                    MaShipper = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDH = table.Column<int>(type: "int", nullable: false),
                    TenShipper = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NgayLayHang = table.Column<DateTime>(type: "date", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    TenCongty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuanLySh__5C944AF68EF3404B", x => x.MaShipper);
                    table.ForeignKey(
                        name: "FK__QuanLyShip__MaDH__5FB337D6",
                        column: x => x.MaDH,
                        principalTable: "DonHang",
                        principalColumn: "MaDH");
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiDH",
                columns: table => new
                {
                    MaTTDH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDH = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrangTha__4484B8558FE01F3D", x => x.MaTTDH);
                    table.ForeignKey(
                        name: "FK__TrangThaiD__MaDH__619B8048",
                        column: x => x.MaDH,
                        principalTable: "DonHang",
                        principalColumn: "MaDH");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_RoleID",
                table: "Account",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaDH",
                table: "ChiTietDonHang",
                column: "MaDH");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaSP",
                table: "ChiTietDonHang",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaKH",
                table: "DonHang",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_AccountID",
                table: "KhachHang",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_QuanLyShipper_MaDH",
                table: "QuanLyShipper",
                column: "MaDH");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaDM",
                table: "SanPham",
                column: "MaDM");

            migrationBuilder.CreateIndex(
                name: "IX_TrangThaiDH_MaDH",
                table: "TrangThaiDH",
                column: "MaDH");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "QuanLyShipper");

            migrationBuilder.DropTable(
                name: "TinTuc");

            migrationBuilder.DropTable(
                name: "TrangThaiDH");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "DanhMucSP");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "RoleAccount");
        }
    }
}
