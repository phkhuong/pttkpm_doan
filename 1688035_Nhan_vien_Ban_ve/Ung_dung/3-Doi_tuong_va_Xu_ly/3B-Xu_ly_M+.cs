using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Globalization;
using System.Net;
using Newtonsoft.Json;

//************************* M+ (Model for All ) **********************************
public partial class XL_UNG_DUNG
{
    static XL_UNG_DUNG Ung_dung = null;

    public XL_DU_LIEU Du_lieu_Ung_dung = null;
    public List<XL_NGUOI_DUNG_NOI_BO> Danh_sach_Nguoi_dung_Noi_bo = new List<XL_NGUOI_DUNG_NOI_BO>();

    public static XL_UNG_DUNG Khoi_dong_Ung_dung()
    {
        if(Ung_dung == null)
        {
            Ung_dung = new XL_UNG_DUNG();
            Ung_dung.Khoi_dong_Du_lieu_Ung_dung();
        }
        else
        {
            //Cập nhập danh sách đặt vé cho Tất cả người dùng 
            Ung_dung.Danh_sach_Nguoi_dung_Noi_bo.ForEach(Nguoi_dung =>
            {
                Nguoi_dung.Danh_sach_Phim = Ung_dung.Du_lieu_Ung_dung.Danh_sach_Phim;
                Nguoi_dung.Danh_sach_Dat_ve = new List<XL_DAT_VE>();
                Nguoi_dung.Danh_sach_Phim.ForEach(Phim =>
                {

                    Phim.Danh_sach_Dat_ve.ForEach(Dat_ve =>
                    {
                        if (Dat_ve.Suat_chieu.Rap.Ma_so == Nguoi_dung.Rap.Ma_so && Dat_ve.Trang_thai == "DAT_VE")
                        {
                            Nguoi_dung.Danh_sach_Dat_ve.Add(Dat_ve);
                        }
                    });
                });
            });
        }
        return Ung_dung;
    }
    void Khoi_dong_Du_lieu_Ung_dung()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Du_lieu_tu_Dich_vu = XL_DU_LIEU.Doc_Du_lieu();
        Du_lieu_Ung_dung = Du_lieu_tu_Dich_vu;

