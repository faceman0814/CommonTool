using AntdUI;

using Button = AntdUI.Button;

namespace FaceMan.Tools.Views.CodeGenerator
{
    partial class CodeField
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
            table_base = new Table();
            buttonDEL = new Button();
            buttonADD = new Button();
            flowPanel3 = new FlowPanel();
            SuspendLayout();
            // 
            // table_base
            // 
            table_base.EmptyHeader = true;
            table_base.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            table_base.Gap = 8;
            table_base.Location = new Point(6, 41);
            table_base.Name = "table_base";
            table_base.ShowTip = false;
            table_base.Size = new Size(744, 342);
            table_base.TabIndex = 30;
            table_base.Text = "table1";
            // 
            // buttonDEL
            // 
            buttonDEL.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            buttonDEL.Location = new Point(89, 3);
            buttonDEL.Name = "buttonDEL";
            buttonDEL.Size = new Size(80, 32);
            buttonDEL.TabIndex = 5;
            buttonDEL.Text = "删除";
            buttonDEL.Type = TTypeMini.Error;
            buttonDEL.WaveSize = 0;
            // 
            // buttonADD
            // 
            buttonADD.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            buttonADD.Location = new Point(3, 3);
            buttonADD.Name = "buttonADD";
            buttonADD.Size = new Size(80, 32);
            buttonADD.TabIndex = 2;
            buttonADD.Text = "新增";
            buttonADD.Type = TTypeMini.Primary;
            buttonADD.WaveSize = 0;
            // 
            // flowPanel3
            // 
            flowPanel3.Location = new Point(3, 127);
            flowPanel3.Name = "flowPanel3";
            flowPanel3.Size = new Size(744, 38);
            flowPanel3.TabIndex = 27;
            flowPanel3.Text = "flowPanel3";
            // 
            // CodeField
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            Controls.Add(buttonDEL);
            Controls.Add(table_base);
            Controls.Add(buttonADD);
            Controls.Add(flowPanel3);
            Name = "CodeField";
            Size = new Size(763, 407);
            ResumeLayout(false);
        }

        #endregion
        private Table table_base;
        private FlowPanel flowPanel3;
        private Button buttonADD;
        private Button buttonDEL;
    }
}