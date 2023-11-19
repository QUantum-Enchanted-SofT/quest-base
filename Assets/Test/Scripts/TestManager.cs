using QuestBase.Sound;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    private async void Awake()
    {
        SoundPlayer.Instance.PlayBGM(QuestBase.BgmType.PlayTheFox);

        await Task.Delay(3);

        SoundPlayer.Instance.PlaySE(QuestBase.SeType.Select);
    }
}
