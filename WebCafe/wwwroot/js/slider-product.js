var kichThuoc = document.getElementsByClassName("slide-detail-product")[0].clientWidth;
var chuyenSlide = document.getElementsByClassName("chuyen-slide-detail")[0];
var Img = chuyenSlide.getElementsByTagName("img")
var maxNext = kichThuoc * Img.length;
maxNext -= kichThuoc;
var next = 0;
function nextSlideDetail() {
    if (next < maxNext) next += kichThuoc;
    else next = 0;
    chuyenSlide.style.marginLeft = '-' + next + 'px';
}

function prevSlideDetail() {
    if (next == 0) next = maxNext;
    else next -= kichThuoc;
    chuyenSlide.style.marginLeft = '-' + next + 'px';
}

setInterval(function () {
    nextSlideDetail();
}, 15000)
