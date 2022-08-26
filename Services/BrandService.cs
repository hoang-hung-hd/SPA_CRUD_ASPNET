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
            return _context.Brands;
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
            var brand = _context.Brands.Find(id);
            if (brand == null) throw new KeyNotFoundException("Brand not found");
            return brand;
        }
    }
}
