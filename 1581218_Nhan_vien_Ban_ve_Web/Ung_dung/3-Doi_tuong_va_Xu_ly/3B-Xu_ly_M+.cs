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
    List<XL_NGUOI_DUNG> Danh_sach_Nguoi_dung_Khach_tham_quan = new List<XL_NGUOI_DUNG>();
    List<XL_NGUOI_DUNG> Danh_sach_Nguoi_dung_Noi_bo = new List<XL_NGUOI_DUNG>();
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
        Danh_sach_Nguoi_dung_Noi_bo = Du_lieu_Ung_dung.Danh_sach_Nguoi_dung_Noi_bo;
        if (HttpContext.Current.Session["Nguoi_dung_Dang_nhap"] == null)
        {
            var Nguoi_dung = new XL_NGUOI_DUNG();
            Nguoi_dung.Ten_Dang_nhap = "CHUA_DANG_NHAP";
            Nguoi_dung.Danh_sach_Phim_Xem = Du_lieu_Ung_dung.Danh_sach_Phim;
            Nguoi_dung.Danh_sach_Rap = Du_lieu_Ung_dung.Cong_ty.Danh_sach_Rap;
            HttpContext.Current.Session["Nguoi_dung_Dang_nhap"] = Nguoi_dung;
        }

    }
    //============= Xử lý Chức năng của Người dùng đăng nhập ==============
    //Lưu ý Quan trọng : Tất cả thông tin xử lý phải dựa vào thông tin của chính Người dùng đăng nhập 
    public XL_NGUOI_DUNG Dang_nhap(string Ten_Dang_nhap, string Mat_khau)
    {
        var Nguoi_dung = Danh_sach_Nguoi_dung_Noi_bo.FirstOrDefault(
                                x => x.Ten_Dang_nhap == Ten_Dang_nhap
                                      && x.Mat_khau == Mat_khau
                                      );

        if (Nguoi_dung != null)
        {   //Khởi động  Thông tin Online  
            Nguoi_dung.Danh_sach_Phim_Xem = Du_lieu_Ung_dung.Danh_sach_Phim;
            Nguoi_dung.Danh_sach_Rap = Du_lieu_Ung_dung.Cong_ty.Danh_sach_Rap;
            HttpContext.Current.Session["Nguoi_dung_Dang_nhap"] = Nguoi_dung;
        }
        return Nguoi_dung;
    }

    public string Khoi_dong_Man_hinh_chinh()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 

        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Xem_Man_hinh_Chinh(Nguoi_dung_Dang_nhap);
        return Chuoi_HTML;
    }

    public string Xem_Danh_sach_Phim_dang_chieu()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        Nguoi_dung_Dang_nhap.Danh_sach_Phim_Xem = Du_lieu_Ung_dung.Danh_sach_Phim;
        var Chuoi_HTML = Tao_Chuoi_HTML_Xem_Man_hinh_Chinh(Nguoi_dung_Dang_nhap);
        return Chuoi_HTML;
    }

    public string Thanh_toan_Ve_dat()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        Nguoi_dung_Dang_nhap.Danh_sach_Phim_Xem = Du_lieu_Ung_dung.Danh_sach_Phim;
        var Chuoi_HTML = Tao_Chuoi_HTML_Thanh_toan_Ve_dat(Nguoi_dung_Dang_nhap.Danh_sach_Phim_Xem,Nguoi_dung_Dang_nhap);
        return Chuoi_HTML;
    }

    public string Tra_cuu(string Chuoi_Tra_cuu)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 
        Nguoi_dung_Dang_nhap.Danh_sach_Phim_Xem = Tra_cuu_Phim(Chuoi_Tra_cuu, Du_lieu_Ung_dung.Danh_sach_Phim);
        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Xem_Man_hinh_Chinh(Nguoi_dung_Dang_nhap);
        return Chuoi_HTML;
    }

    public string Chon_Phim(string Ma_so_Phim)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 
        var Phim = Du_lieu_Ung_dung.Danh_sach_Phim.FirstOrDefault(x => x.Ma_so == Ma_so_Phim);
        Nguoi_dung_Dang_nhap.Phim_chon = Phim;
        var Chuoi_HTML = "<iframe class='KHUNG_CHUC_NANG' src='MH_CHi_tiet_Phim.cshtml'  ></iframe>";
        return Chuoi_HTML;
    }
    public string Chon_Phim_Ve_dat(string Ma_so_Phim)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 
        var Phim = Du_lieu_Ung_dung.Danh_sach_Phim.FirstOrDefault(x => x.Ma_so == Ma_so_Phim);
        Nguoi_dung_Dang_nhap.Phim_chon = Phim;
        var Chuoi_HTML = "<iframe class='KHUNG_CHUC_NANG' src='MH_Thanh_toan_Ve_dat.cshtml'  ></iframe>";
        return Chuoi_HTML;
    }

    public string Tao_Chuoi_HTML_Xem_Man_hinh_Chinh(XL_NGUOI_DUNG Nguoi_dung_Dang_nhap)
    {
        var Chuoi_HTML = $"<div>" +
                $"{ Tao_Chuoi_HTML_Nguoi_dung_Dang_nhap(Nguoi_dung_Dang_nhap)}" +
                $"{ Tao_Chuoi_HTML_Danh_sach_Phim_Xem(Nguoi_dung_Dang_nhap.Danh_sach_Phim_Xem)}" +
            $"</div>";
        return Chuoi_HTML;
    }

    public string Khoi_dong_Man_hinh_Chi_tiet_Phim()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 

        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Chi_tiet_Phim(Nguoi_dung_Dang_nhap.Phim_chon, Nguoi_dung_Dang_nhap);
        return Chuoi_HTML;
    }
    public string Khoi_dong_Man_hinh_Lich_chieu_Phim()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 

        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Lich_chieu_Phim(Nguoi_dung_Dang_nhap.Phim_chon, Nguoi_dung_Dang_nhap);
        return Chuoi_HTML;
    }
    public string Tao_Chuoi_HTML_Lich_chieu_Phim(XL_PHIM Phim, XL_NGUOI_DUNG Nguoi_dung_Dang_nhap)
    {
        var Chuoi_HTML = $"<div>" +
            $"{Tao_Chuoi_Lich_chieu_phim(Phim)}" +
            $"</div>";
        return Chuoi_HTML;
    }
    public string Tao_Chuoi_HTML_Chi_tiet_Phim(XL_PHIM Phim, XL_NGUOI_DUNG Nguoi_dung_Dang_nhap)
    {
        var Chuoi_HTML = $"<div>" +
            $"{Tao_Chuoi_Chi_tiet_Phim(Phim)}" +
            $"</div>";
        return Chuoi_HTML;
    }

    public string Khoi_dong_Man_hinh_Chon_Ghe()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        //var Danh_sach_Ghe_Chon = Nguoi_dung_Dang_nhap.Dat_ve.Danh_sach_Ghe_dat;
        var Danh_sach_Ghe_Chon = Nguoi_dung_Dang_nhap.Ban_ve.Danh_sach_Ghe_ban;
        //var Chuoi_HTML = Tao_Chuoi_Danh_sach_Ghe(Danh_sach_Ghe_Chon);
        var Chuoi_HTML = Tao_chuoi_HTML_Man_hinh_Chon_Ghe(Danh_sach_Ghe_Chon);
        return Chuoi_HTML;
    }
    public string Khoi_dong_Man_hinh_Chon_Ghe_Ve_dat()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];

        var Danh_sach_Ghe_Chon = Nguoi_dung_Dang_nhap.Dat_ve.Danh_sach_Ghe_dat;

        var Chuoi_HTML = Tao_chuoi_HTML_Man_hinh_Chon_Ghe_Ve_dat(Danh_sach_Ghe_Chon);
        return Chuoi_HTML;
    }

    public string Chon_Ghe(XL_GHE Ghe_Chon)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var So_luong = Nguoi_dung_Dang_nhap.Ban_ve.So_luong;
        var Danh_sach_Ghe_Chon = Nguoi_dung_Dang_nhap.Ban_ve.Danh_sach_Ghe_ban;
        var Da_chon = Danh_sach_Ghe_Chon.Any(x => x.Ma_so == Ghe_Chon.Ma_so);
        if (Da_chon)
        {
            Danh_sach_Ghe_Chon.Remove(Danh_sach_Ghe_Chon.FirstOrDefault(x => x.Ma_so == Ghe_Chon.Ma_so));
        }
        else if (Danh_sach_Ghe_Chon.Count == So_luong)
        {
            Danh_sach_Ghe_Chon.RemoveAt(0);
            Danh_sach_Ghe_Chon.Add(Ghe_Chon);
        }
        else
        {
            Danh_sach_Ghe_Chon.Add(Ghe_Chon);
        }
        var Chuoi_HTML = Tao_chuoi_HTML_Man_hinh_Chon_Ghe(Danh_sach_Ghe_Chon);
        return Chuoi_HTML;

    }
    public string Chon_Ghe_Ve_dat(XL_GHE Ghe_Chon)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Ve_dat = Tim_Ve_dat(Ghe_Chon, Nguoi_dung_Dang_nhap.Phim_chon, Nguoi_dung_Dang_nhap.Ban_ve.Suat_chieu);
        
        Nguoi_dung_Dang_nhap.Ban_ve.So_luong = Ve_dat.Danh_sach_Ghe_dat.Count();
        Nguoi_dung_Dang_nhap.Ban_ve.Tien = Ve_dat.Danh_sach_Ghe_dat.Count() * Nguoi_dung_Dang_nhap.Phim_chon.Don_gia;
        Nguoi_dung_Dang_nhap.Dat_ve = Ve_dat;
        
        var Chuoi_HTML = Tao_chuoi_HTML_Man_hinh_Chon_Ghe_Ve_dat(Ve_dat.Danh_sach_Ghe_dat);
        return Chuoi_HTML;        

    }

    public string Tao_Chuoi_Danh_sach_Ghe(List<XL_GHE> Danh_sach_Ghe_Chon)
    {
        var Chuoi_HTML = "";
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Rap = Nguoi_dung_Dang_nhap.Danh_sach_Rap.FirstOrDefault(x => x.Ma_so == Nguoi_dung_Dang_nhap.Ban_ve.Suat_chieu.Rap.Ma_so);
        var Phong_chieu = Rap.Danh_sach_Phong_chieu.Find(x => x.Ma_so == Nguoi_dung_Dang_nhap.Ban_ve.Suat_chieu.Phong_chieu.Ma_so);
        var Danh_sach_Ghe = Phong_chieu.Danh_sach_Ghe;
        var Danh_sach_Ghe_trong = Nguoi_dung_Dang_nhap.Ban_ve.Suat_chieu.Danh_sach_Ghe_trong;
        var Danh_sach_Ban_ve = Nguoi_dung_Dang_nhap.Phim_chon.Danh_sach_Ban_ve;
        var Danh_sach_Ban_ve_cua_Phong_chieu = Danh_sach_Ban_ve.FindAll(Ve_ban => Ve_ban.Suat_chieu.Phong_chieu.Ma_so == Phong_chieu.Ma_so);
        var Danh_sach_Ghe_ban = new List<XL_GHE>();
        Danh_sach_Ban_ve_cua_Phong_chieu.ForEach(Ve_ban => Ve_ban.Danh_sach_Ghe_ban.ForEach(Ghe_ban => Danh_sach_Ghe_ban.Add(Ghe_ban)));
        var Count = 0;
        Danh_sach_Ghe.ForEach(Ghe =>
        {
            Count++;
            var Tinh_trang_Ghe = 0;
            var Chuoi_Xu_ly_Click = "Th_Ma_so_Ghe.value='XXX';CHON_GHE.submit() ";
            Chuoi_Xu_ly_Click = Chuoi_Xu_ly_Click.Replace("XXX", Ghe.Ma_so);
            var Da_chon = Danh_sach_Ghe_Chon.Any(x => x.Ma_so == Ghe.Ma_so);
            var Chua_dat = Danh_sach_Ghe_trong.Any(x => x.Ma_so == Ghe.Ma_so);
            var Da_ban = Danh_sach_Ghe_ban.Any(x => x.Ma_so == Ghe.Ma_so);

            if (Da_chon)
            {
                Tinh_trang_Ghe = 1;
            }
            if (!Chua_dat)
            {
                Tinh_trang_Ghe = -1;
            }
            if (Da_ban)
            {
                Tinh_trang_Ghe = 2;
            }
            Chuoi_HTML += Tao_chuoi_HTML_Ghe(Ghe, Chuoi_Xu_ly_Click, Tinh_trang_Ghe, Count);

        });

        return Chuoi_HTML;
    }
    public string Tao_Chuoi_Danh_sach_Ghe_Ve_dat(List<XL_GHE> Danh_sach_Ghe_Chon)
    {
        var Chuoi_HTML = "";
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Rap = Nguoi_dung_Dang_nhap.Danh_sach_Rap.FirstOrDefault(x => x.Ma_so == Nguoi_dung_Dang_nhap.Ban_ve.Suat_chieu.Rap.Ma_so);
        var Phong_chieu = Rap.Danh_sach_Phong_chieu.Find(x => x.Ma_so == Nguoi_dung_Dang_nhap.Ban_ve.Suat_chieu.Phong_chieu.Ma_so);
        var Danh_sach_Ghe = Phong_chieu.Danh_sach_Ghe;
        var Danh_sach_Ghe_trong = Nguoi_dung_Dang_nhap.Ban_ve.Suat_chieu.Danh_sach_Ghe_trong;
        var Danh_sach_Ban_ve = Nguoi_dung_Dang_nhap.Phim_chon.Danh_sach_Ban_ve;
        var Danh_sach_Ban_ve_cua_Phong_chieu = Danh_sach_Ban_ve.FindAll(Ve_ban => Ve_ban.Suat_chieu.Phong_chieu.Ma_so == Phong_chieu.Ma_so);
        var Danh_sach_Ghe_ban = new List<XL_GHE>();
        Danh_sach_Ban_ve_cua_Phong_chieu.ForEach(Ve_ban => Ve_ban.Danh_sach_Ghe_ban.ForEach(Ghe_ban => Danh_sach_Ghe_ban.Add(Ghe_ban)));
        var Count = 0;
        Danh_sach_Ghe.ForEach(Ghe =>
        {
            Count++;
            var Tinh_trang_Ghe = 0;
            var Chuoi_Xu_ly_Click = "Th_Ma_so_Ghe.value='XXX';CHON_GHE_VE_DAT.submit() ";
            Chuoi_Xu_ly_Click = Chuoi_Xu_ly_Click.Replace("XXX", Ghe.Ma_so);
            var Da_chon = Danh_sach_Ghe_Chon.Any(x => x.Ma_so == Ghe.Ma_so);
            var Chua_dat = Danh_sach_Ghe_trong.Any(x => x.Ma_so == Ghe.Ma_so);
            var Da_ban = Danh_sach_Ghe_ban.Any(x => x.Ma_so == Ghe.Ma_so);

            
            if (!Chua_dat)
            {
                Tinh_trang_Ghe = -1;
            }
            if (Da_chon)
            {
                Tinh_trang_Ghe = 1;
            }
            if (Da_ban)
            {
                Tinh_trang_Ghe = 2;
            }
            Chuoi_HTML += Tao_chuoi_HTML_Ghe_Ve_dat(Ghe, Chuoi_Xu_ly_Click, Tinh_trang_Ghe, Count);

        });

        return Chuoi_HTML;
    }
    
    public string Tao_chuoi_HTML_Man_hinh_Chon_Ghe(List<XL_GHE> Danh_sach_Ghe_Chon)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Chuoi_HTML =
                $"<div class='row'><div class='col-md-9'>" +
                $" <form id='CHON_GHE' action='MH_Chon_Ghe.cshtml' method='post'>" +
                $"<input name='Th_Ma_so_Ghe' id='Th_Ma_so_Ghe' type='hidden' />" +
                $"<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_GHE' />" +
                $"<div class='ticket-wrapper'>" +
                            $"<div class='booking-bg'><div class='row'><div class='col-md-12'>" +
                                        $"<section class='booking-ticket'><h2 class='booking-title'>Chọn Ghế: &nbsp;</h2>" +
                                            $"<div class='seat-map-wrapper'><div class='col-md-12'><div class='seat-map ng-scope'>" +
                                                        $"{Tao_Chuoi_Danh_sach_Ghe(Danh_sach_Ghe_Chon)}" +
                                                    $"</div><div class='screen'>Màn Hình</div>" +
                                                    $"<div class='seat-cinema'>" +
                                                        $"<span class='seat-cinema-selected'>Ghế đang chọn</span>" +
                                                        $"<span class='seat-cinema-unavailable'>Ghế đã bán</span>" +
                                                        $"<span class='seat-cinema-ghedat'>Ghế đã đặt</span>" +
                                                        $"<span class='seat-cinema-normal'>Có thể chọn</span>" +
                                                    $"</div></div></div></section></div></div></div>" +

                        $"</div>" +
                    $"</div></form>" +

                    $"<div class='col-md-3'><div class='ticket-header'><section class='ticket-feature'>" +
                                $"<article class='row'><div class='col-md-12' style='text-align:center'></div>" +
                                    $"{Tao_Bang_Thong_tin_Ve(Nguoi_dung_Dang_nhap)}" +
                                $"</article></section></div></div></div>";
        return Chuoi_HTML;
    }
    public string Tao_chuoi_HTML_Man_hinh_Chon_Ghe_Ve_dat(List<XL_GHE> Danh_sach_Ghe_Chon)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Chuoi_HTML =
                $"<div class='row'><div class='col-md-9'>" +
                $" <form id='CHON_GHE_VE_DAT' action='MH_Chon_Ghe_Thanh_toan_Ve_dat.cshtml' method='post'>" +
                $"<input name='Th_Ma_so_Ghe' id='Th_Ma_so_Ghe' type='hidden' />" +
                $"<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_GHE_VE_DAT' />" +
                $"<div class='ticket-wrapper'>" +
                            $"<div class='booking-bg'><div class='row'><div class='col-md-12'>" +
                                        $"<section class='booking-ticket'><h2 class='booking-title'>Chọn Ghế: &nbsp;</h2>" +
                                            $"<div class='seat-map-wrapper'><div class='col-md-12'><div class='seat-map ng-scope'>" +
                                                        $"{Tao_Chuoi_Danh_sach_Ghe_Ve_dat(Danh_sach_Ghe_Chon)}" +
                                                    $"</div><div class='screen'>Màn Hình</div>" +
                                                    $"<div class='seat-cinema'>" +
                                                        $"<span class='seat-cinema-selected'>Ghế đang chọn</span>" +
                                                        $"<span class='seat-cinema-unavailable'>Ghế đã bán</span>" +
                                                        $"<span class='seat-cinema-normal'>Ghế trống</span>" +
                                                        $"<span class='seat-cinema-ghedat'>Ghế đặt chọn để thanh toán</span>" +
                                                    $"</div></div></div></section></div></div></div>" +

                        $"</div>" +
                    $"</div></form>" +

                    $"<div class='col-md-3'><div class='ticket-header'><section class='ticket-feature'>" +
                                $"<article class='row'><div class='col-md-12' style='text-align:center'></div>" +
                                    $"{Tao_Bang_Thong_tin_Ve_da_dat(Nguoi_dung_Dang_nhap, Danh_sach_Ghe_Chon)}" +
                                $"</article></section></div></div></div>";
        return Chuoi_HTML;
    }

    public string Tao_chuoi_HTML_Man_hinh_Thong_tin_Ca_nhan()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Chuoi_HTML = $"<div class='row'><div class='col-md-9'>" +
            $"{Tao_chuoi_Nhap_Thong_tin_Ca_nhan()}" +
            $"</div>" +
            $"<div class='col-md-3'><div class='ticket-header'><section class='ticket-feature'>" +
                                $"<article class='row'><div class='col-md-12' style='text-align:center'></div>" +
                                    $"{Tao_Bang_Thong_tin_Ve(Nguoi_dung_Dang_nhap)}" +
                                $"</article></section></div></div></div>" +
            $"<div class='col-md-9 text-center'>" +
            $"<form id='QUAY_LAI' method='post'>" +
                $"<input id='Th_Ma_so_Chuc_nang' name='Th_Ma_so_Chuc_nang' value='QUAY_LAI' type='hidden' />" +
            $"</form>" +
            $"<button class='btn btn-primary' type='button' style='border-radius:0;background-color:#f26b38;border:none;' onclick='QUAY_LAI.submit()'>Quay lại</button>" +
            $"\r<button class='btn btn-primary' type='button' style='border-radius:0;background-color:#f26b38;border:none;' onclick='BAN_VE.submit()'>Bán Vé</button>" +
            $"</div>";
        return Chuoi_HTML;
    }
    public string Tao_chuoi_HTML_Man_hinh_Thong_tin_Ca_nhan_Ve_dat()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Chuoi_HTML = $"<div class='row'><div class='col-md-9'>" +
            $"{Tao_chuoi_Thong_tin_Ca_nhan()}" +
            $"</div>" +
            $"<div class='col-md-3'><div class='ticket-header'><section class='ticket-feature'>" +
                                $"<article class='row'><div class='col-md-12' style='text-align:center'></div>" +
                                    $"{Tao_Bang_Thong_tin_Ve(Nguoi_dung_Dang_nhap)}" +
                                $"</article></section></div></div></div>" +
            $"<div class='col-md-9 text-center'>" +
            $"<form id='QUAY_LAI' method='post'>" +
                $"<input id='Th_Ma_so_Chuc_nang' name='Th_Ma_so_Chuc_nang' value='QUAY_LAI' type='hidden' />" +
            $"</form>" +
            $"<button class='btn btn-primary' type='button' style='border-radius:0;background-color:#f26b38;border:none;' onclick='QUAY_LAI.submit()'>Quay lại</button>" +
            $"\r<button class='btn btn-primary' type='button' style='border-radius:0;background-color:#f26b38;border:none;' onclick='XAC_NHAN_THANH_TOAN_VE_DAT.submit()'>Thanh toán</button>" +
            $"</div>";
        return Chuoi_HTML;
    }
    public string Tao_chuoi_Thong_tin_Ca_nhan()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Chuoi_HTML = $"<h3>Thông Tin Cá Nhân</h3>" +
            $"<form id='XAC_NHAN_THANH_TOAN_VE_DAT' method='post'>" +
            $"<input name='Th_Ma_so_Chuc_nang' type='hidden' value='XAC_NHAN_THANH_TOAN_VE_DAT'/>" +
            $"<div class='form-group row'>" +
            $"<label for='Th_Ho_ten' class='col-2 col-form-label'>Họ Tên</label>" +
            $"<div class='col-10'>" +
            $"<input class='form-control' type='text' id='Th_Ho_ten' name='Th_Ho_ten' value='{Nguoi_dung_Dang_nhap.Dat_ve.Nguoi_dung_Khach_tham_quan.Ho_ten}'>" +
            $"</div></div>" +
            $"<div class='form-group row'>" +
            $"<label for='Th_Email' class='col-2 col-form-label'>Email</label>" +
            $"<div class='col-10'>" +
            $"<input class='form-control' type='text' id='Th_Email' name='Th_Email' value='{Nguoi_dung_Dang_nhap.Dat_ve.Nguoi_dung_Khach_tham_quan.Email}'>" +
            $"</div></div>" +
            $"<div class='form-group row'>" +
            $"<label for='Th_So_Dien_thoai' class='col-2 col-form-label'>Số điện thoại</label>" +
            $"<div class='col-10'>" +
            $"<input class='form-control' type='text' id='Th_So_Dien_thoai' name='Th_So_Dien_thoai' value='{Nguoi_dung_Dang_nhap.Dat_ve.Nguoi_dung_Khach_tham_quan.Dien_thoai}'>" +
            $"</div></div>" +
            $"<div class='form-group row'>" +
            $"<label for='Th_Ma_nhan_ve' class='col-2 col-form-label'>Mã Nhận Vé</label>" +
            $"<div class='col-10'>" +
            $"<input class='form-control' type='text' id='Th_Ma_nhan_ve' name='Th_Ma_nhan_ve'>" +
            $"</div></div>" +
            $"</form>";
        return Chuoi_HTML;
    }
    public string Tao_chuoi_Nhap_Thong_tin_Ca_nhan()
    {
        var Chuoi_HTML = $"<h3>Thông Tin Cá Nhân</h3>" +
            $"<form id='BAN_VE' method='post'>" +
            $"<input name='Th_Ma_so_Chuc_nang' type='hidden' value='BAN_VE'/>" +
            $"<div class='form-group row'>" +
            $"<label for='Th_Ho_ten' class='col-2 col-form-label'>Họ Tên</label>" +
            $"<div class='col-10'>" +
            $"<input class='form-control' type='text' id='Th_Ho_ten' name='Th_Ho_ten'>" +
            $"</div></div>" +
            $"<div class='form-group row'>" +
            $"<label for='Th_Email' class='col-2 col-form-label'>Email</label>" +
            $"<div class='col-10'>" +
            $"<input class='form-control' type='text' id='Th_Email' name='Th_Email'>" +
            $"</div></div>" +
            $"<div class='form-group row'>" +
            $"<label for='Th_So_Dien_thoai' class='col-2 col-form-label'>Số điện thoại</label>" +
            $"<div class='col-10'>" +
            $"<input class='form-control' type='text' id='Th_So_Dien_thoai' name='Th_So_Dien_thoai'>" +
            $"</div></div>" +
            $"<div class='form-group row'>" +
            $"<label for='Th_Ma_nhan_ve' class='col-2 col-form-label'>Mã Nhận Vé</label>" +
            $"<div class='col-10'>" +
            $"<input class='form-control' type='text' id='Th_Ma_nhan_ve' name='Th_Ma_nhan_ve'>" +
            $"</div></div>" +
            $"</form>";
        return Chuoi_HTML;
    }

    public string Ban_ve()
    {
        var Chuoi_HTML = "";
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        string Kq = XL_DU_LIEU.Ghi_Ban_ve_Moi(Nguoi_dung_Dang_nhap.Phim_chon, Nguoi_dung_Dang_nhap.Ban_ve);
        if (Kq == "OK")
        {
            Chuoi_HTML += $"<div class='alert alert-success'>Bạn đã bán vé thành công</div>";
        }
        else
        {
            Chuoi_HTML += $"<div class='alert alert-warning'>Đã có lỗi xảy ra, vui lòng nhập lại thông tin</div>";
        }
        return Chuoi_HTML;

    }
    public string Xac_nhan_Thanh_toan_Ve_dat()
    {
        var Chuoi_HTML = "";
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        string Kq = XL_DU_LIEU.Ghi_Ban_ve_Moi(Nguoi_dung_Dang_nhap.Phim_chon, Nguoi_dung_Dang_nhap.Ban_ve);
        string Doi_trang_thai = XL_DU_LIEU.Ghi_Xac_nhan_Thanh_toan_Ve_dat(Nguoi_dung_Dang_nhap.Phim_chon, Nguoi_dung_Dang_nhap.Dat_ve);
        if (Kq == "OK" && Doi_trang_thai == "OK")
        {
            Chuoi_HTML += $"<div class='alert alert-success'>Bạn đã thanh toán vé thành công</div>";
        }
        else
        {
            Chuoi_HTML += $"<div class='alert alert-warning'>Đã có lỗi xảy ra, vui lòng nhập lại thông tin</div>";
        }
        return Chuoi_HTML;

    }
    public XL_DAT_VE Tim_Ve_dat(XL_GHE Ghe_dat, XL_PHIM Phim_chon, XL_SUAT_CHIEU Suat_chieu)
    {
        var Ve_dat = new XL_DAT_VE();
        var Danh_sach_Dat_ve = Phim_chon.Danh_sach_Dat_ve;
        var Danh_sach_Dat_ve_cua_Suat_chieu = Danh_sach_Dat_ve.FindAll(x => x.Suat_chieu.Ma_so == Suat_chieu.Ma_so);
        Ve_dat = Danh_sach_Dat_ve_cua_Suat_chieu.FirstOrDefault(x => x.Danh_sach_Ghe_dat.Any(Ghe => Ghe.Ma_so == Ghe_dat.Ma_so));
        return Ve_dat;
    }
}

