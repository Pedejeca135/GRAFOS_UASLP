namespace editorDeGrafos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.terminal = new System.Windows.Forms.TextBox();
            this.matrixTB = new System.Windows.Forms.TextBox();
            this.statusTB = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeXRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matrizIncidenciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.isomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fuerzaBrutaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traspuestaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intercambioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caminosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eulerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hamiltonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IsomtextBox = new System.Windows.Forms.TextBox();
            this.brToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.toolStripSeparator1,
            this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(116, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.New_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.Load_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.Save_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.helpToolStripButton.Text = "He&lp";
            // 
            // terminal
            // 
            this.terminal.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.terminal.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.terminal.Location = new System.Drawing.Point(978, 12);
            this.terminal.Multiline = true;
            this.terminal.Name = "terminal";
            this.terminal.ReadOnly = true;
            this.terminal.Size = new System.Drawing.Size(151, 122);
            this.terminal.TabIndex = 1;
            this.terminal.TabStop = false;
            this.terminal.TextChanged += new System.EventHandler(this.terminal_TextChanged);
            // 
            // matrixTB
            // 
            this.matrixTB.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.matrixTB.Location = new System.Drawing.Point(755, 140);
            this.matrixTB.Multiline = true;
            this.matrixTB.Name = "matrixTB";
            this.matrixTB.ReadOnly = true;
            this.matrixTB.Size = new System.Drawing.Size(603, 709);
            this.matrixTB.TabIndex = 2;
            this.matrixTB.TabStop = false;
            // 
            // statusTB
            // 
            this.statusTB.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.statusTB.Location = new System.Drawing.Point(755, 12);
            this.statusTB.Multiline = true;
            this.statusTB.Name = "statusTB";
            this.statusTB.ReadOnly = true;
            this.statusTB.Size = new System.Drawing.Size(217, 122);
            this.statusTB.TabIndex = 3;
            this.statusTB.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveToolStripMenuItem,
            this.removeXRToolStripMenuItem,
            this.moreToolStripMenuItem,
            this.matrizIncidenciaToolStripMenuItem,
            this.isomToolStripMenuItem,
            this.caminosToolStripMenuItem,
            this.brToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(116, 1);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // moveToolStripMenuItem
            // 
            this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            this.moveToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.moveToolStripMenuItem.Text = "Move(A,M)";
            this.moveToolStripMenuItem.Click += new System.EventHandler(this.Move_Click);
            // 
            // removeXRToolStripMenuItem
            // 
            this.removeXRToolStripMenuItem.Name = "removeXRToolStripMenuItem";
            this.removeXRToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.removeXRToolStripMenuItem.Text = "Remove(X,R)";
            this.removeXRToolStripMenuItem.Click += new System.EventHandler(this.Remove_Click);
            // 
            // moreToolStripMenuItem
            // 
            this.moreToolStripMenuItem.Name = "moreToolStripMenuItem";
            this.moreToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.moreToolStripMenuItem.Text = "MoRe(F)";
            this.moreToolStripMenuItem.Click += new System.EventHandler(this.MoRe_Click);
            // 
            // matrizIncidenciaToolStripMenuItem
            // 
            this.matrizIncidenciaToolStripMenuItem.Name = "matrizIncidenciaToolStripMenuItem";
            this.matrizIncidenciaToolStripMenuItem.Size = new System.Drawing.Size(125, 20);
            this.matrizIncidenciaToolStripMenuItem.Text = "Matriz de Incidencia";
            this.matrizIncidenciaToolStripMenuItem.Click += new System.EventHandler(this.maIn_Click);
            // 
            // isomToolStripMenuItem
            // 
            this.isomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fuerzaBrutaToolStripMenuItem,
            this.traspuestaToolStripMenuItem,
            this.intercambioToolStripMenuItem});
            this.isomToolStripMenuItem.Name = "isomToolStripMenuItem";
            this.isomToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.isomToolStripMenuItem.Text = "Isom";
            this.isomToolStripMenuItem.Click += new System.EventHandler(this.isoForm_Click);
            // 
            // fuerzaBrutaToolStripMenuItem
            // 
            this.fuerzaBrutaToolStripMenuItem.Name = "fuerzaBrutaToolStripMenuItem";
            this.fuerzaBrutaToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.fuerzaBrutaToolStripMenuItem.Text = "Fuerza Bruta";
            this.fuerzaBrutaToolStripMenuItem.Click += new System.EventHandler(this.fuerzaBrutaToolStripMenuItem_Click);
            // 
            // traspuestaToolStripMenuItem
            // 
            this.traspuestaToolStripMenuItem.Name = "traspuestaToolStripMenuItem";
            this.traspuestaToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.traspuestaToolStripMenuItem.Text = "Traspuesta";
            this.traspuestaToolStripMenuItem.Click += new System.EventHandler(this.traspuestaToolStripMenuItem_Click);
            // 
            // intercambioToolStripMenuItem
            // 
            this.intercambioToolStripMenuItem.Name = "intercambioToolStripMenuItem";
            this.intercambioToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.intercambioToolStripMenuItem.Text = "Intercambio(Manual)";
            this.intercambioToolStripMenuItem.Click += new System.EventHandler(this.intercambioToolStripMenuItem_Click);
            // 
            // caminosToolStripMenuItem
            // 
            this.caminosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eulerToolStripMenuItem,
            this.hamiltonToolStripMenuItem});
            this.caminosToolStripMenuItem.Name = "caminosToolStripMenuItem";
            this.caminosToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.caminosToolStripMenuItem.Text = "Caminos";
            this.caminosToolStripMenuItem.Click += new System.EventHandler(this.caminosToolStripMenuItem_Click);
            // 
            // eulerToolStripMenuItem
            // 
            this.eulerToolStripMenuItem.Name = "eulerToolStripMenuItem";
            this.eulerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eulerToolStripMenuItem.Text = "Euler";
            this.eulerToolStripMenuItem.Click += new System.EventHandler(this.eulerToolStripMenuItem_Click);
            // 
            // hamiltonToolStripMenuItem
            // 
            this.hamiltonToolStripMenuItem.Name = "hamiltonToolStripMenuItem";
            this.hamiltonToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hamiltonToolStripMenuItem.Text = "Hamilton";
            this.hamiltonToolStripMenuItem.Click += new System.EventHandler(this.hamiltonToolStripMenuItem_Click);
            // 
            // IsomtextBox
            // 
            this.IsomtextBox.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.IsomtextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.IsomtextBox.Location = new System.Drawing.Point(1135, 12);
            this.IsomtextBox.Multiline = true;
            this.IsomtextBox.Name = "IsomtextBox";
            this.IsomtextBox.ReadOnly = true;
            this.IsomtextBox.Size = new System.Drawing.Size(223, 122);
            this.IsomtextBox.TabIndex = 6;
            this.IsomtextBox.TabStop = false;
            // 
            // brToolStripMenuItem
            // 
            this.brToolStripMenuItem.Name = "brToolStripMenuItem";
            this.brToolStripMenuItem.Size = new System.Drawing.Size(30, 20);
            this.brToolStripMenuItem.Text = "br";
            this.brToolStripMenuItem.Click += new System.EventHandler(this.brToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.IsomtextBox);
            this.Controls.Add(this.statusTB);
            this.Controls.Add(this.matrixTB);
            this.Controls.Add(this.terminal);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Grafo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.TextBox terminal;
        private System.Windows.Forms.TextBox matrixTB;
        private System.Windows.Forms.TextBox statusTB;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeXRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matrizIncidenciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem isomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fuerzaBrutaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traspuestaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intercambioToolStripMenuItem;
        private System.Windows.Forms.TextBox IsomtextBox;
        private System.Windows.Forms.ToolStripMenuItem caminosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eulerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hamiltonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brToolStripMenuItem;
    }
}

