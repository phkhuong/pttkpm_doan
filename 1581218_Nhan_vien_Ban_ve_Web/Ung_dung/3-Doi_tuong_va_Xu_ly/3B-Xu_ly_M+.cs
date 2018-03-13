using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Globalization;
using System.Net;

//************************* M+ (Model for All ) **********************************
public partial class XL_DU_LIEU
{
    public static XL_DU_LIEU Khoi_dong_Du_lieu_Ung_dung()
    {
        var Du_lieu_Ung_dung = Doc_Du_lieu();

        return Du_lieu_Ung_dung;
    }
}

//************************* View-Layers/Prsenetaition VL/PL **********************************
public partial class XL_DU_LIEU
{
    public string Dia_chi_Media = $"{Dia_chi_Dich_vu}/Media";
    public CultureInfo Dinh_dang_VN = CultureInfo.GetCultureInfo("vi-VN");
    public string Tao_Chuoi_HTML_Nguoi_dung_Dang_nhap(XL_NGUOI_DUNG_NOI_BO Nguoi_dung)
    {
        var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{Nguoi_dung.Ma_so}.png' " +
                 "style='width:90px;height:90px;' />";
        var Chuoi_Thong_tin = $"<div class='btn' style='text-align:left'> " +
                       $"Nhân viên Bán vé {Nguoi_dung.Ho_ten}" +
                       
                      $"</div>";

        var Chuoi_HTML = $"<div   style='margin-bottom:10px' >" +
                           $"{Chuoi_Hinh}" + $"{Chuoi_Thong_tin}" +

                          $"</div>";
        return Chuoi_HTML;
    }
    public string Tao_Chuoi_HTML_Danh_sach_Phim(List<XL_PHIM> Danh_sach)
    {
        var Chuoi_HTML_Danh_sach = "<div class='row'>";

        Danh_sach.ForEach(Phim =>
        {
            var Chuoi_Xu_ly_Click = "Th_Ma_so.value='XXX';HE_THONG.submit() ";
            Chuoi_Xu_ly_Click = Chuoi_Xu_ly_Click.Replace("XXX", Phim.Ma_so);
            var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{Phim.Ma_so}.jpg' class='card-img-top HINH'/>";

            var Chuoi_Thong_tin = $"<div class='card-block THONG_TIN'>" +
                                      $"<h6 class='text-center'>{Phim.Ten}</h6>" +
                                  $"</div>";

            var Chuoi_HTML = $"<div class='KHUNG col-6 col-sm-6 col-md-4 col-lg-3' onclick=\"" + $"{Chuoi_Xu_ly_Click}" + "\">" +
                                 $"<div class='card'>" +
                                     $"{Chuoi_Hinh}" +
                                     $"<div class='OVERLAY'><div class='OVERLAY_TEXT'>Chọn</div></div>" +


                                     $"{Chuoi_Thong_tin}" +
                                 $"</div>" +
                             "</div>";

            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        });

        Chuoi_HTML_Danh_sach += "</div>";

        return Chuoi_HTML_Danh_sach;
    }

