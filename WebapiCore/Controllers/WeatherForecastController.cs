using DAL;
using Microsoft.AspNetCore.Mvc;
using Utility;
using Utility.Object;

namespace WebapiCore.Controllers
{
    [ApiController]
    [Route("api")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IDapper _dapper;
        private readonly ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDapper dapper)
        {
            _logger = logger;
            _dapper = dapper;
        }

        #region API EMC mới
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> GetTokenAuthen(AccGetToken acc)
        {
            //sửa hảm này để tạo token
            if (acc.TenDangNhap == "emcdb" && acc.MatKhauHeThong == "emc@123567")
            {
                return Ok(new ReturnKQ
                {
                    Token = TokenHelper.GenerateToken(acc.TenDangNhap)
                });
            }
            else
            {
                ReturnCanhBao rt = new ReturnCanhBao();
                rt.ThongTin = "Sai thông tin đăng nhập";
                return Ok(rt);
            }
        }
       
        [HttpPost]
        [Route("SiteID")]
        public async Task<IActionResult> GetSiteIdName(SiteIDRequest rq)
        {
            //sửa hàm này để dùng token bên trên
            var OBAL = new BAL.GetDataBAL(_dapper);
            var dt = new Object();
            if (TokenHelper.ValidationToken(rq.Token))
            {
                dt = await OBAL.LaySiteID();

                if (dt == null)
                {
                    return NotFound();
                }

                return Ok(dt);
            }
            else
            {
                ReturnCanhBao rt = new ReturnCanhBao();
                rt.ThongTin = "Sai Token";
                return Ok(rt);
            }
        }
        [HttpPost]
        [Route("BangLuyKe")]
        public async Task<IActionResult> LayBangLuyKe([FromBody] BangLuyKeRequest request)
        {
            //sửa hàm này để dùng token bên trên
            var OBAL = new BAL.GetDataBAL(_dapper);
            var dt = new Object();
            if (TokenHelper.ValidationToken(request.Token))
            {
                int startMonthMonitor = request.StartMonthMonitor;
                int endMonthMonitor = request.EndMonthMonitor;
                int YearMonitor = request.YearMonitor;
                int SiteId = request.SiteId;

                var bangLuyKeRecords = await OBAL.GetBangLuyKe(startMonthMonitor, endMonthMonitor, YearMonitor, SiteId);

                if (bangLuyKeRecords == null)
                {
                    return NotFound();
                }

                return Ok(bangLuyKeRecords);
            }
            else
            {
                ReturnCanhBao rt = new ReturnCanhBao();
                rt.ThongTin = "Sai Token";
                return Ok(rt);
            }
        }

        [HttpPost]
        [Route("blkTheoTinh")]
        public async Task<IActionResult> GetBLKTheoTinh([FromBody] BangLuyKeTheoTinhBo request)
        {
            var OBAL = new BAL.GetDataBAL(_dapper);
            var dt = new Object();
            if (TokenHelper.ValidationToken(request.Token))
            {
                
                int MonthMonitor = request.MonthMonitor;
                int YearMonitor = request.YearMonitor;

                var bangLuyKeRecords = await OBAL.GetBLK_Tinh(MonthMonitor, YearMonitor);

                if (bangLuyKeRecords == null)
                {
                    return NotFound();
                }

                return Ok(bangLuyKeRecords);
                //return Ok(dt);
            }
            else
            {
                ReturnCanhBao rt = new ReturnCanhBao();
                rt.ThongTin = "Sai Token";
                return Ok(rt);
            }
        }

        [HttpPost]
        [Route("blkTheoBo")]
        public async Task<IActionResult> GetBLKTheoBo([FromBody] BangLuyKeTheoTinhBo request)
        {
            var OBAL = new BAL.GetDataBAL(_dapper);
            var dt = new Object();
            if (TokenHelper.ValidationToken(request.Token))
            {

                int MonthMonitor = request.MonthMonitor;
                int YearMonitor = request.YearMonitor;

                var bangLuyKeRecords = await OBAL.GetBLK_Bo(MonthMonitor, YearMonitor);

                if (bangLuyKeRecords == null)
                {
                    return NotFound();
                }

                return Ok(bangLuyKeRecords);
                //return Ok(dt);
            }
            else
            {
                ReturnCanhBao rt = new ReturnCanhBao();
                rt.ThongTin = "Sai Token";
                return Ok(rt);
            }
        }

