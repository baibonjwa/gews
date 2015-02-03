using System.Windows.Forms;
using LibEntity;

namespace LibBusiness
{
    public class DataBindUtil
    {
        public static void LoadMineName(ListBox lb, int selectedValue = -1)
        {
            var mines = Mine.FindAll();
            if (mines.Length <= 0) return;
            // 绑定矿井信息
            lb.DataSource = mines;
            lb.DisplayMember = "MineName";
            lb.ValueMember = "MineId";
            lb.SelectedValue = selectedValue;
        }

        public static void LoadMineName(DataGridView dgv, int selectedValue = -1)
        {
            var mines = Mine.FindAll();
            if (mines.Length <= 0) return;
            // 绑定矿井信息
            // 禁止自动生成列(※位置不可变)
            dgv.AutoGenerateColumns = false;
            dgv.DataSource = mines;
            dgv.Columns[0].DataPropertyName = "MineId";
            dgv.Columns[1].DataPropertyName = "MineName";
        }

        public static void LoadHorizontalName(ListBox lb, int mineId, int selectedValue = -1)
        {
            var horizontal = Horizontal.FindAllByMineId(mineId);
            if (horizontal.Length <= 0) return;
            // 绑定矿井信息
            lb.DataSource = horizontal;
            lb.DisplayMember = "HorizontalName";
            lb.ValueMember = "HorizontalId";
            lb.SelectedValue = selectedValue;
        }


    }
}
