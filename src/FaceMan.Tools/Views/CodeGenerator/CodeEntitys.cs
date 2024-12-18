using AntdUI;

using FaceMan.Tools.Db;
using FaceMan.Tools.DB.CodeGenerator;
using FaceMan.Tools.Views.CodeGenerator;

using Window = AntdUI.Window;

namespace FaceMan.Tools.Views
{
    public partial class CodeEntitys : UserControl
    {
        private Window _window;
        AntList<Entity> antList;
        public CodeEntitys(Window window)
        {
            _window = window;
            InitializeComponent();
            //初始化表格列头
            InitTableColumns();
            InitData();
            BindEventHandler();
        }

        private void InitTableColumns()
        {
            table_base.Columns = new ColumnCollection() {
                new Column("Name", "实体名称",ColumnAlign.Center),
                new Column("CellLinks", "操作", ColumnAlign.Center)
                {
                    Fixed = true,//冻结列
                },
            };
        }

        private void InitData()
        {
            var res = new AntList<Entity>();
            var entities = DBHelper.sqlite.Select<Entity>().ToList();
            foreach (var item in entities)
            {
                item.CellLinks = new CellLink[] {
                    new CellButton(Guid.NewGuid().ToString(),"编辑",TTypeMini.Primary),
                    new CellButton(Guid.NewGuid().ToString(),"删除",TTypeMini.Error) };
                res.Add(item);
            }
            table_base.Binding<Entity>(res);
        }

        private void BindEventHandler()
        {
            buttonADD.Click += ButtonADD_Click;
        }

        private void ButtonADD_Click(object sender, EventArgs e)
        {
            var form = new CodeField(_window) { Size = new Size(700, 400) };
            Modal.open(new Modal.Config(_window, "", form, TType.None)
            {
                BtnHeight = 0,
            });
            if (form.submit)
            {
                antList.Add(form._entity);
            }
        }
    }
}
