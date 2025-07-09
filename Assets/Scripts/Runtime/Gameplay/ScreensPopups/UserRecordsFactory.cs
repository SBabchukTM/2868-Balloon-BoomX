using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Factory;
using Runtime.Gameplay.HelperServices.SettingsProvider;
using Runtime.Gameplay.HelperServices.UserData;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.ScreensPopups
{
    public class UserRecordsFactory : IInitializable
    {
        private readonly UserDataService _userDataService;
        private readonly IAssetProvider _assetProvider;
        private readonly GameObjectFactory _gameObjectFactory;

        private GameObject _recordPrefab;
    
        public UserRecordsFactory(UserDataService userDataService, IAssetProvider assetProvider,
            GameObjectFactory gameObjectFactory)
        {
            _userDataService = userDataService;
            _assetProvider = assetProvider;
            _gameObjectFactory = gameObjectFactory;
        }
    
        public async void Initialize()
        {
            _recordPrefab = await _assetProvider.Load<GameObject>(ConstPrefabs.UserRecordDisplayPrefab);
        }

        public List<UserRecordDisplay> CreateRecordDisplayList()
        {
            var recordsData = CreateRecordsDataList();
        
            List<UserRecordDisplay> result = new(recordsData.Count);
        
            for (int i = 0; i < recordsData.Count; i++)
            {
                var display = _gameObjectFactory.Create<UserRecordDisplay>(_recordPrefab);
                display.Initialize(i + 1, recordsData[i].Name, recordsData[i].Time);
                result.Add(display);
            }
        
            return result;
        }

        private List<RecordData> CreateRecordsDataList()
        {
            var records = CreateFakeRecords();

            var usedData = _userDataService.GetUserData();
            var userRecord = new RecordData(usedData.UserAccountData.Username, usedData.UserProgressData.BestTime);
            records.Add(userRecord);

            records = records.OrderBy(x => x.Time == 0).ThenBy(x => x.Time).ToList();
            return records;
        }

        private List<RecordData> CreateFakeRecords() => new()
        {
            new RecordData("Marta", 38),
            new RecordData("John", 38),
            new RecordData("Michael", 39),
            new RecordData("Micah", 40),
            new RecordData("Sandy", 42),
            new RecordData("Moe", 45),
            new RecordData("Dorothy", 47),
            new RecordData("Lisa", 50),
            new RecordData("Arthur", 51),
            new RecordData("Mike", 55),
            new RecordData("Luke", 55),
            new RecordData("Mona", 60),
            new RecordData("Dan", 62),
            new RecordData("Mob", 65),
            new RecordData("Ivan", 66),
            new RecordData("Lucy", 68),
            new RecordData("Bill", 70),
            new RecordData("Sophie", 77),
            new RecordData("Lamar", 80),
            new RecordData("Daniel", 89),
        };
    
        private class RecordData
        {
            public string Name;
            public int Time;

            public RecordData(string name, int time)
            {
                Name = name;
                Time = time;
            }
        }
    }
}
