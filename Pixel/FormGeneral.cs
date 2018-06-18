using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Bitmap pictureBitmap = null;
        string pictureName = string.Empty;
        CancellationTokenSource cancelToken = new CancellationTokenSource();
        int avaliableSize = 150000;
        string sheetsDirectoryPath = Environment.CurrentDirectory + "\\Sheets";


        private void ExportPicture(Bitmap resized)
        {
            OpenExcelApp();
            try
            {
                ConvertBitmapToExcelSheet(resized);
            }
            catch (OperationCanceledException)
            {
                FinalizeProcess("Cancelled");
                return;
            }
            catch (Exception ex)
            {
                FinalizeProcess("Error");
                MessageBox.Show(ex.Message, "Something wrong!!!");
                return;
            }
            try
            {
                excelSheet.SaveAs(sheetsDirectoryPath + $"\\{pictureName}.xlsx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + $"\nPlease close all programs accessing {pictureName}.xlsx, and try again.", "Something wrong!!!");
                FinalizeProcess("Could not save result");
                return;
            }
            FinalizeProcess("Completed");
        }

        private void ConvertBitmapToExcelSheet(Bitmap resized)
        {
            for (int column = 0; column < resized.Width; column++)
            {
                string letter = DearVisualStudio.GiveMeLetterForThisColumn(column);
                UpdateLabelProgress(column, resized.Width);

                for (int row = 0; row < resized.Height; row++)
                {
                    dynamic cell = excelSheet.Cells[row + 1, letter];
                    dynamic interior = cell.Interior;
                    interior.Color = ColorTranslator.ToOle(resized.GetPixel(column, row));

                    cancelToken.Token.ThrowIfCancellationRequested();
                }
            }
        }

        private void AskForOpeningDirectory()
        {
            if (LabelProgress.Text != "Completed")
                return;

            DialogResult result = MessageBox.Show("Your sheet is ready!\n" +
                                      "Do you want to open folder?",
                                      "All done", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Process.Start(sheetsDirectoryPath);
            }
        }


        private void BtnCancel_Click(object sender, EventArgs e) => cancelToken.Cancel();
    }
}
