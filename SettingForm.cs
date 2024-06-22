using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoL4YChecker
{
    public partial class SettingForm : Form
    {
        private MainForm mainForm;

        public SettingForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            textBoxChatDir.Text = mainForm.ChatDir;
        }

        private void buttonChatDir_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog()
            {
                FileName = "ChatLog",
                Filter = "Folder|.",
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true
            })
            {
                var initialDir = @"C:\Gravity\Ragnarok\Chat";
                if (Directory.Exists(initialDir))
                {
                    openFileDialog.InitialDirectory = initialDir;
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxChatDir.Text = Path.GetDirectoryName(openFileDialog.FileName);
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            mainForm.ChatDir = textBoxChatDir.Text;
            var settingFilePath = Path.Combine(Environment.CurrentDirectory, "settings.ini");
            var text = $"ChatLogDir={textBoxChatDir.Text}";
            File.WriteAllText(settingFilePath, text);

            Close();
        }
    }
}
