using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ConsoleAppl_POC.AppService
{
    public sealed class WebAPICliente
    {
        private static string _address = "http://localhost:62526/api/cliente/";

        #region Http Client

        public async void GetAllByHttpClient(long countLoop)
        {
            //_minutoFinal = SetDate(DateTime.Now);

            // Create an HttpClient instance
            HttpClient client = new HttpClient();
            // Send a request asynchronously continue when complete
            HttpResponseMessage response = await client.GetAsync(_address);
            // Check that response was successful or throw exception
            response.EnsureSuccessStatusCode();
            // Lê a resposta da requisição assincrona dos Clientes e escreve na tela.
            var clientes = await response.Content.ReadAsAsync<ICollection<Cliente>>();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Clientes retornados do WebAPI: ");

            foreach (var cliente in clientes)
            {
                sb.AppendFormat("ID: {0}, Nome: {1}, SobreNome: {2}, Apelido: {3}, DataNascimento: {4}, Idade: {5} \n",
                cliente.Id, cliente.Nome, cliente.SobreNome, cliente.Apelido, cliente.DataNascimento, cliente.Idade);

                sb.AppendFormat("Time Imprimido: {0}\n", DateTime.Now.ToString());
                sb.AppendLine(" =========================================== \n");
            }

            Console.WriteLine(sb.ToString());
        }

        public async void GetByIDByHttpClient(int id)
        {
            // Create an HttpClient instance
            HttpClient client = new HttpClient();
            // Send a request asynchronously continue when complete
            HttpResponseMessage response = await client.GetAsync(_address + id.ToString());
            // Check that response was successful or throw exception
            response.EnsureSuccessStatusCode();
            // Lê a resposta da requisição assincrona dos Clientes e escreve na tela.
            var cliente = await response.Content.ReadAsAsync<Cliente>();

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("O cliente {0} retornado do WebAPI: \n", id.ToString());

            if (cliente != null)
            {
                sb.AppendFormat("ID: {0}, Nome: {1}, SobreNome: {2}, Apelido: {3}, DataNascimento: {4}, Idade: {5} \n",
                cliente.Id, cliente.Nome, cliente.SobreNome, cliente.Apelido, cliente.DataNascimento, cliente.Idade);
            }
            else
            {
                sb.AppendLine("Nada...");
            }

            sb.AppendFormat("Time Imprimido: {0}\n", DateTime.Now.ToString());

            sb.AppendLine(" =========================================== \n");

            Console.WriteLine(sb.ToString());
        }

        public async void InsertClienteByHttpClient(string nome, string sobreNome, string apelido, int idade, DateTime dateNascimento)
        {
            var clienteHere = new Cliente
            {
                Nome = nome,
                SobreNome = sobreNome,
                Apelido = apelido,
                Idade = idade,
                DataNascimento = dateNascimento
            };

            var insered = false;

            var clienteJson = "{ Nome:'" + nome + "', SobreNome:'" + sobreNome + "', Apelido:'" + apelido + "', Idade:" + idade + ",DataNascimento:'" + dateNascimento + "' }";

            using (HttpClient client = new HttpClient())
            {

                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("Nome", nome));

                HttpContent content = new FormUrlEncodedContent(postData);

                var mediaType = content.Headers.ContentType.MediaType = "application/json";

                //HttpResponseMessage response = await client.PostAsJsonAsync<Cliente>(_address, clienteHere);
                var response = await client.PostAsync(new Uri(_address), content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    insered = await response.Content.ReadAsAsync<bool>();
            }

            Console.WriteLine("Cliente inserido com: " + (insered ? "Sucesso" : "Hummm...deu ruim!"));
        }

        public async void DeleteClienteByHttpClient(int id)
        {
            var insered = false;

            // Create an HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                // Send a request asynchronously continue when complete
                HttpResponseMessage response = await client.DeleteAsync(_address + id.ToString());
                // Check that response was successful or throw exception
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    insered = await response.Content.ReadAsAsync<bool>();

            }

            Console.WriteLine("Cliente deletado com: " + (insered ? "Sucesso" : "Hummm...deu ruim!"));
        }

        #endregion


    }
}
