using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Admin___GameLogged
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            //coletar os dados informados pelo usuário
            string acesso = txtUser.Text;
            string senha = txtPassword.Text;
            try
            {
                //efetuar conexão com o banco de dados e verificar se os dados estão corretos
                ConexaoBanco conexao = new ConexaoBanco();


                //query para verificar se o usuário e senha existem na tabela
                string sql = "SELECT COUNT(*) FROM gamelogged.funcionario WHERE acesso = @user AND password = @pass";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@user", acesso),
                    new MySqlParameter("@pass", senha)
                };

                //executa a query e obtém o resultado
                int resultado = Convert.ToInt32(conexao.ExecutarConsultaScalar(sql, parametros));

                //verifica se o resultado é maior que 0, ou seja, se encontrou um usuário com as credenciais fornecidas
                if (resultado > 0)
                {
                    GerenciadorLogs.RegistrarLog($"Login bem-sucedido para o usuário: {acesso}");
                    // Login com sucesso! Abre o painel
                    AdminPainel admin = new AdminPainel();
                    admin.Show();
                    this.Hide();
                }
                else
                {
                    GerenciadorLogs.RegistrarLog($"Falha de login para o usuário: {acesso}");
                    MessageBox.Show("Usuário ou senha incorretos.");
                }

            }
            catch (Exception ex)
            {
                GerenciadorLogs.RegistrarLog($"Erro ao autenticar o usuário: {acesso}. Detalhes do erro: {ex.Message}");
                MessageBox.Show("Erro ao autenticar: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
