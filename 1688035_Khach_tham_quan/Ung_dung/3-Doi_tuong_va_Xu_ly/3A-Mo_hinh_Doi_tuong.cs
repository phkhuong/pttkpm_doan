using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class XL_LAN_DANG_NHAP
{
    public DateTime Ngay = DateTime.Now;

}
//*************************** Đối tượng Dữ liệu   *********
public partial class XL_DU_LIEU
{
    public XL_CONG_TY Cong_ty = new XL_CONG_TY();
    public List<XL_NGUOI_DUNG_NOI_BO> Danh_sach_Nguoi_dung_Noi_bo = new List<XL_NGUOI_DUNG_NOI_BO>();
    public List<XL_NGUOI_DUNG_KHACH_THAM_QUAN> Danh_sach_Nguoi_dung_Khach_tham_quan = new List<XL_NGUOI_DUNG_KHACH_THAM_QUAN>();
    public List<XL_PHIM> Danh_sach_Phim = new List<XL_PHIM>();
}
//=========== Đối tượng  Con người ===============
public class XL_NGUOI_DUNG
{
    public string Ho_ten, Dien_thoai, Email, Ma_so = "", Ten_Dang_nhap, Mat_khau;
    public long Diem_tich_luy;
    public List<XL_PHIM> Danh_sach_Phim_Xem = new List<XL_PHIM>();
    public List<XL_RAP> Danh_sach_Rap = new List<XL_RAP>();
    public XL_PHIM Phim_chon = new XL_PHIM();
    public XL_DAT_VE Dat_ve = new XL_DAT_VE();
}
public class XL_NGUOI_DUNG_NOI_BO
{
    public string Ho_ten, Ma_so = "", Ten_Dang_nhap, Mat_khau;
    public XL_NHOM_NGUOI_DUNG Nhom_Nguoi_dung = new XL_NHOM_NGUOI_DUNG();
    public XL_RAP Rap = new XL_RAP();
    public long Doanh_thu;//Tính toán

    public List<XL_LAN_DANG_NHAP> Danh_sach_Lan_Dang_nhap = new List<XL_LAN_DANG_NHAP>();
}
public class XL_NGUOI_DUNG_KHACH_THAM_QUAN
{
    public string Ho_ten, Dien_thoai, Email, Ma_so = "", Ten_Dang_nhap, Mat_khau;
    public long Diem_tich_luy;
}

//*************************** Đối tượng Tổ chức  *********
public class XL_CONG_TY
{
    public string Ten, Ma_so = "";
    public List<XL_RAP> Danh_sach_Rap = new List<XL_RAP>();

}
public class XL_RAP
{
    public string Ma_so, Ten = "";
    public int So_ghe = 0;
    public List<XL_PHONG_CHIEU> Danh_sach_Phong_chieu = new List<XL_PHONG_CHIEU>();
}
 
public class XL_PHONG_CHIEU
{
    public string Ma_so, Ten = "";
    public int So_ghe = 0;
    public List<XL_GHE> Danh_sach_Ghe = new List<XL_GHE>();

}

public class XL_KHACH_HANG
{
    public string Ho_ten,Dien_thoai,Email, Ma_so = "";

}
//*************************** Đối tượng Xử lý Chính *********
public class XL_PHIM
{
    public string Ten, Ten_tieng_Anh, Phan_loai, Quoc_gia, Dao_dien, Dien_vien, Noi_dung, Dich_thuat, Ma_so = "", Trang_thai = "DANG_CHIEU";
    public List<string> The_loai = new List<string>();
    public DateTime Khoi_chieu = DateTime.Now;
    public long Don_gia, Thoi_luong;
    public double Rating;
    public List<XL_SUAT_CHIEU> Danh_sach_Suat_chieu = new List<XL_SUAT_CHIEU>();
}

public class XL_SUAT_CHIEU
{
    public string Ma_so = "";
    public DateTime Bat_dau = DateTime.Now;
    public List<XL_GHE> Danh_sach_Ghe_trong = new List<XL_GHE>();
    public XL_RAP Rap = new XL_RAP();
    public XL_PHONG_CHIEU Phong_chieu = new XL_PHONG_CHIEU();
}

public class XL_DAT_VE
{
    public string Ma_so = "", Ma_nhan_ve = "";
    public List<XL_GHE> Danh_sach_Ghe_dat = new List<XL_GHE>();
    public XL_SUAT_CHIEU Suat_chieu = new XL_SUAT_CHIEU();
    public DateTime Ngay_dat = DateTime.Now, Ngay_thanh_toan = DateTime.Now, Ngay_huy = DateTime.Now;
    public long Don_gia,So_luong,Tien;
    public XL_NGUOI_DUNG_KHACH_THAM_QUAN Nguoi_dung_Khach_tham_quan = new XL_NGUOI_DUNG_KHACH_THAM_QUAN();
    public string Trang_thai= "DAT_VE";

}

public class XL_GHE
{
    public string Ma_so = "";
}

//=========== Danh mục Nhóm  ===============
public class XL_NHOM_NGUOI_DUNG
{
    public string Ten = "", Ma_so = "";
    public string Dia_chi_Dang_nhap = "";
}