using Domain.Entities;
using Domain.Interface;
using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ClienteRepositoryFake : IRepository<Cliente>
    {
        private static volatile ICollection<Cliente> listCliente;
        private static object syncRoot = new Object();

        public ClienteRepositoryFake()
        {
            if (listCliente == null)
                lock (syncRoot)
                    listCliente = Builder<Cliente>.CreateListOfSize(20).Build();
        }


        public async Task<Cliente> Get(int id)
        {
            // Simulando Assync na mão...
            return await Task.Run(() => listCliente.FirstOrDefault(x => x.Id == id));
        }


        public async Task<ICollection<Cliente>> GetAll()
        {
            // Simulando Assync na mão...
            return await Task.Run(() => listCliente);
        }


        public async Task<ICollection<Cliente>> GetAll(Func<Cliente, bool> functionExpress)
        {
            return await Task.Run(() => listCliente.Where<Cliente>(functionExpress).ToList());
        }


        public async Task<bool> Insert(Cliente entity)
        {
            return await Task.Run(() =>
            {
                bool inserted = false;

                if (listCliente.FirstOrDefault(x => x.Id == entity.Id) == null)
                {
                    entity.Id = (listCliente.Max(x => x.Id) + 1);
                    listCliente.Add(entity);
                    inserted = true;
                }
                return inserted;
            });
        }


        public async Task<bool> Alter(Cliente entity)
        {
            return await Task.Run(() =>
            {
                bool changed = false;
                var cliente = listCliente.FirstOrDefault(x => x.Id == entity.Id);

                if (cliente != null)
                {
                    var remove = listCliente.Remove(cliente);
                    if (remove)
                    {
                        listCliente.Add(entity);
                        changed = true;
                    }
                }

                return changed;
            });
        }


        public async Task<bool> Delete(Cliente entity)
        {
            return await Task.Run(() => listCliente.Remove(entity));
        }
    }
}
