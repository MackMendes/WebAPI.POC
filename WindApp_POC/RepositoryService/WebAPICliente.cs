using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace WindApp_POC.RepositoryService
{
    public sealed class WebAPICliente
    {
        private static string _address = "http://localhost:62526/api/cliente/";
        private static string jsonMediaType = "application/json";

        #region Http Client

        public async Task<ICollection<Cliente>> GetAllByHttpClient()
        {
            // Create an HttpClient instance
            HttpClient client = new HttpClient();
            // Send a request asynchronously continue when complete
            HttpResponseMessage response = await client.GetAsync(_address);
            // Check that response was successful or throw exception
            response.EnsureSuccessStatusCode();
            // Lê a resposta da requisição assincrona dos Clientes e escreve na tela.
            ICollection<Cliente> clientes = await response.Content.ReadAsAsync<ICollection<Cliente>>();

            return clientes;
        }

        public async Task<Cliente> GetByIDByHttpClient(int id)
        {
            // Create an HttpClient instance
            HttpClient client = new HttpClient();
            // Send a request asynchronously continue when complete
            HttpResponseMessage response = await client.GetAsync(_address + id.ToString());
            // Check that response was successful or throw exception
            response.EnsureSuccessStatusCode();
            // Lê a resposta da requisição assincrona dos Clientes e escreve na tela.
            Cliente cliente = await response.Content.ReadAsAsync<Cliente>();

            return cliente;
        }

        public async Task<bool> InsertClienteByHttpClient(string nome, string sobreNome, string apelido, int idade, DateTime dateNascimento)
        {
            var cliente = new Cliente
            {
                Nome = nome,
                SobreNome = sobreNome,
                Apelido = apelido,
                Idade = idade,
                DataNascimento = dateNascimento
            };

            var insered = false;

            using (HttpClient client = new HttpClient())
            {
                HttpContent content = this.CreateJsonObjectContent(cliente);

                var response = await client.PostAsync(new Uri(_address), content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    insered = await response.Content.ReadAsAsync<bool>();
            }

            return insered;
        }

        public async Task<bool> DeleteClienteByHttpClient(int id)
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

            return insered;
        }

        #endregion


        private HttpContent CreateJsonObjectContent<T>(T model) where T : class
        {
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            HttpContent content = new ObjectContent<T>(model, jsonFormatter);

            return content;            
        }
    }
}
