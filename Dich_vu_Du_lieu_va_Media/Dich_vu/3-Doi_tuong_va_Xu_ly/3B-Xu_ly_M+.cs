using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Globalization;
//************************* M+ (Model for All ) **********************************
public partial class XL_DICH_VU
{
    static XL_DICH_VU Dich_vu = null;

    XL_DU_LIEU Du_lieu_Dich_vu = new XL_DU_LIEU();
    XL_CONG_TY Cong_ty = new XL_CONG_TY();
    List<XL_PHIM> Danh_sach_Phim = new List<XL_PHIM>();
    public static XL_DICH_VU Khoi_dong_Dich_vu()
    {
        Dich_vu = new XL_DICH_VU();// Tạm thời không CaChing 
        Dich_vu.Khoi_dong_Du_lieu_Dich_vu();
        return Dich_vu;
    }
    void Khoi_dong_Du_lieu_Dich_vu()
    {
        var Du_lieu_Luu_tru = XL_DU_LIEU.Doc_Du_lieu_Luu_tru();
        Du_lieu_Dich_vu = Du_lieu_Luu_tru;
        // Tính toán == >Bổ sung Thông tin
        //================== Phim =================
        Danh_sach_Phim = Du_lieu_Dich_vu.Danh_sach_Phim;
        Danh_sach_Phim.ForEach(Phim =>
        {
            Phim.Doanh_thu = Tinh_Doanh_thu_Phim(Phim, DateTime.Today);
        });
    }

    // Tạo Dữ liệu cho Phân hệ 
    public XL_DU_LIEU Tao_Du_lieu_Phan_he_Khach_Tham_quan()
    {
        var Du_lieu_Phan_he = new XL_DU_LIEU();
        Du_lieu_Phan_he.Cong_ty.Ten = Du_lieu_Dich_vu.Cong_ty.Ten;
        Du_lieu_Phan_he.Cong_ty.Ma_so = Du_lieu_Dich_vu.Cong_ty.Ma_so;
        Du_lieu_Phan_he.Cong_ty.Danh_sach_Rap = Du_lieu_Dich_vu.Cong_ty.Danh_sach_Rap;
        Du_lieu_Dich_vu.Danh_sach_Phim.ForEach(Phim =>
        {
            var Phim_cua_Phan_he = new XL_PHIM();
            Du_lieu_Phan_he.Danh_sach_Phim.Add(Phim_cua_Phan_he);
            Phim_cua_Phan_he.Ten = Phim.Ten;
            Phim_cua_Phan_he.Ma_so = Phim.Ma_so;
            Phim_cua_Phan_he.Ten_tieng_Anh = Phim.Ten_tieng_Anh;
            Phim_cua_Phan_he.Rating = Phim.Rating;
            Phim_cua_Phan_he.Phan_loai = Phim.Phan_loai;
            Phim_cua_Phan_he.Khoi_chieu = Phim.Khoi_chieu;
            Phim_cua_Phan_he.Quoc_gia = Phim.Quoc_gia;
            Phim_cua_Phan_he.Dao_dien = Phim.Dao_dien;
            Phim_cua_Phan_he.Dien_vien = Phim.Dien_vien;
            Phim_cua_Phan_he.Noi_dung = Phim.Noi_dung;
            Phim_cua_Phan_he.Dich_thuat = Phim.Dich_thuat;
            Phim_cua_Phan_he.Don_gia = Phim.Don_gia;
            Phim_cua_Phan_he.Trang_thai = Phim.Trang_thai;
            Phim_cua_Phan_he.Thoi_luong = Phim.Thoi_luong;
            Phim_cua_Phan_he.Danh_sach_Suat_chieu = Phim.Danh_sach_Suat_chieu;
        });
        return Du_lieu_Phan_he;
    }

