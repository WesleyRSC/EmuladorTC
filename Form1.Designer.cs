namespace EmuladorTC
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.botaoConectar = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mensagens = new System.Windows.Forms.TabPage();
            this.btnSalvarTab1 = new System.Windows.Forms.Button();
            this.debug = new System.Windows.Forms.Label();
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.txtTempoExibicao = new System.Windows.Forms.TextBox();
            this.TempoExibicao = new System.Windows.Forms.Label();
            this.texto4 = new System.Windows.Forms.Label();
            this.texto3 = new System.Windows.Forms.Label();
            this.texto2 = new System.Windows.Forms.Label();
            this.texto1 = new System.Windows.Forms.Label();
            this.txtTexto4 = new System.Windows.Forms.TextBox();
            this.txtTexto3 = new System.Windows.Forms.TextBox();
            this.txtTexto2 = new System.Windows.Forms.TextBox();
            this.txtTexto1 = new System.Windows.Forms.TextBox();
            this.config = new System.Windows.Forms.TabPage();
            this.btnSalvarTab2 = new System.Windows.Forms.Button();
            this.wifi = new System.Windows.Forms.GroupBox();
            this.checkBoxWifi = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMac = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rbDhcp = new System.Windows.Forms.RadioButton();
            this.rbIpFixo = new System.Windows.Forms.RadioButton();
            this.txtNomeCliente = new System.Windows.Forms.TextBox();
            this.txtGatewayCliente = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mascaraCliente = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ipCliente = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ipServidor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.porta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.botaoConsulta = new System.Windows.Forms.Button();
            this.txtBuscarProduto = new System.Windows.Forms.TextBox();
            this.txtResultadoConsulta = new System.Windows.Forms.TextBox();
            this.pbImagemG2 = new System.Windows.Forms.PictureBox();
            this.ppFundo = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbModelo = new System.Windows.Forms.ComboBox();
            this.lblModelo = new System.Windows.Forms.Label();
            this.txtResultadoConsulta2 = new System.Windows.Forms.TextBox();
            this.pbGifImagem = new System.Windows.Forms.PictureBox();
            this.pReiniciar = new System.Windows.Forms.Panel();
            this.pCarregar2 = new System.Windows.Forms.Panel();
            this.pCarregar3 = new System.Windows.Forms.Panel();
            this.pCarregar1 = new System.Windows.Forms.Panel();
            this.lblCarregando = new System.Windows.Forms.Label();
            this.lblBuscaPrecoG2 = new System.Windows.Forms.Label();
            this.lblBemVindo = new System.Windows.Forms.Label();
            this.tReiniciar = new System.Windows.Forms.Timer(this.components);
            this.pReiniciarConfig = new System.Windows.Forms.Panel();
            this.lblIpLocal = new System.Windows.Forms.Label();
            this.lblIpServidor = new System.Windows.Forms.Label();
            this.lblInteface = new System.Windows.Forms.Label();
            this.pCarregar6 = new System.Windows.Forms.Panel();
            this.pCarregar5 = new System.Windows.Forms.Panel();
            this.pCarregar4 = new System.Windows.Forms.Panel();
            this.lblAoServidor = new System.Windows.Forms.Label();
            this.lblConectando = new System.Windows.Forms.Label();
            this.txtIniciaG1 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.mensagens.SuspendLayout();
            this.config.SuspendLayout();
            this.wifi.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagemG2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppFundo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGifImagem)).BeginInit();
            this.pReiniciar.SuspendLayout();
            this.pReiniciarConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // botaoConectar
            // 
            this.botaoConectar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(161)))), ((int)(((byte)(0)))));
            this.botaoConectar.FlatAppearance.BorderSize = 0;
            this.botaoConectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botaoConectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botaoConectar.ForeColor = System.Drawing.Color.White;
            this.botaoConectar.Location = new System.Drawing.Point(5, 41);
            this.botaoConectar.Name = "botaoConectar";
            this.botaoConectar.Size = new System.Drawing.Size(379, 29);
            this.botaoConectar.TabIndex = 0;
            this.botaoConectar.Text = "Conectar";
            this.botaoConectar.UseVisualStyleBackColor = false;
            this.botaoConectar.Click += new System.EventHandler(this.Button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.mensagens);
            this.tabControl1.Controls.Add(this.config);
            this.tabControl1.Location = new System.Drawing.Point(390, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(230, 518);
            this.tabControl1.TabIndex = 3;
            // 
            // mensagens
            // 
            this.mensagens.BackColor = System.Drawing.Color.WhiteSmoke;
            this.mensagens.Controls.Add(this.btnSalvarTab1);
            this.mensagens.Controls.Add(this.debug);
            this.mensagens.Controls.Add(this.txtDebug);
            this.mensagens.Controls.Add(this.txtTempoExibicao);
            this.mensagens.Controls.Add(this.TempoExibicao);
            this.mensagens.Controls.Add(this.texto4);
            this.mensagens.Controls.Add(this.texto3);
            this.mensagens.Controls.Add(this.texto2);
            this.mensagens.Controls.Add(this.texto1);
            this.mensagens.Controls.Add(this.txtTexto4);
            this.mensagens.Controls.Add(this.txtTexto3);
            this.mensagens.Controls.Add(this.txtTexto2);
            this.mensagens.Controls.Add(this.txtTexto1);
            this.mensagens.Location = new System.Drawing.Point(4, 22);
            this.mensagens.Name = "mensagens";
            this.mensagens.Padding = new System.Windows.Forms.Padding(3);
            this.mensagens.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mensagens.Size = new System.Drawing.Size(222, 492);
            this.mensagens.TabIndex = 0;
            this.mensagens.Text = "Mensagens";
            // 
            // btnSalvarTab1
            // 
            this.btnSalvarTab1.Location = new System.Drawing.Point(6, 248);
            this.btnSalvarTab1.Name = "btnSalvarTab1";
            this.btnSalvarTab1.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarTab1.TabIndex = 12;
            this.btnSalvarTab1.Text = "Salvar";
            this.btnSalvarTab1.UseVisualStyleBackColor = true;
            this.btnSalvarTab1.Click += new System.EventHandler(this.btnSalvarTab1_Click);
            // 
            // debug
            // 
            this.debug.AutoSize = true;
            this.debug.Location = new System.Drawing.Point(17, 276);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(39, 13);
            this.debug.TabIndex = 11;
            this.debug.Text = "Debug";
            // 
            // txtDebug
            // 
            this.txtDebug.Location = new System.Drawing.Point(6, 293);
            this.txtDebug.Multiline = true;
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.ReadOnly = true;
            this.txtDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDebug.Size = new System.Drawing.Size(210, 193);
            this.txtDebug.TabIndex = 10;
            this.txtDebug.TextChanged += new System.EventHandler(this.TxtDebug_TextChanged);
            // 
            // txtTempoExibicao
            // 
            this.txtTempoExibicao.Location = new System.Drawing.Point(17, 215);
            this.txtTempoExibicao.Name = "txtTempoExibicao";
            this.txtTempoExibicao.Size = new System.Drawing.Size(43, 20);
            this.txtTempoExibicao.TabIndex = 9;
            this.txtTempoExibicao.Text = "5";
            this.txtTempoExibicao.TextChanged += new System.EventHandler(this.txtTempoExibicao_TextChanged);
            // 
            // TempoExibicao
            // 
            this.TempoExibicao.AutoSize = true;
            this.TempoExibicao.Location = new System.Drawing.Point(17, 197);
            this.TempoExibicao.Name = "TempoExibicao";
            this.TempoExibicao.Size = new System.Drawing.Size(98, 13);
            this.TempoExibicao.TabIndex = 8;
            this.TempoExibicao.Text = "Tempo de Exibição";
            // 
            // texto4
            // 
            this.texto4.AutoSize = true;
            this.texto4.Location = new System.Drawing.Point(17, 157);
            this.texto4.Name = "texto4";
            this.texto4.Size = new System.Drawing.Size(43, 13);
            this.texto4.TabIndex = 7;
            this.texto4.Text = "Texto 4";
            // 
            // texto3
            // 
            this.texto3.AutoSize = true;
            this.texto3.Location = new System.Drawing.Point(17, 114);
            this.texto3.Name = "texto3";
            this.texto3.Size = new System.Drawing.Size(43, 13);
            this.texto3.TabIndex = 6;
            this.texto3.Text = "Texto 3";
            // 
            // texto2
            // 
            this.texto2.AutoSize = true;
            this.texto2.Location = new System.Drawing.Point(17, 71);
            this.texto2.Name = "texto2";
            this.texto2.Size = new System.Drawing.Size(43, 13);
            this.texto2.TabIndex = 5;
            this.texto2.Text = "Texto 2";
            // 
            // texto1
            // 
            this.texto1.AutoSize = true;
            this.texto1.Location = new System.Drawing.Point(17, 27);
            this.texto1.Name = "texto1";
            this.texto1.Size = new System.Drawing.Size(43, 13);
            this.texto1.TabIndex = 4;
            this.texto1.Text = "Texto 1";
            // 
            // txtTexto4
            // 
            this.txtTexto4.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTexto4.Location = new System.Drawing.Point(17, 173);
            this.txtTexto4.MaxLength = 20;
            this.txtTexto4.Name = "txtTexto4";
            this.txtTexto4.Size = new System.Drawing.Size(155, 23);
            this.txtTexto4.TabIndex = 3;
            this.txtTexto4.Text = "TEXTO4";
            // 
            // txtTexto3
            // 
            this.txtTexto3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTexto3.Location = new System.Drawing.Point(17, 130);
            this.txtTexto3.MaxLength = 20;
            this.txtTexto3.Name = "txtTexto3";
            this.txtTexto3.Size = new System.Drawing.Size(155, 23);
            this.txtTexto3.TabIndex = 2;
            this.txtTexto3.Text = "TEXTO3";
            // 
            // txtTexto2
            // 
            this.txtTexto2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTexto2.Location = new System.Drawing.Point(17, 87);
            this.txtTexto2.MaxLength = 20;
            this.txtTexto2.Name = "txtTexto2";
            this.txtTexto2.Size = new System.Drawing.Size(155, 23);
            this.txtTexto2.TabIndex = 1;
            this.txtTexto2.Text = "TEXTO2";
            // 
            // txtTexto1
            // 
            this.txtTexto1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTexto1.Location = new System.Drawing.Point(17, 46);
            this.txtTexto1.MaxLength = 20;
            this.txtTexto1.Name = "txtTexto1";
            this.txtTexto1.Size = new System.Drawing.Size(155, 23);
            this.txtTexto1.TabIndex = 0;
            this.txtTexto1.Text = "TEXTO1";
            // 
            // config
            // 
            this.config.BackColor = System.Drawing.Color.WhiteSmoke;
            this.config.Controls.Add(this.btnSalvarTab2);
            this.config.Controls.Add(this.wifi);
            this.config.Controls.Add(this.groupBox2);
            this.config.Controls.Add(this.groupBox1);
            this.config.Location = new System.Drawing.Point(4, 22);
            this.config.Name = "config";
            this.config.Padding = new System.Windows.Forms.Padding(3);
            this.config.Size = new System.Drawing.Size(222, 492);
            this.config.TabIndex = 1;
            this.config.Text = "Configurações";
            // 
            // btnSalvarTab2
            // 
            this.btnSalvarTab2.Location = new System.Drawing.Point(10, 458);
            this.btnSalvarTab2.Name = "btnSalvarTab2";
            this.btnSalvarTab2.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarTab2.TabIndex = 13;
            this.btnSalvarTab2.Text = "Salvar";
            this.btnSalvarTab2.UseVisualStyleBackColor = true;
            this.btnSalvarTab2.Click += new System.EventHandler(this.btnSalvarTab1_Click);
            // 
            // wifi
            // 
            this.wifi.Controls.Add(this.checkBoxWifi);
            this.wifi.Location = new System.Drawing.Point(4, 290);
            this.wifi.Name = "wifi";
            this.wifi.Size = new System.Drawing.Size(212, 180);
            this.wifi.TabIndex = 7;
            this.wifi.TabStop = false;
            this.wifi.Text = "Wi-Fi";
            // 
            // checkBoxWifi
            // 
            this.checkBoxWifi.AutoSize = true;
            this.checkBoxWifi.Location = new System.Drawing.Point(41, 0);
            this.checkBoxWifi.Name = "checkBoxWifi";
            this.checkBoxWifi.Size = new System.Drawing.Size(15, 14);
            this.checkBoxWifi.TabIndex = 0;
            this.checkBoxWifi.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMac);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.rbDhcp);
            this.groupBox2.Controls.Add(this.rbIpFixo);
            this.groupBox2.Controls.Add(this.txtNomeCliente);
            this.groupBox2.Controls.Add(this.txtGatewayCliente);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.mascaraCliente);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.ipCliente);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(4, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 181);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cliente";
            // 
            // txtMac
            // 
            this.txtMac.Location = new System.Drawing.Point(51, 145);
            this.txtMac.Name = "txtMac";
            this.txtMac.Size = new System.Drawing.Size(155, 20);
            this.txtMac.TabIndex = 7;
            this.txtMac.TabStop = false;
            this.txtMac.Text = "00:00:00:00:00:00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "MAC:";
            // 
            // rbDhcp
            // 
            this.rbDhcp.AutoSize = true;
            this.rbDhcp.Location = new System.Drawing.Point(112, 44);
            this.rbDhcp.Name = "rbDhcp";
            this.rbDhcp.Size = new System.Drawing.Size(55, 17);
            this.rbDhcp.TabIndex = 11;
            this.rbDhcp.Text = "DHCP";
            this.rbDhcp.UseVisualStyleBackColor = true;
            // 
            // rbIpFixo
            // 
            this.rbIpFixo.AutoSize = true;
            this.rbIpFixo.Checked = true;
            this.rbIpFixo.Location = new System.Drawing.Point(50, 44);
            this.rbIpFixo.Name = "rbIpFixo";
            this.rbIpFixo.Size = new System.Drawing.Size(56, 17);
            this.rbIpFixo.TabIndex = 10;
            this.rbIpFixo.TabStop = true;
            this.rbIpFixo.Text = "Ip Fixo";
            this.rbIpFixo.UseVisualStyleBackColor = true;
            // 
            // txtNomeCliente
            // 
            this.txtNomeCliente.Location = new System.Drawing.Point(51, 19);
            this.txtNomeCliente.Name = "txtNomeCliente";
            this.txtNomeCliente.Size = new System.Drawing.Size(155, 20);
            this.txtNomeCliente.TabIndex = 3;
            this.txtNomeCliente.Text = "EmuTC";
            // 
            // txtGatewayCliente
            // 
            this.txtGatewayCliente.Location = new System.Drawing.Point(51, 119);
            this.txtGatewayCliente.Name = "txtGatewayCliente";
            this.txtGatewayCliente.Size = new System.Drawing.Size(155, 20);
            this.txtGatewayCliente.TabIndex = 6;
            this.txtGatewayCliente.Text = "192.168.0.1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Gateway:";
            // 
            // mascaraCliente
            // 
            this.mascaraCliente.Location = new System.Drawing.Point(51, 93);
            this.mascaraCliente.Name = "mascaraCliente";
            this.mascaraCliente.Size = new System.Drawing.Size(155, 20);
            this.mascaraCliente.TabIndex = 5;
            this.mascaraCliente.Text = "255.255.255.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Mascara:";
            // 
            // ipCliente
            // 
            this.ipCliente.Location = new System.Drawing.Point(51, 67);
            this.ipCliente.Name = "ipCliente";
            this.ipCliente.Size = new System.Drawing.Size(155, 20);
            this.ipCliente.TabIndex = 4;
            this.ipCliente.Text = "192.168.0.100";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nome:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ipServidor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.porta);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(4, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 91);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Servidor";
            // 
            // ipServidor
            // 
            this.ipServidor.Location = new System.Drawing.Point(51, 28);
            this.ipServidor.Name = "ipServidor";
            this.ipServidor.Size = new System.Drawing.Size(155, 20);
            this.ipServidor.TabIndex = 1;
            this.ipServidor.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IP:";
            // 
            // porta
            // 
            this.porta.Location = new System.Drawing.Point(51, 54);
            this.porta.Name = "porta";
            this.porta.Size = new System.Drawing.Size(155, 20);
            this.porta.TabIndex = 2;
            this.porta.Text = "6500";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Porta:";
            // 
            // botaoConsulta
            // 
            this.botaoConsulta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(161)))), ((int)(((byte)(0)))));
            this.botaoConsulta.FlatAppearance.BorderSize = 0;
            this.botaoConsulta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botaoConsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botaoConsulta.ForeColor = System.Drawing.Color.White;
            this.botaoConsulta.Location = new System.Drawing.Point(117, 492);
            this.botaoConsulta.Name = "botaoConsulta";
            this.botaoConsulta.Size = new System.Drawing.Size(151, 32);
            this.botaoConsulta.TabIndex = 7;
            this.botaoConsulta.Text = "Consultar";
            this.botaoConsulta.UseVisualStyleBackColor = false;
            this.botaoConsulta.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // txtBuscarProduto
            // 
            this.txtBuscarProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarProduto.Location = new System.Drawing.Point(132, 340);
            this.txtBuscarProduto.Name = "txtBuscarProduto";
            this.txtBuscarProduto.Size = new System.Drawing.Size(126, 26);
            this.txtBuscarProduto.TabIndex = 8;
            this.txtBuscarProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtResultadoConsulta
            // 
            this.txtResultadoConsulta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(186)))), ((int)(((byte)(20)))));
            this.txtResultadoConsulta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResultadoConsulta.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultadoConsulta.ForeColor = System.Drawing.SystemColors.Window;
            this.txtResultadoConsulta.Location = new System.Drawing.Point(81, 199);
            this.txtResultadoConsulta.Name = "txtResultadoConsulta";
            this.txtResultadoConsulta.ReadOnly = true;
            this.txtResultadoConsulta.Size = new System.Drawing.Size(227, 23);
            this.txtResultadoConsulta.TabIndex = 9;
            this.txtResultadoConsulta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pbImagemG2
            // 
            this.pbImagemG2.ErrorImage = null;
            this.pbImagemG2.Image = ((System.Drawing.Image)(resources.GetObject("pbImagemG2.Image")));
            this.pbImagemG2.InitialImage = null;
            this.pbImagemG2.Location = new System.Drawing.Point(5, 78);
            this.pbImagemG2.Name = "pbImagemG2";
            this.pbImagemG2.Size = new System.Drawing.Size(383, 408);
            this.pbImagemG2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImagemG2.TabIndex = 10;
            this.pbImagemG2.TabStop = false;
            // 
            // ppFundo
            // 
            this.ppFundo.Image = ((System.Drawing.Image)(resources.GetObject("ppFundo.Image")));
            this.ppFundo.Location = new System.Drawing.Point(-2, 0);
            this.ppFundo.Name = "ppFundo";
            this.ppFundo.Size = new System.Drawing.Size(630, 554);
            this.ppFundo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ppFundo.TabIndex = 11;
            this.ppFundo.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbModelo
            // 
            this.cbModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbModelo.FormattingEnabled = true;
            this.cbModelo.Items.AddRange(new object[] {
            "Busca Preço G2",
            "Busca Preço"});
            this.cbModelo.Location = new System.Drawing.Point(92, 9);
            this.cbModelo.Name = "cbModelo";
            this.cbModelo.Size = new System.Drawing.Size(152, 28);
            this.cbModelo.TabIndex = 12;
            this.cbModelo.SelectedIndexChanged += new System.EventHandler(this.CbModelo_SelectedIndexChanged);
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModelo.Location = new System.Drawing.Point(12, 12);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(65, 20);
            this.lblModelo.TabIndex = 13;
            this.lblModelo.Text = "Modelo:";
            // 
            // txtResultadoConsulta2
            // 
            this.txtResultadoConsulta2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(186)))), ((int)(((byte)(20)))));
            this.txtResultadoConsulta2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResultadoConsulta2.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultadoConsulta2.ForeColor = System.Drawing.SystemColors.Window;
            this.txtResultadoConsulta2.Location = new System.Drawing.Point(81, 223);
            this.txtResultadoConsulta2.Name = "txtResultadoConsulta2";
            this.txtResultadoConsulta2.ReadOnly = true;
            this.txtResultadoConsulta2.Size = new System.Drawing.Size(227, 23);
            this.txtResultadoConsulta2.TabIndex = 14;
            this.txtResultadoConsulta2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pbGifImagem
            // 
            this.pbGifImagem.Location = new System.Drawing.Point(118, 232);
            this.pbGifImagem.Name = "pbGifImagem";
            this.pbGifImagem.Size = new System.Drawing.Size(148, 110);
            this.pbGifImagem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGifImagem.TabIndex = 15;
            this.pbGifImagem.TabStop = false;
            this.pbGifImagem.Visible = false;
            // 
            // pReiniciar
            // 
            this.pReiniciar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pReiniciar.Controls.Add(this.pCarregar2);
            this.pReiniciar.Controls.Add(this.pCarregar3);
            this.pReiniciar.Controls.Add(this.pCarregar1);
            this.pReiniciar.Controls.Add(this.lblCarregando);
            this.pReiniciar.Controls.Add(this.lblBuscaPrecoG2);
            this.pReiniciar.Controls.Add(this.lblBemVindo);
            this.pReiniciar.Location = new System.Drawing.Point(118, 228);
            this.pReiniciar.Name = "pReiniciar";
            this.pReiniciar.Size = new System.Drawing.Size(148, 114);
            this.pReiniciar.TabIndex = 16;
            // 
            // pCarregar2
            // 
            this.pCarregar2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.pCarregar2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.pCarregar2.Location = new System.Drawing.Point(29, 99);
            this.pCarregar2.Name = "pCarregar2";
            this.pCarregar2.Size = new System.Drawing.Size(6, 6);
            this.pCarregar2.TabIndex = 4;
            // 
            // pCarregar3
            // 
            this.pCarregar3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.pCarregar3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.pCarregar3.Location = new System.Drawing.Point(43, 99);
            this.pCarregar3.Name = "pCarregar3";
            this.pCarregar3.Size = new System.Drawing.Size(6, 6);
            this.pCarregar3.TabIndex = 4;
            // 
            // pCarregar1
            // 
            this.pCarregar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.pCarregar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.pCarregar1.Location = new System.Drawing.Point(17, 99);
            this.pCarregar1.Name = "pCarregar1";
            this.pCarregar1.Size = new System.Drawing.Size(6, 6);
            this.pCarregar1.TabIndex = 3;
            // 
            // lblCarregando
            // 
            this.lblCarregando.AutoSize = true;
            this.lblCarregando.BackColor = System.Drawing.Color.Transparent;
            this.lblCarregando.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarregando.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(161)))), ((int)(((byte)(0)))));
            this.lblCarregando.Location = new System.Drawing.Point(12, 85);
            this.lblCarregando.Name = "lblCarregando";
            this.lblCarregando.Size = new System.Drawing.Size(67, 13);
            this.lblCarregando.TabIndex = 2;
            this.lblCarregando.Text = "Carregando";
            // 
            // lblBuscaPrecoG2
            // 
            this.lblBuscaPrecoG2.AutoSize = true;
            this.lblBuscaPrecoG2.BackColor = System.Drawing.Color.Transparent;
            this.lblBuscaPrecoG2.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscaPrecoG2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.lblBuscaPrecoG2.Location = new System.Drawing.Point(10, 48);
            this.lblBuscaPrecoG2.Name = "lblBuscaPrecoG2";
            this.lblBuscaPrecoG2.Size = new System.Drawing.Size(135, 20);
            this.lblBuscaPrecoG2.TabIndex = 1;
            this.lblBuscaPrecoG2.Text = "BUSCA PREÇO G2";
            // 
            // lblBemVindo
            // 
            this.lblBemVindo.AutoSize = true;
            this.lblBemVindo.BackColor = System.Drawing.Color.Transparent;
            this.lblBemVindo.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBemVindo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(161)))), ((int)(((byte)(0)))));
            this.lblBemVindo.Location = new System.Drawing.Point(11, 35);
            this.lblBemVindo.Name = "lblBemVindo";
            this.lblBemVindo.Size = new System.Drawing.Size(91, 14);
            this.lblBemVindo.TabIndex = 0;
            this.lblBemVindo.Text = "BEM VINDO AO";
            // 
            // tReiniciar
            // 
            this.tReiniciar.Interval = 1000;
            this.tReiniciar.Tick += new System.EventHandler(this.TReiniciar_Tick);
            // 
            // pReiniciarConfig
            // 
            this.pReiniciarConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pReiniciarConfig.Controls.Add(this.lblIpLocal);
            this.pReiniciarConfig.Controls.Add(this.lblIpServidor);
            this.pReiniciarConfig.Controls.Add(this.lblInteface);
            this.pReiniciarConfig.Controls.Add(this.pCarregar6);
            this.pReiniciarConfig.Controls.Add(this.pCarregar5);
            this.pReiniciarConfig.Controls.Add(this.pCarregar4);
            this.pReiniciarConfig.Controls.Add(this.lblAoServidor);
            this.pReiniciarConfig.Controls.Add(this.lblConectando);
            this.pReiniciarConfig.Location = new System.Drawing.Point(117, 228);
            this.pReiniciarConfig.Name = "pReiniciarConfig";
            this.pReiniciarConfig.Size = new System.Drawing.Size(150, 114);
            this.pReiniciarConfig.TabIndex = 17;
            this.pReiniciarConfig.Paint += new System.Windows.Forms.PaintEventHandler(this.PReiniciarConfig_Paint);
            // 
            // lblIpLocal
            // 
            this.lblIpLocal.AutoSize = true;
            this.lblIpLocal.BackColor = System.Drawing.Color.Transparent;
            this.lblIpLocal.Font = new System.Drawing.Font("Consolas", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIpLocal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.lblIpLocal.Location = new System.Drawing.Point(3, 85);
            this.lblIpLocal.Name = "lblIpLocal";
            this.lblIpLocal.Size = new System.Drawing.Size(45, 10);
            this.lblIpLocal.TabIndex = 10;
            this.lblIpLocal.Text = "IP LOCAL";
            // 
            // lblIpServidor
            // 
            this.lblIpServidor.AutoSize = true;
            this.lblIpServidor.BackColor = System.Drawing.Color.Transparent;
            this.lblIpServidor.Font = new System.Drawing.Font("Consolas", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIpServidor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.lblIpServidor.Location = new System.Drawing.Point(3, 74);
            this.lblIpServidor.Name = "lblIpServidor";
            this.lblIpServidor.Size = new System.Drawing.Size(80, 10);
            this.lblIpServidor.TabIndex = 9;
            this.lblIpServidor.Text = "IP DO SERVIDOR:";
            // 
            // lblInteface
            // 
            this.lblInteface.AutoSize = true;
            this.lblInteface.BackColor = System.Drawing.Color.Transparent;
            this.lblInteface.Font = new System.Drawing.Font("Consolas", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInteface.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.lblInteface.Location = new System.Drawing.Point(3, 62);
            this.lblInteface.Name = "lblInteface";
            this.lblInteface.Size = new System.Drawing.Size(55, 10);
            this.lblInteface.TabIndex = 5;
            this.lblInteface.Text = "INTERFACE:";
            // 
            // pCarregar6
            // 
            this.pCarregar6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.pCarregar6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.pCarregar6.Location = new System.Drawing.Point(82, 45);
            this.pCarregar6.Name = "pCarregar6";
            this.pCarregar6.Size = new System.Drawing.Size(6, 6);
            this.pCarregar6.TabIndex = 8;
            // 
            // pCarregar5
            // 
            this.pCarregar5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.pCarregar5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.pCarregar5.Location = new System.Drawing.Point(70, 45);
            this.pCarregar5.Name = "pCarregar5";
            this.pCarregar5.Size = new System.Drawing.Size(6, 6);
            this.pCarregar5.TabIndex = 8;
            // 
            // pCarregar4
            // 
            this.pCarregar4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.pCarregar4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.pCarregar4.Location = new System.Drawing.Point(58, 45);
            this.pCarregar4.Name = "pCarregar4";
            this.pCarregar4.Size = new System.Drawing.Size(6, 6);
            this.pCarregar4.TabIndex = 7;
            // 
            // lblAoServidor
            // 
            this.lblAoServidor.AutoSize = true;
            this.lblAoServidor.BackColor = System.Drawing.Color.Transparent;
            this.lblAoServidor.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAoServidor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.lblAoServidor.Location = new System.Drawing.Point(21, 22);
            this.lblAoServidor.Name = "lblAoServidor";
            this.lblAoServidor.Size = new System.Drawing.Size(108, 20);
            this.lblAoServidor.TabIndex = 6;
            this.lblAoServidor.Text = "AO SERVIDOR";
            // 
            // lblConectando
            // 
            this.lblConectando.AutoSize = true;
            this.lblConectando.BackColor = System.Drawing.Color.Transparent;
            this.lblConectando.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConectando.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(29)))), ((int)(((byte)(91)))));
            this.lblConectando.Location = new System.Drawing.Point(26, 6);
            this.lblConectando.Name = "lblConectando";
            this.lblConectando.Size = new System.Drawing.Size(99, 20);
            this.lblConectando.TabIndex = 5;
            this.lblConectando.Text = "CONECTANDO";
            // 
            // txtIniciaG1
            // 
            this.txtIniciaG1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(186)))), ((int)(((byte)(20)))));
            this.txtIniciaG1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIniciaG1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIniciaG1.ForeColor = System.Drawing.SystemColors.Window;
            this.txtIniciaG1.Location = new System.Drawing.Point(81, 199);
            this.txtIniciaG1.Name = "txtIniciaG1";
            this.txtIniciaG1.ReadOnly = true;
            this.txtIniciaG1.Size = new System.Drawing.Size(227, 23);
            this.txtIniciaG1.TabIndex = 18;
            this.txtIniciaG1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 546);
            this.Controls.Add(this.txtIniciaG1);
            this.Controls.Add(this.txtBuscarProduto);
            this.Controls.Add(this.pReiniciarConfig);
            this.Controls.Add(this.pReiniciar);
            this.Controls.Add(this.txtResultadoConsulta2);
            this.Controls.Add(this.txtResultadoConsulta);
            this.Controls.Add(this.pbGifImagem);
            this.Controls.Add(this.pbImagemG2);
            this.Controls.Add(this.lblModelo);
            this.Controls.Add(this.cbModelo);
            this.Controls.Add(this.botaoConsulta);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.botaoConectar);
            this.Controls.Add(this.ppFundo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emulador Busca Preço G2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.mensagens.ResumeLayout(false);
            this.mensagens.PerformLayout();
            this.config.ResumeLayout(false);
            this.wifi.ResumeLayout(false);
            this.wifi.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagemG2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ppFundo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGifImagem)).EndInit();
            this.pReiniciar.ResumeLayout(false);
            this.pReiniciar.PerformLayout();
            this.pReiniciarConfig.ResumeLayout(false);
            this.pReiniciarConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button botaoConectar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage mensagens;
        private System.Windows.Forms.TabPage config;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox porta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ipServidor;
        private System.Windows.Forms.TextBox ipCliente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNomeCliente;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtGatewayCliente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox mascaraCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button botaoConsulta;
        private System.Windows.Forms.TextBox txtBuscarProduto;
        private System.Windows.Forms.TextBox txtResultadoConsulta;
        private System.Windows.Forms.Label texto4;
        private System.Windows.Forms.Label texto3;
        private System.Windows.Forms.Label texto2;
        private System.Windows.Forms.Label texto1;
        private System.Windows.Forms.TextBox txtTexto4;
        private System.Windows.Forms.TextBox txtTexto3;
        private System.Windows.Forms.TextBox txtTexto2;
        private System.Windows.Forms.TextBox txtTexto1;
        private System.Windows.Forms.TextBox txtTempoExibicao;
        private System.Windows.Forms.Label TempoExibicao;
        private System.Windows.Forms.PictureBox pbImagemG2;
        private System.Windows.Forms.PictureBox ppFundo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton rbDhcp;
        private System.Windows.Forms.RadioButton rbIpFixo;
        private System.Windows.Forms.TextBox txtMac;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbModelo;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.GroupBox wifi;
        private System.Windows.Forms.CheckBox checkBoxWifi;
        private System.Windows.Forms.Label debug;
        private System.Windows.Forms.TextBox txtDebug;
        private System.Windows.Forms.TextBox txtResultadoConsulta2;
        private System.Windows.Forms.PictureBox pbGifImagem;
        private System.Windows.Forms.Panel pReiniciar;
        private System.Windows.Forms.Label lblCarregando;
        private System.Windows.Forms.Label lblBuscaPrecoG2;
        private System.Windows.Forms.Label lblBemVindo;
        private System.Windows.Forms.Panel pCarregar1;
        private System.Windows.Forms.Panel pCarregar2;
        private System.Windows.Forms.Panel pCarregar3;
        private System.Windows.Forms.Timer tReiniciar;
        private System.Windows.Forms.Panel pReiniciarConfig;
        private System.Windows.Forms.Label lblAoServidor;
        private System.Windows.Forms.Label lblConectando;
        private System.Windows.Forms.Panel pCarregar4;
        private System.Windows.Forms.Panel pCarregar5;
        private System.Windows.Forms.Panel pCarregar6;
        private System.Windows.Forms.Label lblIpLocal;
        private System.Windows.Forms.Label lblIpServidor;
        private System.Windows.Forms.Label lblInteface;
        private System.Windows.Forms.Button btnSalvarTab1;
        private System.Windows.Forms.Button btnSalvarTab2;
        private System.Windows.Forms.TextBox txtIniciaG1;
    }
}

