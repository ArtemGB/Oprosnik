namespace Mev_lab5
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.File = new System.Windows.Forms.ToolStripMenuItem();
            this.CreatFileMI = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileMI = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenForUsingMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFileMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMI = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(800, 24);
            this.MainMenuStrip.TabIndex = 0;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // File
            // 
            this.File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreatFileMI,
            this.OpenFileMI,
            this.OpenForUsingMI,
            this.SaveFileMI,
            this.SaveAsMI});
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(48, 20);
            this.File.Text = "Файл";
            // 
            // CreatFileMI
            // 
            this.CreatFileMI.Name = "CreatFileMI";
            this.CreatFileMI.Size = new System.Drawing.Size(220, 22);
            this.CreatFileMI.Text = "Создать";
            this.CreatFileMI.Click += new System.EventHandler(this.CreatFileMI_Click);
            // 
            // OpenFileMI
            // 
            this.OpenFileMI.Name = "OpenFileMI";
            this.OpenFileMI.Size = new System.Drawing.Size(220, 22);
            this.OpenFileMI.Text = "Открыть";
            this.OpenFileMI.Click += new System.EventHandler(this.OpenFileMI_Click);
            // 
            // OpenForUsingMI
            // 
            this.OpenForUsingMI.Name = "OpenForUsingMI";
            this.OpenForUsingMI.Size = new System.Drawing.Size(220, 22);
            this.OpenForUsingMI.Text = "Открыть для прохождения";
            this.OpenForUsingMI.Click += new System.EventHandler(this.OpenForUsingMI_Click);
            // 
            // SaveFileMI
            // 
            this.SaveFileMI.Name = "SaveFileMI";
            this.SaveFileMI.Size = new System.Drawing.Size(220, 22);
            this.SaveFileMI.Text = "Сохранить";
            this.SaveFileMI.Click += new System.EventHandler(this.SaveFileMI_Click);
            // 
            // SaveAsMI
            // 
            this.SaveAsMI.Name = "SaveAsMI";
            this.SaveAsMI.Size = new System.Drawing.Size(220, 22);
            this.SaveAsMI.Text = "Сохранить как";
            this.SaveAsMI.Click += new System.EventHandler(this.SaveAsMI_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog2_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "СоцОпр";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem File;
        private System.Windows.Forms.ToolStripMenuItem CreatFileMI;
        private System.Windows.Forms.ToolStripMenuItem OpenFileMI;
        private System.Windows.Forms.ToolStripMenuItem SaveFileMI;
        private System.Windows.Forms.ToolStripMenuItem SaveAsMI;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem OpenForUsingMI;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}