    public XL_DU_LIEU Tao_Du_lieu_Phan_he_Nhan_vien()
    {
        var Du_lieu_Phan_he = new XL_DU_LIEU();
        Du_lieu_Phan_he.Cong_ty.Ten = Du_lieu_Dich_vu.Cong_ty.Ten;
        Du_lieu_Phan_he.Cong_ty.Ma_so = Du_lieu_Dich_vu.Cong_ty.Ma_so;
        Du_lieu_Phan_he.Cong_ty.Danh_sach_Rap = Du_lieu_Dich_vu.Cong_ty.Danh_sach_Rap;
        Du_lieu_Phan_he.Cong_ty.Danh_sach_Nhan_vien = Du_lieu_Dich_vu.Cong_ty.Danh_sach_Nhan_vien;

        Du_lieu_Dich_vu.Danh_sach_Phim.ForEach(Phim =>
        {
            var Phim_cua_Phan_he = new XL_PHIM();
            Du_lieu_Phan_he.Danh_sach_Phim.Add(Phim_cua_Phan_he);
            Phim_cua_Phan_he.Ten = Phim.Ten;
            Phim_cua_Phan_he.Ma_so = Phim.Ma_so;
            Phim_cua_Phan_he.Ten_tieng_Anh = Phim.Ten_tieng_Anh;
            Phim_cua_Phan_he.Rating = Phim.Rating;
            Phim_cua_Phan_he.Phan_loai = Phim.Phan_loai;
            Phim_cua_Phan_he.Khoi_chieu = Phim.Khoi_chieu;
            Phim_cua_Phan_he.Quoc_gia = Phim.Quoc_gia;
            Phim_cua_Phan_he.Dao_dien = Phim.Dao_dien;
            Phim_cua_Phan_he.Dien_vien = Phim.Dien_vien;
            Phim_cua_Phan_he.Noi_dung = Phim.Noi_dung;
            Phim_cua_Phan_he.Dich_thuat = Phim.Dich_thuat;
            Phim_cua_Phan_he.Don_gia = Phim.Don_gia;
            Phim_cua_Phan_he.Trang_thai = Phim.Trang_thai;
            Phim_cua_Phan_he.Thoi_luong = Phim.Thoi_luong;
            Phim_cua_Phan_he.Danh_sach_Suat_chieu = Phim.Danh_sach_Suat_chieu;
        });
        return Du_lieu_Phan_he;
    }
    public XL_DU_LIEU Tao_Du_lieu_Phan_he_Quan_ly_Nhan_vien()
    {
        var Du_lieu_Phan_he = new XL_DU_LIEU();
        Du_lieu_Phan_he.Cong_ty.Ten = Du_lieu_Dich_vu.Cong_ty.Ten;
        Du_lieu_Phan_he.Cong_ty.Ma_so = Du_lieu_Dich_vu.Cong_ty.Ma_so;
        Du_lieu_Phan_he.Cong_ty.Danh_sach_Nhan_vien = Du_lieu_Dich_vu.Cong_ty.Danh_sach_Nhan_vien;
        Du_lieu_Phan_he.Cong_ty.Danh_sach_Quan_ly_Nhan_vien = Du_lieu_Dich_vu.Cong_ty.Danh_sach_Quan_ly_Nhan_vien;
        return Du_lieu_Phan_he;
    }
    public XL_DU_LIEU Tao_Du_lieu_Phan_he_Quan_ly_Phim()
    {
        var Du_lieu_Phan_he = new XL_DU_LIEU();
        Du_lieu_Phan_he.Cong_ty.Ten = Du_lieu_Dich_vu.Cong_ty.Ten;
        Du_lieu_Phan_he.Cong_ty.Ma_so = Du_lieu_Dich_vu.Cong_ty.Ma_so;
        Du_lieu_Phan_he.Cong_ty.Danh_sach_Quan_ly_Phim = Du_lieu_Dich_vu.Cong_ty.Danh_sach_Quan_ly_Phim;

        Du_lieu_Dich_vu.Danh_sach_Phim.ForEach(Phim =>
        {
            var Phim_cua_Phan_he = new XL_PHIM();
            Du_lieu_Phan_he.Danh_sach_Phim.Add(Phim_cua_Phan_he);
            Phim_cua_Phan_he.Ten = Phim.Ten;
            Phim_cua_Phan_he.Ma_so = Phim.Ma_so;
            Phim_cua_Phan_he.Ten_tieng_Anh = Phim.Ten_tieng_Anh;
            Phim_cua_Phan_he.Rating = Phim.Rating;
            Phim_cua_Phan_he.Phan_loai = Phim.Phan_loai;
            Phim_cua_Phan_he.Khoi_chieu = Phim.Khoi_chieu;
            Phim_cua_Phan_he.Quoc_gia = Phim.Quoc_gia;
            Phim_cua_Phan_he.Dao_dien = Phim.Dao_dien;
            Phim_cua_Phan_he.Dien_vien = Phim.Dien_vien;
            Phim_cua_Phan_he.Noi_dung = Phim.Noi_dung;
            Phim_cua_Phan_he.Dich_thuat = Phim.Dich_thuat;
            Phim_cua_Phan_he.Don_gia = Phim.Don_gia;
            Phim_cua_Phan_he.Trang_thai = Phim.Trang_thai;
            Phim_cua_Phan_he.Thoi_luong = Phim.Thoi_luong;
            Phim_cua_Phan_he.Danh_sach_Suat_chieu = Phim.Danh_sach_Suat_chieu;
            Phim_cua_Phan_he.Doanh_thu = Phim.Doanh_thu;
        });
        return Du_lieu_Phan_he;
    }
}
//************************* Business-Layers BL **********************************
public partial class XL_DICH_VU
{
    
    // Tính toán
    // Tính toán cơ sở : Trực tiếp trên  Đối tượng 
    public long Tinh_Doanh_thu_Phim(XL_PHIM Phim, DateTime Ngay)
    {
        var Danh_sach_Ban_ve_Ngay = Phim.Danh_sach_Ban_ve.FindAll(Ban_ve =>
                    Ban_ve.Ngay.Day == Ngay.Day && Ban_ve.Ngay.Month == Ngay.Month && Ban_ve.Ngay.Year == Ngay.Year);
        var Doanh_thu_Ban_ve = Danh_sach_Ban_ve_Ngay.Sum(Ban_ve => Ban_ve.Tien);
        var Danh_sach_Dat_ve_Ngay = Phim.Danh_sach_Dat_ve.FindAll(Dat_ve =>
                   Dat_ve.Trang_thai =="DA_THANH_TOAN"
                   && Dat_ve.Ngay_thanh_toan.Day == Ngay.Day 
                   && Dat_ve.Ngay_thanh_toan.Month == Ngay.Month
                   && Dat_ve.Ngay_thanh_toan.Year == Ngay.Year);
        var Doanh_thu_Dat_ve = Danh_sach_Dat_ve_Ngay.Sum(Ban_ve => Ban_ve.Tien);
        var Doanh_thu = Doanh_thu_Ban_ve + Doanh_thu_Dat_ve;
        return Doanh_thu;
    }

