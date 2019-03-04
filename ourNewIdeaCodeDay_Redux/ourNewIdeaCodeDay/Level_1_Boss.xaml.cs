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
using System.Windows.Shapes;
using System.Threading;

namespace ourNewIdeaCodeDay
{
    /// <summary>
    /// Interaction logic for Level_1_Boss.xaml
    /// </summary>
    public partial class Level_1_Boss : Window
    {
        public bool userCanStrike;
        public bool isOver = false;
        Random rand = new Random();
        public character char1 = new character();
        enemydude noobFighter = new enemydude();
        
        public class moves
        {
            public string name;
            public int damage;
            public int heal;
            public int cost;
            public string description;
            public string spriteLocation;
        }
        public class dude
        { 
            public List<moves> possibleMoves = new List<moves>();
            public int energy;
            public int health;
            public int swaglevel;
            public string name;
            public BitmapImage main;
        }
        public class character : dude
        {


            
        }
        public class enemydude : dude
        {

        }
        public Level_1_Boss()
        {
            
            InitializeComponent();

            SkipButton.Visibility = System.Windows.Visibility.Hidden;
            //SkipButton.Visibility = System.Windows.Visibility.Visible;
            userCanStrike = true;
                
                CharImgBox.Source = new BitmapImage(new Uri("../CodeDay/Character/character.png", UriKind.Relative));
               
                char1.name = "Steve";
                char1.energy = 85;
                char1.health = 90;
                char1.swaglevel = 0;
                char1.possibleMoves.Add(new moves() { name = "Shank", description = "basic attack", cost = 6, damage = 8, spriteLocation = "../CodeDay/Character/character shank.png" });
                char1.possibleMoves.Add(new moves() { name = "Weed(medical)", description = "Healing and energy!", damage = 0, heal = 20, cost = -5, spriteLocation = "../CodeDay/Character/Character dope.png" });
                char1.possibleMoves.Add(new moves() { name = "Pop a cap", description = "pop a cap in his ass!", cost = 15, damage = 14, spriteLocation = "../CodeDay/Character/character shooting.png" });
                char1.possibleMoves.Add(new moves() { name = "Nutshot", description = "Use sparingly.  Causes revenge.", damage = 29, cost = 30, spriteLocation = "../CodeDay/Character/character kick.png" });

            //EnemyImgBox.Source = new BitmapImage(new Uri("../CodeDay/Amish King/Amish king attack.gif", UriKind.Relative));
            EnemyImgElement.Source = new Uri("../CodeDay/Amish King/Amish king attack.gif", UriKind.Relative);
                EnemyImgElement.Play();
                noobFighter.name = "AHMISH KING";
                noobFighter.health = 100;
                noobFighter.energy = 100;
                noobFighter.swaglevel = 10;
                noobFighter.possibleMoves.Add(new moves() { name = "Chain", description = "You shall be slain at MY hand", cost = 6, damage = 7, spriteLocation = "../CodeDay/Amish King/Amish king chain.gif" });
                noobFighter.possibleMoves.Add(new moves() { name = "Lazor", description = "BURN AT THE HAND OF THE LORD!", damage = 30, cost = 25, spriteLocation = "../CodeDay/Amish King/Amish king fire.gif" });
                noobFighter.possibleMoves.Add(new moves() { name = "Belt", description = "You gonna get a whoopun, son!", cost = 17, damage = 17, spriteLocation = "../CodeDay/Amish King/Amish king belt.gif" });



                for (int i = 0; i < char1.possibleMoves.Count(); i++)
                {
                    MovesList.Items.Add(char1.possibleMoves[i].name + " Cost: " + char1.possibleMoves[i].cost);
                }
                updateStuff();
            
        }

