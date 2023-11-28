using ProyectoBenSon.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBenSon.DAL
{
    public class ProdutoRepositorio : IRepositorio<Produto>
    {
        private readonly Context _context = new Context();

        public void Add(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public void Delete(Produto produto)
        {
            _context.Produtos.Remove(produto);
        }

        public void Update(Produto produto)
        {
            _context.Produtos.Update(produto);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Produto[]> GetAllAsync()
        {
            return await _context.Produtos.ToArrayAsync();
        }

        public async Task<Produto> GetByIdAsync(int ProdutoId)
        {
            return await _context.Produtos.Where(P => P.Id == ProdutoId).FirstOrDefaultAsync();
        }
    }
}
