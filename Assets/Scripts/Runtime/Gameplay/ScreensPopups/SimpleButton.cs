﻿using Core.Services.Audio;
using Runtime.Gameplay.HelperServices.Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

namespace Runtime.Gameplay.ScreensPopups
{
    [RequireComponent(typeof(Animation), typeof(Button))]
    public class SimpleButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Animation _pressAnimation;

        private IAudioService _audioService;
        public Button Button => _button;

#if UNITY_EDITOR
        private void Reset()
        {
            _button = GetComponent<Button>();
            _pressAnimation = GetComponent<Animation>();

            UnityEventTools.AddPersistentListener(_button.onClick, PlayPressAnimation); 
            _pressAnimation.playAutomatically = false;

            _pressAnimation.clip = Resources.Load<AnimationClip>("ButtonClickAnim");
            _pressAnimation.AddClip(Resources.Load<AnimationClip>("ButtonClickAnim"), "ButtonClickAnim");
        }
#endif

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public void PlayPressAnimation()
        {
            _pressAnimation.Play();
            _audioService.PlaySound(ConstAudio.PressButtonSound);
        }
    }
}