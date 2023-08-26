using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame_v4._2
{
    internal class LogGamePlay
    {
        private string? _userName;
        public string? UserName { get => _userName; }
        private string _filePath;
        public string FilePath { get => _filePath; }
        public LogGamePlay()
        {

            _userName = GetUserName();
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), $"GamePlayLog_{_userName}.txt");
        }
        public string CreateFile()
        {
            //Checks if the file already exists
            if (File.Exists(_filePath))
            {
                Console.WriteLine($"File already exists at {_filePath}");
            }
            else
            {
                using (FileStream fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
                {
                    //Creates a new Empty text file
                    Console.WriteLine($"File Created {_filePath}");
                }
            }
            return _filePath;
        }
        public void WriteGuess(string guess, string answer, string filePath)
        {
            filePath = _filePath;
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string lineToWrite = $"{date} -- Answer - {answer}: Guess - {guess}";
            if (guess == "" && answer == "")
            {
                lineToWrite = "******************************************************************************************";
            }

            using (FileStream fs = new FileStream(_filePath, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(lineToWrite);
                }
            }
        }

        private string GetUserName()
        {
            Console.WriteLine("Please enter your name:");
            string userName = Console.ReadLine();
            if (userName == null)
            {
                userName = "Guest";
            }
            _userName = userName;
            return _userName;
        }
    }
}