        [HttpPost]
        [Route("blkToanQuoc")]
        public async Task<IActionResult> GetBLKTheoToanQuoc([FromBody] BangLuyKeTheoTinhBo request)
        {
            var OBAL = new BAL.GetDataBAL(_dapper);
            var dt = new Object();
            if (TokenHelper.ValidationToken(request.Token))
            {

                int MonthMonitor = request.MonthMonitor;
                int YearMonitor = request.YearMonitor;

                var bangLuyKeRecords = await OBAL.GetBLK_ToanQuoc(MonthMonitor, YearMonitor);

                if (bangLuyKeRecords == null)
                {
                    return NotFound();
                }

                return Ok(bangLuyKeRecords);
                //return Ok(dt);
            }
            else
            {
                ReturnCanhBao rt = new ReturnCanhBao();
                rt.ThongTin = "Sai Token";
                return Ok(rt);
            }
        }

        [HttpPost]
        [Route("blkVung")]
        public async Task<IActionResult> GetBLKTheoVung([FromBody] BangLuyKeVungMien request)
        {
            var OBAL = new BAL.GetDataBAL(_dapper);
            var dt = new Object();
            if (TokenHelper.ValidationToken(request.Token))
            {

                int MonthMonitor = request.MonthMonitor;
                int YearMonitor = request.YearMonitor;
               // int VungMien1 = request.VungMien1;

                var bangLuyKeRecords = await OBAL.GetBLK_Vung(MonthMonitor, YearMonitor);

                if (bangLuyKeRecords == null)
                {
                    return NotFound();
                }

                return Ok(bangLuyKeRecords);
                //return Ok(dt);
            }
            else
            {
                ReturnCanhBao rt = new ReturnCanhBao();
                rt.ThongTin = "Sai Token";
                return Ok(rt);
            }
        }

        [HttpPost]
        [Route("blkMien")]
        public async Task<IActionResult> GetBLKTheoMien([FromBody] BangLuyKeVungMien request)
        {
            var OBAL = new BAL.GetDataBAL(_dapper);
            var dt = new Object();
            if (TokenHelper.ValidationToken(request.Token))
            {

                int MonthMonitor = request.MonthMonitor;
                int YearMonitor = request.YearMonitor;
               // int VungMien1 = request.VungMien1;

                var bangLuyKeRecords = await OBAL.GetBLK_Mien(MonthMonitor, YearMonitor);

                if (bangLuyKeRecords == null)
                {
                    return NotFound();
                }

                return Ok(bangLuyKeRecords);
                //return Ok(dt);
            }
            else
            {
                ReturnCanhBao rt = new ReturnCanhBao();
                rt.ThongTin = "Sai Token";
                return Ok(rt);
            }
        }
        [HttpPost]
        [Route("blkTheoQuy")]
        public async Task<IActionResult> GetBLKTheoQuy([FromBody] BangLuyKeTheoQuy request)
        {
            var OBAL = new BAL.GetDataBAL(_dapper);
            var dt = new Object();
            if (TokenHelper.ValidationToken(request.Token))
            {
                int LayQuy = request.LayQuy;                
                int YearMonitor = request.YearMonitor;
                int TinhDiaPhuong = request.TinhDiaPhuong;

                var bangLuyKeRecords = await OBAL.GetBLK_TheoQuy(LayQuy, YearMonitor, TinhDiaPhuong);

                if (bangLuyKeRecords == null)
                {
                    return NotFound();
                }

                return Ok(bangLuyKeRecords);
                //return Ok(dt);
            }
            else
            {
                ReturnCanhBao rt = new ReturnCanhBao();
                rt.ThongTin = "Sai Token";
                return Ok(rt);
            }
        }

        [HttpPost]
        [Route("chiSoTheoDonVi")]
        public async Task<IActionResult> GetChiSoDonVi([FromBody] ChiSoDonViRequest request)
        {
            var OBAL = new BAL.GetDataBAL(_dapper);
            var dt = new Object();
            if (TokenHelper.ValidationToken(request.Token))
            {
                int MonthMonitor = request.MonthMonitor;
                string ChiSo = request.ChiSo;
                int YearMonitor = request.YearMonitor;
                int TinhDiaPhuong = request.TinhDiaPhuong;

                var bangLuyKeRecords = await OBAL.GetChiSoDonVi(YearMonitor, TinhDiaPhuong,ChiSo,MonthMonitor);

                if (bangLuyKeRecords == null)
                {
                    return NotFound();
                }

                return Ok(bangLuyKeRecords);
            }
            else
            {
                ReturnCanhBao rt = new ReturnCanhBao();
                rt.ThongTin = "Sai Token";
                return Ok(rt);
            }
        }

