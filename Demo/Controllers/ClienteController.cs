using Demo.Models;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        // GET: api/<ClienteController>
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            var clienteService = new ClientServices();
            {
                var cliente = clienteService.GetClientes();
                if (cliente != null)
                {
                    return Ok(cliente);
                }
                return NotFound("Mensaje: There is not clients");
            }
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            var clienteService = new ClientServices();
            {
                var cliente = clienteService.GetClientById(id);
                if (cliente != null)
                {
                    return Ok(cliente);
                }
            }
            return NotFound("Mensaje: El cliente no existe");
        }

        [HttpGet]
        [Route("Async")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAsync()
        {
            var clienteService = new ClientServices();
            {
                var cliente = await clienteService.GetClienteAsync();
                if(cliente != null)
                {
                    return Ok(cliente);
                }
            }
            return NotFound("Mensaje: El cliente no existe");
        }

        // POST api/<ClienteController>
        [HttpPost]
        public void Post([FromBody] Cliente cliente)
        {
            var clienteService = new ClientServices();
            {
                clienteService.AddClient(cliente);

            }
        }

        [HttpPost]
        [Route("ASYNC")]
        public async Task PostAsync([FromBody] Cliente cliente)
        {
            try
            {
                var clienteService = new ClientServices();
                {
                    cliente.id = 0;
                    await clienteService.AddClientAsync(cliente);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Cliente cliente)
        {
            try
            {
                var clienteService = new ClientServices();
                {
                    cliente.id = id;
                    clienteService.UpdateClient(cliente, true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("Update/{id}")]
        public void UpdateClient(int id, [FromBody] Cliente cliente)
        {
            try
            {
                var clienteService = new ClientServices();
                {
                    cliente.id = id;
                    clienteService.UpdateClientDML(cliente);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
