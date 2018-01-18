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

        //Print the existing data in the DB tables
        private static void PrintDBTables(ScoreContext context)
        {
            Console.WriteLine("Players: ");
            foreach(var item in context.Players)
            {
                Console.WriteLine($"PlayerID: {item.PlayerID}, Username: {item.PlayerName}");
            }
            Console.WriteLine();
            Console.WriteLine("Levels: ");
            foreach(var item in context.Levels)
            {
                Console.WriteLine($"LevelID: {item.LevelID}, Name of level: {item.NameOfLevel}, Max birds: {item.NumberOfBirds}");
            }
            Console.WriteLine();
            Console.WriteLine("Score: ");
            foreach(var item in context.Scores)
            {
                Console.WriteLine($"Player: {item.Player.PlayerName}, Level: {item.Level.NameOfLevel}, Highscore: {item.Highscore}");
            }
        }

        //Search the DB if the user exists, otherwise add the user
        private static void SearchOrAdd(ScoreContext context)
        {
            bool searchOrAddLoop = true;
            Console.WriteLine("Enter username to see information, or enter a new username to add a new player.");
            string inputString = Console.ReadLine();
            bool result = context.Players.Any(x => x.PlayerName == inputString);

            while(searchOrAddLoop)
            {
                if(result)
                {
                    var search = from s in context.Scores
                                 where s.Player.PlayerName.Equals(inputString)
                                 select s;
                    bool hasNoScore = context.Scores.Any(x => x.Player.PlayerName == inputString);

                    if(hasNoScore)
                    {
                        var search2 = (from s in context.Scores
                                       where s.Player.PlayerName.Equals(inputString)
                                       select s.Highscore).Sum();
                        Console.Clear();

                        Console.WriteLine($"Score table for {inputString}:\n");

                        foreach(var item in search)
                        {
                            Console.WriteLine($"Level: {item.Level.NameOfLevel} \nMoves made: {item.Highscore}" + $"({item.Level.NumberOfBirds - item.Highscore} move(s) left)");
                            Console.WriteLine();
                        }
                        Console.WriteLine("Total moves made: " + search2);

                    }
                    else
                    {
                        Console.WriteLine($"Score table for {inputString}: ");
                        Console.WriteLine($"\nUser {inputString} has no current score.");
                    }

                    Console.ReadKey();
                    Console.Clear();
                    searchOrAddLoop = false;
                }
                else
                {
                    Console.WriteLine("Username does not exist. Do you want to add it to the database?");
                    Console.WriteLine("---- Y/N? ----");
                    string useranswer = Console.ReadLine().ToUpper();
                    if(useranswer == "Y")
                    {
                        context.Players.Add(new Player { PlayerName = inputString });
                        context.SaveChanges();
                        Console.Clear();
                        searchOrAddLoop = false;
                    }
                    else if (useranswer == "N")
                    {
                        Console.Clear();
                        searchOrAddLoop = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, try again.");
                    }
                }
            }
        }


        //Function to update a users score on a level.
        private static void UpdateScore(ScoreContext context)
        {
            Console.Clear();
            Console.WriteLine("Type the Username you want to update: ");
            string usernameInput = Console.ReadLine();
            bool searchresult = context.Players.Any(x => x.PlayerName == usernameInput);

            if(searchresult)
            {
                int usernameID = GetUserID(context, usernameInput);
                PrintLevelsFromScoreList(context);

                int levelIDInput;
                while(true)
                {
                    Console.WriteLine("Enter LevelID: ");
                    if (int.TryParse(Console.ReadLine(), out levelIDInput))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Failed, try again");
                    }
                }
                bool levelResult = context.Levels.Any(x => x.LevelID == levelIDInput);

                if(levelResult)
                {
                    Console.WriteLine($"Enter the new score for player: {usernameInput} on LevelID: {levelIDInput}");
                    int highscoreInputInt;
                    while(true)
                    {
                        if(int.TryParse(Console.ReadLine(), out highscoreInputInt))
                        {
                            var getMaxBirds = context.Levels.Where(l => l.LevelID == levelIDInput).FirstOrDefault();

                            if(highscoreInputInt > getMaxBirds.NumberOfBirds || highscoreInputInt < 0)
                            {
                                Console.WriteLine("Input is too large, check max allowed score.");
                            }
                            else
                            {
                                break;
                            }

                        }
                        else
                        {
                            Console.WriteLine("Incorrect input.");
                        }
                    }
                    bool CheckForUserScore = context.Scores.Any(s => s.Player.PlayerID == usernameID && s.Level.LevelID == levelIDInput);

                    if(CheckForUserScore)
                    {
                        var userHighScore = context.Scores
                            .Where(s => s.Player.PlayerID == usernameID && s.Level.LevelID == levelIDInput)
                            .FirstOrDefault();
                        userHighScore.Highscore = highscoreInputInt;
                        context.SaveChanges();

                        Console.WriteLine($"{userHighScore.Player.PlayerName.ToUpper()}'s score updated on {userHighScore.Level.NameOfLevel.ToUpper()} with highscore {userHighScore.Highscore}");
                        Console.ReadLine();
                    }
                    else
                    {
                        var GetUsername = context.Players
                            .Where(u => u.PlayerID == usernameID)
                            .FirstOrDefault();
                        var GetLevelName = context.Levels
                            .Where(l => l.LevelID == levelIDInput)
                            .FirstOrDefault();

                        context.Scores.Add(new Score { Player = GetUsername, Level = GetLevelName, Highscore = highscoreInputInt });
                        context.SaveChanges();

                        Console.WriteLine($"New score added for user {GetUsername.PlayerName.ToUpper()} on {GetLevelName.NameOfLevel.ToUpper()}, highscore: {highscoreInputInt}.");
                        Console.ReadLine();
                    }

                }
                else
                {
                    Console.WriteLine("Level ID does not exist.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Username does not exit.");
                Console.ReadLine();
                Console.Clear();
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

        //Adding a new level to the database
        private static void AddLevel(ScoreContext context)
        {
            bool checkBirdLoop = true;

            Console.WriteLine("Enter name of level: ");
            string newLevel = Console.ReadLine();
            while (checkBirdLoop)
            {
                Console.WriteLine("Enter the amount of birds the level has (1 - 15");
                Console.Write("Number: ");
                string birdAmount = Console.ReadLine();
                int birdAmountValidNumber;
                int.TryParse(birdAmount, out birdAmountValidNumber);

                if(birdAmountValidNumber <= 15 && birdAmountValidNumber >= 1)
                {
                    context.Levels.Add(new Level { NameOfLevel = newLevel, NumberOfBirds = birdAmountValidNumber });
                    context.SaveChanges();
                    Console.Clear();
                    checkBirdLoop = false;
                }
                else
                {
                    Console.WriteLine("Please enter amount of birds between 1 - 15.");
                }
            }

        }


    }
}