        [HttpPost]
        [Route("BangLuyKeTheoThang")]
        public async Task<IActionResult> GetBangLuyKeTheoThang([FromBody] BangLuyKeTheoThangRequest request)
        {
            var OBAL = new BAL.GetDataBAL(_dapper);
            var dt = new Object();
            if (TokenHelper.ValidationToken(request.Token))
            {
                int MonthMonitor = request.MonthMonitor;
                int YearMonitor = request.YearMonitor;
                int TinhDiaPhuong = request.TinhDiaPhuong;

                var bangLuyKeRecords = await OBAL.GetBangLuyKeTheoThang(MonthMonitor, YearMonitor, TinhDiaPhuong);

                if (bangLuyKeRecords == null)
                {
                    return NotFound();
                }

                return Ok(bangLuyKeRecords);
            }
            else
            {
                ReturnCanhBao rt = new ReturnCanhBao();
                rt.ThongTin = "Sai Token";
                return Ok(rt);
            }
        }
        #endregion
        #region
        //[HttpPost]
        //[Route("danhmuc-linhvuc")]
        //public async Task<IActionResult> GetDataDP(GoiTinDanhMucLinhVuc dmlv)
        //{
        //    var OBAL = new BAL.GetDataBAL(_dapper);
        //    var dt = new Object();
        //    if (await OBAL.CheckToken(dmlv.Token))
        //    {
        //        if (dmlv.MaKhaiThac == 1)
        //        {
        //            dt = await OBAL.LayDanhMuc();
        //        }
        //        else if (dmlv.MaKhaiThac == 2)
        //        {
        //            dt = await OBAL.LayLinhVuc();
        //        }
        //    }
        //    else
        //    {
        //        ReturnCanhBao rt = new ReturnCanhBao();
        //        dt = rt.ThongTin = "Sai hoặc không có Token";
        //    }
        //    return Ok(dt);
        //}

        //[HttpPost]
        //[Route("gettoken")]
        //public async Task<IActionResult> GetToKen(AccGetToken acc)
        //{
        //    string input = Utility.Crypton.CreateMD5(acc.TenDangNhap + acc.MatKhauHeThong);
        //    ReturnKQ kq = new ReturnKQ();
        //    kq.Token = input;
        //    return Ok(kq);
        //}

        //[HttpPost]
        //[Route("guidulieudanhmuc")]
        //public async Task<IActionResult> GuiDulieuDanhMuc(GoiTinGuiDuLieuDanhMuc dldm)
        //{
        //    var OBAL = new BAL.GetDataBAL(_dapper);
        //    var dt = new Object();
        //    if (await OBAL.CheckToken(dldm.Token))
        //    {
        //        try
        //        {
        //            var result = await OBAL.ThemDuLieuDanhMuc(dldm.DuLieuDanhMuc);

        //            if (result > 0)
        //            {
        //                dt = "Dữ liệu danh mục đã được thêm thành công.";
        //                return Ok(dt);
        //            }
        //            else
        //            {
        //                ReturnCanhBao rt = new ReturnCanhBao();
        //                dt = rt.ThongTin = "Thêm dữ liệu danh mục thất bại.";
        //                return BadRequest(dt);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ReturnCanhBao rt = new ReturnCanhBao();
        //            dt = rt.ThongTin = "Đã xảy ra lỗi khi xử lý dữ liệu danh mục.";
        //            return StatusCode(500, dt);
        //        }
        //    }
        //    else
        //    {
        //        ReturnCanhBao rt = new ReturnCanhBao();
        //        dt = rt.ThongTin = "Sai hoặc không có Token";
        //        return BadRequest(dt);

        //    }
        //}


        //[HttpPost]
        //[Route("guidulieulinhvuc")]
        //public async Task<IActionResult> GuiDuLieuLinhVuc(GoiTinGuiDuLieuLinhVuc dllv)
        //{
        //    var OBAL = new BAL.GetDataBAL(_dapper);
        //    var dt = new Object();
        //    if (await OBAL.CheckToken(dllv.Token))
        //    {
        //        try
        //        {
        //            var result = await OBAL.ThemDuLieuLinhVuc(dllv.DuLieuLinhVuc);

        //            if (result > 0)
        //            {
        //                dt = "Dữ liệu lĩnh vực đã được thêm thành công.";
        //                return Ok(dt);
        //            }
        //            else
        //            {
        //                ReturnCanhBao rt = new ReturnCanhBao();
        //                dt = rt.ThongTin = "Thêm dữ liệu lĩnh vực thất bại.";
        //                return BadRequest(dt);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ReturnCanhBao rt = new ReturnCanhBao();
        //            dt = rt.ThongTin = "Đã xảy ra lỗi khi xử lý dữ liệu lĩnh vực.";
        //            return StatusCode(500, dt);
        //        }
        //    }
        //    else
        //    {
        //        ReturnCanhBao rt = new ReturnCanhBao();
        //        dt = rt.ThongTin = "Sai hoặc không có Token";
        //        return BadRequest(dt);

        //    }
        //}
        #endregion
        //class object
        public class AccGetToken
        {
            public string TenDangNhap { set; get; }
            public string MatKhauHeThong { set; get; }
        }
        public class ReturnKQ
        {
            public string Token { set; get; }
        }
        public class ReturnCanhBao
        {
            public string ThongTin { set; get; }
        }

    }
}