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

        public static void LoadWorkTime(ListControl lb, int timeGroupId, int selectedValue = -1)
        {
            var workingTimes = WorkingTime.FindAllByWorkTimeGroupId(timeGroupId);
            if (workingTimes != null)
            {
                DataBindListControl(lb, workingTimes, "WorkTimeName", "WorkTimeName", selectedValue);
            }
        }


        public static void LoadWorkTime(DataGridViewComboBoxColumn dgvcbc
            , int timeGroupId, int selectedValue = -1)
        {
            var workingTimes = WorkingTime.FindAllByWorkTimeGroupId(timeGroupId);
            foreach (var t in workingTimes)
            {
                dgvcbc.Items.Add(t.WorkTimeName);
            }
        }

        public static string JudgeWorkTimeNow(string workStyle)
        {
            //获取班次
            WorkingTime[] workingTimes = WorkingTime.FindAllByWorkTimeName(workStyle);
            //小时
            int hour = DateTime.Now.Hour;
            string workTime = "";
            for (int i = 0; i < workingTimes.Length; i++)
            {
                //对比小时
                if (hour > Convert.ToInt32(workingTimes[i].WorkTimeFrom.ToString().Remove(2)) && hour <= Convert.ToInt32(workingTimes[i].WorkTimeTo.ToString().Remove(2)))
                {
                    //获取当前时间对应班次
                    workTime = workingTimes[i].WorkTimeName;
                }
            }
            return workTime;
        }



    }
}
