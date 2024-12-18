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
            //���������ļ�
            LoadAppConfig();
            //���ز˵�
            LoadMenu();
            //���ػ�ӭҳ
            InitData();
            //���¼�
            BindEventHandler();
        }
        private void BindEventHandler()
        {
            button_color.Click += Button_color_Click;
            menu.SelectChanged += Menu_SelectChanged;
            //����ϵͳ��ǳɫ�仯
            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
        }

        private void Button_color_Click(object sender, EventArgs e)
        {
            // ��ȡ appsettings.json �ļ�
            var appSettings = AppSetting.GetAppSettings();
            var value = appSettings["ColorMode"]?.ToString();
            if (value == "Auto")
            {
                //��������
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
            // ��ȡ appsettings.json �ļ�
            var appSettings = AppSetting.GetAppSettings();
            // ����ɫ��ģʽ
            var value = appSettings["ColorMode"]?.ToString();
            if (value == "Auto")
            {
                ThemeHelper.SetColorMode(this, ThemeHelper.IsLightMode());
            }
            else
            {
                ThemeHelper.SetColorMode(this, value == "Light");
            }

            // ���ض���
            var animation = appSettings["Animation"]?.ToString();
            AntdUI.Config.Animation = animation == "True";

            // ������Ӱ
            var shadow = appSettings["ShadowEnabled"]?.ToString();
            AntdUI.Config.ShadowEnabled = shadow == "True";

            // ������
            var scrollbar = appSettings["ScrollBarHide"]?.ToString();
            AntdUI.Config.ScrollBarHide = scrollbar == "True";

            // �����ڵ��� Message/Notification
            var popup = appSettings["ShowInWindow"]?.ToString();
            AntdUI.Config.ShowInWindow = popup == "True";

            // ֪ͨ��Ϣ�߽�ƫ����XY��Message/Notification��
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
                bool rootVisible = false; // ���ڱ���Ƿ���ʾ���ڵ�

                foreach (var item in rootItem.Value)
                {
                    var childText = item.Text.ToLower();

                    // ����ӽڵ���������ı�
                    if (childText.Contains(filter))
                    {
                        var menuItem = new AntdUI.MenuItem
                        {
                            Text = item.Text,
                            IconSvg = item.IconSvg,
                            Tag = item.Tag,
                        };
                        rootMenu.Sub.Add(menuItem);
                        rootVisible = true; // ������ӽڵ����������ʾ���ڵ�
                    }
                }

                // ������ڵ���������ı������пɼ����ӽڵ㣬����ʾ���ڵ�
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
                //������ӿؼ�����Ҫ����dpi
                AutoDpi(control);
                panel_content.Controls.Add(control);
            }
        }
    }
}
