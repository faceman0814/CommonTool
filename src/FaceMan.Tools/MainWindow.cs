using AntdUI;

using FaceMan.Tools.Models;
using FaceMan.Tools.Utils;
using FaceMan.Tools.Views;

using Microsoft.Win32;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using Window = AntdUI.Window;

namespace FaceMan.Tools
{
    public partial class MainWindow : Window
    {
        private UserControl control;
        public MainWindow()
        {
            InitializeComponent();
            //加载配置文件
            LoadAppConfig();
            //加载菜单
            LoadMenu();
            //加载欢迎页
            InitData();
            //绑定事件
            BindEventHandler();
        }
        private void BindEventHandler()
        {
            button_color.Click += Button_color_Click;
            menu.SelectChanged += Menu_SelectChanged;
            //监听系统深浅色变化
            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
        }

        private void Button_color_Click(object sender, EventArgs e)
        {
            // 读取 appsettings.json 文件
            var appSettings = AppSetting.GetAppSettings();
            var value = appSettings["ColorMode"]?.ToString();
            if (value == "Auto")
            {
                //反向设置
                ThemeHelper.SetColorMode(this, !ThemeHelper.IsLightMode());
                AppSetting.UpdateAppSetting("ColorMode", ThemeHelper.IsLightMode() ? "Dark" : "Light");
            }
            else
            {
                ThemeHelper.SetColorMode(this, value == "Dark");
                AppSetting.UpdateAppSetting("ColorMode", value == "Dark" ? "Light" : "Dark");
            }
        }

        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.General)
            {
                ThemeHelper.SetColorMode(this, ThemeHelper.IsLightMode());
            }
        }

        private void LoadAppConfig()
        {
            // 读取 appsettings.json 文件
            var appSettings = AppSetting.GetAppSettings();
            // 加载色彩模式
            var value = appSettings["ColorMode"]?.ToString();
            if (value == "Auto")
            {
                ThemeHelper.SetColorMode(this, ThemeHelper.IsLightMode());
            }
            else
            {
                ThemeHelper.SetColorMode(this, value == "Light");
            }

            // 加载动画
            var animation = appSettings["Animation"]?.ToString();
            AntdUI.Config.Animation = animation == "True";

            // 加载阴影
            var shadow = appSettings["ShadowEnabled"]?.ToString();
            AntdUI.Config.ShadowEnabled = shadow == "True";

            // 滚动条
            var scrollbar = appSettings["ScrollBarHide"]?.ToString();
            AntdUI.Config.ScrollBarHide = scrollbar == "True";

            // 窗口内弹出 Message/Notification
            var popup = appSettings["ShowInWindow"]?.ToString();
            AntdUI.Config.ShowInWindow = popup == "True";

            // 通知消息边界偏移量XY（Message/Notification）
            var messageOffset = appSettings["NoticeWindowOffsetXY"]?.ToString();
            AntdUI.Config.NoticeWindowOffsetXY = Convert.ToInt32(messageOffset);
        }

        private void LoadMenu(string filter = "")
        {
            menu.Items.Clear();

            foreach (var rootItem in DataUtil.MenuItems)
            {
                var rootKey = rootItem.Key.ToLower();
                var rootMenu = new AntdUI.MenuItem { Text = rootItem.Key };
                bool rootVisible = false; // 用于标记是否显示根节点

                foreach (var item in rootItem.Value)
                {
                    var childText = item.Text.ToLower();

                    // 如果子节点包含搜索文本
                    if (childText.Contains(filter))
                    {
                        var menuItem = new AntdUI.MenuItem
                        {
                            Text = item.Text,
                            IconSvg = item.IconSvg,
                            Tag = item.Tag,
                        };
                        rootMenu.Sub.Add(menuItem);
                        rootVisible = true; // 如果有子节点包含，则显示根节点
                    }
                }

                // 如果根节点包含搜索文本，或有可见的子节点，则显示根节点
                if (rootKey.Contains(filter) || rootVisible)
                {
                    menu.Items.Add(rootMenu);
                }
            }
        }

        private void InitData()
        {
            control = new Wellcome();
            AutoDpi(control);
            panel_content.Controls.Add(control);
        }
        private void Menu_SelectChanged(object sender, MenuSelectEventArgs e)
        {
            var name = e.Value.Tag;
            panel_content.Controls.Clear();
            switch (name)
            {
                case "CodeEntitys":
                    control = new CodeEntitys(this);
                    break;
                default:
                    break;
            }
            if (control != null)
            {
                //容器添加控件，需要调整dpi
                AutoDpi(control);
                panel_content.Controls.Add(control);
            }
        }
    }
}
