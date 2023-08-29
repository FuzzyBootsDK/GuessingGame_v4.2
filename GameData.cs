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
        private int _difficultyCalcMaxValue = 21;
        private int _randomNumber = 1;
        private string _userName = string.Empty;

        private List<SaveDataModel> _loadSaveData;
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
                _difficultyLevel = _difficultyLevel;
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
                //Generate a randomNumber between 41 - 80
                _difficultyLevelName = "Moderate";
                _difficultyCalcMinValue = 41;
                _difficultyCalcMaxValue = 80;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
            else if (_difficultyLevel == 3)
            {
                //Generate a randomNumber between 81 - 120
                _difficultyLevelName = "Advanced";
                _difficultyCalcMinValue = 81;
                _difficultyCalcMaxValue = 120;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
            else if (_difficultyLevel == 4)
            {
                //Generate a randomNumber between 121 - 160
                _difficultyLevelName = "Skilled";
                _difficultyCalcMinValue = 121;
                _difficultyCalcMaxValue = 160;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
            else if (_difficultyLevel == 5)
            {
                //Generate a randomNumber between 161 - 200
                _difficultyLevelName = "Hardcore";
                _difficultyCalcMinValue = 161;
                _difficultyCalcMaxValue = 200;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
            else if (_difficultyLevel == 6)
            {
                //Generate a randomNumber between 201 - 240
                _difficultyLevelName = "Nightmare";
                _difficultyCalcMinValue = 201;
                _difficultyCalcMaxValue = 240;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
            else
            {
                //Generate a randomNumber between 241 - 300
                _difficultyLevelName = "Legendary";
                _difficultyCalcMinValue = 241;
                _difficultyCalcMaxValue = 300;
                _randomNumber = RandomNumber.NewRandomNumber(_difficultyCalcMinValue, _difficultyCalcMaxValue);
            }
        }
        private  void LoadGame(string userName)
        {
            string loadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SaveGame.csv");
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
                    _difficultyLevel = item.DifficultyLevel;
                    SetDifficultyLevel(_difficultyLevel);
                }
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
                using var reader = new StreamReader(saveFilePath);
                using var csv = new CsvReader(reader, csvConfig);
                existingData = csv.GetRecords<SaveDataModel>().ToList();
            }
            else
            {
                existingData = new List<SaveDataModel>();
            }
            existingData.Add(new SaveDataModel { UserName = userName, DifficultyLevel = difficultyLevel });
            using var writer = new StreamWriter(saveFilePath);

            using var saveGame = new CsvWriter(writer, csvConfig);

            saveGame.WriteRecords(existingData);
        }
    }
}
