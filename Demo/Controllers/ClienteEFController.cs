#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo.Data;
using Demo.Models;
using Demo.Services;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteEFController : ControllerBase
    {
        private readonly DemoContext _context;

        public ClienteEFController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/ClienteEF
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            return await _context.Cliente.ToListAsync();
        }

        // GET: api/ClienteEF/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int? id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/ClienteEF/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("EF/{id}")]
        //public async Task<IActionResult> PutCliente(int? id, Cliente cliente)
        //{
        //    if (id != cliente.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(cliente).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ClienteExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


       // [HttpPut("Dapper/{id}")]
       //// [Route("ASYNC")]
       // public async Task PutDP(int id, [FromBody] Cliente cliente)
       // {
       //     try
       //     {
       //         var clienteService = new ClientServices();
       //         {
       //             cliente.id = id;
       //             await clienteService.UpdateClientAsync(cliente, id,true);
       //         }
       //     }
       //     catch (Exception)
       //     {

       //         throw;
       //     }
       // }

       // [HttpPut("DML/{id}")]
       // // [Route("ASYNC")]
       // public async Task PutDML(int id, [FromBody] Cliente cliente)
       // {
       //     try
       //     {
       //         var clienteService = new ClientServices();
       //         {
       //             cliente.id = id;
       //             await clienteService.UpdateClientAsync(cliente, id, false);
       //         }
       //     }
       //     catch (Exception)
       //     {

       //         throw;
       //     }
       // }


        [HttpPut("UPDATE/{framwork}/{id}")]
        public async Task MultiPut(int id, String framwork, [FromBody] Cliente cliente)
        {
            if (framwork == "DML") {
                try
                {
                    var clienteService = new ClientServices();
                    {
                        cliente.id = id;
                        await clienteService.UpdateClientAsync(cliente, id, false);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }else if (framwork == "DAPPER"){
                try
                {
                    var clienteService = new ClientServices();
                    {
                        cliente.id = id;
                        await clienteService.UpdateClientAsync(cliente, id, true);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }else if (framwork == "EF") { 
                _context.Entry(cliente).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        throw;
                    
                }

            }

        }





        // POST: api/ClienteEF
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.id }, cliente);
        }

        // DELETE: api/ClienteEF/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int? id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int? id)
        {
            return _context.Cliente.Any(e => e.id == id);
        }
    }
}
