using DAL;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
//builder.Services.AddRazorPages();
//router
builder.Services.AddRazorPages().
         AddRazorPagesOptions(options =>
         {

             // Thêm Page Route (Rewrite) cho thư mục gốc
             // Truy cập /chinh-sach.html  là truy cập /Privacy
             options.Conventions.AddPageRoute(
                 "/Danhmuc",
                 "/meomeo"
             );
             options.Conventions.AddPageRoute(
                "/Danhmuc",
                "/sanpham/{nameproduct?}"
            );
             options.Conventions.AddPageRoute(
                "/ChiTiet",
                "/tin-chi-tiet/{nameproduct?}"
            );
             // Thêm Page Route cho trang trong Areas
             // Truy cập /sanpham/ten-san-pham = /Product/Detail/ten-san-pưham
             //options.Conventions.AddAreaPageRoute(
             //    areaName: "Product",
             //    pageName: "/Detail",
             //    route: "/sanpham/{nameproduct?}"
             //    );
         });

//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
// các service về nghiệp vụ a  viết trong BAL xong call đên IDapperService ấy
// đoạn này đầu tiên a phải adđScoped hoặc adđSigleton cái IDapper service đầu tiên
builder.Services.AddSingleton<IDapper, Dapperr>();
// init các sericve sau
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
