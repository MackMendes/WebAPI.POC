using Domain.Entities;
using Domain.Interface;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI_POC.Controllers
{
    public class ClienteController : ApiController
    {
        private IRepository<Cliente> repositoryCliente;

        public ClienteController()
        {
            this.repositoryCliente = new ClienteRepositoryFake();
        }


        [HttpGet]
        // GET: api/Cliente
        public async Task<IEnumerable<Cliente>> Get()
        {
            return await repositoryCliente.GetAll();
        }

        [HttpGet]
        // GET: api/Cliente/5
        public async Task<Cliente> Get(int id)
        {
            var result = await repositoryCliente.GetAll(x => x.Id == id);
            return result.FirstOrDefault();
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var inserted = await repositoryCliente.Insert(cliente);
                    if (!inserted)
                        throw new Exception("Cliente não foi inserido...");

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, cliente);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = cliente.Id }));
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/Cliente/5
        [HttpPut]
        public async Task<HttpResponseMessage> Put(int id, [FromBody]Cliente cliente)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != cliente.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var changed = await repositoryCliente.Alter(cliente);
            if (!changed)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new ApplicationException("Cliente não foi alterado..."));

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE: api/Cliente/5
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var cliente = await repositoryCliente.Get(id);

            if (cliente == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            try
            {
                var exclued = await repositoryCliente.Delete(cliente);
                if (!exclued)
                    throw new ApplicationException("Cliente não foi excluído...");
            }
            catch (ApplicationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, cliente);
        }
    }
}
