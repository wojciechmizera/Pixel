using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel
{
    public partial class MainForm : Form
    {
        Microsoft.Office.Interop.Excel.Application excelApp;
        Workbooks excelWorkBooks;
        Workbook excelBook;
        Worksheet excelSheet;
        Hashtable otherExcelProcesses;
        Process excelProcess;

        private void FinalizeProcess(string resultOnProgressLabel)
        {
            LabelProgress.Text = resultOnProgressLabel;
            CloseExcelApp();
        }

        private void CheckExistingExcellProcesses()
        {
            Process[] AllProcesses = Process.GetProcessesByName("excel");
            otherExcelProcesses = new Hashtable();
            int iCount = 0;

            foreach (Process ExcelProcess in AllProcesses)
            {
                otherExcelProcesses.Add(ExcelProcess.Id, iCount);
                iCount = iCount + 1;
            }
        }

        private Process FindOurExcellProcess()
        {
            Process theProces = null;
            Process[] AllProcesses = Process.GetProcessesByName("excel");
            foreach (Process excelprocess in AllProcesses)
                if (otherExcelProcesses.ContainsKey(excelprocess.Id) == false)
                    theProces = excelprocess;
            return theProces;

        }

        private void CloseExcelApp()
        {
            Marshal.FinalReleaseComObject(excelSheet);
            excelWorkBooks.Close();
            Marshal.FinalReleaseComObject(excelBook);
            Marshal.FinalReleaseComObject(excelWorkBooks);
            excelApp.Quit();
            Marshal.FinalReleaseComObject(excelApp);

            excelProcess.Kill();
        }


        private void OpenExcelApp()
        {
            excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelWorkBooks = excelApp.Workbooks;
            excelBook = excelWorkBooks.Add();
            excelSheet = excelBook.ActiveSheet;
            excelSheet.Name = pictureName.ToUpper();

            excelProcess = FindOurExcellProcess();
        }
    }
}
