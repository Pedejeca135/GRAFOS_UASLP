namespace editorDeGrafos
{
    partial class GraphForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphForm));
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
            this.operacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveAllAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moReFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undirectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matrizDeIncidenciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bridgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algoritmosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.isomorfismoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fuerzaBrutaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.transpuestaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intercambioManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caminosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eulerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hamiltonToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dijkstraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.floydToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warshallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kruskalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.isomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fuerzaBrutaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traspuestaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intercambioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IsomtextBox = new System.Windows.Forms.TextBox();
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
            this.operacionesToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.algoritmosToolStripMenuItem,
            this.isomToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(116, 1);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(380, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // operacionesToolStripMenuItem
            // 
            this.operacionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveMToolStripMenuItem,
            this.moveAllAToolStripMenuItem,
            this.removeXToolStripMenuItem,
            this.moReFToolStripMenuItem,
            this.linkingToolStripMenuItem});
            this.operacionesToolStripMenuItem.Name = "operacionesToolStripMenuItem";
            this.operacionesToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.operacionesToolStripMenuItem.Text = "Operaciones";
            // 
            // moveMToolStripMenuItem
            // 
            this.moveMToolStripMenuItem.Name = "moveMToolStripMenuItem";
            this.moveMToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.moveMToolStripMenuItem.Text = "Move (M) ";
            this.moveMToolStripMenuItem.Click += new System.EventHandler(this.Move_Click);
            // 
            // moveAllAToolStripMenuItem
            // 
            this.moveAllAToolStripMenuItem.Name = "moveAllAToolStripMenuItem";
            this.moveAllAToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.moveAllAToolStripMenuItem.Text = "Move All (A)";
            this.moveAllAToolStripMenuItem.Click += new System.EventHandler(this.MoveAll_Click);
            // 
            // removeXToolStripMenuItem
            // 
            this.removeXToolStripMenuItem.Name = "removeXToolStripMenuItem";
            this.removeXToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeXToolStripMenuItem.Text = "Remove (R)";
            this.removeXToolStripMenuItem.Click += new System.EventHandler(this.Remove_Click);
            // 
            // moReFToolStripMenuItem
            // 
            this.moReFToolStripMenuItem.Name = "moReFToolStripMenuItem";
            this.moReFToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.moReFToolStripMenuItem.Text = "MoRe (F)";
            this.moReFToolStripMenuItem.Click += new System.EventHandler(this.MoRe_Click);
            // 
            // linkingToolStripMenuItem
            // 
            this.linkingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undirectToolStripMenuItem,
            this.directToolStripMenuItem});
            this.linkingToolStripMenuItem.Name = "linkingToolStripMenuItem";
            this.linkingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.linkingToolStripMenuItem.Text = "Linking";
            this.linkingToolStripMenuItem.Click += new System.EventHandler(this.linking_Click);
            // 
            // undirectToolStripMenuItem
            // 
            this.undirectToolStripMenuItem.Name = "undirectToolStripMenuItem";
            this.undirectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.undirectToolStripMenuItem.Text = "Undirect (U)";
            this.undirectToolStripMenuItem.Click += new System.EventHandler(this.linking_D_Click);
            // 
            // directToolStripMenuItem
            // 
            this.directToolStripMenuItem.Name = "directToolStripMenuItem";
            this.directToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.directToolStripMenuItem.Text = "Direct (D)";
            this.directToolStripMenuItem.Click += new System.EventHandler(this.linking_U_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.matrizDeIncidenciaToolStripMenuItem,
            this.bridgesToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "Vista";
            // 
            // matrizDeIncidenciaToolStripMenuItem
            // 
            this.matrizDeIncidenciaToolStripMenuItem.Name = "matrizDeIncidenciaToolStripMenuItem";
            this.matrizDeIncidenciaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.matrizDeIncidenciaToolStripMenuItem.Text = "Matriz de Incidencia";
            this.matrizDeIncidenciaToolStripMenuItem.Click += new System.EventHandler(this.maIn_Click);
            // 
            // bridgesToolStripMenuItem
            // 
            this.bridgesToolStripMenuItem.Name = "bridgesToolStripMenuItem";
            this.bridgesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bridgesToolStripMenuItem.Text = "Bridges ";
            this.bridgesToolStripMenuItem.Click += new System.EventHandler(this.brToolStripMenuItem_Click);
            // 
            // algoritmosToolStripMenuItem
            // 
            this.algoritmosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.isomorfismoToolStripMenuItem,
            this.caminosToolStripMenuItem1,
            this.dijkstraToolStripMenuItem,
            this.floydToolStripMenuItem,
            this.warshallToolStripMenuItem,
            this.primToolStripMenuItem,
            this.kruskalToolStripMenuItem});
            this.algoritmosToolStripMenuItem.Name = "algoritmosToolStripMenuItem";
            this.algoritmosToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.algoritmosToolStripMenuItem.Text = "Algoritmos";
            // 
            // isomorfismoToolStripMenuItem
            // 
            this.isomorfismoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fuerzaBrutaToolStripMenuItem1,
            this.transpuestaToolStripMenuItem,
            this.intercambioManualToolStripMenuItem});
            this.isomorfismoToolStripMenuItem.Name = "isomorfismoToolStripMenuItem";
            this.isomorfismoToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.isomorfismoToolStripMenuItem.Text = "Isomorfismo";
            // 
            // fuerzaBrutaToolStripMenuItem1
            // 
            this.fuerzaBrutaToolStripMenuItem1.Name = "fuerzaBrutaToolStripMenuItem1";
            this.fuerzaBrutaToolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.fuerzaBrutaToolStripMenuItem1.Text = "Fuerza Bruta";
            this.fuerzaBrutaToolStripMenuItem1.Click += new System.EventHandler(this.fuerzaBrutaToolStripMenuItem_Click);
            // 
            // transpuestaToolStripMenuItem
            // 
            this.transpuestaToolStripMenuItem.Name = "transpuestaToolStripMenuItem";
            this.transpuestaToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.transpuestaToolStripMenuItem.Text = "Transpuesta";
            this.transpuestaToolStripMenuItem.Click += new System.EventHandler(this.traspuestaToolStripMenuItem_Click);
            // 
            // intercambioManualToolStripMenuItem
            // 
            this.intercambioManualToolStripMenuItem.Name = "intercambioManualToolStripMenuItem";
            this.intercambioManualToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.intercambioManualToolStripMenuItem.Text = "Intercambio ( Manual)";
            this.intercambioManualToolStripMenuItem.Click += new System.EventHandler(this.intercambioToolStripMenuItem_Click);
            // 
            // caminosToolStripMenuItem1
            // 
            this.caminosToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eulerToolStripMenuItem1,
            this.hamiltonToolStripMenuItem1});
            this.caminosToolStripMenuItem1.Name = "caminosToolStripMenuItem1";
            this.caminosToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.caminosToolStripMenuItem1.Text = "Caminos";
            // 
            // eulerToolStripMenuItem1
            // 
            this.eulerToolStripMenuItem1.Name = "eulerToolStripMenuItem1";
            this.eulerToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.eulerToolStripMenuItem1.Text = "Euler";
            this.eulerToolStripMenuItem1.Click += new System.EventHandler(this.eulerToolStripMenuItem_Click);
            // 
            // hamiltonToolStripMenuItem1
            // 
            this.hamiltonToolStripMenuItem1.Name = "hamiltonToolStripMenuItem1";
            this.hamiltonToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.hamiltonToolStripMenuItem1.Text = "Hamilton";
            this.hamiltonToolStripMenuItem1.Click += new System.EventHandler(this.hamiltonToolStripMenuItem_Click);
            // 
            // dijkstraToolStripMenuItem
            // 
            this.dijkstraToolStripMenuItem.Name = "dijkstraToolStripMenuItem";
            this.dijkstraToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.dijkstraToolStripMenuItem.Text = "Dijkstra";
            this.dijkstraToolStripMenuItem.Click += new System.EventHandler(this.dijkstraToolStripMenuItem_Click);
            // 
            // floydToolStripMenuItem
            // 
            this.floydToolStripMenuItem.Name = "floydToolStripMenuItem";
            this.floydToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.floydToolStripMenuItem.Text = "Floyd";
            // 
            // warshallToolStripMenuItem
            // 
            this.warshallToolStripMenuItem.Name = "warshallToolStripMenuItem";
            this.warshallToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.warshallToolStripMenuItem.Text = "Warshall";
            // 
            // primToolStripMenuItem
            // 
            this.primToolStripMenuItem.Name = "primToolStripMenuItem";
            this.primToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.primToolStripMenuItem.Text = "Prim";
            this.primToolStripMenuItem.Click += new System.EventHandler(this.primToolStripMenuItem_Click);
            // 
            // kruskalToolStripMenuItem
            // 
            this.kruskalToolStripMenuItem.Name = "kruskalToolStripMenuItem";
            this.kruskalToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.kruskalToolStripMenuItem.Text = "Kruskal";
            this.kruskalToolStripMenuItem.Click += new System.EventHandler(this.kruskalToolStripMenuItem_Click);
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
            // GraphForm
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
            this.Name = "GraphForm";
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
        private System.Windows.Forms.TextBox IsomtextBox;
        private System.Windows.Forms.ToolStripMenuItem operacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem directToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undirectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveAllAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matrizDeIncidenciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moReFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bridgesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem isomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fuerzaBrutaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traspuestaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intercambioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algoritmosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem caminosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eulerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hamiltonToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem isomorfismoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fuerzaBrutaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem transpuestaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intercambioManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dijkstraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem floydToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warshallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem primToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kruskalToolStripMenuItem;
    }
}

