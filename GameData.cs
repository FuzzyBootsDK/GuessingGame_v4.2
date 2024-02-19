using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;

namespace GuessingGame_v4._2
{
    internal class GameData
    {
        private int _difficultyLevel = 1;
        private string _difficultyLevelName = string.Empty;
        private int _numberOfCorrectGuesses = 0;
        private int _difficultyCalcMinValue = 1;
        private int _difficultyCalcMaxValue = 41;
        private int _randomNumber = 1;
        private string _userName = string.Empty;

        private List<SaveDataModel>? _loadSaveData;
        private List<GameDataModel> _data;
        public List<GameDataModel> Data { get => _data; }
        public int RamdomNumber { get => _randomNumber; }
        public string DifficultyLevelName { get => _difficultyLevelName; }
        public int DifficultyLevel { get => _difficultyLevel; }
        
        public GameData(string userName)
        {
            _userName = userName;
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Answers.csv");

            using var reader = new StreamReader(filePath);
            var csvConfig = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
            };
            using var csv = new CsvReader(reader, csvConfig);

            _data = csv.GetRecords<GameDataModel>().ToList();
            LoadGame(_userName);

        }
        
        public void CalculateDifficultyLevel(int numberOfCorrectGuesses)
        {
            _numberOfCorrectGuesses = numberOfCorrectGuesses;
            if (_numberOfCorrectGuesses == 5)
            {
                _difficultyLevel += 1;
                SetDifficultyLevel(_difficultyLevel);
                _numberOfCorrectGuesses = 0;
            }
            else
            {
                SetDifficultyLevel(_difficultyLevel);
            }
        }
        private void SetDifficultyLevel(int difficultyLevel)
        {
            _difficultyLevel = difficultyLevel;
            if (_difficultyLevel == 1)
            {
                //Generate a randomNumber between 1 - 40
                _difficultyLevelName = "Beginner";
                _difficultyCalcMinValue = 1;
                _difficultyCalcMaxValue = 40;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
            else if (_difficultyLevel == 2)
            {
                //Generate a randomNumber between 41 - 76
                _difficultyLevelName = "Moderate";
                _difficultyCalcMinValue = 41;
                _difficultyCalcMaxValue = 76;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
            else if (_difficultyLevel == 3)
            {
                //Generate a randomNumber between 77 - 117
                _difficultyLevelName = "Advanced";
                _difficultyCalcMinValue = 77;
                _difficultyCalcMaxValue = 117;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
            else if (_difficultyLevel == 4)
            {
                //Generate a randomNumber between 118 - 156
                _difficultyLevelName = "Skilled";
                _difficultyCalcMinValue = 118;
                _difficultyCalcMaxValue = 156;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
            else if (_difficultyLevel == 5)
            {
                //Generate a randomNumber between 157 - 196
                _difficultyLevelName = "Hardcore";
                _difficultyCalcMinValue = 157;
                _difficultyCalcMaxValue = 196;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
            else if (_difficultyLevel == 6)
            {
                //Generate a randomNumber between 197 - 234
                _difficultyLevelName = "Nightmare";
                _difficultyCalcMinValue = 197;
                _difficultyCalcMaxValue = 234;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
            else
            {
                //Generate a randomNumber between 235 - 300
                _difficultyLevelName = "Legendary";
                _difficultyCalcMinValue = 235;
                _difficultyCalcMaxValue = 300;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
        }
        private void LoadGame(string userName)
        {
            string loadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SaveGame.csv");

            // Check if the file exists before attempting to load it
            if (File.Exists(loadFilePath))
            {
                using var reader = new StreamReader(loadFilePath);
                var csvConfig = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    HasHeaderRecord = true,
                };

                using var loadSaveGame = new CsvReader(reader, csvConfig);
                _loadSaveData = loadSaveGame.GetRecords<SaveDataModel>().ToList();

                foreach (var item in _loadSaveData)
                {
                    if (item.UserName == userName)
                    {
                        _difficultyLevel = (int)item.DifficultyLevel;
                        SetDifficultyLevel(_difficultyLevel);
                        return; // Exit the loop if the user is found
                    }
                }

                // Handle the case where the specified user is not found in the file
                Console.WriteLine($"User '{userName}' not found in SaveGame.csv");
            }
            else
            {
                // Handle the case where the file doesn't exist
                Console.WriteLine("SaveGame.csv does not exist. Creating the file with header.");

                // Create the file with the header
                using var writer = new StreamWriter(loadFilePath);
                var csvConfig = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    HasHeaderRecord = true,
                };
                using var saveGame = new CsvWriter(writer, csvConfig);

                saveGame.WriteField("UserName");
                saveGame.WriteField("DifficultyLevel");
                saveGame.NextRecord();

                _loadSaveData = new List<SaveDataModel>();
            }
        }
        public void SaveGame(string userName, int difficultyLevel)
        {
            List<SaveDataModel> data = new List<SaveDataModel>();
            List<SaveDataModel> existingData;

            data.Add(new SaveDataModel { UserName = userName, DifficultyLevel = difficultyLevel });

            string saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SaveGame.csv");

            var csvConfig = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
            };

            if (File.Exists(saveFilePath))
            {
                // If the file already exists, read existing data
                using var reader = new StreamReader(saveFilePath);
                using var csv = new CsvReader(reader, csvConfig);
                existingData = csv.GetRecords<SaveDataModel>().ToList();
            }
            else
            {
                // If the file doesn't exist, create it with the header
                StreamWriter streamWriter = new StreamWriter(saveFilePath);
                using var writerSave = streamWriter;
                using var saveGameWriter = new CsvWriter(writerSave, csvConfig);

                saveGameWriter.WriteField("UserName");
                saveGameWriter.WriteField("DifficultyLevel");
                saveGameWriter.NextRecord();

                existingData = new List<SaveDataModel>();
            }

            // Add the new data to the existing data
            existingData.Add(new SaveDataModel { UserName = userName, DifficultyLevel = difficultyLevel });

            // Write all data (including the new one) back to the file
            using var writer = new StreamWriter(saveFilePath);
            using var saveGame = new CsvWriter(writer, csvConfig);

            saveGame.WriteRecords(existingData);
        }
    }
}
