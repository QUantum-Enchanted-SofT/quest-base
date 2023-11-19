using QuestBase.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase
{
    public enum BgmType
    {
        PlayTheFox,
    }

    public enum SeType
    {
        Select,
    }

    public class SoundDefinitions
    {
        public const string SoundPlayerPrefabPath = "Sound/SoundPlayer";
        public const string BgmClipTablePath = "Sound/BgmAudioClipTable";
        public const string SeClipTablePath = "Sound/SeAudioClipTable";
        public const string BgmDataTablePath = "Sound/BgmDataTable";
        public const string SeDataTablePath = "Sound/SeDataTable";

        public const string AudioMixerMasterVolumeKey = "MasterVolume";
        public const string AudioMixerBgmVolumeKey = "BGMVolume";
        public const string AudioMixerSeVolumeKey = "SEVolume";
    }
}
