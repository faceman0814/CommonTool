using AntdUI;

using FaceMan.Tools.Db;
using FaceMan.Tools.DB.CodeGenerator;

namespace FaceMan.Tools.Views.CodeGenerator
{
    public partial class CodeField : UserControl
    {
        private Window _window;
        public Entity _entity;
        public bool submit;

        public CodeField(Window window)
        {
            _window = window;

            InitializeComponent();
            //设置默认值
            InitData();
            // 绑定事件
            BindEventHandler();
        }

        private void InitData()
        {
            var res = new AntList<Field>();
            var entities = DBHelper.sqlite.Select<Field>().ToList();
            foreach (var item in entities)
            {
                item.CellLinks = new CellLink[] {
                    new CellButton(Guid.NewGuid().ToString(),"编辑",TTypeMini.Primary),
                    new CellButton(Guid.NewGuid().ToString(),"删除",TTypeMini.Error) };
                res.Add(item);
            }
            table_base.Binding<Field>(res);
        }

        private void BindEventHandler()
        {

        }
    }
}
