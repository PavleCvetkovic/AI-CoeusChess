namespace ChessTG
{
    partial class Form1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.novaIgraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDodajCrnog = new System.Windows.Forms.Button();
            this.btnDodajBelog = new System.Windows.Forms.Button();
            this.btnDodajTopa = new System.Windows.Forms.Button();
            this.lblNaPotezu = new System.Windows.Forms.Label();
            this.btnIgraj = new System.Windows.Forms.Button();
            this.lblPotezi = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(25, 29);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(471, 471);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novaIgraToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(702, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // novaIgraToolStripMenuItem
            // 
            this.novaIgraToolStripMenuItem.Name = "novaIgraToolStripMenuItem";
            this.novaIgraToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.novaIgraToolStripMenuItem.Text = "&Nova igra";
            this.novaIgraToolStripMenuItem.Click += new System.EventHandler(this.novaIgraToolStripMenuItem_Click);
            // 
            // btnDodajCrnog
            // 
            this.btnDodajCrnog.Location = new System.Drawing.Point(555, 62);
            this.btnDodajCrnog.Name = "btnDodajCrnog";
            this.btnDodajCrnog.Size = new System.Drawing.Size(112, 23);
            this.btnDodajCrnog.TabIndex = 2;
            this.btnDodajCrnog.Text = "Dodaj crnog kralja";
            this.btnDodajCrnog.UseVisualStyleBackColor = true;
            this.btnDodajCrnog.Click += new System.EventHandler(this.btnDodajCrnog_Click);
            // 
            // btnDodajBelog
            // 
            this.btnDodajBelog.Location = new System.Drawing.Point(555, 104);
            this.btnDodajBelog.Name = "btnDodajBelog";
            this.btnDodajBelog.Size = new System.Drawing.Size(112, 23);
            this.btnDodajBelog.TabIndex = 3;
            this.btnDodajBelog.Text = "Dodaj belog kralja";
            this.btnDodajBelog.UseVisualStyleBackColor = true;
            this.btnDodajBelog.Click += new System.EventHandler(this.btnDodajBelog_Click);
            // 
            // btnDodajTopa
            // 
            this.btnDodajTopa.Location = new System.Drawing.Point(555, 149);
            this.btnDodajTopa.Name = "btnDodajTopa";
            this.btnDodajTopa.Size = new System.Drawing.Size(112, 23);
            this.btnDodajTopa.TabIndex = 4;
            this.btnDodajTopa.Text = "Dodaj topa";
            this.btnDodajTopa.UseVisualStyleBackColor = true;
            this.btnDodajTopa.Click += new System.EventHandler(this.btnDodajTopa_Click);
            // 
            // lblNaPotezu
            // 
            this.lblNaPotezu.AutoSize = true;
            this.lblNaPotezu.Location = new System.Drawing.Point(632, 257);
            this.lblNaPotezu.Name = "lblNaPotezu";
            this.lblNaPotezu.Size = new System.Drawing.Size(35, 13);
            this.lblNaPotezu.TabIndex = 5;
            this.lblNaPotezu.Text = "label1";
            // 
            // btnIgraj
            // 
            this.btnIgraj.Location = new System.Drawing.Point(569, 191);
            this.btnIgraj.Name = "btnIgraj";
            this.btnIgraj.Size = new System.Drawing.Size(75, 23);
            this.btnIgraj.TabIndex = 6;
            this.btnIgraj.Text = "Igraj";
            this.btnIgraj.UseVisualStyleBackColor = true;
            this.btnIgraj.Click += new System.EventHandler(this.btnIgraj_Click);
            // 
            // lblPotezi
            // 
            this.lblPotezi.AutoSize = true;
            this.lblPotezi.Location = new System.Drawing.Point(632, 282);
            this.lblPotezi.Name = "lblPotezi";
            this.lblPotezi.Size = new System.Drawing.Size(0, 13);
            this.lblPotezi.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(632, 311);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(514, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Crni napadnut:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(542, 338);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(514, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Na potezu:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(514, 282);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Broj obradjenih poteza:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 591);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPotezi);
            this.Controls.Add(this.btnIgraj);
            this.Controls.Add(this.lblNaPotezu);
            this.Controls.Add(this.btnDodajTopa);
            this.Controls.Add(this.btnDodajBelog);
            this.Controls.Add(this.btnDodajCrnog);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "i";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem novaIgraToolStripMenuItem;
        private System.Windows.Forms.Button btnDodajCrnog;
        private System.Windows.Forms.Button btnDodajBelog;
        private System.Windows.Forms.Button btnDodajTopa;
        private System.Windows.Forms.Label lblNaPotezu;
        private System.Windows.Forms.Button btnIgraj;
        private System.Windows.Forms.Label lblPotezi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

