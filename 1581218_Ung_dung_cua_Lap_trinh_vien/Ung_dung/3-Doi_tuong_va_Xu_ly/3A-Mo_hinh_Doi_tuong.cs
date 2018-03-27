using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class XL_CHUC_NANG
{
    public string Ten, Ma_so;
}


//*************************** Đối tượng Dữ liệu   *********
public partial class XL_DU_LIEU
{
   public List<XL_NGUOI_DUNG> Danh_sach_Nguoi_dung_Noi_bo = new List<XL_NGUOI_DUNG>();
    public List<XL_NGUOI_DUNG_KHACH_THAM_QUAN> Danh_sach_Nguoi_dung_Khach_tham_quan = new List<XL_NGUOI_DUNG_KHACH_THAM_QUAN>();

}
//=========== Đối tượng Con người ===============
public class XL_NGUOI_DUNG
{
    public string Ho_ten, Ma_so = "", Ten_Dang_nhap, Mat_khau;
    public XL_NHOM_NGUOI_DUNG Nhom_Nguoi_dung = new XL_NHOM_NGUOI_DUNG();
}
public class XL_NGUOI_DUNG_KHACH_THAM_QUAN
{
    public string Ho_ten, Dien_thoai, Email, Ma_so = "", Ten_Dang_nhap, Mat_khau;
    public long Diem_tich_luy;
}
public class XL_LAP_TRINH_VIEN
{
    public string Ho_ten, Ma_so = "", Ten_Dang_nhap, Mat_khau;
    public List<XL_NGUOI_DUNG> Danh_sach_Nguoi_dung_Noi_bo = new List<XL_NGUOI_DUNG>();
    public List<XL_NGUOI_DUNG> Danh_sach_Nguoi_dung_Noi_bo_Xem = new List<XL_NGUOI_DUNG>();
    public List<XL_NGUOI_DUNG> Danh_sach_Nguoi_dung_Noi_bo_Chon = new List<XL_NGUOI_DUNG>();
    // Chức năng 
    public XL_CHUC_NANG Chuc_nang_Khoi_dong_MH_Chinh = new XL_CHUC_NANG()
    { Ten = "Khởi động", Ma_so = "KHOI_DONG_MH_CHINH" };
    public XL_CHUC_NANG Chuc_nang_Chon_Nguoi_dung = new XL_CHUC_NANG()
    { Ten = "Chọn người dùng", Ma_so = "CHON_NGUOI_DUNG" };
    public XL_CHUC_NANG Chuc_nang_Tra_cuu_Nguoi_dung = new XL_CHUC_NANG()
    { Ten = "Tra cứu người dùng", Ma_so = "TRA_CUU_NGUOI_DUNG" };
 

}
 
public class XL_NHOM_NGUOI_DUNG
{
    public string Ten, Ma_so = "";
}


 
