var kichThuoc = document.getElementsByClassName("slide")[0].clientWidth;
var chuyenSlide = document.getElementsByClassName("chuyen-slide")[0];
var Img = chuyenSlide.getElementsByTagName("img")
var maxNext = kichThuoc * Img.length;
maxNext -= kichThuoc;
var next = 0;
function nextSlide() {
    if (next < maxNext) next += kichThuoc;
    else next = 0;
    chuyenSlide.style.marginLeft = '-' + next + 'px';
}

function prevSlide() {
    if (next == 0) next = maxNext;
    else next -= kichThuoc;
    chuyenSlide.style.marginLeft = '-' + next + 'px';
}

setInterval(function () {
    nextSlide();
}, 15000)
