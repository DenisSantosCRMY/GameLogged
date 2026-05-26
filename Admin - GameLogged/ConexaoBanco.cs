using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin___GameLogged
{
    internal class ConexaoBanco
    {
        //os dados estavam muito expostos , então criei uma classe para guardar a string de conexão, e o método conectar() para retornar a conexão com o banco de dados
        private string dadosConexao = "server=localhost;port=3306;database=gamelogged;user=root;password=teste123";

        //preciso deixar o metodo privado depois
        public MySqlConnection conectar()
        {
            return new MySqlConnection(dadosConexao);
        }

        //método para executar os comandos de insert, update e delete, ele recebe a query e os parametros (caso haja) e retorna o numero de linhas afetadas
        public int ExecutarComandoQuery(string sql, MySqlParameter[] parametros = null)
        {
            using (MySqlConnection conexao = conectar())
            {
                try
                {
                    conexao.Open();
                    using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                    {
                        //verificando se possui valor dentro do parametros (apenas por garantia)
                        if(parametros != null)
                        {
                            comando.Parameters.AddRange(parametros);
                        }

                        //Efetuar comando do CRUD
                        return comando.ExecuteNonQuery();
                    }
                }
                //para caso de problema nas queries
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar o comando no banco de dados: " + ex.Message);
                }
            }
        }

        //criado para o login
        public object ExecutarConsultaScalar(string sql, MySqlParameter[] parametros = null)
        {
            using (MySqlConnection conexao = conectar())
            {
                try
                {
                    conexao.Open();
                    using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                    {
                        //verificando se possui valor dentro do parametros (apenas por garantia)
                        if (parametros != null)
                        {
                            comando.Parameters.AddRange(parametros);
                        }

                        //Efetuar comando do CRUD
                        return comando.ExecuteScalar();
                    }
                }
                //para caso de problema nas queries
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar o comando no banco de dados: " + ex.Message);
                }
            }
        }

        //método para executar os comandos de select, ele recebe a query e retorna um DataTable com os dados consultados
        public DataTable ExecutarConsultar(string sql)
        {
            using (MySqlConnection conexao = conectar())
            {
                try
                {
                    conexao.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conexao);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                    

                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar consulta: " + ex.Message);
                }
            }
        }
    }
}
