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

        public Task<bool> deleteAsync(Guid id)
        {
            var itemDoBanco = await _context.Set<T>().FirstOrDefaultAsync(i => i.id == item.id);
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

        public Task<IEnumerable<T>> selectAsync()
        {
            throw new NotImplementedException();
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
