using System;
using System.Windows.Forms;

namespace Storwins_Mod.Maker
{
    public partial class ModMakerMDI : Form
    {
        private int childFormNumber = 0;

        public ModMakerMDI()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Mod List Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Mod List Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmAbout = new AboutBox();
            frmAbout.ShowDialog(this);
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var url = "https://www.paypal.com/cgi-bin/webscr";
            url += "?cmd=" + "_s-xclick" + "&hosted_button_id=" + "PNDLK5FBJVM3A";
            System.Diagnostics.Process.Start(url);
        }

        private void homepageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string url = "http://alienbug.dk/products/storwins-mod-installer/";
            System.Diagnostics.Process.Start(url);
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string url = "https://github.com/Alienbug/Storwins-Mod/wiki";
            System.Diagnostics.Process.Start(url);
        }

        private void feedbackOrBugReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string url = "https://github.com/Alienbug/Storwins-Mod/issues";
            System.Diagnostics.Process.Start(url);
        }

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var frmOptions = new MakerOptions();
            frmOptions.ShowDialog(this);
        }
    }
}
