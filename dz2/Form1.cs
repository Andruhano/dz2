using System.Diagnostics;

namespace dz2
{
    public partial class Form1 : Form
    {

        private Stopwatch stopwatch;
        private DateTime targetDateTime;
        private int clickCount = 0;
        private int maxRecord = 0;
        private System.Windows.Forms.Timer gameTimer;

        public Form1()
        {
            InitializeComponent();
            targetDateTime = new DateTime(DateTime.Now.Year + 1, 1, 1, 0, 0, 0); 

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 1000 }; 
            timer.Tick += (s, e) =>
            {
                TimeSpan remaining = targetDateTime - DateTime.Now;

                if (remaining.TotalSeconds > 0)
                {
                    label1.Text = $"До Нового года осталось: {remaining.Days} дней, {remaining.Hours} часов, " +
                                          $"{remaining.Minutes} минут, {remaining.Seconds} секунд";
                }
                else
                {
                    label1.Text = "Событие наступило!";
                    timer.Stop();
                }
            };
            timer.Start();
            Button startButton = new Button { Text = "Начать игру", Dock = DockStyle.Top };
            Label clickLabel = new Label { Text = "Кликов: 0", Dock = DockStyle.Top, AutoSize = true };

            Controls.Add(startButton);
            Controls.Add(clickLabel);

            startButton.Click += (s, e) =>
            {
                clickCount = 0;
                clickLabel.Text = "Кликов: 0";
                startButton.Enabled = false;

                gameTimer = new System.Windows.Forms.Timer { Interval = 20000 }; 
                gameTimer.Tick += (timerSender, timerEventArgs) =>
                {
                    gameTimer.Stop();
                    maxRecord = Math.Max(maxRecord, clickCount);
                    MessageBox.Show($"Игра окончена! Кликов: {clickCount}, Рекорд: {maxRecord}");
                    startButton.Enabled = true;
                };
                gameTimer.Start();

                MouseClick += (mouseSender, mouseEventArgs) =>
                {
                    clickCount++;
                    clickLabel.Text = $"Кликов: {clickCount}";
                };
            };
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
