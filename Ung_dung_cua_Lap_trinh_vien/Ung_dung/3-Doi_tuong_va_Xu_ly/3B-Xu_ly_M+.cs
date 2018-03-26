using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Globalization;
using System.Net;

public partial class XL_UNG_DUNG
{
    static XL_UNG_DUNG Ung_dung = null;

    XL_DU_LIEU Du_lieu_Ung_dung = null;
    List<XL_LAP_TRINH_VIEN> Danh_sach_Nguoi_dung = new List<XL_LAP_TRINH_VIEN>();
    
    public static XL_UNG_DUNG Khoi_dong_Ung_dung()
    {   if (Ung_dung == null)
        {
            Ung_dung = new XL_UNG_DUNG();
            Ung_dung.Khoi_dong_Du_lieu_Ung_dung();
        }
        else
        {
           
        }
        return  Ung_dung;
    }
    void Khoi_dong_Du_lieu_Ung_dung()
    {
        var Du_lieu_tu_Dich_vu = XL_DU_LIEU.Doc_Du_lieu();
        Du_lieu_Ung_dung = Du_lieu_tu_Dich_vu;
         
    }  
    public XL_LAP_TRINH_VIEN Khoi_dong_Lap_trinh_vien()
    {
        var Lap_trinh_vien= (XL_LAP_TRINH_VIEN)HttpContext.Current.Session["Lap_trinh_vien"];
        if (Lap_trinh_vien == null)
        {
            Lap_trinh_vien = new XL_LAP_TRINH_VIEN();
            Lap_trinh_vien.Danh_sach_Nguoi_dung = Ung_dung.Du_lieu_Ung_dung.Danh_sach_Nguoi_dung;
           
            HttpContext.Current.Session["Lap_trinh_vien"] = Lap_trinh_vien;
        }
        
        return Lap_trinh_vien;
    }
    // Xử lý Chức năng của Lập trình viên :
    public string Khoi_dong_MH_Chinh()
    {
        var Lap_trinh_vien = (XL_LAP_TRINH_VIEN)HttpContext.Current.Session["Lap_trinh_vien"];
        
        Lap_trinh_vien.Danh_sach_Nguoi_dung_Xem = Lap_trinh_vien.Danh_sach_Nguoi_dung;
        Lap_trinh_vien.Danh_sach_Nguoi_dung_Chon = new List<XL_NGUOI_DUNG>();

        var Chuoi_HTML =  Tao_Chuoi_HTML_Xem() ;
        return Chuoi_HTML;
    }
    public string Chon_Nguoi_dung(string Ma_so_Nguoi_dung)
    {
        var Lap_trinh_vien = (XL_LAP_TRINH_VIEN)HttpContext.Current.Session["Lap_trinh_vien"];
        // Xử lý 
        var Nguoi_dung = Lap_trinh_vien.Danh_sach_Nguoi_dung.FirstOrDefault(x => x.Ma_so == Ma_so_Nguoi_dung);
        var Chuoi_HTML = "";
        if (Nguoi_dung.Nhom_Nguoi_dung.Ma_so == "NHAN_VIEN_BAN_VE")
        {
            var Dia_chi_MH_Dang_nhap = "http://localhost:59613/1-Dich_vu_Giao_tiep/DV_Nhan_vien_Ban_ve.cshtml";
            var Tham_so = $"Th_Ma_so_Chuc_nang=DANG_NHAP&Th_Ten_Dang_nhap={Nguoi_dung.Ten_Dang_nhap}" +
                $"&Th_Mat_khau={Nguoi_dung.Mat_khau}";
            var Dia_chi_Xu_ly = $"{Dia_chi_MH_Dang_nhap}?{Tham_so}";
            Chuoi_HTML = $"<iframe " +
                        $"src='{Dia_chi_Xu_ly}' " +
                         $"style='width:100%;height:1000vh;border:none'" +
                $"></iframe>";
        }
        else
        {
            Chuoi_HTML = "Sẽ xử lý Đăng nhập " + Nguoi_dung.Nhom_Nguoi_dung.Ten;
        }
        
        return Chuoi_HTML;
    }
    public string Tra_cuu_Nguoi_dung(string Chuoi_Tra_cuu)
    {
        var Lap_trinh_vien = (XL_LAP_TRINH_VIEN)HttpContext.Current.Session["Lap_trinh_vien"]; 
        Lap_trinh_vien.Danh_sach_Nguoi_dung_Xem =  Tra_cuu_Nguoi_dung(Chuoi_Tra_cuu , Lap_trinh_vien.Danh_sach_Nguoi_dung);
        var Chuoi_HTML = Tao_Chuoi_HTML_Xem();
        return Chuoi_HTML;
    }
 
