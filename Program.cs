﻿namespace GuessingGame_v4._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new();
            int numberOfCorrectGuesses = 0;
            int difficultyLevel = 0;
            List<string> usedAnswers = new();
            int numberOfUsedAnswers = 0;

            Console.Clear();
            Game.logo();
            string? userName = game.UserName;
            string filePath = game.FilePath;

            game.WelcomePlayer();
            Console.Clear();
            Console.WriteLine("How many guesses would you like to have?");
            Console.WriteLine("A minimum of 7 is recommended.");
            //Sets the number of guesses to 0 and the max guesses to 0
            int maxGuesses = Convert.ToInt32(Console.ReadLine());

            GameData gameData = new(userName);
            while (game.PlayAgain == true)
            {
                if (numberOfCorrectGuesses == 5)
                {
                    usedAnswers.Clear();
                }
                while (gameData.RamdomNumber == 0 || usedAnswers.Contains(gameData.Data[gameData.RamdomNumber].Answer))
                {
                    gameData.CalculateDifficultyLevel(numberOfCorrectGuesses);
                }
                gameData.CalculateDifficultyLevel(numberOfCorrectGuesses);
                GameDataModel gameDataModel = gameData.Data[gameData.RamdomNumber];
                difficultyLevel = gameData.DifficultyLevel;
                game.Play(maxGuesses, filePath, userName, gameDataModel.Category, gameDataModel.Answer, gameDataModel.Hint_1, gameDataModel.Hint_2, gameData.DifficultyLevel, gameData.DifficultyLevelName, numberOfCorrectGuesses);
                numberOfCorrectGuesses += 1;
                usedAnswers.Add(gameDataModel.Answer);
                numberOfUsedAnswers += 1;
            }
            Console.WriteLine("Thanks for playing.");
            gameData.SaveGame(userName, difficultyLevel);
            CopyFile(filePath,userName);
            Console.ReadKey();
        }
        static void CopyFile(string filePath, string userName)
        {
            string currentUserName = Environment.UserName;
            string newFilePath = $"C:\\Users\\{currentUserName}\\Desktop\\GamePlayLog_{userName}.txt";
            File.Copy(filePath, newFilePath, true);
            Console.WriteLine($"The log file is moved to {newFilePath}");
        }
    }
}