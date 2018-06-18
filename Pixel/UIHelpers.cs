using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel
{
    public partial class MainForm : Form
    {
        delegate void UpdateProgressLabelInvoker(int currentPosition, int totalWidth);

        private void ShowLabelProgress()
        {
            LabelProgress.Visible = true;
            LabelProgress.Text = "Working: 0.00%...";
        }


        private void UpdateLabelProgress(int currentPosition, int totalWidth)
        {
            if (this.InvokeRequired)
                BeginInvoke(new UpdateProgressLabelInvoker(UpdateLabelProgress), currentPosition, totalWidth);
            else
                LabelProgress.Text = string.Format("Working: {0:n}%...", (double)currentPosition /totalWidth * 100);
        }

        private void ShowLabelPicture(OpenFileDialog selectPictureDialog)
        {
            LabelPicture.Text = $"{selectPictureDialog.SafeFileName} ({pictureBitmap.Width}x{pictureBitmap.Height})";
            LabelPicture.Visible = true;
        }


        private void SetUpPicture()
        {
            CenterPicture.BackgroundImageLayout = ImageLayout.Zoom;
            CenterPicture.BackgroundImage = pictureBitmap;
        }
    }
}
