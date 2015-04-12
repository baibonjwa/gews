using System;
using System.Windows.Forms;
using LibCommon;

namespace sys5
{
    public partial class WarningResultAnalysis : Form
    {
        public WarningResultAnalysis(WarningReasonItems warningType)
        {
            InitializeComponent();
            Text = warningType + "分析";
        }

        private void btnStartAnalysis_Click(object sender, EventArgs e)
        {
            //Http:server:7071/RestoreBounds/WarningResultAnalysis/
            //tunnelid:
            //TimeoutException
            //ruleid.

            dataGridView1.Rows.Clear();

            string[] row0 = {"C# 3.0 Pocket Reference", "Albahari", "O'Reilly", "Sebastopol, CA", "2008"};
            string[] row1 = {"CLR via C#", "Richter", "Microsoft", "Redmond, WA", "2006"};
            string[] row2 = {"Mastering Regular Expressions", "Friedl", "O'Reilly", "Sebastopol, CA", "1997"};
            string[] row3 = {"C++ Primer", "Lippman, Lajoie", "Addison-Wesley", "Massachusetts", "1998"};
            string[] row4 = {"C++ Programming Style", "Cargill", "Addison-Wesley", "Massachusetts", "1992"};
            string[] row5 = {"The C Programming Language", "Kernighan, Ritchie", "Bell Labs", "USA", "1988"};

            dataGridView1.Rows.Add(row0);
            dataGridView1.Rows.Add(row1);
            dataGridView1.Rows.Add(row2);
            dataGridView1.Rows.Add(row3);
            dataGridView1.Rows.Add(row4);
            dataGridView1.Rows.Add(row5);
        }

        private void WarningResultAnalysis_Load(object sender, EventArgs e)
        {
            //this.cbbWorkFace
        }
    }
}