using QuestBase.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGG.SceneManagement
{
    public class MenuInitData : SceneInitData
    {
        public int DefaultStageId { get; private set; }
        public bool TutorialMode { get; private set; }
        public int TutorialStep { get; private set; }

        public MenuInitData(int defaultStageId = -1, bool tutorialMode = false, int tutorialStep = 0)
        {
            this.DefaultStageId = defaultStageId;
            this.TutorialMode = tutorialMode;
            this.TutorialStep = tutorialStep;
        }
    }
}
