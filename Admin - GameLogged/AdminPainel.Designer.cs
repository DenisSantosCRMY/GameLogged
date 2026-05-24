namespace Admin___GameLogged
{
    partial class AdminPainel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_dashboard = new System.Windows.Forms.Button();
            this.bt_usuario = new System.Windows.Forms.Button();
            this.bt_solicitações = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bt_logout = new System.Windows.Forms.Button();
            this.btNovoCadastro = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btAtualizar = new System.Windows.Forms.Button();
            this.btLogs = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_dashboard
            // 
            this.bt_dashboard.Location = new System.Drawing.Point(12, 124);
            this.bt_dashboard.Name = "bt_dashboard";
            this.bt_dashboard.Size = new System.Drawing.Size(216, 34);
            this.bt_dashboard.TabIndex = 0;
            this.bt_dashboard.Text = "Dashboard";
            this.bt_dashboard.UseVisualStyleBackColor = true;
            this.bt_dashboard.Click += new System.EventHandler(this.bt_dashboard_Click);
            // 
            // bt_usuario
            // 
            this.bt_usuario.Location = new System.Drawing.Point(12, 164);
            this.bt_usuario.Name = "bt_usuario";
            this.bt_usuario.Size = new System.Drawing.Size(216, 37);
            this.bt_usuario.TabIndex = 1;
            this.bt_usuario.Text = "Usuários";
            this.bt_usuario.UseVisualStyleBackColor = true;
            this.bt_usuario.Click += new System.EventHandler(this.bt_usuario_Click);
            // 
            // bt_solicitações
            // 
            this.bt_solicitações.Location = new System.Drawing.Point(12, 529);
            this.bt_solicitações.Name = "bt_solicitações";
            this.bt_solicitações.Size = new System.Drawing.Size(216, 38);
            this.bt_solicitações.TabIndex = 2;
            this.bt_solicitações.Text = "Solicitações";
            this.bt_solicitações.UseVisualStyleBackColor = true;
            this.bt_solicitações.Click += new System.EventHandler(this.bt_solicitações_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(255, 123);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(874, 532);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // bt_logout
            // 
            this.bt_logout.Location = new System.Drawing.Point(12, 620);
            this.bt_logout.Name = "bt_logout";
            this.bt_logout.Size = new System.Drawing.Size(123, 35);
            this.bt_logout.TabIndex = 4;
            this.bt_logout.Text = "Sair";
            this.bt_logout.UseVisualStyleBackColor = true;
            this.bt_logout.Click += new System.EventHandler(this.bt_logout_Click);
            // 
            // btNovoCadastro
            // 
            this.btNovoCadastro.Location = new System.Drawing.Point(255, 79);
            this.btNovoCadastro.Name = "btNovoCadastro";
            this.btNovoCadastro.Size = new System.Drawing.Size(120, 38);
            this.btNovoCadastro.TabIndex = 5;
            this.btNovoCadastro.Text = "Novo Cadastro";
            this.btNovoCadastro.UseVisualStyleBackColor = true;
            this.btNovoCadastro.Click += new System.EventHandler(this.bt_novo_cadastro_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(216, 37);
            this.button1.TabIndex = 6;
            this.button1.Text = "Jogos";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 250);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(216, 37);
            this.button2.TabIndex = 7;
            this.button2.Text = "Plataformas";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btAtualizar
            // 
            this.btAtualizar.Location = new System.Drawing.Point(381, 79);
            this.btAtualizar.Name = "btAtualizar";
            this.btAtualizar.Size = new System.Drawing.Size(106, 38);
            this.btAtualizar.TabIndex = 8;
            this.btAtualizar.Text = "Atualizar";
            this.btAtualizar.UseVisualStyleBackColor = true;
            this.btAtualizar.Click += new System.EventHandler(this.btAtualizar_Click);
            // 
            // btLogs
            // 
            this.btLogs.Location = new System.Drawing.Point(141, 620);
            this.btLogs.Name = "btLogs";
            this.btLogs.Size = new System.Drawing.Size(103, 35);
            this.btLogs.TabIndex = 9;
            this.btLogs.Text = "Logs";
            this.btLogs.UseVisualStyleBackColor = true;
            this.btLogs.Click += new System.EventHandler(this.btLogs_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 293);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(216, 37);
            this.button3.TabIndex = 10;
            this.button3.Text = "Conquistas";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // AdminPainel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(17)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(1141, 667);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btLogs);
            this.Controls.Add(this.btAtualizar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btNovoCadastro);
            this.Controls.Add(this.bt_logout);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.bt_solicitações);
            this.Controls.Add(this.bt_usuario);
            this.Controls.Add(this.bt_dashboard);
            this.Name = "AdminPainel";
            this.Text = "Painel - Dash";
            this.Load += new System.EventHandler(this.Painel_Dash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_dashboard;
        private System.Windows.Forms.Button bt_usuario;
        private System.Windows.Forms.Button bt_solicitações;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button bt_logout;
        private System.Windows.Forms.Button btNovoCadastro;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btAtualizar;
        private System.Windows.Forms.Button btLogs;
        private System.Windows.Forms.Button button3;
    }
}