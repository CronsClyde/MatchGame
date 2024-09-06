using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSeoncdsElapsed;
        int matchesFound;
        int matchesFoundAnswer;

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        public MainWindow()
        {
            InitializeComponent();
            
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetupGame();
        }

        private void SetupGame()
        {
            try
            {
                List<string> animalEmoji = new List<string>()
                {
                    "🐈‍","🐈‍",
                    "🐅","🐅",
                    "🦬","🦬",
                    "🦔","🦔",
                    "🦒","🦒",
                    "🐁","🐁",
                    "🐕‍🦺","🐕‍🦺",
                    "🦇","🦇",
                    "🦀","🦀",
                    "🐣","🐣",
                    "🐬","🐬",
                    "🐳","🐳",
                    "🦦","🦦",
                    "⭐" , "⭐",
                    "☃️" ,"☃️",
                    "🦐","🦐",
                    "🍀","🍀",
                    "🐧","🐧"
                };

                matchesFoundAnswer = animalEmoji.Count / 2;

                Random random = new Random();

                foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
                {
                    if (textBlock.Name != "TimerTextBlock")
                    {
                        textBlock.Visibility = Visibility.Visible;
                        int index = random.Next(animalEmoji.Count);
                        string nextEmoji = animalEmoji[index];
                        textBlock.Text = nextEmoji;
                        animalEmoji.RemoveAt(index);
                    } 
                }
                timer.Start();
                tenthsOfSeoncdsElapsed = 0;
                matchesFound = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"에러");
            }

        }  

        private void TextBlock_MouserDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSeoncdsElapsed++;
            TimerTextBlock.Text = (tenthsOfSeoncdsElapsed / 10F).ToString("0.0s");
            if (matchesFound == matchesFoundAnswer)
            {
                timer.Stop();
                TimerTextBlock.Text = TimerTextBlock.Text + " - Play Again?";
                MessageBox.Show("축하합니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void TimerTextBlock_MouseDown(object sender, EventArgs e)
        {
            if (matchesFound == matchesFoundAnswer)
            {
                SetupGame();
            }
        }

    }
}