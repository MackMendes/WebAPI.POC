using ConsoleAppl_POC.AppService;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppl_POC
{
    class Program
    {
        #region Propies

        private static string _minutoInicial;
        private static string _minutoFinal;
        private static readonly WebAPICliente _webAPICliente = new WebAPICliente();

        #endregion


        #region Main

        static void Main(string[] args)
        {
            var dateNow = DateTime.Now;
            _minutoInicial = SetDate(dateNow);

            //Verifica performance de pegar vários registros pelo WebAPI
            Parallel.For(0, 100, x =>
            {
                _webAPICliente.GetAllByHttpClient(x);
            });

            // Pegar um cliente por ID
            //GetClienteByID();

            // Deu Ruim...
            //InsertCliente();

            // Delete
            //DeleteClienteByID();

            Console.WriteLine("Pressione qualquer tecla para SAIR...");
            Console.ReadLine();
        }

        #endregion


        #region Method Private

        private static string SetDate(DateTime dateNow) 
        {
           return  _minutoInicial = "Minuto: " + dateNow.Minute + " Segundo: " + dateNow.Second + " MilessimoSegundo: " + dateNow.Millisecond;
        }

        private static void GetClienteByID()
        {
            Console.WriteLine("Informe o ID do cliente: ");
            var inputKey = Console.ReadLine();
            _webAPICliente.GetByIDByHttpClient(Convert.ToInt32(inputKey));
        }

        private static void InsertCliente()
        {
            var cliente = new Cliente();

            Console.WriteLine("Inserir cliente, por favor informe: ");
            Console.WriteLine("Nome: ");
            cliente.Nome = Console.ReadLine();
            Console.WriteLine("SobreNome: ");
            cliente.SobreNome = Console.ReadLine();

            Console.WriteLine("Idade: ");
            cliente.Idade = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Apelido: ");
            cliente.Apelido = Console.ReadLine();

            Console.WriteLine("Data de Nascimento: ");
            cliente.DataNascimento = Convert.ToDateTime(Console.ReadLine());

            _webAPICliente.InsertClienteByHttpClient(cliente.Nome, cliente.SobreNome, cliente.Apelido, cliente.Idade, cliente.DataNascimento);
        }

        private static void DeleteClienteByID()
        {
            Console.WriteLine("Informe o ID do cliente: ");
            var inputKey = Console.ReadLine();
            _webAPICliente.DeleteClienteByHttpClient(Convert.ToInt32(inputKey));
        }

        #endregion

    }
}
