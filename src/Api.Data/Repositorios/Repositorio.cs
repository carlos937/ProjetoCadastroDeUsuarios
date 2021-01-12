using Data.Context;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : BaseEntity
    {
        public readonly MyContext _context;
        public Repositorio(MyContext context)
        {
            _context = context;
        }

        public async Task deleteAsync(Guid id)
        {
            var itemDoBanco = await _context.Set<T>().FirstOrDefaultAsync(i => i.id == id);
            itemDoBanco.lixeira = true;
            if (itemDoBanco == null)
            {
               throw new Exception("Item não encontrado.");
            }
            await _context.SaveChangesAsync();

        }

        public async Task<T> InsertAsync(T item)
        {

                if (item.id == Guid.Empty)
                {
                    item.id = new Guid();
                }

                item.dataDeCadastro = HelperHorario.HoraDeBrasilia;
                item.dataDeAtualizacao = HelperHorario.HoraDeBrasilia;

                _context.Set<T>().Add(item);
               await _context.SaveChangesAsync();


            return item;
        }

        public async Task<IEnumerable<T>> selectAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> find(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync( i => i.id == id);
        }

        public async Task<T> updateAsync(T item)
        {
        
            var itemDoBanco = await _context.Set<T>().FirstOrDefaultAsync(i => i.id == item.id);

            if(itemDoBanco == null)
            {
                return null;
            }

            _context.Entry(itemDoBanco).CurrentValues.SetValues(item);

            itemDoBanco.dataDeAtualizacao = HelperHorario.HoraDeBrasilia;

            await _context.SaveChangesAsync();

            return item;
        }
    }
}
