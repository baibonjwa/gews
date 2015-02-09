using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using LibEntity;

namespace LibBusiness
{
    public class DataBindUtil
    {
        private static void DataBindListControl(ListControl lc, ICollection<object> dataSource, string displayMember, string valueMember, int selectedValue = -1)
        {
            if (dataSource.Count <= 0) return;
            lc.DataSource = dataSource;
            lc.DisplayMember = displayMember;
            lc.ValueMember = valueMember;
            lc.SelectedValue = selectedValue;
        }

        private static void DataBindListControl(DataGridView dgv, ICollection<object> dataSource, string displayMember, string valueMember)
        {
            if (dataSource.Count <= 0) return;
            dgv.AutoGenerateColumns = false;
            dgv.DataSource = dataSource;
            dgv.Columns[0].DataPropertyName = displayMember;
            dgv.Columns[1].DataPropertyName = valueMember;
        }

        public static void LoadMineName(ListControl lb, int selectedValue = -1)
        {
            var mines = Mine.FindAll();
            if (mines != null) DataBindListControl(lb, mines, "MineName", "MineId", selectedValue);
        }

        public static void LoadMineName(DataGridView dgv, int selectedValue = -1)
        {
            var mines = Mine.FindAll();
            DataBindListControl(dgv, mines, "MineId", "MineName");
        }

        public static void LoadHorizontalName(ListControl lb, int mineId, int selectedValue = -1)
        {
            var horizontals = Horizontal.FindAllByMineId(mineId);
            if (horizontals != null) DataBindListControl(lb, horizontals, "HorizontalName", "HorizontalId", selectedValue);
        }

        public static void LoadLithology(ListControl lb, int selectedValue = -1)
        {
            var lithologys = Lithology.FindAll();
            if (lithologys != null) DataBindListControl(lb, lithologys, "LithologyName", "LithologyId", selectedValue);
        }

        public static void LoadTeamMemberByTeamName(ListControl lb, string teamName, int selectedValue = -1)
        {
            var teamMember = Team.FindOneByTeamName(teamName);
            if (teamMember != null)
            {
                var teamMembers = teamMember.ToString().Split(',');
                DataBindListControl(lb, teamMembers, "TeamName", "TeamName", selectedValue);
            }
        }



    }
}
