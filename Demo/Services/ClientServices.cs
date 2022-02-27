using Dapper;
using DapperExtensions;
using Demo.DML;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Demo.Services
{
    public class ClientServices
    {
        private SqlConnection _Conn = new();
       private static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(@"Data Source=DESKTOP-3O61836;Initial Catalog=ClienteIX; Integrated Security=True; Pooling=False");
            
        }

        public Cliente GetClientById(int id)
        {
            _Conn = GetSqlConnection();
            _Conn.Open();
            //Select
            var cliente = _Conn.Query<Cliente>("SELECT *  FROM  Cliente").Where(f => f.id == id).ToList();
            return cliente.Count != 0 ? cliente.First() : null;

        }

        public IEnumerable<Cliente> GetClientes()
        {
            _Conn = GetSqlConnection();
            _Conn.Open();
            var cliente = _Conn.Query<Cliente>("Select * from Cliente").ToList();
            return cliente;

        }
        public async Task<IEnumerable<Cliente>> GetClienteAsync()
        {
            _Conn = GetSqlConnection();
            _Conn.Open();
            var cliente = _Conn.Query<Cliente>("Select * from Cliente").ToList();
            return cliente;

        }

        public void AddClient(Cliente cliente)
        {
            try
            {
                _Conn = GetSqlConnection();
                _Conn.Open();
                var strInsert = DMLGenerator.CreateInsertStatement(cliente);
                var clientes = _Conn.Execute(strInsert, cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddClientAsync(Cliente cliente, bool UseDapper = true)
        {
            try
            {
                _Conn = GetSqlConnection();
                _Conn.Open();
                if (UseDapper)
                {
                    await _Conn.InsertAsync(cliente);
                    _Conn.Close();
                }
                else
                {
                    var strInsert = DMLGenerator.CreateInsertStatement(cliente);
                    var clientes = _Conn.Execute(strInsert, cliente);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateClient(Cliente cliente, bool UseDapper = true)
        {
            try
            {
                _Conn = GetSqlConnection();
                _Conn.Open();

                if (UseDapper)
                {
                    _Conn.Update(cliente);
                    _Conn.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task UpdateClientAsync(Cliente cliente, int id ,bool UseDapper = true)
        {
            try
            {
                _Conn = GetSqlConnection();
                _Conn.Open();

                if (UseDapper)
                {
                    await _Conn.UpdateAsync(cliente);
                    _Conn.Close();
                }
                else
                {
                    var strUpdate = DMLGenerator.UpdateInsertStatement(cliente, id);
                    var clientes = _Conn.Execute(strUpdate, cliente);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateClientDML(Cliente cliente)
        {
            try
            {
                _Conn = GetSqlConnection();
                _Conn.Open();
                var strUpdate = DMLGenerator.UpdateInsertStatement(cliente, cliente.id);
                var clientes = _Conn.Execute(strUpdate, cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
