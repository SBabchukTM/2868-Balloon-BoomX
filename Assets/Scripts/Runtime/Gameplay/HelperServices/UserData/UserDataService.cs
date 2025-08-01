﻿using Core;
using Core.Compressor;
using Runtime.Gameplay.HelperServices.Path;

namespace Runtime.Gameplay.HelperServices.UserData
{
    public class UserDataService
    {
        private readonly IPersistentDataProvider _persistentDataProvider;
        private readonly BaseCompressor _compressor;

        private Data.UserData _userData;

        public UserDataService(IPersistentDataProvider persistentDataProvider, BaseCompressor compressor)
        {
            _persistentDataProvider = persistentDataProvider;
            _compressor = compressor;
        }

        public void Initialize()
        {
#if DEV
            _userData = _persistentDataProvider.Load<Data.UserData>(ConstDataPath.UserDataPath, ConstDataPath.UserDataFileName) ?? new Data.UserData();
#else
            _userData = _persistentDataProvider.Load<UserData>(ConstDataPath.UserDataPath, ConstDataPath.UserDataFileName,null, _compressor) ?? new UserData();
#endif
        }

        public Data.UserData GetUserData()
        {
            return _userData;
        }
        
        public void SaveUserData()
        {
            if(_userData == null)
                return;

#if DEV
            _persistentDataProvider.Save(_userData, ConstDataPath.UserDataPath, ConstDataPath.UserDataFileName);
#else
            _persistentDataProvider.Save(_userData, ConstDataPath.UserDataPath, ConstDataPath.UserDataFileName, null, _compressor);
#endif
        }
    }
}