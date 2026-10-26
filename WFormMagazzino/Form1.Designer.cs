namespace WFormMagazzino
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.codice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prezzo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantita = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bElimina = new System.Windows.Forms.Button();
            this.bModifica = new System.Windows.Forms.Button();
            this.bCerca = new System.Windows.Forms.Button();
            this.bInserisci = new System.Windows.Forms.Button();
            this.nudQty = new System.Windows.Forms.NumericUpDown();
            this.nudPrezzo = new System.Windows.Forms.NumericUpDown();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.txtCodice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrezzo)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codice,
            this.nome,
            this.prezzo,
            this.quantita});
            this.dataGridView1.Location = new System.Drawing.Point(377, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(553, 225);
            this.dataGridView1.TabIndex = 3;
            // 
            // codice
            // 
            this.codice.HeaderText = "CODICE";
            this.codice.MinimumWidth = 6;
            this.codice.Name = "codice";
            this.codice.Width = 125;
            // 
            // nome
            // 
            this.nome.HeaderText = "NOME";
            this.nome.MinimumWidth = 6;
            this.nome.Name = "nome";
            this.nome.Width = 125;
            // 
            // prezzo
            // 
            this.prezzo.HeaderText = "PREZZO";
            this.prezzo.MinimumWidth = 6;
            this.prezzo.Name = "prezzo";
            this.prezzo.Width = 125;
            // 
            // quantita
            // 
            this.quantita.HeaderText = "QUANTITA";
            this.quantita.MinimumWidth = 6;
            this.quantita.Name = "quantita";
            this.quantita.Width = 125;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bElimina);
            this.groupBox1.Controls.Add(this.bModifica);
            this.groupBox1.Controls.Add(this.bCerca);
            this.groupBox1.Controls.Add(this.bInserisci);
            this.groupBox1.Controls.Add(this.nudQty);
            this.groupBox1.Controls.Add(this.nudPrezzo);
            this.groupBox1.Controls.Add(this.txtNome);
            this.groupBox1.Controls.Add(this.txtCodice);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 225);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PRODOTTO";
            // 
            // bElimina
            // 
            this.bElimina.Location = new System.Drawing.Point(180, 182);
            this.bElimina.Name = "bElimina";
            this.bElimina.Size = new System.Drawing.Size(130, 23);
            this.bElimina.TabIndex = 11;
            this.bElimina.Text = "ELIMINA";
            this.bElimina.UseVisualStyleBackColor = true;
            this.bElimina.Click += new System.EventHandler(this.bElimina_Click);
            // 
            // bModifica
            // 
            this.bModifica.Location = new System.Drawing.Point(180, 149);
            this.bModifica.Name = "bModifica";
            this.bModifica.Size = new System.Drawing.Size(130, 23);
            this.bModifica.TabIndex = 10;
            this.bModifica.Text = "MODIFICA";
            this.bModifica.UseVisualStyleBackColor = true;
            this.bModifica.Click += new System.EventHandler(this.bModifica_Click);
            // 
            // bCerca
            // 
            this.bCerca.Location = new System.Drawing.Point(180, 120);
            this.bCerca.Name = "bCerca";
            this.bCerca.Size = new System.Drawing.Size(130, 23);
            this.bCerca.TabIndex = 9;
            this.bCerca.Text = "CERCA";
            this.bCerca.UseVisualStyleBackColor = true;
            this.bCerca.Click += new System.EventHandler(this.bCerca_Click);
            // 
            // bInserisci
            // 
            this.bInserisci.Location = new System.Drawing.Point(180, 85);
            this.bInserisci.Name = "bInserisci";
            this.bInserisci.Size = new System.Drawing.Size(130, 23);
            this.bInserisci.TabIndex = 8;
            this.bInserisci.Text = "INSERISCI";
            this.bInserisci.UseVisualStyleBackColor = true;
            this.bInserisci.Click += new System.EventHandler(this.bInserisci_Click);
            // 
            // nudQty
            // 
            this.nudQty.Location = new System.Drawing.Point(18, 183);
            this.nudQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQty.Name = "nudQty";
            this.nudQty.Size = new System.Drawing.Size(120, 22);
            this.nudQty.TabIndex = 7;
            this.nudQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudPrezzo
            // 
            this.nudPrezzo.DecimalPlaces = 2;
            this.nudPrezzo.Location = new System.Drawing.Point(18, 111);
            this.nudPrezzo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPrezzo.Name = "nudPrezzo";
            this.nudPrezzo.Size = new System.Drawing.Size(120, 22);
            this.nudPrezzo.TabIndex = 6;
            this.nudPrezzo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(18, 50);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(130, 22);
            this.txtNome.TabIndex = 5;
            // 
            // txtCodice
            // 
            this.txtCodice.Location = new System.Drawing.Point(180, 50);
            this.txtCodice.Name = "txtCodice";
            this.txtCodice.Size = new System.Drawing.Size(130, 22);
            this.txtCodice.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "CODICE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "QUANTITA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "PREZZO";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "NOME";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 250);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrezzo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn codice;
        private System.Windows.Forms.DataGridViewTextBoxColumn nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn prezzo;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantita;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bElimina;
        private System.Windows.Forms.Button bModifica;
        private System.Windows.Forms.Button bCerca;
        private System.Windows.Forms.Button bInserisci;
        private System.Windows.Forms.NumericUpDown nudQty;
        private System.Windows.Forms.NumericUpDown nudPrezzo;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.TextBox txtCodice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