        //Bổ sung Thông tin cần thiết cho Tất cả người dùng 
        Danh_sach_Nguoi_dung_Noi_bo = Du_lieu_Ung_dung.Danh_sach_Nguoi_dung_Noi_bo.FindAll(Nguoi_dung => Nguoi_dung.Nhom_Nguoi_dung.Ma_so == "NHAN_VIEN_BAN_VE");
        Danh_sach_Nguoi_dung_Noi_bo.ForEach(Nguoi_dung =>
        {
            Nguoi_dung.Danh_sach_Phim = Du_lieu_Ung_dung.Danh_sach_Phim;
            Nguoi_dung.Danh_sach_Phim.ForEach(Phim =>
            {
                
                Phim.Danh_sach_Dat_ve.ForEach(Dat_ve =>
                {
                    if(Dat_ve.Suat_chieu.Rap.Ma_so == Nguoi_dung.Rap.Ma_so && Dat_ve.Trang_thai == "DAT_VE")
                    {
                        Nguoi_dung.Danh_sach_Dat_ve.Add(Dat_ve);
                    }
                });
            });
            Nguoi_dung.Ban_ve = new XL_BAN_VE();
        });
    }
    //============= Xử lý Chức năng của Người dùng đăng nhập ==============
    //Lưu ý Quan trọng : Tất cả thông tin xử lý phải dựa vào thông tin của chính Người dùng đăng nhập

    //--------------------------MH Đăng nhập-----------------------------------------------------------
    public XL_NGUOI_DUNG_NOI_BO Dang_nhap(string Ten_Dang_nhap, string Mat_khau)
    {
        var Nguoi_dung = Ung_dung.Du_lieu_Ung_dung.Danh_sach_Nguoi_dung_Noi_bo.FirstOrDefault(
                                x => x.Ten_Dang_nhap == Ten_Dang_nhap
                                      && x.Mat_khau == Mat_khau
                                      );

        if (Nguoi_dung != null)
        {   //Khởi động  Thông tin Online  
            Nguoi_dung.Danh_sach_Dat_ve_Xem = Nguoi_dung.Danh_sach_Dat_ve;
            Nguoi_dung.Ban_ve = new XL_BAN_VE();
            HttpContext.Current.Session["Nguoi_dung_Dang_nhap"] = Nguoi_dung;
        }
        return Nguoi_dung;
    }

    //-----------------------------------------MH Chính---------------------------------------------------
    public string Khoi_dong_Man_hinh_chinh()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 

        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Xem_Man_hinh_Chinh(Nguoi_dung_Dang_nhap);
        return Chuoi_HTML;
    }
    public string Ban_ve_Truc_tiep()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        Nguoi_dung_Dang_nhap.Phim_chon = new XL_PHIM();
        Nguoi_dung_Dang_nhap.Ban_ve = new XL_BAN_VE();
        var Chuoi_HTML = Tao_Chuoi_HTML_Xem_Man_hinh_Chinh(Nguoi_dung_Dang_nhap);
        return Chuoi_HTML;
    }
    public string Sang_Trang_Thanh_toan_Dat_ve()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 
        Nguoi_dung_Dang_nhap.Danh_sach_Dat_ve_Xem = Nguoi_dung_Dang_nhap.Danh_sach_Dat_ve;
        var Chuoi_HTML = "<iframe class='KHUNG_CHUC_NANG' src='MH_Thanh_toan_Dat_ve.cshtml' style='padding-top:20px' ></iframe>";
        return Chuoi_HTML;
    }

    public string Tra_cuu(string Chuoi_Tra_cuu)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 
        Nguoi_dung_Dang_nhap.Danh_sach_Dat_ve_Xem = Tra_cuu_Ve_dat(Chuoi_Tra_cuu, Nguoi_dung_Dang_nhap.Danh_sach_Dat_ve);
        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = "<iframe class='KHUNG_CHUC_NANG' src='MH_Thanh_toan_Dat_ve.cshtml'  style='padding-top:20px'></iframe>";
        return Chuoi_HTML;
    }

    public string Chon_Phim(string Ma_so_Phim)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 
        var Phim = Du_lieu_Ung_dung.Danh_sach_Phim.FirstOrDefault(x => x.Ma_so == Ma_so_Phim);
        Nguoi_dung_Dang_nhap.Phim_chon = Phim;
        var Chuoi_HTML = "<iframe class='KHUNG_CHUC_NANG' src='MH_CHi_tiet_Phim.cshtml' style='padding-top:20px' ></iframe>";
        return Chuoi_HTML;
    }

    public string Tao_Chuoi_HTML_Xem_Man_hinh_Chinh(XL_NGUOI_DUNG_NOI_BO Nguoi_dung_Dang_nhap)
    {
        var Chuoi_HTML = $"<div class='container' style='padding-top:20px;'>"+
            $"<h3>LỊCH CHIẾU "+Nguoi_dung_Dang_nhap.Rap.Ten.ToUpper()+$"</h3>"+
            $"<form id='MH_CHI_TIET_PHIM' name='MH_CHI_TIET_PHIM' method='post' style='margin:30px 0px;'>"+
                $"<label for='Th_Ngay'>Chọn ngày: </label>"+
                $"<input id='Th_Ngay' name='Th_Ngay' type='date' value='2018-02-09' onchange='chonNgay(this.value)' style='margin-left:20px' />"+
                $"<label for='Th_So_luong'>Số lượng vé: </label>"+
                $"<input id='Th_So_luong' name='Th_So_luong' type='number' value='1' />"+
                $"<input name='Th_Ma_so_Phim' id='Th_Ma_so_Phim' type='hidden' />"+
                $"<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_SUAT_CHIEU' />"+
                $"<input name='Th_Ma_so_Suat_chieu' id='Th_Ma_so_Suat_chieu' type='hidden' /><hr>"+
                $"<div id='Kq'>"+
                $"</div>"+
            $"</form>"+
            $"</div>";
        return Chuoi_HTML;
    }

    //MH Chi tiết phim
    public string Khoi_dong_Man_hinh_Chi_tiet_Phim()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        // Xử lý 

        // Tạo chuỗi HTML kết quả xem 
        var Chuoi_HTML = Tao_Chuoi_HTML_Chi_tiet_Phim(Nguoi_dung_Dang_nhap.Phim_chon, Nguoi_dung_Dang_nhap);
        return Chuoi_HTML;
    }
    public string Tao_Chuoi_HTML_Chi_tiet_Phim(XL_PHIM Phim, XL_NGUOI_DUNG_NOI_BO Nguoi_dung_Dang_nhap)
    {
        var Chuoi_HTML = $"<div>" +
            $"{Tao_Chuoi_Chi_tiet_Phim(Phim)}" +
            $"</div>";
        return Chuoi_HTML;
    }

    //-------------------------MH Chọn Ghế---------------------------------------------------------------------
    public string Khoi_dong_Man_hinh_Chon_Ghe()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Danh_sach_Ghe_Chon = Nguoi_dung_Dang_nhap.Ban_ve.Danh_sach_Ghe_ban;
        var Chuoi_HTML = Tao_chuoi_HTML_Man_hinh_Chon_Ghe(Danh_sach_Ghe_Chon);
        return Chuoi_HTML;
    }

    public string Chon_Ghe(XL_GHE Ghe_Chon)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
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

    public string Tao_Chuoi_Danh_sach_Ghe(List<XL_GHE> Danh_sach_Ghe_Chon)
    {
        var Chuoi_HTML = "";
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Rap = Nguoi_dung_Dang_nhap.Rap;
        var Phong_chieu = Rap.Danh_sach_Phong_chieu.Find(x => x.Ma_so == Nguoi_dung_Dang_nhap.Ban_ve.Suat_chieu.Phong_chieu.Ma_so);
        var Danh_sach_Ghe = Phong_chieu.Danh_sach_Ghe;
        var Danh_sach_Ghe_trong = Nguoi_dung_Dang_nhap.Ban_ve.Suat_chieu.Danh_sach_Ghe_trong;
        var Count = 0;
        Danh_sach_Ghe.ForEach(Ghe =>
        {
            Count++;
            var Tinh_trang_Ghe = 0;
            var Chuoi_Xu_ly_Click = "Th_Ma_so_Ghe.value='XXX';CHON_GHE.submit() ";
            Chuoi_Xu_ly_Click = Chuoi_Xu_ly_Click.Replace("XXX", Ghe.Ma_so);
            var Da_chon = Danh_sach_Ghe_Chon.Any(x => x.Ma_so == Ghe.Ma_so);
            var Chua_dat = Danh_sach_Ghe_trong.Any(x => x.Ma_so == Ghe.Ma_so);
            if (Da_chon)
            {
                Tinh_trang_Ghe = 1;
            }
            if (!Chua_dat)
            {
                Tinh_trang_Ghe = -1;
            }
            Chuoi_HTML += Tao_chuoi_HTML_Ghe(Ghe, Chuoi_Xu_ly_Click, Tinh_trang_Ghe, Count);

        });

        return Chuoi_HTML;
    }
    public string Tao_chuoi_HTML_Man_hinh_Chon_Ghe(List<XL_GHE> Danh_sach_Ghe_Chon)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
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
                                                        $"<span class='seat-cinema-normal'>Có thể chọn</span>" +
                                                    $"</div></div></div></section></div></div></div>" +

                        $"</div>" +
                    $"</div></form>" +

                    $"<div class='col-md-3'><div class='ticket-header'><section class='ticket-feature'>" +
                                $"<article class='row'><div class='col-md-12' style='text-align:center'></div>" +
                                    $"{Tao_Bang_Thong_tin_Ve(Nguoi_dung_Dang_nhap)}" +
                                $"</article></section></div>" +
                                $"<form class='text-center' id='DIEU_HUONG' action='MH_Chon_Ghe.cshtml' method='post'>" +
                    $"<input id='Th_Ma_so_Chuc_nang' name='Th_Ma_so_Chuc_nang' type='hidden' />" +
                    //$"<button class='btn btn-primary' type='button' style='border-radius:0;background-color:#f26b38;border:none;' onclick=\"Th_Ma_so_Chuc_nang.value = 'QUAY_LAI';DIEU_HUONG.submit()\">Quay lại</button>" +
                    $"<button class='btn btn-primary' type='button' style='border-radius:0;background-color:#f26b38;border:none;' onclick=\"Th_Ma_so_Chuc_nang.value = 'BAN_VE';DIEU_HUONG.submit()\">Đặt vé</button>" +
                $"</form>"+
                                $"</div></div>";
        return Chuoi_HTML;
    }

    public string Ban_ve()
    {
        var Chuoi_HTML = "";
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        Nguoi_dung_Dang_nhap.Ban_ve.Ngay = DateTime.Now;
        Nguoi_dung_Dang_nhap.Ban_ve.Nhan_vien_Ban_ve.Ma_so = Nguoi_dung_Dang_nhap.Ma_so;
        Nguoi_dung_Dang_nhap.Ban_ve.Nhan_vien_Ban_ve.Ho_ten = Nguoi_dung_Dang_nhap.Ho_ten;
        string Kq = XL_DU_LIEU.Ghi_Ban_ve_Moi(Nguoi_dung_Dang_nhap.Phim_chon, Nguoi_dung_Dang_nhap.Ban_ve);
        if(Kq == "OK")
        {
            Kq = XL_DU_LIEU.Ghi_Thay_doi_Danh_sach_Ghe_trong_Phan_he_Khach_tham_quan(Nguoi_dung_Dang_nhap.Phim_chon, Nguoi_dung_Dang_nhap.Ban_ve);
            if (Kq == "OK")
            {
                var Suat_chieu = Nguoi_dung_Dang_nhap.Phim_chon.Danh_sach_Suat_chieu.FirstOrDefault(x => x.Ma_so == Nguoi_dung_Dang_nhap.Ban_ve.Suat_chieu.Ma_so);
                Suat_chieu.Danh_sach_Ghe_trong.RemoveAll(Ghe_trong => Nguoi_dung_Dang_nhap.Ban_ve.Danh_sach_Ghe_ban.Any(Ghe_ban => Ghe_ban.Ma_so == Ghe_trong.Ma_so));
                Chuoi_HTML += $"<div class='alert alert-success'>Bạn đã bán vé thành công</div>";
            }
            else
            {
                Chuoi_HTML += $"<div class='alert alert-warning'>Đã có lỗi xảy ra, vui lòng nhập lại thông tin</div>";
            }
        }
        else
        {
            Chuoi_HTML += $"<div class='alert alert-warning'>Đã có lỗi xảy ra, vui lòng nhập lại thông tin. {Kq}</div>";
        }
        return Chuoi_HTML;

    }

    //-----------------------------------MH Thanh toán Đặt vé----------------------------------
    public string Khoi_dong_Man_hinh_Thanh_toan_Dat_ve()
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        Nguoi_dung_Dang_nhap.Danh_sach_Dat_ve_Xem = Nguoi_dung_Dang_nhap.Danh_sach_Dat_ve;
        var Chuoi_HTML = Tao_Chuoi_HTML_Xem_Man_hinh_Thanh_toan_Dat_ve(Nguoi_dung_Dang_nhap.Danh_sach_Dat_ve_Xem);
        return Chuoi_HTML;
    }

    public string Thanh_toan_Ve_dat(XL_DAT_VE Ve_Dat)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Chuoi_HTML = "";
        var Ngay_Thanh_toan_Ban_dau = Ve_Dat.Ngay_thanh_toan;
        Ve_Dat.Ngay_thanh_toan = DateTime.Now;
        Ve_Dat.Nhan_vien_Ban_ve.Ma_so = Nguoi_dung_Dang_nhap.Ma_so;
        Ve_Dat.Nhan_vien_Ban_ve.Ho_ten = Nguoi_dung_Dang_nhap.Ho_ten;
        var Ma_so_Ve = Ve_Dat.Ma_so;
        var Chuoi_cat = Ma_so_Ve.Split('_');
        var Ma_so_Phim = Chuoi_cat[0] + '_' + Chuoi_cat[1];
        string Kq = XL_DU_LIEU.Ghi_Xac_nhan_Thanh_toan_Ve_dat(Ma_so_Phim, Ve_Dat);
        if (Kq == "OK")
        {
            
            Chuoi_HTML += $"<div class='alert alert-success'>Bạn đã thanh toán thành công</div>";
        }
        else
        {
            Ve_Dat.Ngay_thanh_toan = Ngay_Thanh_toan_Ban_dau;
            Ve_Dat.Nhan_vien_Ban_ve.Ma_so = null;
            Ve_Dat.Nhan_vien_Ban_ve.Ho_ten = null;
            Chuoi_HTML += $"<div class='alert alert-warning'>Đã có lỗi xảy ra</div>";
        }
        return Chuoi_HTML;
    }
    //--------------------------------------Dich_vu_Giao_tiep---------------------------------------
    public string Ghi_Dat_ve_Moi(string Ma_so_Phim, XL_DAT_VE Dat_ve)
    {
        var Chuoi_Kq_Ghi = "OK";
        var Phim = Du_lieu_Ung_dung.Danh_sach_Phim.FirstOrDefault(x => x.Ma_so == Ma_so_Phim);
        if(Phim != null)
        {
            var So_luot_Dat_ve = Phim.Danh_sach_Dat_ve.Count;
            So_luot_Dat_ve++;
            Dat_ve.Ma_so = Phim.Ma_so + "_DV_" + So_luot_Dat_ve.ToString();
            
            var Suat_chieu = Phim.Danh_sach_Suat_chieu.FirstOrDefault(x => x.Ma_so == Dat_ve.Suat_chieu.Ma_so);
            if(Suat_chieu != null)
            {
                Phim.Danh_sach_Dat_ve.Add(Dat_ve);
                Suat_chieu.Danh_sach_Ghe_trong.RemoveAll(Ghe_trong => Dat_ve.Danh_sach_Ghe_dat.Any(Ghe_dat => Ghe_dat.Ma_so == Ghe_trong.Ma_so));
            }
            else
            {
                Chuoi_Kq_Ghi = $"ERROR_{Suat_chieu.Ma_so}_OF_{Ma_so_Phim}_DOESN'T_EXIST";
            }
                
        }
        else
        {
            Chuoi_Kq_Ghi = $"ERROR_{Ma_so_Phim}_DOESN'T_EXIST";
        }
        return Chuoi_Kq_Ghi;
    }
}

