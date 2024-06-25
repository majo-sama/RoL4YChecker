using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RoL4YChecker
{
    public partial class MainForm : Form
    {
        const string DEFAULT_CHAT_DIR = @"C:\Gravity\Ragnarok\Chat";
        public string ChatDir { get; set; } = DEFAULT_CHAT_DIR;
        private int currentPhase = 1;
        private FileSystemWatcher watcher;

        private List<string> chatFiles = new List<string>();
        private DateTime? chatLogCreateStartTime = null;

        private List<Quest> questList = null;

        private List<Label> displayItems = new List<Label>();

        private Point mousePoint;

        private readonly Font fontNormal = new Font("Meiryo", 12, FontStyle.Bold);
        private readonly Font fontStrikeout = new Font("Meiryo", 12, FontStyle.Strikeout | FontStyle.Bold);

        public MainForm()
        {
            InitializeComponent();



            displayItems.Add(labelQuest_1);
            displayItems.Add(labelQuest_2);
            displayItems.Add(labelQuest_3);
            displayItems.Add(labelQuest_4);
            displayItems.Add(labelQuest_5);
            displayItems.Add(labelQuest_6);
            displayItems.Add(labelQuest_7);
            displayItems.Add(labelQuest_8);

            foreach (Label item in displayItems)
            {
                item.Text = "";
            }

            labelQuest_1.Text = "右クリック > 設定 で";
            labelQuest_2.Text = "チャットログのディレクトリを指定";
            labelQuest_3.Text = "/savechat 実行で反映";

            watcher = new FileSystemWatcher();
            watcher.Path = ChatDir;
            watcher.Filter = "*.txt";
            watcher.NotifyFilter = NotifyFilters.FileName;
            watcher.Created += Watcher_Changed;
            watcher.EnableRaisingEvents = true;

            questList = LoadQuestCsv();
            if (questList == null)
            {
                Close();
            }

            this.MouseDown += MainForm_MouseDown;
            this.MouseMove += MainForm_MouseMove;

            var settingFilePath = Path.Combine(Environment.CurrentDirectory, "settings.ini");
            if (File.Exists(settingFilePath))
            {
                var settingLines = File.ReadAllLines(settingFilePath);
                foreach (var line in settingLines)
                {
                    if (line.StartsWith("ChatLogDir="))
                    {
                        var parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            ChatDir = parts[1];
                            break;
                        }
                    }
                }
            }

        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private async void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            // 現在時刻が最後にファイルの作成が発生してから1秒以内のとき
            if (chatLogCreateStartTime.HasValue && DateTime.Now < chatLogCreateStartTime.Value.AddSeconds(1))
            {
                chatFiles.Add(e.FullPath);
                return;
            }

            chatLogCreateStartTime = DateTime.Now;
            chatFiles.Clear();
            chatFiles.Add(e.FullPath);

            await Task.Run(async () =>
            {
                await Task.Delay(1000);
                ReadChatLogs();
                UpdateDisplay();
            });
        }

        private void ReadChatLogs()
        {
            if (chatFiles.Count == 0) {
                return;
            }

            string targetChatLogFile = null;
            IEnumerable<string> lines = null;
            foreach (var file in chatFiles) {

                if (File.Exists(file)) {

                    lines = File.ReadLines(file, Encoding.GetEncoding("Shift_JIS"));

                    if (lines.Any(l => l.StartsWith("ミッション発生　『") || l.EndsWith("』　ミッション完了")))
                    {
                        targetChatLogFile = file;
                        break;
                    }
                }
            }

            if (targetChatLogFile == null)
            {
                return;
            }

            bool hasQuestLog = false;

            foreach (var line in lines)
            {
                if (line.EndsWith("』　ミッション完了"))
                {
                    var matches = Regex.Matches(line, @"『(.*?)』");
                    var questName = matches[0].Groups[1].Value;

                    var quest = questList.FirstOrDefault(q => q.Name.Equals(questName));
                    quest.Status = Quest.QuestStatus.Cleared;
                    hasQuestLog = true;
                }
                else if (line.StartsWith("ミッション発生　『"))
                {
                    var matches = Regex.Matches(line, @"『(.*?)』");
                    var questName = matches[0].Groups[1].Value;

                    var quest = questList.FirstOrDefault(q => q.Name.Equals(questName));
                    quest.Status = Quest.QuestStatus.Accepted;
                    hasQuestLog = true;
                }
            }

            if (!hasQuestLog)
            {
                return;
            }

            // 受注またはクリアされたクエスト一覧
            var acceptedOrClearedQuests = questList.Where(q => q.Status != Quest.QuestStatus.Waiting);
            if (acceptedOrClearedQuests.Count() > 0)
            {
                currentPhase = acceptedOrClearedQuests.Max(q => q.Phase);
            }

            // ただし、currentPhaseのクエストがすべてクリア済みの場合、次のフェーズの扱いとする
            if (questList.Count(q => q.Phase == currentPhase && q.Status == Quest.QuestStatus.Cleared) == 8)
            {
                currentPhase++;
            }

            foreach (var quest in questList.Where(q => q.Phase < currentPhase))
            {
                quest.Status = Quest.QuestStatus.Cleared;
            }

        }

        private void UpdateDisplay()
        {
            var currentPhaseQuests = questList.Where(q => q.Phase == currentPhase).ToList();
            //if (currentPhase == 6)
            //{
            //    currentPhaseQuests = questList.Where(q => q.Phase == 5).ToList();
            //}


            Invoke(new Action(() =>
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i + 1 > currentPhaseQuests.Count)
                    {
                        displayItems[i].Text = "";
                        continue;
                    }

                    displayItems[i].Text = currentPhaseQuests[i].Number + " " + currentPhaseQuests[i].Name;

                    switch (currentPhaseQuests[i].Status)
                    {
                        case Quest.QuestStatus.Waiting:
                            displayItems[i].ForeColor = Color.Gray;
                            displayItems[i].Font = fontNormal;
                            break;
                        case Quest.QuestStatus.Accepted:
                            displayItems[i].ForeColor = Color.Black;
                            displayItems[i].Font = fontNormal;
                            break;
                        case Quest.QuestStatus.Cleared:
                            displayItems[i].ForeColor = Color.Black;
                            displayItems[i].Font = fontStrikeout;
                            break;
                    }
                }
            }));




        }

        private void toolStripMenuItemSetting_Click(object sender, EventArgs e)
        {
            var settingForm = new SettingForm(this);
            settingForm.ShowDialog();
        }

        private List<Quest> LoadQuestCsv()
        {
            var csvPath = Path.Combine(Environment.CurrentDirectory, "quests.csv");
            if (!File.Exists(csvPath))
            {
                MessageBox.Show("quests.csvが存在しません。");
                return null;
            }


            List<Quest> quests;
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.HasHeaderRecord = true;
            config.HeaderValidated = null;
            config.MissingFieldFound = null;
            config.IncludePrivateMembers = true;
            using (var reader = new StreamReader(csvPath))
            using (var csv = new CsvReader(reader, config))
            {
                quests = csv.GetRecords<Quest>().ToList();
            }
            return quests;
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
