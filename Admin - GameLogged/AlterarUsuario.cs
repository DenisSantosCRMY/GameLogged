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
    public partial class AlterarUsuario : Form
    {
        public AlterarUsuario()
        {
            InitializeComponent();
        }

        //botão para alterar um usuario existente, com validação de campos e tratamento de erros
        private void btAlterar_Click(object sender, EventArgs e)
        {
            //verificar se os campos estão preenchidos
            if (string.IsNullOrEmpty(txtNickname.Text) || string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtDtnasc.Text))
            {
                MessageBox.Show("Preencha todos os campos para alterar um usuário.");
                return;

            }

            try
            {
                //instanciando conexao
                ConexaoBanco conexao = new ConexaoBanco();

                string sql = "UPDATE gamelogged.usuario SET nome = @nome, email = @email, password = @password, dt_nasc = @dtnasc WHERE nickname = @nickname";

                //coleta os parametros dentro da tela
                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@nickname", txtNickname.Text),
                    new MySqlParameter("@nome", txtNome.Text),
                    new MySqlParameter("@email", txtEmail.Text),
                    new MySqlParameter("@password", txtPassword.Text),
                    new MySqlParameter("@dtnasc", DateTime.Parse(txtDtnasc.Text))
                };

                //executa o comando sql
                int resultado = conexao.ExecutarComandoQuery(sql, parametros);

                if (resultado > 0)
                {
                    GerenciadorLogs.RegistrarLog($"Usuário '{txtNickname.Text}' alterado com sucesso.");
                    MessageBox.Show("Usuário alterado com sucesso!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    GerenciadorLogs.RegistrarLog($"Falha ao alterar usuário '{txtNickname.Text}'.");
                    MessageBox.Show("Erro ao alterar usuário. Tente novamente.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro critico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