//************************* View-Layers/Prsenetaition VL/PL **********************************
public partial class XL_UNG_DUNG
{
    public string Dia_chi_Media = $"{XL_DU_LIEU.Dia_chi_Dich_vu}/Media";
    public CultureInfo Dinh_dang_VN = CultureInfo.GetCultureInfo("vi-VN");

    public string Tao_Chuoi_HTML_Danh_sach_Phim_Xem(List<XL_PHIM> Danh_sach)
    {
        var Chuoi_HTML_Danh_sach = "<div class='container'><form id='MH_CHINH' name='MH_CHINH' method='post'>";
        var Chuoi_Input = "<input name='Th_Ma_so_Phim' id='Th_Ma_so_Phim' type='hidden' />"+
            "<input name='Th_Ma_so_Chuc_nang' type='hidden' value='CHON_PHIM'/>"
            ;
        Chuoi_HTML_Danh_sach += Chuoi_Input;
        Chuoi_HTML_Danh_sach += "<div class='row'>";
        Danh_sach.ForEach(Phim =>
        {
            var Chuoi_Xu_ly_Click = $"Th_Ma_so_Phim.value='{Phim.Ma_so}';MH_CHINH.submit() ";
            var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{Phim.Ma_so}.jpg' class='card-img-top HINH'/>";

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
    public string Tao_Chuoi_Suat_chieu( XL_NGUOI_DUNG_NOI_BO Nguoi_dung, DateTime Ngay_chon)
    {
        var Chuoi_Html = "<div class='row'>";

        
        foreach(XL_PHIM Phim in Nguoi_dung.Danh_sach_Phim)
        {
            var Co_Suat_chieu = false;
            var Chuoi_Danh_sach_Suat_chieu = $"<div class='col-lg-6 col-md-6>'><h5>{Phim.Ten}</h5><div><ul>";
            foreach (XL_SUAT_CHIEU Suat_chieu in Phim.Danh_sach_Suat_chieu)
            {
                if (Suat_chieu.Rap.Ma_so == Nguoi_dung.Rap.Ma_so && Suat_chieu.Bat_dau.Date == Ngay_chon.Date)
                {
                    Co_Suat_chieu = true;
                    var Chuoi_Xu_ly_Click = $"Th_Ma_so_Phim.value='{Phim.Ma_so}';Th_Ma_so_Suat_chieu.value='{Suat_chieu.Ma_so}';MH_CHI_TIET_PHIM.submit() ";
                    Chuoi_Danh_sach_Suat_chieu += $"<li onclick=\"" + $"{Chuoi_Xu_ly_Click}" + "\">" + $"{Suat_chieu.Bat_dau.ToString("HH:mm")}" + "</li>";
                }
            }
            if (Co_Suat_chieu)
            {
                Chuoi_Danh_sach_Suat_chieu += "</ul></div></div>";
                Chuoi_Html += Chuoi_Danh_sach_Suat_chieu;
            }
            
        }
        
        
        Chuoi_Html += "</div>";
        return Chuoi_Html;
    }

    public string Tao_chuoi_HTML_Ghe(XL_GHE Ghe, string Chuoi_Xu_ly_Click = "", int Tinh_trang_Ghe = 0, int Count = 0)
    {
        string Hang_ghe = Ghe.Ma_so[0].ToString();
        string So_ghe = Ghe.Ma_so[1].ToString();
        //var So_ghe = 0;
        //if (Count % 9 == 0)
        //    So_ghe = 9;
        //else
        //    So_ghe = Count % 9;
        var Chuoi_HTML = "";
        if (Count % 9 == 1)
        {
            Chuoi_HTML += "<ul style='list-style-type: none;margin:5px;padding-left: 0px;cursor: pointer;'>";
            Chuoi_HTML += $"<li style='display:inline-block;text-align:center;width: 20px;height: 20px;border: solid 1px;'>{Hang_ghe}</li><li style='display:inline-block;'><ul style='list-style-type: none;padding: 0px;'>";
        }

        if (Tinh_trang_Ghe == -1)
        {
            Chuoi_HTML += $"<li style='background-color:orange;display:inline-block;text-align:center;width: 20px;height: 20px;margin-left:2px;'>{So_ghe}</li>";
        }
        else if (Tinh_trang_Ghe == 1)
        {
            Chuoi_HTML += $"<li onclick=\"" + $"{Chuoi_Xu_ly_Click}" + $"\" style='display:inline-block;background-color: green;text-align:center;width: 20px;height: 20px;margin-left:2px;'>{So_ghe}</li>";
        }
        else
        {
            Chuoi_HTML += $"<li onclick=\"" + $"{Chuoi_Xu_ly_Click}" + $"\" style ='display:inline-block;background-color: #dbdee1;text-align:center;width: 20px;height: 20px;margin-left:2px;'>{So_ghe}</li>";
        }
        if (Count % 9 == 0)
            Chuoi_HTML += $"</ul></li><li style='display:inline-block;text-align:center;width: 20px;height: 20px;border: solid 1px;margin-left:2px;'>{Hang_ghe}</li></ul>";
        return Chuoi_HTML;
    }

    public string Tao_Bang_Thong_tin_Ve(XL_NGUOI_DUNG_NOI_BO Nguoi_dung)
    {
        var Chuoi_HTML = $"<div class='col-md-12'><div class='ticket-detail'>";
        var Ten_Phim = Nguoi_dung.Phim_chon.Ten;
        var Ten_Phim_Tieng_Anh = Nguoi_dung.Phim_chon.Ten_tieng_Anh;
        var Rap = Nguoi_dung.Ban_ve.Suat_chieu.Rap.Ten;
        var Phong_chieu = Nguoi_dung.Ban_ve.Suat_chieu.Phong_chieu.Ten;
        var Thoi_gian = Nguoi_dung.Ban_ve.Suat_chieu.Bat_dau.ToString("dddd, dd/MM/yyyy hh:mm", Dinh_dang_VN);
        var Danh_sach_Ghe_dat = Nguoi_dung.Ban_ve.Danh_sach_Ghe_ban;
        var So_ve_Da_dat = Nguoi_dung.Ban_ve.Danh_sach_Ghe_ban.Count();
        var So_luong = Nguoi_dung.Ban_ve.So_luong.ToString();
        var Tien = Nguoi_dung.Ban_ve.Tien.ToString("n0", Dinh_dang_VN);

        var Chuoi_Thong_tin = $"<h2 class='ticket-title'>{Ten_Phim}</h2>" +
            $"<h2 class='ticket-title en'>{Ten_Phim_Tieng_Anh}</h2>" +
            $"<div class='ticket-info'>" +
            $"<p><b>Rạp: &nbsp;</b>{Rap} | {Phong_chieu}</p>" +
            $"<p><b>Suất chiếu: &nbsp;</b>{Thoi_gian}</p>" +
            $"<p><b>Số lượng vé đặt: &nbsp;</b>{So_luong}</p>" +
            $"<p><b>Ghế: &nbsp;</b>";
        for(int i = 0; i< So_ve_Da_dat; i++)
        {
            if (i == So_ve_Da_dat - 1)
                Chuoi_Thong_tin += Danh_sach_Ghe_dat[i].Ma_so;
            else
                Chuoi_Thong_tin += Danh_sach_Ghe_dat[i].Ma_so + ", ";
        }
        Chuoi_Thong_tin+= $"</p>" +
            $"</div>" +
            $"<div class='ticket-price-total'>" +
            $"<p><b>Tổng tiền: &nbsp;</b><span>{Tien}</span></p>" + $"</div>";
        Chuoi_HTML += Chuoi_Thong_tin + $"</div></div>";
        return Chuoi_HTML;
    }

    public string Tao_Chuoi_HTML_Xem_Man_hinh_Thanh_toan_Dat_ve(List<XL_DAT_VE> Danh_sach_Dat_ve)
    {
        var Nguoi_dung_Dang_nhap = (XL_NGUOI_DUNG_NOI_BO)HttpContext.Current.Session["Nguoi_dung_Dang_nhap"];
        var Chuoi_HTML_Danh_sach = "<div class='row'>";
        foreach(XL_DAT_VE Dat_ve in Danh_sach_Dat_ve)
        {
            var Ma_so = Dat_ve.Ma_so;
            var Chuoi_cat = Ma_so.Split('_');
            var Ma_so_Phim = Chuoi_cat[0] + '_' + Chuoi_cat[1];
            var Phim = Nguoi_dung_Dang_nhap.Danh_sach_Phim.FirstOrDefault(x => x.Ma_so == Ma_so_Phim);
            var Ten_Phim = Phim.Ten;
            var Rap = Dat_ve.Suat_chieu.Rap.Ten;
            var Ma_nhan_ve = Dat_ve.Ma_nhan_ve;
            var Ho_ten = Dat_ve.Nguoi_dung_Khach_tham_quan.Ho_ten;
            var Sdt = Dat_ve.Nguoi_dung_Khach_tham_quan.Dien_thoai;
            var Email = Dat_ve.Nguoi_dung_Khach_tham_quan.Email;
            var Thoi_diem_dat = Dat_ve.Ngay_thanh_toan.ToString("dddd, dd/MM/yyyy hh:mm", Dinh_dang_VN);
            var Phong_chieu = Dat_ve.Suat_chieu.Phong_chieu.Ten;
            var Thoi_gian = Dat_ve.Suat_chieu.Bat_dau.ToString("dddd, dd/MM/yyyy hh:mm", Dinh_dang_VN);
            var Danh_sach_Ghe_dat = Dat_ve.Danh_sach_Ghe_dat;
            var So_luong = Dat_ve.So_luong.ToString();
            var So_ve_Da_dat = Dat_ve.Danh_sach_Ghe_dat.Count();
            var Tien = Dat_ve.Tien.ToString("n0", Dinh_dang_VN);
            var Chuoi_Thong_tin = $"<div class='col-md-3'><div class='ticket-header'><section class='ticket-feature'>" +
                                $"<article class='row'><div class='col-md-12' style='text-align:center'></div>"+
                $"<div class='col-md-12'><div class='ticket-detail'><h2 class='ticket-title'>{Ma_so}</h2>" +
                $"<h2 class='ticket-title'>{Ten_Phim}</h2>"+
            $"<div class='ticket-info'>" +
            $"<p><b>Mã nhận vé: &nbsp;</b>{Ma_nhan_ve}</p>"+
            $"<p><b>Họ tên: &nbsp;</b>{Ho_ten}</p>" +
            $"<p><b>Điện thoại: &nbsp;</b>{Sdt}</p>" +
            $"<p><b>Email: &nbsp;</b>{Email}</p>" +
            $"<p><b>Rạp: &nbsp;</b>{Rap} | {Phong_chieu}</p>" +
            $"<p><b>Suất chiếu: &nbsp;</b>{Thoi_gian}</p>" +
            $"<p><b>Thời điểm đặt: &nbsp;</b>{Thoi_diem_dat}</p>" +
            $"<p><b>Số lượng vé đặt: &nbsp;</b>{So_luong}</p>" +
            $"<p><b>Ghế: &nbsp;</b>";
            for (int i = 0; i < So_ve_Da_dat; i++)
            {
                if (i == So_ve_Da_dat - 1)
                    Chuoi_Thong_tin += Danh_sach_Ghe_dat[i].Ma_so;
                else
                    Chuoi_Thong_tin += Danh_sach_Ghe_dat[i].Ma_so + ", ";
            }
            Chuoi_Thong_tin += $"</p>" +
                $"</div>" +
                $"<div class='ticket-price-total'>" +
                $"<p><b>Tổng tiền: &nbsp;</b><span>{Tien}</span></p>" + $"</div>";
            Chuoi_Thong_tin += $"</div></div>" +
                $"</article>" +
                $"<form method='post'>" +
                                        $"<input name='Th_Ma_so_Chuc_nang' type='hidden' value='THANH_TOAN' />" +
                                         $"<input name='Th_Ma_so_Ve' type='hidden' value='{Ma_so}' />" +
                $"<button type='submit' class='btn btn-primary'>Thanh toán</button></form>" +
                $"</section></div>" +
                
                $"</div>";
            Chuoi_HTML_Danh_sach += Chuoi_Thong_tin;
        }
        Chuoi_HTML_Danh_sach += $"</div>";
        return Chuoi_HTML_Danh_sach;
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
    public List<XL_DAT_VE> Tra_cuu_Ve_dat(string Chuoi_Tra_cuu, List<XL_DAT_VE> Danh_sach)
    {
        Danh_sach = Danh_sach.FindAll(Ve_Dat =>
            Ve_Dat.Nguoi_dung_Khach_tham_quan.Ho_ten.ToUpper().Contains(Chuoi_Tra_cuu.ToUpper()) || Ve_Dat.Nguoi_dung_Khach_tham_quan.Dien_thoai == Chuoi_Tra_cuu
            || Ve_Dat.Nguoi_dung_Khach_tham_quan.Email == Chuoi_Tra_cuu || Ve_Dat.Ma_so == Chuoi_Tra_cuu
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
        var Suat_chieu = Phim.Danh_sach_Suat_chieu.FirstOrDefault(S => String.Equals(Ma_so,S.Ma_so));
        return Suat_chieu;
    }

}

//************************* Data-Layers DL **********************************
public partial class XL_DU_LIEU
{
    public static string Dia_chi_Dich_vu = "http://localhost:59900";
    public static string Dia_chi_Dich_vu_Quan_ly_Rap_Phim = $"{Dia_chi_Dich_vu}/1-Dich_vu_Giao_tiep/DV_Nhan_vien_Ban_ve.cshtml";
    public static string Dia_chi_Phan_he_Khach_tham_quan = "http://localhost:63413/1-Dich_vu_Giao_tiep/DV_Chinh.cshtml";

    public static XL_DU_LIEU Doc_Du_lieu()
    {
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;

        var Tham_so = "Ma_so_Xu_ly=KHOI_DONG_DU_LIEU_PHAN_HE_NHAN_VIEN";
        var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Quan_ly_Rap_Phim}?{Tham_so}";
        var Chuoi_JSON = Xu_ly.DownloadString(Dia_chi_Xu_ly);
        var Du_lieu = JsonConvert.DeserializeObject<XL_DU_LIEU>(Chuoi_JSON);

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
    public static string Ghi_Xac_nhan_Thanh_toan_Ve_dat(string Ma_so_Phim, XL_DAT_VE Ve_dat)
    {
        var Kq = "";
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;
        var Tham_so = $"Ma_so_Xu_ly=GHI_XAC_NHAN_THANH_TOAN_VE_DAT&Ma_so_Phim={Ma_so_Phim}";
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
        if (Kq == "OK")
        {
            Ve_dat.Trang_thai = "DA_THANH_TOAN";
        }
        return Kq;

    }

    public static string Ghi_Thay_doi_Danh_sach_Ghe_trong_Phan_he_Khach_tham_quan(XL_PHIM Phim, XL_BAN_VE Ban_ve)
    {
        var Kq = "";
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;
        var Tham_so = $"Ma_so_Xu_ly=GHI_THAY_DOI_DANH_SACH_GHE_TRONG&Ma_so_Phim={Phim.Ma_so}&Ma_so_Suat_chieu={Ban_ve.Suat_chieu.Ma_so}";
        var Dia_chi_Xu_ly = $"{Dia_chi_Phan_he_Khach_tham_quan}?{Tham_so}";
        var Chuoi_JSON = Json.Encode(Ban_ve.Danh_sach_Ghe_ban);
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