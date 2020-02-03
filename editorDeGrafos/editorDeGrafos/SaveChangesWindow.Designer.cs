namespace editorDeGrafos
{
    partial class SaveChangesWindow
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
            this.textInterface = new System.Windows.Forms.Label();
            this.Save = new System.Windows.Forms.Button();
            this.notSave = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textInterface
            // 
            this.textInterface.AutoSize = true;
            this.textInterface.Location = new System.Drawing.Point(98, 54);
            this.textInterface.Name = "textInterface";
            this.textInterface.Size = new System.Drawing.Size(161, 13);
            this.textInterface.TabIndex = 0;
            this.textInterface.Text = "Do you want to save changes at";
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(57, 138);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(123, 40);
            this.Save.TabIndex = 1;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // notSave
            // 
            this.notSave.Location = new System.Drawing.Point(224, 138);
            this.notSave.Name = "notSave";
            this.notSave.Size = new System.Drawing.Size(123, 38);
            this.notSave.TabIndex = 2;
            this.notSave.Text = "Don\'t Save";
            this.notSave.UseVisualStyleBackColor = true;
            this.notSave.Click += new System.EventHandler(this.notSave_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(388, 138);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(123, 38);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SaveChangesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 239);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.notSave);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.textInterface);
            this.Name = "SaveChangesWindow";
            this.Text = "SaveChangesWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label textInterface;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button notSave;
        private System.Windows.Forms.Button Cancel;
    }
}