    public string Tao_Chuoi_Chi_tiet_Phim(XL_PHIM Phim)
    {
        var Chuoi_HTML = "<div class='row'>";
        var Ten = Phim.Ten;
        var Ten_tieng_Anh = Phim.Ten_tieng_Anh;
        var Rating = Phim.Rating;
        var Thoi_luong = Phim.Thoi_luong;
        var Quoc_gia = Phim.Quoc_gia;
        var Dao_dien = Phim.Dao_dien;
        var Dien_vien = Phim.Dien_vien;
        var The_loai = Phim.The_loai;
        var Nha_san_xuat = Phim.Nha_san_xuat;
        var Ngay_Khoi_chieu = Phim.Khoi_chieu;
        var Noi_dung = Phim.Noi_dung;
        var Chuoi_Hinh = $"<div class='col-md-4'><img src='{Dia_chi_Media}/{Phim.Ma_so}.jpg' /></div>";
        var Chuoi_Thong_tin = $"<div class='col-md-8'>"
                                + $"<h2>{Ten}</h2>";

        if (Phim.Ten_tieng_Anh != "")
        {
            Chuoi_Thong_tin += $"<h2 style='color:#a0a3a7;'>{Ten_tieng_Anh}</h2>";
        }
        var Chuoi_The_loai = "";
        var j = The_loai.Count();
        for (var i = 0; i < j - 1; i++)
        {
            Chuoi_The_loai += $"<span>{The_loai[i]}&sbquo;&nbsp;</span>";
        }
        Chuoi_The_loai += $"<span>{The_loai[j-1]}</span>";         
        Chuoi_Thong_tin += $"<div class='RATING'><strong style='font-size:20px;line-height:24px;'>{Rating.ToString()}</strong><span>/10</span></div>" +
                            $"<div class='THONG_TIN'><label><i class='far fa-clock'></i></label><span>&nbsp;{Thoi_luong} Phút</span></div>" +
                            $"<div class='THONG_TIN'><label>Quốc gia:&nbsp;</label><span>{Quoc_gia}</span></div>" +
                            $"<div class='THONG_TIN'><label>Đạo diễn:&nbsp;</label><span>{Dao_dien}</span></div>" +
                            $"<div class='THONG_TIN'><label>Diễn viên:&nbsp;</label><span>{Dien_vien}</span></div>" +
                            $"<div class='THONG_TIN'><label>Thể loại:&nbsp;</label>{Chuoi_The_loai}</div>" +
                            $"<div class='THONG_TIN'><label>Nhà sản xuất:&nbsp;</label><span>{Nha_san_xuat}</span></div>" +
                            $"<div class='THONG_TIN'><label>Ngày khởi chiếu:&nbsp;</label><span>{Ngay_Khoi_chieu.ToShortDateString()}</span></div>";
        Chuoi_Thong_tin += "</div>";
        var Chuoi_Noi_dung = $"<div class='col-md-12' style='margin-top:15px'><h3>NỘI DUNG PHIM</h3><div class='NOI_DUNG'>{Noi_dung}</div></div>";
        Chuoi_HTML += Chuoi_Hinh + Chuoi_Thong_tin + Chuoi_Noi_dung + "</div>";
        return Chuoi_HTML;
    }
    public string Tao_Chuoi_Suat_chieu(XL_PHIM Phim, List<XL_RAP> Danh_sach_Rap, DateTime Ngay_chon)
    {
        var Chuoi_Html = "";
        foreach (XL_RAP Rap in Danh_sach_Rap)
        {
            Chuoi_Html += $"<h5>{Rap.Ten}</h5>";
            var Chuoi_Danh_sach_Suat_chieu = "<div><ul>";
            foreach (XL_SUAT_CHIEU Suat_chieu in Phim.Danh_sach_Suat_chieu)
            {
                if (Suat_chieu.Rap.Ma_so == Rap.Ma_so && Suat_chieu.Bat_dau.Date == Ngay_chon.Date)
                    Chuoi_Danh_sach_Suat_chieu += $"<li>{Suat_chieu.Bat_dau.ToString("HH:mm")}</li>";
            }
            Chuoi_Danh_sach_Suat_chieu += "</ul></div>";
            Chuoi_Html += Chuoi_Danh_sach_Suat_chieu;
        }
        return Chuoi_Html;
    }


}


//************************* Business-Layers BL **********************************
public partial class XL_DU_LIEU
{

    public List<XL_PHIM> Tra_cuu_Phim(string Chuoi_Tra_cuu, List<XL_PHIM> Danh_sach)
    {
        Danh_sach = Danh_sach.FindAll(Phim =>
            Phim.Ten.ToUpper().Contains(Chuoi_Tra_cuu.ToUpper())
        );
        return Danh_sach;
    }

    public XL_PHIM Tim_Phim(string Ma_so, List<XL_PHIM> Danh_sach)
    {
        var Phim = Danh_sach.Find(P =>
        P.Ma_so.Contains(Ma_so));
        return Phim;
    }

}


//************************* Data-Layers DL **********************************
public partial class XL_DU_LIEU
{
    static string Dia_chi_Dich_vu = "http://localhost:59900";
    static string Dia_chi_Dich_vu_Quan_ly_Rap_Phim = $"{Dia_chi_Dich_vu}/1-Dich_vu_Giao_tiep/DV_Nhan_vien_Ban_ve.cshtml";

    static XL_DU_LIEU Doc_Du_lieu()
    {
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;

        var Tham_so = "Ma_so_Xu_ly=KHOI_DONG_DU_LIEU_PHAN_HE_NHAN_VIEN";
        var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Quan_ly_Rap_Phim}?{Tham_so}";
        var Chuoi_JSON = Xu_ly.DownloadString(Dia_chi_Xu_ly);
        var Du_lieu = Json.Decode<XL_DU_LIEU>(Chuoi_JSON);

        return Du_lieu;
    }
}