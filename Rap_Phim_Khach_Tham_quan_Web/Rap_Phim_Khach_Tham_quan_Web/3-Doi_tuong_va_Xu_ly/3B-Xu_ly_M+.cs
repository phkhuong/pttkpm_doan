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
public partial class XL_UNG_DUNG
{
    static XL_UNG_DUNG Ung_dung = null;

    public XL_DU_LIEU Du_lieu_Ung_dung = null;
    List<XL_NGUOI_DUNG_KHACH_THAM_QUAN> Danh_sach_Nguoi_dung_Khach_tham_quan = new List<XL_NGUOI_DUNG_KHACH_THAM_QUAN>();

    public static XL_UNG_DUNG Khoi_dong_Ung_dung()
    {
        Ung_dung = new XL_UNG_DUNG(); // Không caching 
        Ung_dung.Khoi_dong_Du_lieu_Ung_dung();
        return Ung_dung;
    }
    void Khoi_dong_Du_lieu_Ung_dung()
    {
        var Du_lieu_tu_Dich_vu = XL_DU_LIEU.Doc_Du_lieu();
        Du_lieu_Ung_dung = Du_lieu_tu_Dich_vu;
        //Bổ sung Thông tin cần thiết cho Tất cả người dùng 
        //===> khi xử lý Chức năng của Người dùng đăng nhập không cần đến Dữ liệu của Ứng dụng 
        Danh_sach_Nguoi_dung_Khach_tham_quan = Du_lieu_Ung_dung.Danh_sach_Nguoi_dung_Khach_tham_quan;
        if(HttpContext.Current.Session["Nguoi_dung_Dang_nhap"] == null)
        {
            var Nguoi_dung = new XL_NGUOI_DUNG_KHACH_THAM_QUAN();
            Nguoi_dung.Ten_Dang_nhap = "CHUA_DANG_NHAP";
            Nguoi_dung.Danh_sach_Phim_Xem = Du_lieu_Ung_dung.Danh_sach_Phim;
            HttpContext.Current.Session["Nguoi_dung_Dang_nhap"] = Nguoi_dung;
        }
        
    }
    //============= Xử lý Chức năng của Người dùng đăng nhập ==============
    //Lưu ý Quan trọng : Tất cả thông tin xử lý phải dựa vào thông tin của chính Người dùng đăng nhập 
    public XL_NGUOI_DUNG_KHACH_THAM_QUAN Dang_nhap(string Ten_Dang_nhap, string Mat_khau)
    {
        var Nguoi_dung = Danh_sach_Nguoi_dung_Khach_tham_quan.FirstOrDefault(
                                x => x.Ten_Dang_nhap == Ten_Dang_nhap
                                      && x.Mat_khau == Mat_khau
                                      );

        if (Nguoi_dung != null)
        {   //Khởi động  Thông tin Online  
            Nguoi_dung.Danh_sach_Phim_Xem = Du_lieu_Ung_dung.Danh_sach_Phim;
            HttpContext.Current.Session["Nguoi_dung_Dang_nhap"] = Nguoi_dung;
        }
        return Nguoi_dung;
    }
    public string Khoi_dong_Man_hinh_chinh()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_KHACH_THAM_QUAN)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 

        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Xem(Nguoi_dung_Dang_nhap);
        return Chuoi_HTML;
    }
    public string Khoi_dong_Man_hinh_Chi_tiet_Phim()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_KHACH_THAM_QUAN)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 

        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Chi_tiet_Phim(Nguoi_dung_Dang_nhap.Phim_chon,Nguoi_dung_Dang_nhap);
        return Chuoi_HTML;
    }
    public string Tra_cuu(string Chuoi_Tra_cuu)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_KHACH_THAM_QUAN)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 
        Nguoi_dung_Dang_nhap.Danh_sach_Phim_Xem = Tra_cuu_Phim(Chuoi_Tra_cuu, Du_lieu_Ung_dung.Danh_sach_Phim);
        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Xem(Nguoi_dung_Dang_nhap);
        return Chuoi_HTML;
    }
    public string Chon_Phim(string Ma_so_Phim)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_KHACH_THAM_QUAN)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 
        var Phim = Du_lieu_Ung_dung.Danh_sach_Phim.FirstOrDefault(x => x.Ma_so == Ma_so_Phim);
        Nguoi_dung_Dang_nhap.Phim_chon = Phim;
        var Chuoi_HTML = "<iframe class='KHUNG_CHUC_NANG' src='MH_CHi_tiet_Phim.cshtml'  ></iframe>";
        return Chuoi_HTML;
    }
    public string Tao_Chuoi_HTML_Xem(XL_NGUOI_DUNG_KHACH_THAM_QUAN Nguoi_dung_Dang_nhap)
    {
        var Chuoi_HTML = $"<div>" +
                $"{ Tao_Chuoi_HTML_Nguoi_dung_Dang_nhap(Nguoi_dung_Dang_nhap)}" +
                $"{ Tao_Chuoi_HTML_Danh_sach_Phim_Xem(Nguoi_dung_Dang_nhap.Danh_sach_Phim_Xem)}" +
            $"</div>";
        return Chuoi_HTML;
    }

    public string Tao_Chuoi_HTML_Chi_tiet_Phim(XL_PHIM Phim, XL_NGUOI_DUNG_KHACH_THAM_QUAN Nguoi_dung_Dang_nhap)
    {
        var Chuoi_HTML = $"<div>" +
            $"{Tao_Chuoi_Chi_tiet_Phim(Phim)}" +
            $"</div>";
        return Chuoi_HTML;
    }
}

