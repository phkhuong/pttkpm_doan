using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//*************************** Đối tượng Dữ liệu   *********
public partial class XL_DU_LIEU
{
    public XL_CONG_TY Cong_ty = new XL_CONG_TY();
    public  List<XL_PHIM> Danh_sach_Phim = new List<XL_PHIM>();
}
//*************************** Đối tượng Tổ chức  *********
public class XL_CONG_TY
{
    public string Ten, Ma_so = "";
    public List<XL_RAP> Danh_sach_Rap = new List<XL_RAP>();
    public List<XL_NHAN_VIEN_BAN_VE> Danh_sach_Nhan_vien = new List<XL_NHAN_VIEN_BAN_VE>();
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
public class XL_NHAN_VIEN_BAN_VE
{
    public string Ho_ten, Ma_so = "", Ten_Dang_nhap, Mat_khau;
    public XL_RAP Rap = new XL_RAP();
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
    public List<XL_DAT_VE> Danh_sach_Dat_ve = new List<XL_DAT_VE>();

    public long Doanh_thu;
}

public class XL_SUAT_CHIEU
{
    public string Ma_so = "";
    public DateTime Bat_dau = DateTime.Now;
    public List<XL_GHE> Danh_sach_Ghe_trong = new List<XL_GHE>();
    public XL_RAP Rap = new XL_RAP();
}

public class XL_DAT_VE
{
    public string Ma_so = "", Ma_nhan_ve = "";
    public List<XL_GHE> Danh_sach_Ghe_dat = new List<XL_GHE>();
    public XL_SUAT_CHIEU Suat_chieu = new XL_SUAT_CHIEU();
    public DateTime Ngay_dat = DateTime.Now, Ngay_thanh_toan = DateTime.Now, Ngay_huy = DateTime.Now;
    public long Don_gia,So_luong,Tien;
    public XL_KHACH_HANG Khach_hang = new XL_KHACH_HANG();
    public string Trang_thai= "DAT_VE";

}

public class XL_GHE
{
    public string Ma_so = "";
}