//************************* View-Layers/Prsenetaition VL/PL **********************************
public partial class XL_UNG_DUNG
{
    public string Dia_chi_Media = $"{XL_DU_LIEU.Dia_chi_Dich_vu}/Media";
    public CultureInfo Dinh_dang_VN = CultureInfo.GetCultureInfo("vi-VN");

    public string Tao_Chuoi_HTML_Nguoi_dung_Dang_nhap(XL_NGUOI_DUNG Nguoi_dung)
    {
        var Chuoi_HTML_Nguoi_dung_Dang_nhap = "";
        var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{Nguoi_dung.Ma_so}.png' style = 'width: 50px; height: 50px; margin-left:10%'/>";

        var Chuoi_Thong_tin = $"<span style='color:red'>" +
                                  $"Xin chào {Nguoi_dung.Ho_ten}" +
                              $"</span><hr>";
        return Chuoi_HTML_Nguoi_dung_Dang_nhap = Chuoi_Hinh + Chuoi_Thong_tin;
    }

    public string Tao_Chuoi_HTML_Danh_sach_Phim_Xem(List<XL_PHIM> Danh_sach)
    {
        var Chuoi_HTML_Danh_sach = "<div class='container'><form id='MH_CHINH' name='MH_CHINH' method='post'>";
        var Chuoi_Input = "<input name='Th_Ma_so_Phim' id='Th_Ma_so_Phim' type='hidden' />" +
            "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_PHIM'/>"
            ;
        Chuoi_HTML_Danh_sach += Chuoi_Input;
        Chuoi_HTML_Danh_sach += "<div class='row'>";
        Danh_sach.ForEach(Phim =>
        {
            var Chuoi_Xu_ly_Click = $"Th_Ma_so_Phim.value='{Phim.Ma_so}';MH_CHINH.submit() ";
            var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{Phim.Ma_so}.jpg' style='width: 50px; height:80px'/>";

            var Chuoi_Thong_tin = $"<div class='card-block THONG_TIN'>" +
                                      $"<h6 class='text-center'>{Phim.Ten}</h6>" +
                                  $"</div>";

            var Chuoi_HTML = $"<div class='KHUNG col-6 col-sm-6 col-md-4 col-lg-3' onclick=\"" + $"{Chuoi_Xu_ly_Click}" + "\">" +
                                 $"<div class='card'>" +
                                     $"{Chuoi_Hinh}" +
                                     $"<div class='OVERLAY'><div class='OVERLAY_TEXT'>Bán vé</div></div>" +


                                     $"{Chuoi_Thong_tin}" +
                                 $"</div>" +
                             "</div>";

            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        });

        Chuoi_HTML_Danh_sach += "</div></form></div>";

        return Chuoi_HTML_Danh_sach;
    }