    //======= 
    public string Tao_Chuoi_HTML_Xem()
    {
        var Lap_trinh_vien = (XL_LAP_TRINH_VIEN)HttpContext.Current.Session["Lap_trinh_vien"];
        var Chuoi_HTML = $"<div>" +
                 $"{ Tao_Chuoi_HTML_Danh_sach_Nguoi_dung_Xem()}" +
             $"</div>";
        return Chuoi_HTML;
    }
}

//************************* View-Layers/Presentation Layers VL/PL **********************************
public partial class XL_UNG_DUNG
{    
    public  string Dia_chi_Media = $"{XL_DU_LIEU.Dia_chi_Dich_vu}/Media";
    public  CultureInfo Dinh_dang_VN = CultureInfo.GetCultureInfo("vi-VN");

    

    public string Tao_Chuoi_HTML_Danh_sach_Nguoi_dung_Xem()
    {
        var Lap_trinh_vien = (XL_LAP_TRINH_VIEN)HttpContext.Current.Session["Lap_trinh_vien"];
        var Danh_sach = Lap_trinh_vien.Danh_sach_Nguoi_dung_Xem;
        var Chuoi_HTML_Danh_sach = "<div class='row'>";
        Danh_sach.ForEach(Nguoi_dung =>
        {

            var Chuoi_Hinh = $"<img src='{Dia_chi_Media}/{Nguoi_dung.Ma_so}.png' " +
                             "style='width:90px;height:90px;' />";
            var Chuoi_Chuc_nang_Chon = $"<form method='post'>" +
                      $"<input name='Th_Ma_so_Chuc_nang' type='hidden' value='{Lap_trinh_vien.Chuc_nang_Chon_Nguoi_dung.Ma_so}' />" +
                       $"<input name='Th_Ma_so_Nguoi_dung' type='hidden' value='{Nguoi_dung.Ma_so}' />" +
                       $"<button type='submit' class='btn btn-danger' >Chọn</button>" +
                  "</form>";
          
            var Chuoi_Thong_tin = $"<div class='btn' style='text-align:left'> " +
                          $"{Nguoi_dung.Ho_ten}" +
                          $"<br />{ Nguoi_dung.Nhom_Nguoi_dung.Ten}" +
                           $" { Chuoi_Chuc_nang_Chon}" +
                          $"</div>";


            var Chuoi_HTML = $"<div class='col-md-5' style='margin-bottom:10px' >" +
                               $"{Chuoi_Hinh}" + $"{Chuoi_Thong_tin}" +

                             "</div>";
            Chuoi_HTML_Danh_sach += Chuoi_HTML;
        });
        Chuoi_HTML_Danh_sach += "</div>";
        return Chuoi_HTML_Danh_sach;
    }
  



     
}
//************************* Business-Layers BL **********************************
public partial class XL_UNG_DUNG
{    
    public   List<XL_NGUOI_DUNG> Tra_cuu_Nguoi_dung(
         string Chuoi_Tra_cuu, List<XL_NGUOI_DUNG> Danh_sach)
    {
        Danh_sach = Danh_sach.FindAll(
              Tivi => Tivi.Ho_ten.ToUpper().Contains(Chuoi_Tra_cuu.ToUpper())
         || Tivi.Nhom_Nguoi_dung.Ma_so.Contains(Chuoi_Tra_cuu ));
        return Danh_sach;
    }
    
}
//************************* Data-Layers DL **********************************
public partial class XL_DU_LIEU
{
    public static string Dia_chi_Dich_vu = "http://localhost:50900";
    static string Dia_chi_Dich_vu_Du_lieu = $"{Dia_chi_Dich_vu}/1-Dich_vu_Giao_tiep/DV_Chinh.cshtml";

    public static XL_DU_LIEU Doc_Du_lieu()
    {
         
        var Xu_ly = new WebClient();
        Xu_ly.Encoding = System.Text.Encoding.UTF8;
        var Tham_so = "Ma_so_Xu_ly=KHOI_DONG_DU_LIEU_KHACH_THAM_QUAN";
        var Dia_chi_Xu_ly = $"{Dia_chi_Dich_vu_Du_lieu}?{Tham_so}";
        var Chuoi_JSON = Xu_ly.DownloadString(Dia_chi_Xu_ly);
        var Du_lieu = Json.Decode<XL_DU_LIEU>(Chuoi_JSON);
        return Du_lieu;
    }

   

}