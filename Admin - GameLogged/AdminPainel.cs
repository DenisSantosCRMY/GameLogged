using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
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
    public partial class AdminPainel : Form
    {
        public AdminPainel()
        {
            InitializeComponent();
            carregar_Dados();
        }

        private void Painel_Dash_Load(object sender, EventArgs e)
        {

        }

        //funções do CRUD (Create, Read, Update, Delete)

        public void editar_dados()
        {
            //instanciar o formulário de cadastro
            using (AlterarUsuario alterar = new AlterarUsuario())
            {
                //exibir como modal (tentando bloquear a tela de fundo)
                var viewAlterar = alterar.ShowDialog();

                if (viewAlterar == DialogResult.OK)
                {
                    // Depois que o formulário de cadastro for fechado, recarregamos os dados para mostrar o novo usuário
                    carregar_Dados();
                }
            }

        }

        //excluir os dados do banco de dados
        public void excluir_dados(string idUsuario)
        {
            //confirmação de deleção
            DialogResult confirmacao = MessageBox.Show($"Tem certeza que deseja excluir o usuário com ID {idUsuario}?\nEsta ação não poderá ser desfeita.", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //se for não, simplesmente retorna e não faz nada
            if (confirmacao == DialogResult.No)
            {
                return;
            }

            //se for sim, continua com a exclusão
            try
            {
                // conectar com o banco de dados
                ConexaoBanco conexao = new ConexaoBanco();

                //query de delete
                string sql = "DELETE FROM gamelogged.usuario WHERE id = @id";

                MySqlParameter[] parametros = new MySqlParameter[]
                {
                    new MySqlParameter("@id", idUsuario)
                };

                int linhas = conexao.ExecutarComandoQuery(sql, parametros);

                if (linhas > 0)
                {
                    MessageBox.Show("Usuário removido com sucesso!");

                    carregar_Dados();
                }
                else
                {
                    MessageBox.Show("Nenhum usuário encontrado com o ID fornecido.");
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir: " + ex.Message);
            }
        }

        //cadastrar um novo usuário (abrir o formulário de cadastro)
        public void cadastrar_usuario()
        {
            //instanciar o formulário de cadastro
            using (CadastrarUsuario cadastro = new CadastrarUsuario())
            {
                //exibir como modal (tentando bloquear a tela de fundo)
                var viewCadastar = cadastro.ShowDialog();

                if (viewCadastar == DialogResult.OK)
                {
                    // Depois que o formulário de cadastro for fechado, recarregamos os dados para mostrar o novo usuário
                    carregar_Dados();
                }
            }
        }

        //procurar os dados do usuário (filtro de pesquisa)
        public void procurar_dados()
        {

        }

        //tudo abaixo são tratamento de informações e eventos do datagrid, como clique nos botões, etc

        //refresh da tabela (recarregar os dados do banco para o datagrid)
        public void carregar_Dados()
        {
            dataGridView1.Columns.Clear(); //limpar os botões
            ConexaoBanco bd = new ConexaoBanco();
            MySqlConnection conexao = bd.conectar(); 

            try
            {
                conexao.Open();

                //Comando para selecionar toda a tabela de usuario
                string sql = "SELECT * FROM gamelogged.usuario;";

                //Tradudir o conteudo que esta no sql para o c#
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conexao);

                // Tabela temporaria na memoria como uam objeto
                DataTable tabelaDados = new DataTable();

                //preencher os valores
                adapter.Fill(tabelaDados);

                //Joga os dados dentro do componente visual que você arrastou
                dataGridView1.DataSource = tabelaDados;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["dt_nasc"].HeaderText = "Data de Nascimento"; //Renomear a coluna de nascimento para ficar mais dinamico

                // Coluna de Editar
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                btnEditar.Name = "Editar";
                btnEditar.HeaderText = "";
                btnEditar.Text = "Editar";
                btnEditar.UseColumnTextForButtonValue = true; // Faz o texto "Editar" aparecer no botão
                dataGridView1.Columns.Add(btnEditar);

                // Coluna de Excluir (irei explicar com detalhes)
                DataGridViewButtonColumn btnExcluir = new DataGridViewButtonColumn(); //Instancia um botão 
                btnExcluir.Name = "Excluir"; //Nome da entidade
                btnExcluir.HeaderText = ""; //O conteudo acima do texto (mais para icone)
                btnExcluir.Text = "Excluir"; //Texto que ira aparecer
                btnExcluir.UseColumnTextForButtonValue = true; //Botão ativado para função
                dataGridView1.Columns.Add(btnExcluir); //Adicionar no datagrid


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar tabela: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        //tratar o datagrid para saber qual botão foi clicado, e qual linha, para pegar o ID do usuário e passar para os métodos de editar ou excluir
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignora se clicar no cabeçalho ou fora das linhas
            if (e.RowIndex < 0) return;

            // 1. PEGA O ID DA LINHA CLICADA (Fundamental para ambos os casos)
            // Certifique-se que o nome "id" é o nome da coluna no seu banco/DataTable
            string idSelecionado = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();

            // 2. VERIFICA SE CLICOU NO EDITAR
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Editar")
            {
                // Aqui você pega os outros dados se precisar passar para outro Form
                string nome = dataGridView1.Rows[e.RowIndex].Cells["nome"].Value.ToString();

                // Chama seu método (ajuste os parâmetros se necessário)
                // editar_dados(idSelecionado, nome); 
                MessageBox.Show("Abrindo edição do usuário: " + nome);
               
            }

            // 3. VERIFICA SE CLICOU NO EXCLUIR
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Excluir")
            {
                DialogResult confirmacao = MessageBox.Show(
                    "Tem certeza que deseja excluir este registro?",
                    "Atenção!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmacao == DialogResult.Yes)
                {
                    // Chama seu método de excluir passando o ID
                    excluir_dados(idSelecionado);

                    // O excluir_dados deve chamar o carregar_Dados() no final 
                    // para a linha sumir da tela na hora!
                }
            }
        }


        //todo os botões que possuim na interface, como o de logout, dashboard, etc, podem ser tratados aqui, para abrir outros formulários ou realizar outras ações

        //abrir o datagrid para mostrar os usuários (dashboard)
        private void bt_usuario_Click(object sender, EventArgs e)
        {
            
        }

        //abrir o formulário de solicitações (ainda não criado)
        private void bt_solicitações_Click(object sender, EventArgs e)
        {

        }

        //abrir o formulário de dashboard (ainda não criado)
        private void bt_dashboard_Click(object sender, EventArgs e)
        {

        }

        //abrir o formulário de cadastro para criar um novo usuário
        private void bt_novo_cadastro_Click(object sender, EventArgs e)
        {
            cadastrar_usuario();
        }

        //atualizar os dados do datagrid (refresh)
        private void btAtualizar_Click(object sender, EventArgs e)
        {
            carregar_Dados();
        }

        //abrir o formulário de logs (ainda não criado)
        private void btLogs_Click(object sender, EventArgs e)
        {
            LogsSystem logs = new LogsSystem();
            logs.Show();
            this.Hide();
        }


        //fazer logout
        private void bt_logout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

    }
}
