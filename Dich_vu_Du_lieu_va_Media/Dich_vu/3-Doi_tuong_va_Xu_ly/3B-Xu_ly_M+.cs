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

    public XL_DU_LIEU Du_lieu_Dich_vu = new XL_DU_LIEU();
    List<XL_PHIM> Danh_sach_Phim = new List<XL_PHIM>();
    List<XL_NGUOI_DUNG_NOI_BO> Danh_sach_Nguoi_dung_Noi_bo = new List<XL_NGUOI_DUNG_NOI_BO>();
    List<XL_NGUOI_DUNG_KHACH_THAM_QUAN> Danh_sach_Nguoi_dung_Khach_tham_quan = new List<XL_NGUOI_DUNG_KHACH_THAM_QUAN>();

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
        //================== Người dùng =================
        Danh_sach_Nguoi_dung_Noi_bo = Du_lieu_Dich_vu.Danh_sach_Nguoi_dung_Noi_bo;
        Danh_sach_Nguoi_dung_Khach_tham_quan = Du_lieu_Dich_vu.Danh_sach_Nguoi_dung_Khach_tham_quan;
        //================== Rạp =================

        //================== Công ty =================
    }

    // Tạo Dữ liệu cho Phân hệ 
    public XL_DU_LIEU Tao_Du_lieu_Phan_he_Khach_Tham_quan()
    {
        var Du_lieu_Phan_he = new XL_DU_LIEU();
        Du_lieu_Phan_he.Cong_ty = Du_lieu_Dich_vu.Cong_ty;
        Du_lieu_Phan_he.Danh_sach_Nguoi_dung_Khach_tham_quan = Du_lieu_Dich_vu.Danh_sach_Nguoi_dung_Khach_tham_quan;
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
            Phim_cua_Phan_he.The_loai = Phim.The_loai;
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
        Du_lieu_Phan_he.Cong_ty = Du_lieu_Dich_vu.Cong_ty;
        Du_lieu_Phan_he.Danh_sach_Nguoi_dung_Khach_tham_quan = Du_lieu_Dich_vu.Danh_sach_Nguoi_dung_Khach_tham_quan;
        Du_lieu_Phan_he.Danh_sach_Nguoi_dung_Noi_bo = Du_lieu_Dich_vu.Danh_sach_Nguoi_dung_Noi_bo;

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
            Phim_cua_Phan_he.The_loai = Phim.The_loai;
            Phim_cua_Phan_he.Noi_dung = Phim.Noi_dung;
            Phim_cua_Phan_he.Dich_thuat = Phim.Dich_thuat;
            Phim_cua_Phan_he.Don_gia = Phim.Don_gia;
            Phim_cua_Phan_he.Trang_thai = Phim.Trang_thai;
            Phim_cua_Phan_he.Thoi_luong = Phim.Thoi_luong;
            Phim_cua_Phan_he.Danh_sach_Suat_chieu = Phim.Danh_sach_Suat_chieu;
            Phim_cua_Phan_he.Danh_sach_Dat_ve = Phim.Danh_sach_Dat_ve;
            Phim_cua_Phan_he.Danh_sach_Ban_ve = Phim.Danh_sach_Ban_ve;
            Phim_cua_Phan_he.Doanh_thu = Phim.Doanh_thu;
        });
        return Du_lieu_Phan_he;
    }
    public XL_DU_LIEU Tao_Du_lieu_Phan_he_Quan_ly_Nhan_vien()
    {
        var Du_lieu_Phan_he = new XL_DU_LIEU();
        Du_lieu_Phan_he.Cong_ty = Du_lieu_Dich_vu.Cong_ty;
        Du_lieu_Phan_he.Danh_sach_Nguoi_dung_Noi_bo = Du_lieu_Dich_vu.Danh_sach_Nguoi_dung_Noi_bo;
        return Du_lieu_Phan_he;
    }
    public XL_DU_LIEU Tao_Du_lieu_Phan_he_Quan_ly_Phim()
    {
        var Du_lieu_Phan_he = new XL_DU_LIEU();
        Du_lieu_Phan_he.Cong_ty = Du_lieu_Dich_vu.Cong_ty;
        Du_lieu_Phan_he.Danh_sach_Nguoi_dung_Noi_bo = Du_lieu_Dich_vu.Danh_sach_Nguoi_dung_Noi_bo;

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
            Phim_cua_Phan_he.Nha_san_xuat = Phim.Nha_san_xuat;
            Phim_cua_Phan_he.Dien_vien = Phim.Dien_vien;
            Phim_cua_Phan_he.Noi_dung = Phim.Noi_dung;
            Phim_cua_Phan_he.The_loai = Phim.The_loai;
            Phim_cua_Phan_he.Dich_thuat = Phim.Dich_thuat;
            Phim_cua_Phan_he.Don_gia = Phim.Don_gia;
            Phim_cua_Phan_he.Trang_thai = Phim.Trang_thai;
            Phim_cua_Phan_he.Thoi_luong = Phim.Thoi_luong;
            Phim_cua_Phan_he.Danh_sach_Suat_chieu = Phim.Danh_sach_Suat_chieu;
            Phim_cua_Phan_he.Doanh_thu = Phim.Doanh_thu;
        });
        return Du_lieu_Phan_he;
    }

    public string Ghi_Dat_ve_Moi(string Ma_so_Phim, XL_DAT_VE Dat_ve)
    {
        var Phim = Danh_sach_Phim.FirstOrDefault(x => x.Ma_so == Ma_so_Phim);
        var Chuoi_Kq_Ghi = XL_DU_LIEU.Ghi_Dat_ve_Moi(Phim, Dat_ve);
        return Chuoi_Kq_Ghi;
    }
    public string Ghi_Ban_ve_Moi(string Ma_so_Phim, XL_BAN_VE Ban_ve)
    {
        var Phim = Danh_sach_Phim.FirstOrDefault(x => x.Ma_so == Ma_so_Phim);
        var Chuoi_Kq_Ghi = XL_DU_LIEU.Ghi_Ban_ve_Moi(Phim, Ban_ve);
        return Chuoi_Kq_Ghi;
    }
    public string Ghi_Phim(XL_PHIM phim)
    {
        var Chuoi_Kq_Ghi = XL_DU_LIEU.Ghi_Phim(phim);
        return Chuoi_Kq_Ghi;
    }
    public string Ghi_Phim_Moi(XL_PHIM phim)
    {
        var Chuoi_Kq_Ghi = XL_DU_LIEU.Ghi_Phim_Moi(phim);
        return Chuoi_Kq_Ghi;
    }
    public string Xoa_Phim(string Ma_so)
    {
        var Chuoi_Kq_Ghi = XL_DU_LIEU.Xoa_Phim(Ma_so);
        return Chuoi_Kq_Ghi;
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
                   Dat_ve.Trang_thai == "DA_THANH_TOAN"
                   && Dat_ve.Ngay_thanh_toan.Day == Ngay.Day
                   && Dat_ve.Ngay_thanh_toan.Month == Ngay.Month
                   && Dat_ve.Ngay_thanh_toan.Year == Ngay.Year);
        var Doanh_thu_Dat_ve = Danh_sach_Dat_ve_Ngay.Sum(Ban_ve => Ban_ve.Tien);
        var Doanh_thu = Doanh_thu_Ban_ve + Doanh_thu_Dat_ve;
        return Doanh_thu;
    }
    public string Tinh_Ma_so_Phim_moi(List<XL_PHIM> Danh_sach_Phim)
    {
        var query =
            from ph in Danh_sach_Phim
            select int.Parse(ph.Ma_so.Substring(5));
        int max_id = query.Max();

        return "PHIM_" + (max_id + 1);
    }
      

}
//************************* Data-Layers DL **********************************
public partial class XL_DU_LIEU
{
    static DirectoryInfo Thu_muc_Project = new DirectoryInfo(HostingEnvironment.ApplicationPhysicalPath);
    static DirectoryInfo Thu_muc_Du_lieu = Thu_muc_Project.GetDirectories("2-Du_lieu_Luu_tru")[0];
    static DirectoryInfo Thu_muc_Cong_ty = Thu_muc_Du_lieu.GetDirectories("Cong_ty")[0];
    static DirectoryInfo Thu_muc_Nguoi_dung = Thu_muc_Du_lieu.GetDirectories("Nguoi_dung")[0];
    static DirectoryInfo Thu_muc_Nguoi_dung_Noi_bo = Thu_muc_Nguoi_dung.GetDirectories("Noi_bo")[0];
    static DirectoryInfo Thu_muc_Nguoi_dung_Khach_tham_quan = Thu_muc_Nguoi_dung.GetDirectories("Khach_tham_quan")[0];
    static DirectoryInfo Thu_muc_Phim = Thu_muc_Du_lieu.GetDirectories("Phim")[0];
    //******** Ghi *******
    public static XL_DU_LIEU Doc_Du_lieu_Luu_tru()
    {
        var Du_lieu_Luu_tru = new XL_DU_LIEU();
        Du_lieu_Luu_tru.Cong_ty = Doc_Danh_sach_Cong_ty()[0];
        Du_lieu_Luu_tru.Danh_sach_Nguoi_dung_Khach_tham_quan = Doc_Danh_sach_Nguoi_dung_Khach_tham_quan();
        Du_lieu_Luu_tru.Danh_sach_Nguoi_dung_Noi_bo = Doc_Danh_sach_Nguoi_dung_Noi_bo();
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
    static List<XL_NGUOI_DUNG_NOI_BO> Doc_Danh_sach_Nguoi_dung_Noi_bo()
    {
        var Danh_sach_Nguoi_dung = new List<XL_NGUOI_DUNG_NOI_BO>();
        var Danh_sach_Tap_tin = Thu_muc_Nguoi_dung_Noi_bo.GetFiles("*.json").ToList();
        Danh_sach_Tap_tin.ForEach(Tap_tin =>
        {
            var Duong_dan = Tap_tin.FullName;
            var Chuoi_JSON = File.ReadAllText(Duong_dan);
            var Nguoi_dung = Json.Decode<XL_NGUOI_DUNG_NOI_BO>(Chuoi_JSON);
            Danh_sach_Nguoi_dung.Add(Nguoi_dung);
        });
        return Danh_sach_Nguoi_dung;
    }
    static List<XL_NGUOI_DUNG_KHACH_THAM_QUAN> Doc_Danh_sach_Nguoi_dung_Khach_tham_quan()
    {
        var Danh_sach_Nguoi_dung = new List<XL_NGUOI_DUNG_KHACH_THAM_QUAN>();
        var Danh_sach_Tap_tin = Thu_muc_Nguoi_dung_Khach_tham_quan.GetFiles("*.json").ToList();
        Danh_sach_Tap_tin.ForEach(Tap_tin =>
        {
            var Duong_dan = Tap_tin.FullName;
            var Chuoi_JSON = File.ReadAllText(Duong_dan);
            var Nguoi_dung = Json.Decode<XL_NGUOI_DUNG_KHACH_THAM_QUAN>(Chuoi_JSON);
            Danh_sach_Nguoi_dung.Add(Nguoi_dung);
        });
        return Danh_sach_Nguoi_dung;
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
               Dat_ve.Trang_thai == "DAT_VE" && DateTime.Compare(DateTime.Now, Dat_ve.Ngay_dat) < 0);
            Danh_sach_Dat_ve_Khong_den_Tinh_tien.ForEach(x =>
            {
                Phim.Danh_sach_Dat_ve.Remove(x);
            });
        });
        return Danh_sach_Phim;
    }

    //******** Ghi *******

    public static string Ghi_Dat_ve_Moi(XL_PHIM Phim, XL_DAT_VE Dat_ve)
    {
        var Kq = "";
        Phim.Danh_sach_Dat_ve.Add(Dat_ve);
        var Suat_chieu = Phim.Danh_sach_Suat_chieu.FirstOrDefault(x => x.Ma_so == Dat_ve.Suat_chieu.Ma_so);
        Suat_chieu.Danh_sach_Ghe_trong.RemoveAll(Ghe_trong => Dat_ve.Danh_sach_Ghe_dat.Any(Ghe_dat => Ghe_dat.Ma_so == Ghe_trong.Ma_so));
        Kq = Ghi_Phim(Phim);
        if (Kq != "OK")
        {
            Phim.Danh_sach_Dat_ve.Remove(Dat_ve);
            Dat_ve.Danh_sach_Ghe_dat.ForEach(Ghe_dat => Suat_chieu.Danh_sach_Ghe_trong.Add(Ghe_dat));
        }

        return Kq;
    }
    public static string Ghi_Ban_ve_Moi(XL_PHIM Phim, XL_BAN_VE Ban_ve)
    {
        var Kq = "";
        Phim.Danh_sach_Ban_ve.Add(Ban_ve);
        var Suat_chieu = Phim.Danh_sach_Suat_chieu.FirstOrDefault(x => x.Ma_so == Ban_ve.Suat_chieu.Ma_so);
        Suat_chieu.Danh_sach_Ghe_trong.RemoveAll(Ghe_trong => Ban_ve.Danh_sach_Ghe_ban.Any(Ghe_dat => Ghe_dat.Ma_so == Ghe_trong.Ma_so));
        Kq = Ghi_Phim(Phim);
        if (Kq != "OK")
        {
            Phim.Danh_sach_Ban_ve.Remove(Ban_ve);
            Ban_ve.Danh_sach_Ghe_ban.ForEach(Ghe_ban => Suat_chieu.Danh_sach_Ghe_trong.Add(Ghe_ban));
        }

        return Kq;
    }
    public static string Ghi_Phim(XL_PHIM Phim)
    {
        var Kq = "";
        var Duong_dan = $"{Thu_muc_Phim.FullName}\\{Phim.Ma_so}.json";
        var Chuoi_JSON = Json.Encode(Phim);
        try
        {
            File.WriteAllText(Duong_dan, Chuoi_JSON);
            Kq = "OK";

        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }
        return Kq;

    }
    public static string Ghi_Phim_Moi(XL_PHIM Phim)
    {
        var Kq = "";
        var Duong_dan = $"{Thu_muc_Phim.FullName}\\{Phim.Ma_so}.json";

        if (File.Exists(Duong_dan))
            return "Phim đã tồn  tại.";

        var Chuoi_JSON = Json.Encode(Phim);
        using (var stream = new StreamWriter(Duong_dan))
        {
            try
            {
                stream.Write(Chuoi_JSON);
                Kq = "OK";
            }
            catch (Exception Loi)
            {
                Kq = Loi.Message;
            }
        }

        if (Kq != "OK")
        {
            File.Delete(Duong_dan);
        }
        return Kq;
    }
    public static string Xoa_Phim(string Ma_so_Phim_Xoa)
    {
        var Kq = "";
        var Duong_dan = $"{Thu_muc_Phim.FullName}\\{Ma_so_Phim_Xoa}.json";

        if (!File.Exists(Duong_dan))
            return "Phim không tồn tại.";

        try
        {
            File.Delete(Duong_dan);
            Kq = "OK";
        }
        catch (Exception Loi)
        {
            Kq = Loi.Message;
        }

        return Kq;
    }
}