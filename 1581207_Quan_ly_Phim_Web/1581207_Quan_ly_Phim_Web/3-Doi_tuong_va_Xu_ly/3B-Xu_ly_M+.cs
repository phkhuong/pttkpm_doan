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

    //public string Tao_Chuoi_HTML_Danh_sach_Phim(List<XL_PHIM> Danh_sach)
    //{
    //    var Chuoi_HTML_Danh_sach = "<div class='row'>";

    //    Danh_sach.ForEach(Phim =>
    //    {
    //        var Chuoi_Hinh = $"<div class='KHUNG_HINH mx-auto'>" +
    //                            $"<img src='{Dia_chi_Media}/{Phim.Ma_so}.jpg' class='img-thumbnail HINH'/>" +
    //                         "</div>";

    //        var Chuoi_Thong_tin = $"<div>" +
    //                                  $"<strong>{Phim.Ten}</strong>" +
    //                                  $"<br />Đơn giá: { Phim.Don_gia.ToString("c0", Dinh_dang_VN) }" +
    //                              $"</div>";

    //        var Chuoi_HTML = $"<div class='KHUNG col-xs-12 col-sm-6 col-md-4 col-lg-3'>" +
    //                             $"<div class='THONG_TIN'>" +
    //                                 $"{Chuoi_Hinh}" +
    //                                 $"{Chuoi_Thong_tin}" +
    //                             $"</div>" +
    //                         "</div>";

    //        Chuoi_HTML_Danh_sach += Chuoi_HTML;
    //    });

    //    Chuoi_HTML_Danh_sach += "</div>";

    //    return Chuoi_HTML_Danh_sach;
    //}

    public string Tao_Chuoi_HTML_Danh_sach_Phim(List<XL_PHIM> Danh_sach)
    {
        var Chuoi_HTML_Danh_sach = "<div class='row'>";

        Danh_sach.ForEach(Phim =>
        {
            var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{Phim.Ma_so}.jpg' class='card-img-top HINH'/>";

            var Chuoi_Thong_tin = $"<div class='card-block THONG_TIN'>" +
                                      $"<h6 class='text-center'>{Phim.Ten}</h6>" +
                                  $"</div>";

            var Chuoi_HTML = $"<div class='KHUNG col-6 col-sm-6 col-md-4 col-lg-3'>" +
                                 $"<div class='card'>" +
                                     $"{Chuoi_Hinh}" +
                                     $"<div class='OVERLAY'><div class='OVERLAY_TEXT'>Mua vé</div><div class='OVERLAY_TEXT'>Chi tiết</div></div>" +


                                     $"{Chuoi_Thong_tin}" +
                                 $"</div>" +
                             "</div>";

            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        });

        Chuoi_HTML_Danh_sach += "</div>";

        return Chuoi_HTML_Danh_sach;
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


}

//************************* Data-Layers DL **********************************
public partial class XL_DU_LIEU
{
    static string Dia_chi_Dich_vu = "http://localhost:59900";
    static string Dia_chi_Dich_vu_Quan_ly_Rap_Phim = $"{Dia_chi_Dich_vu}/1-Dich_vu_Giao_tiep/DV_Quan_ly_Phim.cshtml";

    static XL_DU_LIEU Doc_Du_lieu()
    {
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;

        var Tham_so = "Ma_so_Xu_ly=KHOI_DONG_DU_LIEU_PHAN_HE_QUAN_LY_PHIM";
        var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Quan_ly_Rap_Phim}?{Tham_so}";
        var Chuoi_JSON = Xu_ly.DownloadString(Dia_chi_Xu_ly);
        var Du_lieu = Json.Decode<XL_DU_LIEU>(Chuoi_JSON);

        return Du_lieu;
    }
}