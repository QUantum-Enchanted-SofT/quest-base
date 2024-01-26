using QuestBase.Data.SO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestBase.Sound
{
    public class SoundPlayer
    {
        public static SoundPlayer Instance { get; private set; } = new SoundPlayer();

        private SoundPlayerView view = null;

        private Dictionary<BgmType, AudioClip> bgmClips = null;
        private Dictionary<SeType, AudioClip> seClips = null;

        private BgmDataTable bgmDataTable = null;
        private SeDataTable seDataTable = null;

        public AudioSource BGMAudioSource => this.view.BgmAudioSource;

        private SoundPlayer()
        {
            var prefab = Resources.Load(SoundDefinitions.SoundPlayerPrefabPath) as GameObject;
            this.view = GameObject.Instantiate(prefab).GetComponent<SoundPlayerView>();
            GameObject.DontDestroyOnLoad(this.view.gameObject);

            var bgmClipTable = Resources.Load(SoundDefinitions.BgmClipTablePath) as BgmAudioClipTable;
            this.bgmClips = new Dictionary<BgmType, AudioClip>();
            foreach (var entity in bgmClipTable.BgmAudioClipList)
            {
                this.bgmClips.Add(entity.BgmType, entity.Clip);
            }

            var seClipTable = Resources.Load(SoundDefinitions.SeClipTablePath) as SeAudioClipTable;
            this.seClips = new Dictionary<SeType, AudioClip>();
            foreach (var entity in seClipTable.SeAudioClipList)
            {
                this.seClips.Add(entity.SeType, entity.Clip);
            }

            this.bgmDataTable = Resources.Load(SoundDefinitions.BgmDataTablePath) as BgmDataTable;
            this.seDataTable = Resources.Load(SoundDefinitions.SeDataTablePath) as SeDataTable;
        }

        public void PlayBGM(BgmType bgmType)
        {
            var clip = this.bgmClips[bgmType];
            this.view.BgmAudioSource.clip = clip;

            if (this.bgmDataTable.TryGetValue(bgmType, out var bgmData))
            {
                this.view.BgmAudioSource.volume = bgmData.Volume;
            }

            this.view.BgmAudioSource.Play();
        }

        public void StopBGM()
        {
            this.view.BgmAudioSource.Stop();
        }

        public void PlaySE(SeType seType)
        {
            var clip = this.seClips[seType];

            if (this.seDataTable.TryGetValue(seType, out var seData))
            {
                this.view.SeAudioSource.PlayOneShot(clip, seData.Volume);
            }
            else
            {
                this.view.SeAudioSource.PlayOneShot(clip);
            }
        }

        public void PlaySE3d(SeType seType, AudioSource audioSource)
        {
            var clip = this.seClips[seType];

            if (this.seDataTable.TryGetValue(seType, out var seData))
            {
                audioSource.PlayOneShot(clip, seData.Volume);
            }
            else
            {
                audioSource.PlayOneShot(clip);
            }
        }

        /// <summary>
        /// マスターボリュームを設定する
        /// </summary>
        /// <param name="volume">0~1の範囲で設定</param>
        public void SetMasterVolume(float volume)
        {
            var vol = (1 - volume) * -80;
            this.view.AudioMixer.SetFloat(SoundDefinitions.AudioMixerMasterVolumeKey, vol);
        }

        /// <summary>
        /// BGMのボリュームを設定する
        /// </summary>
        /// <param name="volume">0~1の範囲で設定</param>
        public void SetBgmVolume(float volume)
        {
            var vol = (1 - volume) * -80;
            this.view.AudioMixer.SetFloat(SoundDefinitions.AudioMixerBgmVolumeKey, vol);
        }

        /// <summary>
        /// BGMのボリュームを設定する
        /// </summary>
        /// <param name="volume">0~1の範囲で設定</param>
        public void SetSeVolume(float volume)
        {
            var vol = (1 - volume) * -80;
            this.view.AudioMixer.SetFloat(SoundDefinitions.AudioMixerSeVolumeKey, vol);
        }
    }
}
