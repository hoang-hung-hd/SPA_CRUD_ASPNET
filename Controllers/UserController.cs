using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using useCookieAuth.Models;

namespace useCookieAuth.Controllers
{
    public class UserController : Controller
    {
        private AppDbContext _context;
        const string sessionName = "user session";

        public UserController(AppDbContext context)
        {
            _context = context;
            
        }
        public ActionResult Index(HttpContext context)
        {
            CountAccessInfo(context);
            return View();
        }
        public static string CountAccessInfo(HttpContext context)
        {
            var session = context.Session;          // Lấy ISession
            string key_access = "info_access";

            // Lưu vào  Session thông tin truy cập
            // Định nghĩa cấu trúc dữ liệu lưu trong Session
            var accessInfoType = new
            {
                count = 0,
                lasttime = DateTime.Now
            };

            // Đọc chuỗi lưu trong Sessin với key = info_access
            string json = session.GetString(key_access);
            dynamic lastAccessInfo;
            if (json != null)
            {
                // Convert chuỗi Json - thành đối tượng
                lastAccessInfo = JsonConvert.DeserializeObject(json, accessInfoType.GetType());
            }
            else
            {
                // json chưa từng lưu trong Session, accessInfo lấy bằng giá trị khởi  tạo
                lastAccessInfo = accessInfoType;
            }
            if (lastAccessInfo.count == 0)
            {
                return "Chưa truy cập /user lần  nào";
            }

            string thongtin = $"Số lần truy cập /user: {lastAccessInfo.count}  - lần cuối: {lastAccessInfo.lasttime.ToLongTimeString()}";
            return thongtin;
        }

        public IActionResult Index2(HttpContext context)
        {
            var session = context.Session;
            string userId = "User Infor";
            var userInfor = new
            {
                UserId = 0,
                UserName = "",
                UserEmail = "",
            };
            string jsonUser = session.GetString(userId);
            dynamic Infor;
            if (jsonUser != null)
            {
                // Convert chuỗi Json - thành đối tượng
                Infor = JsonConvert.DeserializeObject(jsonUser, userInfor.GetType());
                return View();
            }
            else
            {
                // json chưa từng lưu trong Session, lấy giá trị khởi tạo
                Infor = userInfor;
                return RedirectToAction("Login");
            }

        }
    }
}
