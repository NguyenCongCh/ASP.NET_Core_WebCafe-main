// <div id="thongBao" data-tbao="@ViewBag.ThongBao"></div>

// Lấy dữ liệu từ thuộc tính data-tbao của id="thongBao"
var thongBao = document.getElementById("thongBao").dataset.tbao;
if (thongBao != "") {
    alert(thongBao);
    document.getElementById("thongBao").dataset.tbao = "";
}
