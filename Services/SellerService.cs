using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            this._context = context;
        }

        public List<Seller> FindAll()
        {
            return this._context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            this._context.Add(seller);
            this._context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return this._context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = this._context.Seller.Find(id);
            this._context.Seller.Remove(obj);
            this._context.SaveChanges();
        }
    }
}