using QuestBase.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SGG.UI
{
    public class UICommonDialogView : MonoBehaviour
    {
        [SerializeField]
        private UIText titleText = null;

        [SerializeField]
        private UIText bodyText = null;

        [SerializeField]
        private Button okButton = null;

        [SerializeField]
        private Button noButton = null;

        public UIText TitleText => titleText;
        public UIText BodyText => bodyText;
        public Button OkButton => okButton;
        public Button NoButton => noButton;
    }
}