//************************* View-Layers/Prsenetaition VL/PL **********************************
public partial class XL_UNG_DUNG
{
    public string Dia_chi_Media = $"{XL_DU_LIEU.Dia_chi_Dich_vu}/Media";
    public CultureInfo Dinh_dang_VN = CultureInfo.GetCultureInfo("vi-VN");

    public string Tao_Chuoi_HTML_Nguoi_dung_Dang_nhap(XL_NGUOI_DUNG_KHACH_THAM_QUAN Nguoi_dung)
    {
        return "";
    }

    public string Tao_Chuoi_HTML_Danh_sach_Phim_Xem(List<XL_PHIM> Danh_sach)
    {
        var Chuoi_HTML_Danh_sach = "<div class='container'><form id='HE_THONG' name='HE_THONG' method='post'>";
        var Chuoi_Input = "<input name='Th_Ma_so_Phim' id='Th_Ma_so_Phim' type='hidden' />"+
            "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_PHIM'/>"
            ;
        Chuoi_HTML_Danh_sach += Chuoi_Input;
        Chuoi_HTML_Danh_sach += "<div class='row'>";
        Danh_sach.ForEach(Phim =>
        {
            var Chuoi_Xu_ly_Click = "Th_Ma_so_Phim.value='XXX';;HE_THONG.submit() ";
            Chuoi_Xu_ly_Click = Chuoi_Xu_ly_Click.Replace("XXX", Phim.Ma_so);
            var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{Phim.Ma_so}.jpg' class='card-img-top HINH'/>";

            var Chuoi_Thong_tin = $"<div class='card-block THONG_TIN'>" +
                                      $"<h6 class='text-center'>{Phim.Ten}</h6>" +
                                  $"</div>";

            var Chuoi_HTML = $"<div class='KHUNG col-6 col-sm-6 col-md-4 col-lg-3' onclick=\"" + $"{Chuoi_Xu_ly_Click}" + "\">" +
                                 $"<div class='card'>" +
                                     $"{Chuoi_Hinh}" +
                                     $"<div class='OVERLAY'><div class='OVERLAY_TEXT'>Mua vé</div></div>" +


                                     $"{Chuoi_Thong_tin}" +
                                 $"</div>" +
                             "</div>";

            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        });

        Chuoi_HTML_Danh_sach += "</div></form></div>";

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
        var Ngay_Khoi_chieu =  Phim.Khoi_chieu;
        var Noi_dung = Phim.Noi_dung;
        var Chuoi_Hinh = $"<div class='col-md-4'><img src='{Dia_chi_Media}/{Phim.Ma_so}.jpg' /></div>";
        var Chuoi_Thong_tin = $"<div class='col-md-8'>"
                                + $"<h2>{Ten}</h2>";

        if (Phim.Ten_tieng_Anh != "")
        {
            Chuoi_Thong_tin += $"<h2 style='color:#a0a3a7;'>{Ten_tieng_Anh}</h2>";
        }
        Chuoi_Thong_tin += $"<div class='RATING'><strong style='font-size:20px;line-height:24px;'>{Rating.ToString()}</strong><span>/10</span></div>" +

                            $"<div class='THONG_TIN'><label><i class='far fa-clock'></i></label><span>&nbsp;{Thoi_luong} Phút</span></div>" +
                            $"<div class='THONG_TIN'><label>Quốc gia:&nbsp;</label><span>{Quoc_gia}</span></div>" +
                            $"<div class='THONG_TIN'><label>Đạo diễn:&nbsp;</label><span>{Dao_dien}</span></div>" +
                            $"<div class='THONG_TIN'><label>Diễn viên:&nbsp;</label><span>{Dien_vien}</span></div>" +
//                            $"<div class='THONG_TIN'><label>Thể loại:&nbsp;</label><p></p></div>" +
                            $"<div class='THONG_TIN'><label>Ngày:&nbsp;</label><span>{Ngay_Khoi_chieu.ToShortDateString()}</span></div>";
        Chuoi_Thong_tin += "</div>";
        var Chuoi_Noi_dung = $"<div class='col-md-12' style='margin-top:15px'><h3>NỘI DUNG PHIM</h3><div class='NOI_DUNG'>{Noi_dung}</div></div>";
        Chuoi_HTML += Chuoi_Hinh + Chuoi_Thong_tin + Chuoi_Noi_dung + "</div>";
        return Chuoi_HTML;
    }
    public string Tao_Chuoi_Suat_chieu(XL_PHIM Phim, List<XL_RAP> Danh_sach_Rap, DateTime Ngay_chon)
    {
        var Chuoi_Html = "";
        foreach(XL_RAP Rap in Danh_sach_Rap)
        {
            Chuoi_Html += $"<h5>{Rap.Ten}</h5>";
            var Chuoi_Danh_sach_Suat_chieu = "<div><ul>";
            foreach(XL_SUAT_CHIEU Suat_chieu in Phim.Danh_sach_Suat_chieu)
            {
                if(Suat_chieu.Rap.Ma_so == Rap.Ma_so && Suat_chieu.Bat_dau.Date == Ngay_chon.Date)
                    Chuoi_Danh_sach_Suat_chieu += $"<li>{Suat_chieu.Bat_dau.ToString("HH:mm")}</li>";
            }
            Chuoi_Danh_sach_Suat_chieu += "</ul></div>";
            Chuoi_Html += Chuoi_Danh_sach_Suat_chieu;
        }
        return Chuoi_Html;
    }

    
}


//************************* Business-Layers BL **********************************
public partial class XL_UNG_DUNG
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
    public static string Dia_chi_Dich_vu = "http://localhost:59900";
    public static string Dia_chi_Dich_vu_Quan_ly_Rap_Phim = $"{Dia_chi_Dich_vu}/1-Dich_vu_Giao_tiep/DV_Khach_tham_quan.cshtml";

    public static XL_DU_LIEU Doc_Du_lieu()
    {
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;

        var Tham_so = "Ma_so_Xu_ly=KHOI_DONG_DU_LIEU_PHAN_HE_KHACH_THAM_QUAN";
        var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Quan_ly_Rap_Phim}?{Tham_so}";
        var Chuoi_JSON = Xu_ly.DownloadString(Dia_chi_Xu_ly);
        var Du_lieu = Json.Decode<XL_DU_LIEU>(Chuoi_JSON);

        return Du_lieu;
    }
}