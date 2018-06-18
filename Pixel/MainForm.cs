using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace Pixel
{
    public partial class MainForm : Form
    {
        private void BtnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Pictures| *.jpg; *.png; *.bmp;";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBitmap = new Bitmap(fileDialog.FileName);
                pictureName = Path.GetFileNameWithoutExtension(fileDialog.FileName);
                SetUpPicture();
                ShowLabelPicture(fileDialog);
                BtnExport.Enabled = true;

            }
        }


        private async void BtnExport_Click(object sender, EventArgs e)
        {
            BtnSelect.Enabled = false;
            cancelToken = new CancellationTokenSource();
            Directory.CreateDirectory(sheetsDirectoryPath);
            ShowLabelProgress();

            Bitmap resized = DearVisualStudio.ResizeThePicture(pictureBitmap, avaliableSize);

            CheckExistingExcellProcesses();


            await Task.Factory.StartNew(() => ExportPicture(resized), cancelToken.Token);

            AskForOpeningDirectory();
            BtnSelect.Enabled = true;
        }



        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            avaliableSize = int.Parse(((RadioButton)sender).Text);
        }
    }
}