        private async void OriginButton_Click(object sender, RoutedEventArgs e)
        {
            if (userCanStrike == true)
            {


                if (MovesList.SelectedIndex != -1)
                {

                    moves currentMove = char1.possibleMoves[MovesList.SelectedIndex];
                    if (char1.energy - currentMove.cost > -1)
                    {
                        CharImgBox.Source = new BitmapImage(new Uri(currentMove.spriteLocation));

                        
                        noobFighter.health = noobFighter.health - currentMove.damage;
                        char1.energy = char1.energy - currentMove.cost;
                        userCanStrike = false;
                    }
                    else
                    {
                        MoveDescriptionBlock.Text = "YOU DON'T HAVE ENOUGH ENERGY!!!";
                    }
                }
                if (checkEnemyStats() == false)
                {
                    winLose(true);
                }
                else
                {
                    if(userCanStrike == false)
                    {enemyStrike();}
                }
            }
            else
            {
                MoveDescriptionBlock.Text = "Not your turn!";
            }
            
            updateStuff();
            await sleeper(2);
            CharImgBox.Source = new BitmapImage(new Uri("../CodeDay/Character/character.png", UriKind.Relative));
        }
        public async void enemyStrike()
        {
            updateStuff();
            await sleeper(3);
            bool didStrike = false;
            for (int i = 0; i < noobFighter.possibleMoves.Count(); i++)
                {
                int index = rand.Next(0,noobFighter.possibleMoves.Count());
                moves move = noobFighter.possibleMoves[index];
                if (noobFighter.energy - move.cost > -1)
                {
                    //EnemyImgBox.Source = new BitmapImage(new Uri(move.spriteLocation, UriKind.Relative));
                    EnemyImgElement.Source = new Uri(move.spriteLocation, UriKind.Relative);
                    char1.health = char1.health - move.damage;
                    noobFighter.energy = noobFighter.energy - move.cost;
                    ChatBox.AppendText(noobFighter.name + " used " + move.name + "." + Environment.NewLine + move.description + Environment.NewLine);
                    ChatBox.ScrollToEnd();
                    didStrike = true;
                    userCanStrike = true;
                    break;
                }
                updateStuff();
                await sleeper(2);
                EnemyImgBox.Source = new BitmapImage(new Uri("../CodeDay/Amish King/Amish king attack.gif", UriKind.Relative));
                }
            if (didStrike == false)
            {
                winLose(true); 
            }

            updateStuff();
        }
        public Task sleeper(int time)
        {
            string parse = time.ToString();
            parse = string.Format(parse + "000");
            int finalTime = Convert.ToInt32(parse);
            return Task.Run(() => { Thread.Sleep(rand.Next(1000, finalTime)); });
        }
        public void winLose(bool outcome)
        {
            if (outcome == true)
            {
                //EnemyImgBox.Source = new BitmapImage(new Uri("../CodeDay/Victory.png", UriKind.Relative));
                EnemyImgElement.Source = new Uri("../CodeDay/Victory.png", UriKind.Relative);
                MessageBox.Show("You won","Continue?",MessageBoxButton.YesNo);
            }
            else
            {
                MoveDescriptionBlock.Text = "You lost";
                CharImgBox.Source = new BitmapImage(new Uri("../CodeDay/Defeat.png", UriKind.Relative));
                MessageBox.Show("You lost","",MessageBoxButton.OK);
                //this.Close();
                

            }

        }
        public bool checkUserStats()
        {
            bool okayorno = false;
            if (char1.health > 0)
            {
                okayorno = true;
            } 
            moves winner = new moves();
            
            for (int i = 0; i < char1.possibleMoves.Count(); i++)
            {
                
                if (i == 0)
                {
                    winner = char1.possibleMoves[i];
                }
                else
                {
                    if (char1.possibleMoves[i].cost <= winner.cost)
                    {
                        winner = char1.possibleMoves[i];
                    }
                }
            }
           
                if (char1.energy < winner.cost)
                {
                    okayorno = true;
                    SkipButton.Visibility = System.Windows.Visibility.Visible;
                }
             if (char1.energy == 0)
            {
                SkipButton.Visibility = System.Windows.Visibility.Visible;
            }
            return okayorno;
        }
        public bool checkEnemyStats()
        {
            bool okayorno = false;
            moves winner = new moves();
            
            for (int i = 0; i < noobFighter.possibleMoves.Count(); i++)
            {

                if (i == 0)
                {
                    winner = noobFighter.possibleMoves[i];
                }
                else
                {
                    if (noobFighter.possibleMoves[i].cost <= winner.cost)
                    {
                        winner = noobFighter.possibleMoves[i];
                    }
                }
            }
            if (noobFighter.energy <= winner.cost)
            {
                okayorno = true;
            }
            if (noobFighter.health > 0)
            {
                okayorno = true;
            }

            if (noobFighter.energy == 0)
            {
                SkipButton.Visibility = System.Windows.Visibility.Visible;
            }
            return okayorno;
        }
        public void updateStuff()
        {
            UserEnergyBlock.Text = String.Format("Energy: " + char1.energy.ToString());
            UserHealthBlock.Text = String.Format("Health: " + char1.health.ToString());
            EnemyEnergyBlock.Text = String.Format("Energy: " + noobFighter.energy.ToString());
            EnemyHealthBlock.Text = String.Format("Health: " + noobFighter.health.ToString());
            if (checkUserStats() == false)
                winLose(false);
            else if (checkEnemyStats() == false)
                winLose(true);
        }
        private void MovesList_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            MoveDescriptionBlock.Text = char1.possibleMoves[MovesList.SelectedIndex].description;
            updateStuff();
        }

        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Element_MediaEnded(object sender, RoutedEventArgs e)
        {
            
            EnemyImgElement.Position = new TimeSpan(0, 0, 20);
            EnemyImgElement.Play();
        }
        /*
        private void EnemyImg_end(object sender, RoutedEventArgs e)
        {
            EnemyImgBox.Position = new TimeSpan(0, 0, 20);
            EnemyImgBox.Play();
        }
         * */

    }
}
