using UnityEngine;

namespace Core.Services.Audio
{
    public interface IAudioService
    {
        void PlayMusic(AudioClip clip);
        void PlaySound(string clipId);
        void SetVolume(AudioType audioType, float volume);
    }
}