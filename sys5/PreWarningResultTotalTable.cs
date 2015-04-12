using System;
using System.Windows.Forms;
using LibCommon;

namespace _5.WarningManagement
{
    public partial class PreWarningResultTotalTable : Form
    {
        public PreWarningResultTotalTable()
        {
            InitializeComponent();

            //System.IO.Stream s = default(System.IO.Stream);

            ////Get the images 
            //s = this.GetType().Assembly.GetManifestResourceStream("5.WarningManagement.1.jpg");
            //Bitmap img1 = new Bitmap(s);
            //// s.Close() 

            ////Create the cell types 
            //FarPoint.Win.Spread.CellType.GeneralCellType imgType = new FarPoint.Win.Spread.CellType.GeneralCellType();
            //imgType.BackgroundImage = new FarPoint.Win.Picture(img1, FarPoint.Win.RenderStyle.Normal, Color.White, FarPoint.Win.HorizontalAlignment.Center, FarPoint.Win.VerticalAlignment.Center);
            //// img1 'New Picture(Image.FromStream(s)) 

            ////GeneralCellType imgType = new GeneralCellType();
            ////System.IO.Stream s = default(System.IO.Stream);
            ////s = this.GetType().Assembly.GetManifestResourceStream("GWS.1.jpg");
            ////imgType.BackgroundImage = new FarPoint.Win.Picture(System.Drawing.Image.FromStream(s));

            //this.fpPreWarningResultTotalTable.Sheets[0].Cells[8, 0].CellType = imgType;
            ////this.fpPreWarningResultTotalTable.Sheets[0].Cells[8, 0].Text = "test";
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            FilePrint.CommonPrint(fpPreWarningResultTotalTable, 0);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }
    }
}