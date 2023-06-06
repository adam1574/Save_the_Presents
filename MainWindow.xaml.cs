using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Save_the_Presents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int maxItems = 5;
        int currentItems = 0;

        Random rand = new Random();

        int score = 0;
        int missed = 0;
        int lvl = 1;

        Rect playerHitBox;

        DispatcherTimer gameTimer = new DispatcherTimer();

        List<Rectangle> itemstoRemove = new List<Rectangle>();

        ImageBrush playerImage = new ImageBrush();
        ImageBrush backgroundImage = new ImageBrush();

        int itemSpeed = 10;

        public MainWindow()
        {
            InitializeComponent();

            MyCanvas.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            playerImage.ImageSource = new BitmapImage(new Uri("C:/Users/adaga/Desktop/Save the Presents/Save_the_Presents/img/netLeft.png", UriKind.Relative));
            player1.Fill = playerImage;

            backgroundImage.ImageSource = new BitmapImage(new Uri("C:/Users/adaga/Desktop/Save the Presents/Save_the_Presents/img/background.jpg", UriKind.Relative));
            MyCanvas.Background = backgroundImage;
        }

        private void GameEngine(object sender, EventArgs e)
        {
            scoreText.Content = "Chyceno: " + score;
            missedText.Content = "Minuto " + missed;
            lvlText.Content = "lvl " + lvl;


            if (currentItems < maxItems)
            {
                MakePresents();
                currentItems++;
                itemstoRemove.Clear();
            }


            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "drops")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) + itemSpeed);

                    if (Canvas.GetTop(x) > 720)
                    {
                        itemstoRemove.Add(x);
                        currentItems--;
                        missed++;
                    }

                    Rect presentsHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    playerHitBox = new Rect(Canvas.GetLeft(player1), Canvas.GetTop(player1), player1.Width, player1.Height);

                    if (playerHitBox.IntersectsWith(presentsHitBox))
                    {
                        itemstoRemove.Add(x);
                        currentItems--;
                        score++;
                    }
                }
            }


            foreach (var i in itemstoRemove)
            {
                MyCanvas.Children.Remove(i);
            }

            if (score > 10)
            {
                itemSpeed = 15;
            }
            if (score > 20)
            {
                lvl = 2;
                itemSpeed = 20;
            }
            if (score > 30)
            {
                lvl = 3;
                itemSpeed = 25;
            }
            if (score > 40)
            {
                lvl = 4;
                itemSpeed = 30;
            }
            if (score > 50)
            {
                lvl = 5;
                itemSpeed = 35;
            }
            if (score > 60)
            {
                lvl = 6;
                itemSpeed = 40;
            }
            if (score > 70)
            {
                lvl = 7;
                itemSpeed = 45;
            }
            if (score > 80)
            {
                lvl = 8;
                itemSpeed = 50;
            }
            if (score > 90)
            {
                lvl = 9;
                itemSpeed = 55;
            }
            if (score > 100)
            {
                lvl = 10;
                itemSpeed = 60;
            }

            if (missed > 6)
            {
                gameTimer.Stop();
                MessageBox.Show("Game over!" + Environment.NewLine + "Skóre: " + score +Environment.NewLine + "lvl: " + lvl + Environment.NewLine + "Klikni na OK pro ukončení", "Save the presents");
                ResetGame();
            }


        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

            Point position = e.GetPosition(this);

            double pX = position.X;

            Canvas.SetLeft(player1, pX - 10);

            if (Canvas.GetLeft(player1) < 260)
            {
                playerImage.ImageSource = new BitmapImage(new Uri("C:/Users/adaga/Desktop/Save the Presents/Save_the_Presents/img/netLeft.png", UriKind.Relative));
            }
            else
            {
                playerImage.ImageSource = new BitmapImage(new Uri("C:/Users/adaga/Desktop/Save the Presents/Save_the_Presents/img/netRight.png", UriKind.Relative));
            }

        }

        private void ResetGame()
        {

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
            Environment.Exit(0);


        }

        private void MakePresents()
        {

            ImageBrush presents = new ImageBrush();

            int i = rand.Next(1, 6);

            switch (i)
            {
                case 1:
                    presents.ImageSource = new BitmapImage(new Uri("C:/Users/adaga/Desktop/Save the Presents/Save_the_Presents/img/present_01.png", UriKind.Relative));
                    break;
                case 2:
                    presents.ImageSource = new BitmapImage(new Uri("C:/Users/adaga/Desktop/Save the Presents/Save_the_Presents/img/present_02.png", UriKind.Relative));
                    break;
                case 3:
                    presents.ImageSource = new BitmapImage(new Uri("C:/Users/adaga/Desktop/Save the Presents/Save_the_Presents/img/present_03.png", UriKind.Relative));
                    break;
                case 4:
                    presents.ImageSource = new BitmapImage(new Uri("C:/Users/adaga/Desktop/Save the Presents/Save_the_Presents/img/present_04.png", UriKind.Relative));
                    break;
                case 5:
                    presents.ImageSource = new BitmapImage(new Uri("C:/Users/adaga/Desktop/Save the Presents/Save_the_Presents/img/present_05.png", UriKind.Relative));
                    break;
                case 6:
                    presents.ImageSource = new BitmapImage(new Uri("C:/Users/adaga/Desktop/Save the Presents/Save_the_Presents/img/present_06.png", UriKind.Relative));
                    break;

            }

            Rectangle newRec = new Rectangle
            {
                Tag = "drops",
                Width = 50,
                Height = 50,
                Fill = presents
            };

            Canvas.SetLeft(newRec, rand.Next(10, 450));
            Canvas.SetTop(newRec, rand.Next(60, 150) * -1);

            MyCanvas.Children.Add(newRec);

        }
    }
}
