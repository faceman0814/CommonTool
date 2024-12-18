using AntdUI;

using Button = AntdUI.Button;

namespace FaceMan.Tools
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            titlebar = new WindowBar();
            button_color = new Button();
            menu = new Menu();
            panel_content = new StackPanel();
            titlebar.SuspendLayout();
            SuspendLayout();
            // 
            // titlebar
            // 
            titlebar.Controls.Add(button_color);
            titlebar.Dock = DockStyle.Top;
            titlebar.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            titlebar.Location = new Point(6, 0);
            titlebar.Name = "titlebar";
            titlebar.Size = new Size(794, 40);
            titlebar.SubText = "开发小工具";
            titlebar.TabIndex = 0;
            titlebar.Text = "FaceMan Tools";
            // 
            // button_color
            // 
            button_color.Dock = DockStyle.Right;
            button_color.Ghost = true;
            button_color.IconHoverSvg = "";
            button_color.IconRatio = 0.6F;
            button_color.IconSvg = resources.GetString("button_color.IconSvg");
            button_color.Location = new Point(600, 0);
            button_color.Name = "button_color";
            button_color.Radius = 0;
            button_color.Size = new Size(50, 40);
            button_color.TabIndex = 1;
            button_color.WaveSize = 0;
            // 
            // menu
            // 
            menu.AutoCollapse = true;
            menu.Dock = DockStyle.Left;
            menu.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            menu.Indent = true;
            menu.Location = new Point(6, 40);
            menu.Name = "menu";
            menu.Size = new Size(213, 404);
            menu.TabIndex = 9;
            menu.Unique = true;
            // 
            // panel_content
            // 
            panel_content.AutoScroll = true;
            panel_content.Dock = DockStyle.Fill;
            panel_content.Location = new Point(219, 40);
            panel_content.Name = "panel_content";
            panel_content.Size = new Size(581, 404);
            panel_content.TabIndex = 10;
            panel_content.Text = "stackPanel1";
            panel_content.Vertical = true;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel_content);
            Controls.Add(menu);
            Controls.Add(titlebar);
            Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Name = "MainWindow";
            Opacity = 0.99D;
            Padding = new Padding(6, 0, 0, 6);
            StartPosition = FormStartPosition.CenterScreen;
            titlebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private WindowBar titlebar;
        private Button button_color;
        private Menu menu;
        private StackPanel panel_content;
    }
}
