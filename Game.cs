using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame_v4._2
{
    internal class Game
    {
        private LogGamePlay _logGamePlay;
        private string _category;
        private string _answer;
        private string? _guess;
        private string _hint1;
        private string _hint2;
        private int _numGuesses;
        private int _maxGuesses;
        private bool _gameOver;
        private int _lengthOfAnswer;
        private char _firstLetterOfAnswer;
        private int _whenToGetHint1;
        private int _whenToGetHint2;
        private string _hint1Recieved;
        private string _hint2Recieved;
        private bool _playAgain;
        private string? _userName;
        private string _filePath;
        private int _NumberOfCorrectGuesses;
        private string _difficultyName;
        private int _difficultyLevel;
        public int NumberOfCorrectGuesses { get => _NumberOfCorrectGuesses; }
        public bool PlayAgain { get => _playAgain; }
        public string? UserName { get => _userName; }
        public string FilePath { get => _filePath; }


        public Game()
        {
            _logGamePlay = new LogGamePlay();
            _logGamePlay.CreateFile();
            _category = "";
            _answer = "";
            _guess = "";
            _hint1 = "";
            _hint2 = "";
            _numGuesses = 0;
            _maxGuesses = 0;
            _gameOver = false;
            _lengthOfAnswer = 0;
            _firstLetterOfAnswer = ' ';
            _hint1Recieved = "";
            _hint2Recieved = "";
            _playAgain = true;
            _userName = _logGamePlay.UserName;
            _filePath = _logGamePlay.FilePath;
            _NumberOfCorrectGuesses = 0;
            _difficultyLevel = 0;
            _difficultyName = "";
        }
        public void WelcomePlayer()
        {
            //Console.WriteLine("***************************************************************************************************************************");
            Console.WriteLine("Welcome to the Guessing Game");
            Console.WriteLine("You will be given a category the lenght and the first letter.");
            Console.WriteLine("You will then have to guess the correct answer");
            Console.WriteLine("Good Luck!");
            Console.WriteLine();
            Console.WriteLine("***************************************************************************************************************************");
            Console.WriteLine("When you are ready to start press any key!");
            Console.ReadKey();
        }
        public void Play(int maxGuesses, string filePath, string userName, string category, string answer, string hint1, string hint2, int difficultyLevel, string difficultyName)
        {
            //set all the variables
            _maxGuesses = maxGuesses;
            _filePath = filePath;
            _userName = userName;
            _category = category;
            _answer = answer;
            _hint1 = hint1;
            _hint2 = hint2;
            _difficultyLevel = difficultyLevel;
            _difficultyName = difficultyName;
            _whenToGetHint1 = _maxGuesses / 3;
            _whenToGetHint2 = _maxGuesses / 2;
            _lengthOfAnswer = _answer.Length;
            _firstLetterOfAnswer = _answer[0];
            _NumberOfCorrectGuesses = 0;
            _logGamePlay.WriteGuess("", "", _filePath);

            _gameOver = false;
            _numGuesses = 0;
            _maxGuesses = maxGuesses;
            _hint1Recieved = "";
            _hint2Recieved = "";
            _guess = "";

            while (_gameOver == false && _numGuesses < _maxGuesses)
            {
                if (_numGuesses >= _whenToGetHint1)
                {
                    _hint1Recieved = _hint1;
                }
                if (_numGuesses >= _whenToGetHint2)
                {
                    _hint2Recieved = _hint2;
                }
                Console.Clear();
                logo();
                Console.WriteLine("***************************************************************************************************************************");
                Console.WriteLine($"You are playing at {difficultyLevel} - {difficultyName} difficulty");
                Console.WriteLine($"You have {_maxGuesses - _numGuesses} guesses left");
                Console.WriteLine($"Your last guess was: {_guess}");
                Console.WriteLine($"Hints recieved: {_hint1Recieved}  --  {_hint2Recieved}");
                Console.WriteLine("***************************************************************************************************************************");
                Console.WriteLine($"The Category is {_category}, and the lenth of {_category} is {_lengthOfAnswer}, the first letter is {_firstLetterOfAnswer}");
                Console.WriteLine("***************************************************************************************************************************");
                Console.WriteLine("What is your guess?");
                _guess = Console.ReadLine();
                _logGamePlay.WriteGuess(_guess, _answer, _filePath);
                _numGuesses += 1;
                IsGameOver();
            }
        }
        private void IsGameOver()
        {
            if (_guess == _answer)
            {
                _gameOver = true;
                Console.WriteLine($"Congratulations... You have guessed correctly.");
                Console.WriteLine("Press Any key to continue");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Do you wish to play again?");
                Console.WriteLine("Press Y for Yes or N for No");
                _playAgain = Console.ReadLine().ToUpper() == "Y" ? true : false;
                Console.Clear();
                _NumberOfCorrectGuesses += 1;
            }
            else if (_numGuesses >= _maxGuesses)
            {
                _gameOver = true;
                Console.WriteLine($"Sorry... You have run out of guesses.");
                Console.WriteLine($"The answer was {_answer}");
                Console.WriteLine("Press Any key to continue");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Do you wish to play again?");
                Console.WriteLine("Press Y for Yes or N for No");
                _playAgain = Console.ReadLine().ToUpper() == "Y" ? true : false;
                Console.Clear();
            }
        }
        public static void logo()
        {
            Console.WriteLine("  /$$$$$$                                         /$$                            /$$$$$$                                   ");
            Console.WriteLine(" /$$__  $$                                       |__/                           /$$__  $$                                  ");
            Console.WriteLine("| $$  \\__/ /$$   /$$  /$$$$$$   /$$$$$$$ /$$$$$$$ /$$ /$$$$$$$   /$$$$$$       | $$  \\__/  /$$$$$$  /$$$$$$/$$$$   /$$$$$$ ");
            Console.WriteLine("| $$ /$$$$| $$  | $$ /$$__  $$ /$$_____//$$_____/| $$| $$__  $$ /$$__  $$      | $$ /$$$$ |____  $$| $$_  $$_  $$ /$$__  $$");
            Console.WriteLine("| $$|_  $$| $$  | $$| $$$$$$$$|  $$$$$$|  $$$$$$ | $$| $$  \\ $$| $$  \\ $$      | $$|_  $$  /$$$$$$$| $$ \\ $$ \\ $$| $$$$$$$$");
            Console.WriteLine("| $$  \\ $$| $$  | $$| $$_____/ \\____  $$\\____  $$| $$| $$  | $$| $$  | $$      | $$  \\ $$ /$$__  $$| $$ | $$ | $$| $$_____/");
            Console.WriteLine("|  $$$$$$/|  $$$$$$/|  $$$$$$$ /$$$$$$$//$$$$$$$/| $$| $$  | $$|  $$$$$$$      |  $$$$$$/|  $$$$$$$| $$ | $$ | $$|  $$$$$$$");
            Console.WriteLine(" \\______/  \\______/  \\_______/|_______/|_______/ |__/|__/  |__/ \\____  $$       \\______/  \\_______/|__/ |__/ |__/ \\_______/");
            Console.WriteLine("                                                                /$$  \\ $$                                                  ");
            Console.WriteLine("                                                               |  $$$$$$/                                                  ");
            Console.WriteLine("                                                                \\______/                                                   ");
            Console.WriteLine();
            Console.WriteLine("***************************************************************************************************************************");
            Console.WriteLine();
        }
    }
}
