//system
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
// microsoft
using Microsoft.EntityFrameworkCore;
// models
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            this._context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await this._context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            this._context.Add(seller);
            await this._context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await this._context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await this._context.Seller.FindAsync(id);
                this._context.Seller.Remove(obj);
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateException error)
            {
                throw new IntegrityException("Can't delete seller because he/she sales");
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            var hasAny = await this._context.Seller.AnyAsync(x => x.Id == seller.Id);
            if (!hasAny) throw new NotFoundException("Id not found");

            try
            {
                this._context.Update(seller);
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException error)
            {
                throw new DbConcurrencyException(error.Message);
            }
        }
    }
}