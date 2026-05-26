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
    public partial class CadastrarUsuario : Form
    {
        public CadastrarUsuario()
        {
            InitializeComponent();
        }

        //botão para cadastrar um novo usuario, com validação de campos e tratamento de erros
        private void btCadastrar_Click(object sender, EventArgs e)
        {
            //verificar se os campos estão preenchidos
            if (string.IsNullOrEmpty(txtNickname.Text) || string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtDtnasc.Text))
            {
                MessageBox.Show("Preencha todos os campos para cadastrar um usuário.");
                return;

            }

            try
            {
                //instanciando conexao
                ConexaoBanco conexao = new ConexaoBanco();

                string sql = "INSERT INTO gamelogged.usuario (nickname, nome, email, password, dt_nasc) VALUES (@nickname, @nome, @email, @password, @dtnasc)";

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
                    GerenciadorLogs.RegistrarLog($"Usuário '{txtNickname.Text}' cadastrado com sucesso.");
                    MessageBox.Show("Usuário cadastrado com sucesso!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    GerenciadorLogs.RegistrarLog($"Falha ao cadastrar usuário '{txtNickname.Text}'.");
                    MessageBox.Show("Erro ao cadastrar usuário. Tente novamente.");
                }

            }
            catch (Exception ex)
            {
                GerenciadorLogs.RegistrarLog($"Erro ao cadastrar usuário '{txtNickname.Text}'. Detalhes do erro: {ex.Message}");
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro critico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
