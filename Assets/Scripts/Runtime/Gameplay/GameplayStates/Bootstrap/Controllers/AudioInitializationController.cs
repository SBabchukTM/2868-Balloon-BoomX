﻿using System.Collections.Generic;
using System.Threading;
using Core;
using Core.Services.Audio;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.HelperServices.UserData;
using UnityEngine;
using AudioType = Core.Services.Audio.AudioType;

namespace Runtime.Gameplay.GameplayStates.Bootstrap.Controllers
{
    public class AudioInitializationController : BaseController
    {
        private readonly IAudioService _audioService;
        private readonly ISettingProvider _staticSettingsService;
        private readonly UserDataService _userDataService;

        private CancellationTokenSource _cancellationTokenSource;

        public AudioInitializationController(IAudioService audioService, ISettingProvider staticSettingsService, UserDataService userDataService)
        {
            _audioService = audioService;
            _staticSettingsService = staticSettingsService;
            _userDataService = userDataService;
        }

        public override UniTask Run(CancellationToken cancellationToken)
        {
            base.Run(cancellationToken);

            _cancellationTokenSource = new CancellationTokenSource();
            var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cancellationTokenSource.Token);

            var soundVolume = _userDataService.GetUserData().SettingsData.SoundVolume;
            _audioService.SetVolume(AudioType.Sound, soundVolume);

            var musicVolume = _userDataService.GetUserData().SettingsData.MusicVolume;
            _audioService.SetVolume(AudioType.Music, musicVolume);
            
            PlayMusic(linkedTokenSource.Token).Forget();
            return UniTask.CompletedTask;
        }

        public override UniTask Stop()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            return base.Stop();
        }

        private async UniTask PlayMusic(CancellationToken cancellationToken)
        {
            var audioSettings = _staticSettingsService.Get<AudioConfig>();
            var allMusicAudioData = audioSettings.Audio.FindAll(x => x.AudioType == AudioType.Music);
            var allMusicClips = new List<AudioClip>(allMusicAudioData.Count);

            foreach (var audioData in allMusicAudioData)
                allMusicClips.Add(audioData.Clip);

            var clipsCount = allMusicClips.Count;

            var clipIndex = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                var clipDuration = (int)allMusicClips[clipIndex].length * 1000 + 1000;
                _audioService.PlayMusic(allMusicClips[clipIndex]);
                await UniTask.Delay(clipDuration, cancellationToken: cancellationToken);
                clipIndex++;
                if (clipIndex >= clipsCount)
                    clipIndex = 0;
            }
        }
    }
}