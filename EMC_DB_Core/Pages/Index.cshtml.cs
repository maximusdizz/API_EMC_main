using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMC_DB_Core.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDapper _dapper;
        private readonly ILogger<IndexModel> _logger;
        public int showI = 1;

        public IndexModel(ILogger<IndexModel> logger, IDapper dapper)
        {
            _logger = logger;
            _dapper = dapper;
        }
        //private readonly IDapper _dapper;
        //public IndexModel(IDapper dapper)
        //{
        //    _dapper = dapper;
        //}

        //public async void OnGet()
        //{
        //    //get 1
        //    var OBAL=new BAL.GetDataBAL(_dapper);
        //    var dt=await  OBAL.GetById(0);
        //    //get nhieu
        //    var dt2 =await OBAL.GetAllAsync();
        //}
        public async Task<IActionResult> OnGetAsync()
        {
            //////get 1
            //var OBAL = new BAL.GetDataBAL(_dapper);
            ////var dt = await OBAL.GetById(0);
            //////get nhieu
            ////var dt2 = await OBAL.GetAllAsync();

            ////truyền vào chuỗi json
            //string sJson = @"{
            //                  ""SiteID"": 1,
            //                  ""ViTriTong"": 8
            //                }";
            ////convert về object động
            //var ob = Newtonsoft.Json.JsonConvert.DeserializeObject<Object>(sJson);
            ////insert vao db
            //var icheck = OBAL.ExcuteAsyncObject(ob);
            return Page();
        }

    }
}