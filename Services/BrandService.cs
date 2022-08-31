using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using useCookieAuth.Models;

namespace useCookieAuth.Services
{
    public class BrandService
    {
        private AppDbContext _context;

        public BrandService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Brand> GetAll()
        {
            string sql = "EXEC dbo.GetAllBrand";
            List<Brand> listBrand = _context.Brands.FromSqlRaw<Brand>(sql).ToList();
            return listBrand;
        }

        public Brand GetById(int id)
        {
            return getBrand(id);
        }
        public void Create(Brand model)
        {
            // validate
            if (_context.Brands.Any(x => x.BrandName == model.BrandName))
                throw new Exception("Brand with the name '" + model.BrandName + "' already exists");
            Brand brand = new Brand();
            brand.BrandName = model.BrandName;
            brand.Description = model.Description;

            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public void Update(int id, Brand model)
        {
            var brand = getBrand(id);

            // validate
            if (model.BrandName != brand.BrandName && _context.Brands.Any(x => x.BrandName == model.BrandName && x.BrandId != id))
                throw new Exception("Brand with the name '" + model.BrandName + "' already exists");
            brand.BrandName = model.BrandName;
            brand.Description = model.Description;


            _context.Brands.Update(brand);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var brand = getBrand(id);
            _context.Brands.Remove(brand);
            _context.SaveChanges();
        }

        // helper methods

        private Brand getBrand(int id)
        {
            string sql = "EXEC Get_Brand_ById @BrandId";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@BrandId", Value = id }
            };
            Brand brand = _context.Brands.FromSqlRaw<Brand>(sql, parms.ToArray()).ToList().FirstOrDefault();
            if (brand == null) throw new KeyNotFoundException("Brand not found");
            return brand;
        }
    }
}

/*
 * Lưu ý các điều sau :
       1.Thư viện sử dụng : Microsoft.Data.SqlClient not System.Data.SqlClicent
       2.Lấy 1 record thì phải .Tolist().FirstOrDefault()
 */
