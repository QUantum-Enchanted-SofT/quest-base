using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace QuestBase.Sound
{
    public class SoundPlayerView : MonoBehaviour
    {
        [SerializeField]
        private AudioSource bgmAudioSource = null;

        [SerializeField]
        private AudioSource seAudioSource = null;

        [SerializeField]
        private AudioMixer audioMixer = null;

        public AudioSource BgmAudioSource => this.bgmAudioSource;
        public AudioSource SeAudioSource => this.seAudioSource;
        public AudioMixer AudioMixer => this.audioMixer;
    }
}
