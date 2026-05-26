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
            //EstilizarDataGridView();
        }

        //personalização da grid
        private void EstilizarDataGridView()
        {
            // 1. Cores de Fundo Gerais
            dataGridView1.BackgroundColor = Color.FromArgb(30, 33, 45); // Um tom escuro para o fundo do grid
            dataGridView1.GridColor = Color.FromArgb(45, 50, 65);       // Cor das linhas divisórias
            dataGridView1.BorderStyle = BorderStyle.None;

            // 2. Configurações de Comportamento e Seleção
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleciona a linha inteira
            dataGridView1.MultiSelect = false;                                    // Apenas uma linha por vez
            dataGridView1.AllowUserToResizeRows = false;                           // Trava a altura das linhas
            dataGridView1.RowHeadersVisible = false;                               // Esconde a coluna com a seta na esquerda

            // 3. Estilização do Cabeçalho (Colunas)
            dataGridView1.EnableHeadersVisualStyles = false; // Permite customizar o cabeçalho

            DataGridViewCellStyle estiloCabecalho = new DataGridViewCellStyle();
            estiloCabecalho.BackColor = Color.FromArgb(130, 137, 170);      // Azul escuro idêntico ao menu lateral
            estiloCabecalho.ForeColor = Color.White;                    // Texto em branco
            estiloCabecalho.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            estiloCabecalho.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle = estiloCabecalho;
            dataGridView1.ColumnHeadersHeight = 40; // Dá uma altura elegante para o cabeçalho

            // 4. Estilização das Linhas de Dados
            DataGridViewCellStyle estiloLinhas = new DataGridViewCellStyle();
            estiloLinhas.BackColor = Color.FromArgb(40, 44, 60);         // Cor das linhas normais
            estiloLinhas.ForeColor = Color.White;                       // Texto das linhas
            estiloLinhas.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            estiloLinhas.SelectionBackColor = Color.FromArgb(80, 90, 130); // Cor de fundo quando selecionado
            estiloLinhas.SelectionForeColor = Color.White;
            dataGridView1.DefaultCellStyle = estiloLinhas;

            // 5. Linhas Alternadas (Zebrado para facilitar a leitura)
            DataGridViewCellStyle estiloAlternado = new DataGridViewCellStyle();
            estiloAlternado.BackColor = Color.FromArgb(35, 39, 54);     // Um tom levemente diferente para intercalar
            estiloAlternado.ForeColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle = estiloAlternado;

            // 6. Espaçamento (Padding) interno para as células respirarem
            dataGridView1.RowTemplate.Height = 35; // Aumenta a altura de cada linha
        }

        private void Painel_Dash_Load(object sender, EventArgs e)
        {

        }

        //funções do CRUD (Create, Read, Update, Delete)

        public void editar_dados(string id, string nickname, string nome, string email, string password, string dt_nasc)
        {
            //instanciar o formulário de cadastro
            using (AlterarUsuario alterar = new AlterarUsuario(id, nickname, nome, email, password, dt_nasc))
            {
                GerenciadorLogs.RegistrarLog($"Admin abriu o formulário de edição para o usuário com ID {id}.");
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
        public void excluir_dados(string id)
        {
            //confirmação de deleção
            DialogResult confirmacao = MessageBox.Show($"Tem certeza que deseja excluir o usuário com ID {id}?\nEsta ação não poderá ser desfeita.", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                    new MySqlParameter("@id", id)
                };

                int linhas = conexao.ExecutarComandoQuery(sql, parametros);

                if (linhas > 0)
                {
                    GerenciadorLogs.RegistrarLog($"Usuário com ID {id} foi excluído.");
                    MessageBox.Show("Usuário removido com sucesso!");
                }
                else
                {
                    GerenciadorLogs.RegistrarLog($"Tentativa de exclusão falhou: Nenhum usuário encontrado com ID {id}.");
                    MessageBox.Show("Nenhum usuário encontrado com o ID fornecido.");
                }
            
            }
            catch (Exception ex)
            {
                GerenciadorLogs.RegistrarLog($"Erro ao excluir usuário com ID {id}: {ex.Message}");
                MessageBox.Show("Erro ao excluir: " + ex.Message);
            }
        }

        //cadastrar um novo usuário (abrir o formulário de cadastro)
        public void cadastrar_usuario()
        {
            //instanciar o formulário de cadastro
            using (CadastrarUsuario cadastro = new CadastrarUsuario())
            {
                GerenciadorLogs.RegistrarLog("Admin abriu o formulário de cadastro de usuário.");
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
                GerenciadorLogs.RegistrarLog($"Erro ao carregar tabela: {ex.Message}");
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

            //Coleta o id da coluna fornecida
            string id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();

            //verifica onde clicou para validar (nesse caso o editar)
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Editar")
            {
                // Aqui você pega os outros dados se precisar passar para outro Form
                string nickname = dataGridView1.Rows[e.RowIndex].Cells["nickname"].Value.ToString();
                string nome = dataGridView1.Rows[e.RowIndex].Cells["nome"].Value.ToString();
                string email = dataGridView1.Rows[e.RowIndex].Cells["email"].Value.ToString();
                string password = dataGridView1.Rows[e.RowIndex].Cells["password"].Value.ToString();
                string dt_nasc = dataGridView1.Rows[e.RowIndex].Cells["dt_nasc"].Value.ToString();


                // Chama seu método
                editar_dados(id, nickname, nome, email, password, dt_nasc); 
               
            }

            // Verifica se clicou no excluir
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Excluir")
            {
                //faz um validação se realmente deseja excluir o item
                DialogResult confirmacao = MessageBox.Show(
                    "Tem certeza que deseja excluir este registro?",
                    "Atenção!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmacao == DialogResult.Yes)
                {
                    // Chama seu método de excluir passando o ID
                    excluir_dados(id);
                    carregar_Dados();
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
            GerenciadorLogs.RegistrarLog("Admin acessou a tela de logs.");
            LogsSystem logs = new LogsSystem();
            logs.Show();
            this.Hide();
        }


        //fazer logout
        private void bt_logout_Click(object sender, EventArgs e)
        {
            GerenciadorLogs.RegistrarLog("Admin realizou logout.");
            Login login = new Login();
            login.Show();
            this.Hide();
        }

    }
}
