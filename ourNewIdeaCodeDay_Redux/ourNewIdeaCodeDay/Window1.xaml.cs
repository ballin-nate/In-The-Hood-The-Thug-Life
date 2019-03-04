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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;

namespace ourNewIdeaCodeDay
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        int chatCounter = 0;
        Random rand = new Random();
        public Window1()
        {
            
            InitializeComponent();
            
            NaziChatBubble.IsReadOnly = true;
            NaziChatBubble.Visibility = System.Windows.Visibility.Hidden;
            ChatBubble1.IsReadOnly = true;
            ChatBubble1.Visibility = System.Windows.Visibility.Hidden;
            ProImg.Source = new BitmapImage(new Uri("../CodeDay/Character/character.png", UriKind.Relative));
            AmishImg.Source = new BitmapImage(new Uri("../CodeDay/Amish King/Amish king attack.gif", UriKind.Relative));
            for (int i = 0; i < 2; i++)
            {
                if (chatCounter == 0)
                {
                    MoveTo(ProImg, 27, 150, 3);
                    chatCounter++;
                }
                if (chatCounter == 1)
                {
                    MoveTo(AmishImg, 222, 444, 1);
                    chatCounter++;
                }
            }  
        }
        public async void MoveTo(Image target, double newX, double newY, int howlong)
        {

            double top = target.Margin.Top;
            double left = target.Margin.Left;
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            if (chatCounter == 0)
            {
                DoubleAnimation anim1 = new DoubleAnimation(newY - top, top, TimeSpan.FromSeconds(howlong));
                DoubleAnimation anim2 = new DoubleAnimation(newX - left, left, TimeSpan.FromSeconds(howlong));
                trans.BeginAnimation(TranslateTransform.XProperty, anim1);
                trans.BeginAnimation(TranslateTransform.YProperty, anim2);
            }

            if (chatCounter == 0)
            {
                await sleeper(30, 32);
                ChatBubble1.Visibility = System.Windows.Visibility.Visible;
                ChatBubble1.Text = "Amish gangstas in my ghetto?";
                
            }
            else if (chatCounter == 1)
            {
                await sleeper(50, 51);
                ChatBubble1.Visibility = System.Windows.Visibility.Hidden;
                NaziChatBubble.Visibility = System.Windows.Visibility.Visible;
                NaziChatBubble.Text = "We are the Amish mafia. This place is mine.";
                await sleeper(15, 21);
                NaziChatBubble.Visibility = System.Windows.Visibility.Hidden;
                ChatBubble1.Visibility = System.Windows.Visibility.Visible;
                ChatBubble1.Text = "I won't lose my kingdom without a fight!";
                await sleeper(15, 21);
                Level_1 s = new Level_1();
                s.Show();
                this.Close();
            }
            
        }
        public Task sleeper(int firsttime,int secondtime)
        {
            string firstparse = firsttime.ToString();
            string parse = secondtime.ToString();
            firstparse = string.Format(firstparse + "00");
            parse = string.Format(parse + "00");
            firsttime = Convert.ToInt32(firstparse);
            int finalTime = Convert.ToInt32(parse);
            return Task.Run(() =>
            {
                Thread.Sleep(rand.Next(firsttime, finalTime)); 
            });
        }
    }
}
