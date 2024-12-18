using AntdUI;

using Button = AntdUI.Button;

namespace FaceMan.Tools.Views
{
    partial class CodeEntitys
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
            this.components = new System.ComponentModel.Container();
            this.stackPanel1 = new StackPanel();
            this.table_base = new Table();
            this.flowPanel3 = new FlowPanel();
            this.buttonDEL = new Button();
            this.buttonADD = new Button();
            this.stackPanel1.SuspendLayout();
            this.flowPanel3.SuspendLayout();
            this.SuspendLayout();

            // 
            // stackPanel1
            // 
            this.stackPanel1.Controls.Add(this.table_base);
            this.stackPanel1.Controls.Add(this.flowPanel3);
            this.stackPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackPanel1.Location = new System.Drawing.Point(0, 0);
            this.stackPanel1.Name = "stackPanel1";
            this.stackPanel1.Size = new System.Drawing.Size(750, 560);
            this.stackPanel1.TabIndex = 0;
            this.stackPanel1.Text = "stackPanel1";
            this.stackPanel1.Vertical = true;

            // 
            // flowPanel3
            // 
            this.flowPanel3.Controls.Add(this.buttonDEL);
            this.flowPanel3.Controls.Add(this.buttonADD);
            this.flowPanel3.Location = new System.Drawing.Point(3, 127);
            this.flowPanel3.Name = "flowPanel3";
            this.flowPanel3.Size = new System.Drawing.Size(744, 38);
            this.flowPanel3.TabIndex = 27;
            this.flowPanel3.Text = "flowPanel3";
            // 
            // buttonDEL
            // 
            this.buttonDEL.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonDEL.Location = new System.Drawing.Point(89, 3);
            this.buttonDEL.Name = "buttonDEL";
            this.buttonDEL.Size = new System.Drawing.Size(80, 32);
            this.buttonDEL.TabIndex = 5;
            this.buttonDEL.Text = "删除";
            this.buttonDEL.Type = AntdUI.TTypeMini.Error;
            this.buttonDEL.WaveSize = 0;
            // 
            // buttonADD
            // 
            this.buttonADD.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonADD.Location = new System.Drawing.Point(3, 3);
            this.buttonADD.Name = "buttonADD";
            this.buttonADD.Size = new System.Drawing.Size(80, 32);
            this.buttonADD.TabIndex = 2;
            this.buttonADD.Text = "新增";
            this.buttonADD.Type = AntdUI.TTypeMini.Primary;
            this.buttonADD.WaveSize = 0;

            // 
            // table_base
            // 
            this.table_base.EmptyHeader = true;
            this.table_base.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.table_base.Gap = 8;
            this.table_base.Location = new System.Drawing.Point(3, 215);
            this.table_base.Name = "table_base";
            this.table_base.ShowTip = false;
            this.table_base.Size = new System.Drawing.Size(744, 342);
            this.table_base.TabIndex = 30;
            this.table_base.Text = "table1";

            this.Controls.Add(this.stackPanel1);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(750, 560);
            this.Text = "Code";
            this.stackPanel1.ResumeLayout(false);
            this.flowPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
        private StackPanel stackPanel1;
        private Table table_base;
        private FlowPanel flowPanel3;
        private Button buttonADD;
        private Button buttonDEL;
    }
}