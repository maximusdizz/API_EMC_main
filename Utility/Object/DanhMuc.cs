using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Object
{
    public class DanhMuc
    {
        public int IDDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public DateTime NgayTao { get; set; }
        public int IDLinhVuc { get; set; }
    }

    public class LinhVuc
    {
        public int IDLinhVuc { get; set; }
        public string TenLinhVuc { get; set; }
        public DateTime NgayTao { get; set; }
    }
    public class GoiTinDanhMucLinhVuc
    {
        public int MaKhaiThac { get; set; }
        public string Token { get; set; }
    }

    public class GoiTinGuiDuLieuDanhMuc {
        public List<DataDuLieuDanhMuc> DuLieuDanhMuc { get; set; }
        public string Token { get; set; }
    }
    public class DataDuLieuDanhMuc
    {
        public int MaDanhMuc { get; set; }
        public int SoLuongDuLieu { get; set; }
        public int SoLanKhaiThac { get; set; }
    }

    public class GoiTinGuiDuLieuLinhVuc
    {
        public List<DataDuLieuLinhVuc> DuLieuLinhVuc { get; set; }
        public string Token { get; set; }
    }
    public class DataDuLieuLinhVuc
    {
        public int MaLinhVuc { get; set; }
        public int SoLuongDuLieu { get; set; }
        public int SoLanKhaiThac { get; set; }
    }
    public class BangLuyKe
    {
        public int DVCTT_ToanTrinh_tab1 { get; set; }
        public int TTHC_tab1 { get; set; }
        public int DVCTT_tab1 { get; set; }
        //public string SoHoSoTrucTiep_tab2 { get; set; }
        public int SoHoSoTrucTuyen_tab2 { get; set; }
        public int TongSoHoSo_tab2 { get; set; }
        public int SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3 { get; set; }
        //public int SoTTHC_DuaLen_DVCTT_ToanTrinh_CoHoSo_tab3 { get; set; }
        public int SoDVCTT_PhatSinhHoSo_tab3 { get; set; }
        public int SoTTHC_PhatSinhHoSo_tab3 { get; set; }
        public int TongDVCTTPhatSinhHoSo_tab3 { get; set; }
        public int SoHoSoDVCTT { get; set; }
        public int NTT_DVCTT { get; set; }
        public int TongHoSoDVCTT_ToanTrinh { get; set; }
        public int MonthMonitor { get; set; }
        public int YearMonitor { get; set; }
        public string SiteName { get; set; }
        public string TenVungMien { get; set; }
    }
    public class TheoQuy
    {
        // public string DVCTT_ToanTrinh_tab1 { get; set; }
        //public string TTHC_tab1 { get; set; }
        //public string DVCTT_tab1 { get; set; }
        //public string SoHoSoTrucTiep_tab2 { get; set; }
        public int SoHoSoTrucTuyen_tab2 { get; set; }
        public string TongSoHoSo_tab2 { get; set; }
        public int SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3 { get; set; }
        //public int SoTTHC_DuaLen_DVCTT_ToanTrinh_CoHoSo_tab3 { get; set; }
        public int SoDVCTT_PhatSinhHoSo_tab3 { get; set; }
        public int SoTTHC_PhatSinhHoSo_tab3 { get; set; }
        public int TongDVCTTPhatSinhHoSo_tab3 { get; set; }
        public int SoHoSoDVCTT { get; set; }
        public int NTT_DVCTT { get; set; }
        public int TongHoSoDVCTT_ToanTrinh { get; set; }
        //public string MonthMonitor { get; set; }
        public int YearMonitor { get; set; }
        public string SiteName { get; set; }
    }
    public class BangLuyKeRequest
    {
        public int StartMonthMonitor { get; set; }
        public int EndMonthMonitor { get; set; }
        public int YearMonitor { get; set; }
        public int SiteId { get; set; }
        public string Token { get; set; }
    }

    public class BangLuyKeTheoTinhBo
    {
        public int MonthMonitor { get; set; }
        public int YearMonitor { get; set; }
        public string Token { get; set; }
    }

    public class BangLuyKeVungMien
    {
        //public int VungMien1 { get; set; }
        public int MonthMonitor { get; set; }
        public int YearMonitor { get; set; }
        public string Token { get; set; }
    }

    public class BangLuyKeTheoQuy
    {
        public int LayQuy { get; set; }
        public int TinhDiaPhuong { get; set; }
        public int YearMonitor { get; set; }
        public string Token { get; set; }
    }
    public class BangSiteID
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SubRegion { get; set; }
        public string Region { get; set; }
    }
    public class SiteIDRequest
    {
        public string Token { get; set; }
    }
    public class ChiSoDonVi
    {
        public string Name { get; set; }
        public string MonthMonitor { get; set; }
        public string KetQua { get; set; }
    }
    public class ChiSoDonViRequest
    {
        public int YearMonitor { get; set; }
        public int TinhDiaPhuong { get; set; }
        public string ChiSo { get; set; }
        public int MonthMonitor { get; set; }
        public string Token { get; set; }
    }
    public class BangLuyKeTheoThangRequest
    {
        public int YearMonitor { get; set; }
        public int MonthMonitor { get; set; }
        public int TinhDiaPhuong { get; set; }
        public string Token { get; set; }
    }
}
