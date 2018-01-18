using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds_Labb2
{
    class Program
    {
        static void Main(string[] args)
        {


        }

        private static void UpdateScore(ScoreContext context)
        {
            Console.Clear();
            Console.WriteLine("Type the Username you want to update: ");
            string usernameInput = Console.ReadLine();
            bool searchresult = context.Players.Any(x => x.PlayerName == usernameInput);

            if(searchresult)
            {
                int usernameID = GetUserID(context, usernameInput);
                PrintLevelsFromScoreList();
            }
        }

        //Look if the users ID exists.
        private static int GetUserID(ScoreContext context, string username)
        {
            var getID = context.Players.Where(u => u.PlayerName == username).FirstOrDefault();
            return getID.PlayerID;
        }

        //Shows you the current level list 
        private static void PrintLevelsFromScoreList(ScoreContext context)
        {
            Console.WriteLine();
            Console.WriteLine("List of Levels: ");
            foreach(var item in context.Scores)
            {
                Console.WriteLine($"Name of level: {item.Level.NameOfLevel} ID: {item.Level.LevelID}, Max of birds: {item.Level.NumberOfBirds}\nHighscore: {item.Player.PlayerName} : {item.Highscore}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }


    }
}