    public string Tao_Chuoi_HTML_Thanh_toan_Ve_dat(List<XL_PHIM> Danh_sach, XL_NGUOI_DUNG Nguoi_dung)
    {
        var Chuoi_HTML_Danh_sach = "<div class='container'><form id='MH_CHINH' name='MH_CHINH' method='post'>";
        var Chuoi_Input = "<input name='Th_Ma_so_Phim' id='Th_Ma_so_Phim' type='hidden' />" +
            "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_PHIM_VE_DAT'/>"
            ;
        Chuoi_HTML_Danh_sach += Chuoi_Input;
        Chuoi_HTML_Danh_sach += "<div class='row'>";
        Danh_sach.ForEach(Phim =>
        {
            var Chuoi_Xu_ly_Click = $"Th_Ma_so_Phim.value='{Phim.Ma_so}';MH_CHINH.submit() ";
            var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{Phim.Ma_so}.jpg' style='width: 50px; height:80px'/>";
            var Chuoi_Thong_tin = $"<div class='card-block THONG_TIN'>" +
                                      $"<h6 class='text-center'>{Phim.Ten}</h6>" +
                                  $"</div>";
            var Chuoi_HTML = $"<div class='KHUNG col-6 col-sm-6 col-md-4 col-lg-3' onclick=\"" + $"{Chuoi_Xu_ly_Click}" + "\">" +
                                 $"<div class='card'>" +
                                     $"{Chuoi_Hinh}" +
                                     $"<div class='OVERLAY'><div class='OVERLAY_TEXT'>Thanh toán</div></div>" +


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
        var Ngay_Khoi_chieu = Phim.Khoi_chieu;
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
    public string Tao_Chuoi_Lich_chieu_phim(XL_PHIM Phim)
    {
        var Chuoi_HTML = "";

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
                {
                    var Chuoi_Xu_ly_Click = $"Th_Ma_so_Suat_chieu.value='{Suat_chieu.Ma_so}';MH_CHI_TIET_PHIM.submit() ";
                    Chuoi_Danh_sach_Suat_chieu += $"<li onclick=\"" + $"{Chuoi_Xu_ly_Click}" + "\">" + $"{Suat_chieu.Bat_dau.ToString("HH:mm")}" + "</li>";
                }
            }
            Chuoi_Danh_sach_Suat_chieu += "</ul></div>";
            Chuoi_Html += Chuoi_Danh_sach_Suat_chieu;
        }
        Chuoi_Html += "</form>";
        return Chuoi_Html;
    }

    public string Tao_chuoi_HTML_Ghe(XL_GHE Ghe, string Chuoi_Xu_ly_Click = "", int Tinh_trang_Ghe = 0, int Count = 0)
    {
        string Hang_ghe = Ghe.Ma_so[0].ToString();
        string So_ghe = Ghe.Ma_so[1].ToString();        
        var Chuoi_HTML = "";
        if (Count % 9 == 1)
        {
            Chuoi_HTML += "<ul style='list-style-type: none;margin:5px;cursor: pointer;'>";
            Chuoi_HTML += $"<li style='display:inline-block;text-align:center;width: 20px;height: 20px;border: solid 1px;'>{Hang_ghe}</li><li style='display:inline-block;'><ul style='list-style-type: none;padding: 0px;'>";
        }

        if (Tinh_trang_Ghe == -1) //Đã đặt hàng
        {
            Chuoi_HTML += $"<li style='background-color:orange;display:inline-block;text-align:center;width: 20px;height: 20px;margin-left:2px;'>{So_ghe}</li>";
        }
        else if (Tinh_trang_Ghe == 1) //Đang chọn
        {
            Chuoi_HTML += $"<li onclick=\"" + $"{Chuoi_Xu_ly_Click}" + $"\" style='display:inline-block;background-color: green;text-align:center;width: 20px;height: 20px;margin-left:2px;'>{So_ghe}</li>";
        }
        else if (Tinh_trang_Ghe == 2) //Đã bán
        {
            Chuoi_HTML += $"<li style='background-color:red;display:inline-block;text-align:center;width: 20px;height: 20px;margin-left:2px;'>{So_ghe}</li>";
        }
        else // Ghế trống
        {
            Chuoi_HTML += $"<li onclick=\"" + $"{Chuoi_Xu_ly_Click}" + $"\" style ='display:inline-block;background-color: #dbdee1;text-align:center;width: 20px;height: 20px;margin-left:2px;'>{So_ghe}</li>";
        }
        if (Count % 9 == 0)
            Chuoi_HTML += $"</ul></li><li style='display:inline-block;text-align:center;width: 20px;height: 20px;border: solid 1px;margin-left:2px;'>{Hang_ghe}</li></ul>";
        return Chuoi_HTML;
    }

    public string Tao_chuoi_HTML_Ghe_Ve_dat(XL_GHE Ghe, string Chuoi_Xu_ly_Click = "", int Tinh_trang_Ghe = 0, int Count = 0)
    {
        string Hang_ghe = Ghe.Ma_so[0].ToString();
        string So_ghe = Ghe.Ma_so[1].ToString();       
        var Chuoi_HTML = "";
        if (Count % 9 == 1)
        {
            Chuoi_HTML += "<ul style='list-style-type: none;margin:5px;cursor: pointer;'>";
            Chuoi_HTML += $"<li style='display:inline-block;text-align:center;width: 20px;height: 20px;border: solid 1px;'>{Hang_ghe}</li><li style='display:inline-block;'><ul style='list-style-type: none;padding: 0px;'>";
        }

        if (Tinh_trang_Ghe == -1) // Đã đặt hàng
        {
            Chuoi_HTML += $"<li onclick=\"" + $"{Chuoi_Xu_ly_Click}" + $"\" style='background-color:orange;display:inline-block;text-align:center;width: 20px;height: 20px;margin-left:2px;'>{So_ghe}</li>";
        }
        else if (Tinh_trang_Ghe == 1) // Đang chọn
        {
            Chuoi_HTML += $"<li onclick=\"" + $"{Chuoi_Xu_ly_Click}" + $"\" style='display:inline-block;background-color: green;text-align:center;width: 20px;height: 20px;margin-left:2px;'>{So_ghe}</li>";
        }
        else if (Tinh_trang_Ghe == 2) // Đã bán
        {
            Chuoi_HTML += $"<li style='background-color:red;display:inline-block;text-align:center;width: 20px;height: 20px;margin-left:2px;'>{So_ghe}</li>";
        }
        else
        {
            Chuoi_HTML += $"<li style ='display:inline-block;background-color: #dbdee1;text-align:center;width: 20px;height: 20px;margin-left:2px;'>{So_ghe}</li>";
        }
        if (Count % 9 == 0)
            Chuoi_HTML += $"</ul></li><li style='display:inline-block;text-align:center;width: 20px;height: 20px;border: solid 1px;margin-left:2px;'>{Hang_ghe}</li></ul>";
        return Chuoi_HTML;
        
    }


    public string Tao_Bang_Thong_tin_Ve(XL_NGUOI_DUNG Nguoi_dung)
    {
        var Chuoi_HTML = $"<div class='col-md-12'><div class='ticket-detail'>";
        var Ten_Phim = Nguoi_dung.Phim_chon.Ten;
        var Ten_Phim_Tieng_Anh = Nguoi_dung.Phim_chon.Ten_tieng_Anh;
        var Rap = Nguoi_dung.Ban_ve.Suat_chieu.Rap.Ten;
        var Phong_chieu = Nguoi_dung.Ban_ve.Suat_chieu.Phong_chieu.Ten;
        var Thoi_gian = Nguoi_dung.Ban_ve.Suat_chieu.Bat_dau.ToString("dddd, dd/MM/yyyy hh:mm", Dinh_dang_VN);
        var Danh_sach_Ghe_ban = Nguoi_dung.Ban_ve.Danh_sach_Ghe_ban;
        var So_ve_Da_ban = Nguoi_dung.Ban_ve.Danh_sach_Ghe_ban.Count();
        var So_luong = Nguoi_dung.Ban_ve.So_luong.ToString();
        var Tien = Nguoi_dung.Ban_ve.Tien.ToString("n0", Dinh_dang_VN);

        var Chuoi_Thong_tin = $"<h2 class='ticket-title'>{Ten_Phim}</h2>" +
            $"<h2 class='ticket-title en'>{Ten_Phim_Tieng_Anh}</h2>" +
            $"<div class='ticket-info'>" +
            $"<p><b>Rạp: &nbsp;</b>{Rap} | {Phong_chieu}</p>" +
            $"<p><b>Suất chiếu: &nbsp;</b>{Thoi_gian}</p>" +
            $"<p><b>Số lượng vé đặt: &nbsp;</b>{So_luong}</p>" +
            $"<p><b>Ghế: &nbsp;</b>";
        for (int i = 0; i < So_ve_Da_ban; i++)
        {
            if (i == So_ve_Da_ban - 1)
                Chuoi_Thong_tin += Danh_sach_Ghe_ban[i].Ma_so;
            else
                Chuoi_Thong_tin += Danh_sach_Ghe_ban[i].Ma_so + ", ";
        }
        Chuoi_Thong_tin += $"</p>" +
            $"</div>" +
            $"<div class='ticket-price-total'>" +
            $"<p><b>Tổng tiền: &nbsp;</b><span>{Tien}</span></p>" + $"</div>";
        Chuoi_HTML += Chuoi_Thong_tin + $"</div></div>";
        return Chuoi_HTML;
    }

    public string Tao_Bang_Thong_tin_Ve_da_dat(XL_NGUOI_DUNG Nguoi_dung, List<XL_GHE> Danh_sach_Ghe_chon)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Chuoi_HTML = $"<div class='col-md-12'><div class='ticket-detail'>";
        var Ten_Phim = Nguoi_dung.Phim_chon.Ten;
        var Ten_Phim_Tieng_Anh = Nguoi_dung.Phim_chon.Ten_tieng_Anh;
        var Rap = Nguoi_dung.Ban_ve.Suat_chieu.Rap.Ten;
        var Phong_chieu = Nguoi_dung.Ban_ve.Suat_chieu.Phong_chieu.Ten;
        var Thoi_gian = Nguoi_dung.Ban_ve.Suat_chieu.Bat_dau.ToString("dddd, dd/MM/yyyy hh:mm", Dinh_dang_VN);
        var Danh_sach_Ghe_ban = Danh_sach_Ghe_chon;
        var So_ve_Da_ban = Danh_sach_Ghe_ban.Count();
        var So_luong = Nguoi_dung.Ban_ve.So_luong.ToString();
        var Tien = Nguoi_dung.Ban_ve.Tien.ToString("n0", Dinh_dang_VN);
        Nguoi_dung_Dang_nhap.Ban_ve.Danh_sach_Ghe_ban = Danh_sach_Ghe_chon;
        var Chuoi_Thong_tin = $"<h2 class='ticket-title'>{Ten_Phim}</h2>" +
            $"<h2 class='ticket-title en'>{Ten_Phim_Tieng_Anh}</h2>" +
            $"<div class='ticket-info'>" +
            $"<p><b>Rạp: &nbsp;</b>{Rap} | {Phong_chieu}</p>" +
            $"<p><b>Suất chiếu: &nbsp;</b>{Thoi_gian}</p>" +
            $"<p><b>Số lượng vé đặt: &nbsp;</b>{So_luong}</p>" +
            $"<p><b>Ghế: &nbsp;</b>";
        for (int i = 0; i < So_ve_Da_ban; i++)
        {
            if (i == So_ve_Da_ban - 1)
                Chuoi_Thong_tin += Danh_sach_Ghe_ban[i].Ma_so;
            else
                Chuoi_Thong_tin += Danh_sach_Ghe_ban[i].Ma_so + ", ";
        }
        Chuoi_Thong_tin += $"</p>" +
            $"</div>" +
            $"<div class='ticket-price-total'>" +
            $"<p><b>Tổng tiền: &nbsp;</b><span>{Tien}</span></p>" + $"</div>";
        Chuoi_HTML += Chuoi_Thong_tin + $"</div></div>";
        return Chuoi_HTML;
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
        var Phim = Danh_sach.FirstOrDefault(P =>
        P.Ma_so.Contains(Ma_so));
        return Phim;
    }

