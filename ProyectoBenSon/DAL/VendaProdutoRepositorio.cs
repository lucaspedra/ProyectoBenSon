﻿using System;
using ProyectoBenSon.Model;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBenSon.DAL
{
    public class VendaProdutoRepositorio
    {
        private readonly Context _context = new Context();

        public void Add(VendaProduto venda)
        {
            _context.VendaProdutos.Add(venda);
        }

        public void AddRange(List<VendaProduto> VendaProduto)
        {
            _context.VendaProdutos.AddRange(VendaProduto);
        }

        public void Delete(VendaProduto venda)
        {
            _context.VendaProdutos.Remove(venda);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
