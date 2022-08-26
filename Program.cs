using Microsoft.EntityFrameworkCore;
using useCookieAuth.Models;
using useCookieAuth.Services;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
    services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
        cfg.Cookie.Name = "Prince_Wolf";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
        cfg.IdleTimeout = new TimeSpan(0, 60, 0);    // Thời gian tồn tại của Session
    });

    // Add services to the container.
    services.AddControllersWithViews();
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("useAuthContext")));

    services.AddScoped<BrandService, BrandService>();
}



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