    // Tính toán  Trên Danh sách  

}
//************************* Data-Layers DL **********************************
public partial class XL_DU_LIEU
{    
    static DirectoryInfo Thu_muc_Project = new DirectoryInfo(HostingEnvironment.ApplicationPhysicalPath);
    static DirectoryInfo Thu_muc_Du_lieu = Thu_muc_Project.GetDirectories("2-Du_lieu_Luu_tru")[0];
    static DirectoryInfo Thu_muc_Cong_ty = Thu_muc_Du_lieu.GetDirectories("Cong_ty")[0];
    static DirectoryInfo Thu_muc_Phim = Thu_muc_Du_lieu.GetDirectories("Phim")[0];
    //******** Ghi *******
    public static XL_DU_LIEU Doc_Du_lieu_Luu_tru()
    {
        var Du_lieu_Luu_tru = new XL_DU_LIEU();
        Du_lieu_Luu_tru.Cong_ty = Doc_Danh_sach_Cong_ty()[0];
        Du_lieu_Luu_tru.Danh_sach_Phim = Doc_Danh_sach_Phim();
        return Du_lieu_Luu_tru;
    }
    static List<XL_CONG_TY> Doc_Danh_sach_Cong_ty()
    {
        var Danh_sach_Cong_ty = new List<XL_CONG_TY>();
        var Danh_sach_Tap_tin = Thu_muc_Cong_ty.GetFiles("*.json").ToList();
        Danh_sach_Tap_tin.ForEach(Tap_tin =>
        {
            var Duong_dan = Tap_tin.FullName;
            var Chuoi_JSON = File.ReadAllText(Duong_dan);
            var Cong_ty = Json.Decode<XL_CONG_TY>(Chuoi_JSON);
            Danh_sach_Cong_ty.Add(Cong_ty);
        });
        return Danh_sach_Cong_ty;
    }
    static List<XL_PHIM> Doc_Danh_sach_Phim()
    {
        var Danh_sach_Phim = new List<XL_PHIM>();
        var Danh_sach_Tap_tin = Thu_muc_Phim.GetFiles("*.json").ToList();
        Danh_sach_Tap_tin.ForEach(Tap_tin =>
        {
            var Duong_dan = Tap_tin.FullName;
            var Chuoi_JSON = File.ReadAllText(Duong_dan);
            var Phim = Json.Decode<XL_PHIM>(Chuoi_JSON);

            Danh_sach_Phim.Add(Phim);
            var Danh_sach_Dat_ve_Khong_den_Tinh_tien = Phim.Danh_sach_Dat_ve.FindAll(Dat_ve =>
               Dat_ve.Trang_thai =="DAT_VE" && DateTime.Compare(DateTime.Now, Dat_ve.Ngay_dat) < 0  );
            Danh_sach_Dat_ve_Khong_den_Tinh_tien.ForEach(x =>
            {
                Phim.Danh_sach_Dat_ve.Remove(x);
            });
        });
        return Danh_sach_Phim;
    }
  
    //******** Ghi *******

    //public string Ghi_Ban_ve_Moi(XL_PHIM Phim, XL_BAN_VE Ban_ve)
    //{
    //    var Kq = "";
    //    Phim.Danh_sach_Ban_ve.Add(Ban_ve);
    //    Kq = Ghi_Phim(Phim);
    //    if (Kq != "OK")
    //        Phim.Danh_sach_Ban_ve.Remove(Ban_ve);
    //    return Kq;
    //}
    //public string Ghi_Dat_ve_Moi(XL_PHIM Phim, XL_DAT_VE Dat_ve)
    //{
    //    var Kq = "";
    //    Phim.Danh_sach_Dat_ve.Add(Dat_ve);
    //    Kq = Ghi_Phim(Phim);
    //    if (Kq != "OK")
    //        Phim.Danh_sach_Dat_ve.Remove(Dat_ve);
    //    return Kq;
    //}

    //string Ghi_Phim(XL_PHIM Phim)
    //{
    //    var Kq = "";
    //    var Duong_dan = $"{Thu_muc_Phim.FullName}\\{Phim.Ma_so}.json";
    //    var Chuoi_JSON = Json.Encode(Phim);
    //    try
    //    {
    //        File.WriteAllText(Duong_dan, Chuoi_JSON);
    //        Kq = "OK";

    //    }
    //    catch (Exception Loi)
    //    {
    //        Kq = Loi.Message;
    //    }
    //    return Kq;

    //}

}