using DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Object;

namespace BAL
{
    public class GetDataBAL
    {
        private readonly IDapper _dapper;
        public GetDataBAL(IDapper dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<Utility.Object.DanhMuc>> LayDanhMuc()
        {
            //get nhieu
            var result = await _dapper.GetAllAsync<Utility.Object.DanhMuc>($"Select * from DanhMuc", null, commandType: CommandType.Text);
            return result;
        }
        public async Task<IEnumerable<Utility.Object.LinhVuc>> LayLinhVuc()
        {
            //get nhieu
            var result = await _dapper.GetAllAsync<Utility.Object.LinhVuc>($"Select * from LinhVuc", null, commandType: CommandType.Text);
            return result;
        }

        //Lấy SiteID
        public async Task<IEnumerable<Utility.Object.BangSiteID>> LaySiteID()
        {
            var query = @"
            SELECT 
                stw.Id, 
                stw.Name, 
                bvm.TenVungMien AS 'SubRegion',
                (SELECT bvm2.TenVungMien 
                 FROM Bang_VungMien AS bvm2 
                 WHERE stw.VungMien2 = bvm2.MaMien) AS 'Region'
            FROM 
                SiteW AS stw 
            LEFT JOIN 
                Bang_VungMien AS bvm ON bvm.MaVung = stw.VungMien1";

            var result = await _dapper.GetAllAsync<BangSiteID>(query, null, commandType: CommandType.Text);
            return result;
        }

        public async Task<int> ThemDuLieuDanhMuc(List<DataDuLieuDanhMuc> danhMucList)
        {
            try
            {
                foreach (var danhMuc in danhMucList)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MaDanhMuc", danhMuc.MaDanhMuc, DbType.Int32);
                    parameters.Add("@SoLuongDuLieu", danhMuc.SoLuongDuLieu, DbType.Int32);
                    parameters.Add("@SoLanKhaiThac", danhMuc.SoLanKhaiThac, DbType.Int32);

                    await _dapper.ExcuteAsyncDemo("INSERT INTO ThuDanhMuc (MaDanhMuc, SoLuongDuLieu, SoLanKhaiThac) VALUES (@MaDanhMuc, @SoLuongDuLieu, @SoLanKhaiThac)", parameters);
                }

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> ThemDuLieuLinhVuc(List<DataDuLieuLinhVuc> LinhVucList)
        {
            try
            {
                foreach (var LinhVuc in LinhVucList)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@MaLinhVuc", LinhVuc.MaLinhVuc, DbType.Int32);
                    parameters.Add("@SoLuongDuLieu", LinhVuc.SoLuongDuLieu, DbType.Int32);
                    parameters.Add("@SoLanKhaiThac", LinhVuc.SoLanKhaiThac, DbType.Int32);

                    await _dapper.ExcuteAsyncDemo("INSERT INTO ThuLinhVuc (MaLinhVuc, SoLuongDuLieu, SoLanKhaiThac) VALUES (@MaLinhVuc, @SoLuongDuLieu, @SoLanKhaiThac)", parameters);
                }

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return 0;
            }
        }

        // Select Bảng Lũy Kế
        public async Task<IEnumerable<Utility.Object.BangLuyKe>> GetBangLuyKe(int startMonthMonitor, int endMonthMonitor, int yearMonitor, int siteId)
        {
            var query = @"
        SELECT
            DVCTT_ToanTrinh_tab1,
            TTHC_tab1,
            DVCTT_tab1,           
            SoHoSoTrucTuyen_tab2,       
            TongSoHoSo_tab2,
            SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3,
            SoDVCTT_PhatSinhHoSo_tab3,
            SoTTHC_PhatSinhHoSo_tab3,
            TongDVCTTPhatSinhHoSo_tab3,
            SoHoSoDVCTT,
            NTT_DVCTT,
            TongHoSoDVCTT_ToanTrinh,
            MonthMonitor,
            YearMonitor,
            SiteW.Name AS SiteName
        FROM 
            BangLuyKe
        INNER JOIN SiteW on SiteW.ID = BangLuyKe.SiteID 
        WHERE 
            MonthMonitor BETWEEN @StartMonthMonitor AND @EndMonthMonitor
            AND YearMonitor = @YearMonitor
            AND SiteID = @SiteID";

            var parameters = new DynamicParameters();
            parameters.Add("@StartMonthMonitor", startMonthMonitor, DbType.Int32);
            parameters.Add("@EndMonthMonitor", endMonthMonitor, DbType.Int32);
            parameters.Add("@YearMonitor", yearMonitor, DbType.Int32);
            parameters.Add("@SiteID", siteId, DbType.Int32);

            var result = await _dapper.GetAllAsync<Utility.Object.BangLuyKe>(query, parameters, commandType: CommandType.Text);
            return result;
        }
        // Select Bảng Lũy Kế theo Bộ
        public async Task<IEnumerable<Utility.Object.BangLuyKe>> GetBLK_Bo(int monthMonitor, int yearMonitor)
        {
            var query = @"
        SELECT
            DVCTT_ToanTrinh_tab1,
            TTHC_tab1,
            DVCTT_tab1,           
            SoHoSoTrucTuyen_tab2, 
            TongSoHoSo_tab2,
            SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3,
            SoDVCTT_PhatSinhHoSo_tab3,
            SoTTHC_PhatSinhHoSo_tab3,
            TongDVCTTPhatSinhHoSo_tab3,
            SoHoSoDVCTT,
            NTT_DVCTT,
            TongHoSoDVCTT_ToanTrinh,
            MonthMonitor,
            YearMonitor,
            SiteW.Name AS SiteName
        FROM 
            BangLuyKe
        INNER JOIN SiteW on SiteW.ID = BangLuyKe.SiteID        
        WHERE
            MonthMonitor = @MonthMonitor
            AND YearMonitor = @YearMonitor
            AND SiteW.TinhDiaPhuong = 1";

            var parameters = new DynamicParameters();
            parameters.Add("@MonthMonitor", monthMonitor, DbType.Int32);
            parameters.Add("@YearMonitor", yearMonitor, DbType.Int32);

            var result = await _dapper.GetAllAsync<Utility.Object.BangLuyKe>(query, parameters, commandType: CommandType.Text);
            return result;
        }

        // Select Bảng Lũy Kế theo Tỉnh
        public async Task<IEnumerable<Utility.Object.BangLuyKe>> GetBLK_Tinh(int monthMonitor, int yearMonitor)
        {
            var query = @"
        SELECT
            DVCTT_ToanTrinh_tab1,
            TTHC_tab1,
            DVCTT_tab1,           
            SoHoSoTrucTuyen_tab2,     
            TongSoHoSo_tab2,
            SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3,           
            SoDVCTT_PhatSinhHoSo_tab3,
            SoTTHC_PhatSinhHoSo_tab3,
            TongDVCTTPhatSinhHoSo_tab3,
            SoHoSoDVCTT,
            NTT_DVCTT,
            TongHoSoDVCTT_ToanTrinh,
            MonthMonitor,
            YearMonitor,
            SiteW.Name AS SiteName
        FROM 
            BangLuyKe
        INNER JOIN SiteW on SiteW.ID = BangLuyKe.SiteID        
        WHERE
            MonthMonitor = @MonthMonitor
            AND YearMonitor = @YearMonitor
            AND SiteW.TinhDiaPhuong = 2";

            var parameters = new DynamicParameters();
            parameters.Add("@MonthMonitor", monthMonitor, DbType.Int32);
            parameters.Add("@YearMonitor", yearMonitor, DbType.Int32);

            var result = await _dapper.GetAllAsync<Utility.Object.BangLuyKe>(query, parameters, commandType: CommandType.Text);
            return result;
        }

        public async Task<IEnumerable<Utility.Object.BangLuyKe>> GetBLK_ToanQuoc(int monthMonitor, int yearMonitor)
        {
            var query = @"
        SELECT
            DVCTT_ToanTrinh_tab1,
            TTHC_tab1,
            DVCTT_tab1,           
            SoHoSoTrucTuyen_tab2,      
            TongSoHoSo_tab2,
            SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3,
            SoTTHC_DuaLen_DVCTT_ToanTrinh_CoHoSo_tab3,
            SoDVCTT_PhatSinhHoSo_tab3,
            SoTTHC_PhatSinhHoSo_tab3,
            TongDVCTTPhatSinhHoSo_tab3,
            SoHoSoDVCTT,
            NTT_DVCTT,
            TongHoSoDVCTT_ToanTrinh,
            MonthMonitor,
            YearMonitor,
            SiteW.Name AS SiteName
        FROM 
            BangLuyKe
        INNER JOIN SiteW on SiteW.ID = BangLuyKe.SiteID        
        WHERE
            MonthMonitor = @MonthMonitor
            AND YearMonitor = @YearMonitor
            AND SiteW.TinhDiaPhuong in (1,2)";

            var parameters = new DynamicParameters();
            parameters.Add("@MonthMonitor", monthMonitor, DbType.Int32);
            parameters.Add("@YearMonitor", yearMonitor, DbType.Int32);

            var result = await _dapper.GetAllAsync<Utility.Object.BangLuyKe>(query, parameters, commandType: CommandType.Text);
            return result;
        }

        public async Task<IEnumerable<Utility.Object.BangLuyKe>> GetBLK_Vung(int monthMonitor, int yearMonitor)
        {
            var query = @"
        SELECT
             DVCTT_ToanTrinh_tab1,
             TTHC_tab1,
             DVCTT_tab1,           
             SoHoSoTrucTuyen_tab2,  
             TongSoHoSo_tab2,
             SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3,          
             SoDVCTT_PhatSinhHoSo_tab3,
             SoTTHC_PhatSinhHoSo_tab3,
             TongDVCTTPhatSinhHoSo_tab3,
             SoHoSoDVCTT,
             NTT_DVCTT,
             TongHoSoDVCTT_ToanTrinh,
             MonthMonitor,
             YearMonitor,
             stw.Name AS SiteName,
             bvm.TenVungMien
         FROM 
             BangLuyKe as blk
         INNER JOIN SiteW as stw on stw.ID = blk.SiteID
         INNER JOIN Bang_VungMien as bvm on bvm.MaVung = stw.VungMien1      
         WHERE
             MonthMonitor = @MonthMonitor
             AND YearMonitor = @YearMonitor
             AND stw.TinhDiaPhuong = 2";

            var parameters = new DynamicParameters();
            parameters.Add("@MonthMonitor", monthMonitor, DbType.Int32);
            parameters.Add("@YearMonitor", yearMonitor, DbType.Int32);
            //parameters.Add("@VungMien1", VungMien1, DbType.Int32);

            var result = await _dapper.GetAllAsync<Utility.Object.BangLuyKe>(query, parameters, commandType: CommandType.Text);
            return result;
        }
        public async Task<IEnumerable<Utility.Object.BangLuyKe>> GetBLK_Mien(int monthMonitor, int yearMonitor)
        {
            var query = @"
        SELECT
             DVCTT_ToanTrinh_tab1,
             TTHC_tab1,
             DVCTT_tab1,           
             SoHoSoTrucTuyen_tab2,  
             TongSoHoSo_tab2,
             SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3,          
             SoDVCTT_PhatSinhHoSo_tab3,
             SoTTHC_PhatSinhHoSo_tab3,
             TongDVCTTPhatSinhHoSo_tab3,
             SoHoSoDVCTT,
             NTT_DVCTT,
             TongHoSoDVCTT_ToanTrinh,
             MonthMonitor,
             YearMonitor,
             stw.Name AS SiteName,
             bvm.TenVungMien
         FROM 
             BangLuyKe as blk
         INNER JOIN SiteW as stw on stw.ID = blk.SiteID
         INNER JOIN Bang_VungMien as bvm on bvm.MaMien = stw.VungMien2      
         WHERE
             MonthMonitor = @MonthMonitor
             AND YearMonitor = @YearMonitor
             AND stw.TinhDiaPhuong = 2";

            var parameters = new DynamicParameters();
            parameters.Add("@MonthMonitor", monthMonitor, DbType.Int32);
            parameters.Add("@YearMonitor", yearMonitor, DbType.Int32);
            //parameters.Add("@VungMien1", VungMien1, DbType.Int32);

            var result = await _dapper.GetAllAsync<Utility.Object.BangLuyKe>(query, parameters, commandType: CommandType.Text);
            return result;
        }
        public async Task<IEnumerable<Utility.Object.BangLuyKe>> GetBLK_TheoQuy(int LayQuy, int yearMonitor, int TinhDiaPhuong)
        {
            var query = "";
            var month1 = 0;
            var month2 = 0;

            if (LayQuy == 1)
            {
                query = @"SELECT           
                            SoHoSoTrucTuyen_tab2,  
                            TongSoHoSo_tab2,
                            SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3,
                            SoDVCTT_PhatSinhHoSo_tab3,
                            SoTTHC_PhatSinhHoSo_tab3,
                            TongDVCTTPhatSinhHoSo_tab3,
                            SoHoSoDVCTT,
                            NTT_DVCTT,
                            TongHoSoDVCTT_ToanTrinh,
                            MonthMonitor,
                            YearMonitor,
                            SiteW.Name AS SiteName
                        FROM 
                            BangLuyKe
                        INNER JOIN SiteW on SiteW.ID = BangLuyKe.SiteID        
                        WHERE
                            MonthMonitor = 3
                            AND YearMonitor = @YearMonitor
                            AND SiteW.TinhDiaPhuong = @TinhDiaPhuong";
            }
            else
            {
                if (LayQuy == 2)
                {
                    month1 = 3;
                    month2 = 6;
                }
                else if (LayQuy == 3)
                {
                    month1 = 6;
                    month2 = 9;
                }
                else if (LayQuy == 3)
                {
                    month1 = 9;
                    month2 = 12;
                }

                query = @"
                select SoHoSoTrucTuyen_tab2 - (select SoHoSoTrucTuyen_tab2 
                                FROM 
                                   BangLuyKe as blk1
                                 INNER JOIN SiteW on SiteW.ID = blk1.SiteID        
                                 WHERE
                                   MonthMonitor =  " + month1 + @"
                                   AND YearMonitor = @YearMonitor
                                   AND blk1.SiteID = blk2. SiteID
                                   ) as SoHoSoTrucTuyen_tab2,
                SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3 - (select SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3 
                                FROM 
                                   BangLuyKe as blk1
                                 INNER JOIN SiteW on SiteW.ID = blk1.SiteID        
                                 WHERE
                                   MonthMonitor = " + month1 + @"
                                   AND YearMonitor = @YearMonitor
                                   AND blk1.SiteID = blk2. SiteID
                                   ) as SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3,
                SoTTHC_DuaLen_DVCTT_ToanTrinh_CoHoSo_tab3 - (select SoTTHC_DuaLen_DVCTT_ToanTrinh_CoHoSo_tab3 
                                FROM 
                                   BangLuyKe as blk1
                                 INNER JOIN SiteW on SiteW.ID = blk1.SiteID        
                                 WHERE
                                   MonthMonitor = " + month1 + @"
                                   AND YearMonitor = @YearMonitor
                                   AND blk1.SiteID = blk2. SiteID
                                   ) as SoTTHC_DuaLen_DVCTT_ToanTrinh_CoHoSo_tab3,
                SoDVCTT_PhatSinhHoSo_tab3 - (select SoDVCTT_PhatSinhHoSo_tab3 
                                FROM 
                                   BangLuyKe as blk1
                                 INNER JOIN SiteW on SiteW.ID = blk1.SiteID        
                                 WHERE
                                   MonthMonitor = " + month1 + @"
                                   AND YearMonitor = @YearMonitor
                                   AND blk1.SiteID = blk2. SiteID
                                   ) as SoDVCTT_PhatSinhHoSo_tab3,
                SoTTHC_PhatSinhHoSo_tab3 - (select SoTTHC_PhatSinhHoSo_tab3 
                                FROM 
                                   BangLuyKe as blk1
                                 INNER JOIN SiteW on SiteW.ID = blk1.SiteID        
                                 WHERE
                                   MonthMonitor = " + month1 + @"
                                   AND YearMonitor = @YearMonitor
                                   AND blk1.SiteID = blk2. SiteID
                                   ) as SoTTHC_PhatSinhHoSo_tab3,
                TongDVCTTPhatSinhHoSo_tab3 - (select TongDVCTTPhatSinhHoSo_tab3 
                                FROM 
                                   BangLuyKe as blk1
                                 INNER JOIN SiteW on SiteW.ID = blk1.SiteID        
                                 WHERE
                                   MonthMonitor = " + month1 + @"
                                   AND YearMonitor = @YearMonitor
                                   AND blk1.SiteID = blk2. SiteID
                                   ) as TongDVCTTPhatSinhHoSo_tab3,
                SoHoSoDVCTT - (select SoHoSoDVCTT 
                                FROM 
                                   BangLuyKe as blk1
                INNER JOIN SiteW on SiteW.ID = blk1.SiteID        
                                    WHERE
                                    MonthMonitor = " + month1 + @"
                                    AND YearMonitor = @YearMonitor
                                    AND blk1.SiteID = blk2. SiteID
                                    ) as SoHoSoDVCTT,
                NTT_DVCTT - (select NTT_DVCTT 
                                FROM 
                                    BangLuyKe as blk1
                                    INNER JOIN SiteW on SiteW.ID = blk1.SiteID        
                                    WHERE
                                    MonthMonitor = " + month1 + @"
                                    AND YearMonitor = @YearMonitor
                                    AND blk1.SiteID = blk2. SiteID
                                    ) as NTT_DVCTT,
                TongHoSoDVCTT_ToanTrinh - (select TongHoSoDVCTT_ToanTrinh 
                                FROM 
                                    BangLuyKe as blk1
                                    INNER JOIN SiteW on SiteW.ID = blk1.SiteID        
                                    WHERE
                                    MonthMonitor = " + month1 + @"
                                    AND YearMonitor = @YearMonitor
                                    AND blk1.SiteID = blk2. SiteID
                                    ) as TongHoSoDVCTT_ToanTrinh,
                TongSoHoSo_tab2 - (select TongSoHoSo_tab2 
                                FROM 
                                    BangLuyKe as blk1
                                    INNER JOIN SiteW on SiteW.ID = blk1.SiteID        
                                    WHERE
                                    MonthMonitor = " + month1 + @"
                                    AND YearMonitor = @YearMonitor
                                    AND blk1.SiteID = blk2. SiteID
                                    ) as TongSoHoSo_tab2,
                YearMonitor,
                SiteW.Name AS SiteName
                FROM BangLuyKe as blk2
                    INNER JOIN SiteW on SiteW.ID = blk2.SiteID        
                WHERE
                    SiteW.TinhDiaPhuong = @TinhDiaPhuong
                    AND MonthMonitor = " + month2 + @"
                    AND YearMonitor = @YearMonitor";

            }

            var parameters = new DynamicParameters();
            //parameters.Add(""@MonthMonitor"", monthMonitor, DbType.Int32);
            parameters.Add("@YearMonitor", yearMonitor, DbType.Int32);
            parameters.Add("@TinhDiaPhuong", TinhDiaPhuong, DbType.Int32);

            var result = await _dapper.GetAllAsync<Utility.Object.BangLuyKe>(query, parameters, commandType: CommandType.Text);
            return result;
        }
        // Lấy 1 chỉ số cho các đơn vị
        public async Task<IEnumerable<ChiSoDonVi>> GetChiSoDonVi(int yearMonitor, int tinhDiaPhuong, string chiSo, int monthMonitor)
        {
            var query = $@"
    SELECT 
        SiteW.Name, 
        blk2.MonthMonitor,
        blk2.{chiSo} as 'KetQua'
    FROM 
        BangLuyKe AS blk2
    INNER JOIN 
        SiteW ON SiteW.ID = blk2.SiteID 
    WHERE 
        blk2.MonthMonitor = @MonthMonitor
        AND blk2.YearMonitor = @YearMonitor
        AND SiteW.TinhDiaPhuong = @TinhDiaPhuong";

            // Khởi tạo tham số
            var parameters = new DynamicParameters();
            parameters.Add("@MonthMonitor", monthMonitor, DbType.Int32);
            parameters.Add("@YearMonitor", yearMonitor, DbType.Int32);
            parameters.Add("@TinhDiaPhuong", tinhDiaPhuong, DbType.Int32);

            // Thực thi truy vấn và lấy kết quả
            var result = await _dapper.GetAllAsync<ChiSoDonVi>(query, parameters, commandType: CommandType.Text);
            return result;
        }

        public async Task<IEnumerable<BangLuyKe>> GetBangLuyKeTheoThang(int month, int year, int tinhDiaPhuong)
        {
            string query;
            var parameters = new DynamicParameters();
            parameters.Add("@MonthMonitor", month, DbType.Int32);
            parameters.Add("@YearMonitor", year, DbType.Int32);
            parameters.Add("@TinhDiaPhuong", tinhDiaPhuong, DbType.Int32);

            if (month == 1)
            {
                query = @"
        SELECT           
            DVCTT_ToanTrinh_tab1,
            TTHC_tab1,
            DVCTT_tab1, 
            SoHoSoTrucTuyen_tab2,  
            TongSoHoSo_tab2,
            SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3,
            SoDVCTT_PhatSinhHoSo_tab3,
            SoTTHC_PhatSinhHoSo_tab3,
            TongDVCTTPhatSinhHoSo_tab3,
            SoHoSoDVCTT,
            NTT_DVCTT,
            TongHoSoDVCTT_ToanTrinh,
            MonthMonitor,
            YearMonitor,
            SiteW.Name AS SiteName
        FROM 
            BangLuyKe
        INNER JOIN SiteW ON SiteW.ID = BangLuyKe.SiteID        
        WHERE
            MonthMonitor = @MonthMonitor
            AND YearMonitor = @YearMonitor
            AND SiteW.TinhDiaPhuong = @TinhDiaPhuong";
            }
            else
            {
                query = @"
        SELECT 
            blk2.DVCTT_ToanTrinh_tab1,
            blk2.TTHC_tab1,
            blk2.DVCTT_tab1, 
            blk2.SoHoSoTrucTuyen_tab2 - 
                (SELECT blk1.SoHoSoTrucTuyen_tab2 
                 FROM BangLuyKe AS blk1
                 WHERE blk1.MonthMonitor = @MonthMonitor - 1 AND blk1.YearMonitor = @YearMonitor AND blk1.SiteID = blk2.SiteID
                ) AS SoHoSoTrucTuyen_tab2,
            blk2.SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3 - 
                (SELECT blk1.SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3 
                 FROM BangLuyKe AS blk1
                 WHERE blk1.MonthMonitor = @MonthMonitor - 1 AND blk1.YearMonitor = @YearMonitor AND blk1.SiteID = blk2.SiteID
                ) AS SoDVCTT_ToanTrinh_PhatSinhHoSo_tab3,
            blk2.SoTTHC_DuaLen_DVCTT_ToanTrinh_CoHoSo_tab3 - 
                (SELECT blk1.SoTTHC_DuaLen_DVCTT_ToanTrinh_CoHoSo_tab3 
                 FROM BangLuyKe AS blk1
                 WHERE blk1.MonthMonitor = @MonthMonitor - 1 AND blk1.YearMonitor = @YearMonitor AND blk1.SiteID = blk2.SiteID
                ) AS SoTTHC_DuaLen_DVCTT_ToanTrinh_CoHoSo_tab3,
            blk2.SoDVCTT_PhatSinhHoSo_tab3 - 
                (SELECT blk1.SoDVCTT_PhatSinhHoSo_tab3 
                 FROM BangLuyKe AS blk1
                 WHERE blk1.MonthMonitor = @MonthMonitor - 1 AND blk1.YearMonitor = @YearMonitor AND blk1.SiteID = blk2.SiteID
                ) AS SoDVCTT_PhatSinhHoSo_tab3,
            blk2.SoTTHC_PhatSinhHoSo_tab3 - 
                (SELECT blk1.SoTTHC_PhatSinhHoSo_tab3 
                 FROM BangLuyKe AS blk1
                 WHERE blk1.MonthMonitor = @MonthMonitor - 1 AND blk1.YearMonitor = @YearMonitor AND blk1.SiteID = blk2.SiteID
                ) AS SoTTHC_PhatSinhHoSo_tab3,
            blk2.TongDVCTTPhatSinhHoSo_tab3 - 
                (SELECT blk1.TongDVCTTPhatSinhHoSo_tab3 
                 FROM BangLuyKe AS blk1
                 WHERE blk1.MonthMonitor = @MonthMonitor - 1 AND blk1.YearMonitor = @YearMonitor AND blk1.SiteID = blk2.SiteID
                ) AS TongDVCTTPhatSinhHoSo_tab3,
            blk2.SoHoSoDVCTT - 
                (SELECT blk1.SoHoSoDVCTT 
                 FROM BangLuyKe AS blk1
                 WHERE blk1.MonthMonitor = @MonthMonitor - 1 AND blk1.YearMonitor = @YearMonitor AND blk1.SiteID = blk2.SiteID
                ) AS SoHoSoDVCTT,
            blk2.NTT_DVCTT - 
                (SELECT blk1.NTT_DVCTT 
                 FROM BangLuyKe AS blk1
                 WHERE blk1.MonthMonitor = @MonthMonitor - 1 AND blk1.YearMonitor = @YearMonitor AND blk1.SiteID = blk2.SiteID
                ) AS NTT_DVCTT,
            blk2.TongHoSoDVCTT_ToanTrinh - 
                (SELECT blk1.TongHoSoDVCTT_ToanTrinh 
                 FROM BangLuyKe AS blk1
                 WHERE blk1.MonthMonitor = @MonthMonitor - 1 AND blk1.YearMonitor = @YearMonitor AND blk1.SiteID = blk2.SiteID
                ) AS TongHoSoDVCTT_ToanTrinh,
            blk2.TongSoHoSo_tab2 - 
                (SELECT blk1.TongSoHoSo_tab2 
                 FROM BangLuyKe AS blk1
                 WHERE blk1.MonthMonitor = @MonthMonitor - 1 AND blk1.YearMonitor = @YearMonitor AND blk1.SiteID = blk2.SiteID
                ) AS TongSoHoSo_tab2,
            blk2.YearMonitor,
            SiteW.Name AS SiteName
        FROM BangLuyKe AS blk2
        INNER JOIN SiteW ON SiteW.ID = blk2.SiteID        
        WHERE SiteW.TinhDiaPhuong = @TinhDiaPhuong AND blk2.MonthMonitor = @MonthMonitor AND blk2.YearMonitor = @YearMonitor";
            }

            var result = await _dapper.GetAllAsync<BangLuyKe>(query, parameters, commandType: CommandType.Text);
            return result;
        }


        //Kiểm tra Token
        public async Task<bool> CheckToken(string token)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Token", token, DbType.String);

                var result = await _dapper.GetAllAsync<int>("SELECT COUNT(*) FROM HeThong WHERE Token = @Token", parameters, commandType: CommandType.Text);

                return result.FirstOrDefault() > 0;

            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi kiểm tra token: {e.Message}");
                return false;
            }

        }


    }
}