    public XL_SUAT_CHIEU Tim_Suat_chieu(string Ma_so, XL_PHIM Phim)
    {
        var Suat_chieu = Phim.Danh_sach_Suat_chieu.FirstOrDefault(S => String.Equals(Ma_so, S.Ma_so));
        return Suat_chieu;
    }

}

//************************* Data-Layers DL **********************************
public partial class XL_DU_LIEU
{
    public static string Dia_chi_Dich_vu = "http://localhost:59900";

    public static string Dia_chi_Dich_vu_Quan_ly_Rap_Phim = $"{Dia_chi_Dich_vu}/1-Dich_vu_Giao_tiep/DV_Nhan_vien_Ban_ve.cshtml";

    public static XL_DU_LIEU Doc_Du_lieu()
    {
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;

        var Tham_so = "Ma_so_Xu_ly=KHOI_DONG_DU_LIEU_PHAN_HE_NHAN_VIEN";
        var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Quan_ly_Rap_Phim}?{Tham_so}";
        var Chuoi_JSON = Xu_ly.DownloadString(Dia_chi_Xu_ly);
        var Du_lieu = Json.Decode<XL_DU_LIEU>(Chuoi_JSON);

        return Du_lieu;
    }

    public static string Ghi_Ban_ve_Moi(XL_PHIM Phim, XL_BAN_VE Ban_ve)
    {
        var Kq = "";
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;
        var Tham_so = $"Ma_so_Xu_ly=GHI_BAN_VE_MOI&Ma_so_Phim={Phim.Ma_so}";
        var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Quan_ly_Rap_Phim}?{Tham_so}";
        var Chuoi_JSON = Json.Encode(Ban_ve);
        try
        {
            Kq = Xu_ly.UploadString(Dia_chi_Xu_ly, Chuoi_JSON).Trim();
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        if (Kq == "OK")
        {
            var Suat_chieu = Phim.Danh_sach_Suat_chieu.FirstOrDefault(x => x.Ma_so == Ban_ve.Suat_chieu.Ma_so);
            Suat_chieu.Danh_sach_Ghe_trong.RemoveAll(Ghe_trong => Ban_ve.Danh_sach_Ghe_ban.Any(Ghe_ban => Ghe_ban.Ma_so == Ghe_trong.Ma_so));
        }
        return Kq;

    }
    public static string Ghi_Xac_nhan_Thanh_toan_Ve_dat(XL_PHIM Phim, XL_DAT_VE Ve_dat)
    {
        var Kq = "";
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;
        var Tham_so = $"Ma_so_Xu_ly=GHI_XAC_NHAN_THANH_TOAN_VE_DAT&Ma_so_Phim={Phim.Ma_so}";
        var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Quan_ly_Rap_Phim}?{Tham_so}";
        var Chuoi_JSON = Json.Encode(Ve_dat);
        try
        {
            Kq = Xu_ly.UploadString(Dia_chi_Xu_ly, Chuoi_JSON).Trim();
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }        
        return Kq;

    }
}