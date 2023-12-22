using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCafe.Models;

public partial class TinTuc
{
    [Key]
    public int MaTt { get; set; }

    [Display(Name = "TenTt")]
    [Required(ErrorMessage = "Tiêu đề Tin Tức không được để trống.")]
    [MinLength(2, ErrorMessage = "Tiêu Đề Tin Tức phải dài hơn 2 kí tự.")]
    public string TenTt { get; set; } = null!;

    [Required(ErrorMessage = "Ảnh Tin Tức không được để trống.")]
    public string AnhTt { get; set; } = null!;

    [Required(ErrorMessage = "Mô Tả Ngắn không được để trống.")]
    [MinLength(2, ErrorMessage = "Mô Tả Ngắn phải dài hơn 2 kí tự.")]
    [MaxLength(100, ErrorMessage = "Mô Tả Ngắn không được dài quá 100 kí tự")]
    public string Motangan { get; set; } = null!;

    [Required(ErrorMessage = "Mô Tả Dài không được để trống.")]
    [MinLength(20, ErrorMessage = "Mô Tả Dài phải dài hơn 20 kí tự.")]
    public string Motadai { get; set; } = null!;

    [Required(ErrorMessage = "Tên Tác Giả không được để trống.")]
    [MinLength(2, ErrorMessage = "Tên Tác Giả phải dài hơn 2 kí tự.")]
    [RegularExpression(@"^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆ fFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTu UùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ0-9]*$", ErrorMessage =
        "Tên Tác Giả không được chứa kí tự đặc biệt.")]
    public string Tacgia { get; set; } = null!;

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
    public DateTime CreateDate { get; set; }

    public bool LoaiTin { get; set; }
}
