using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.UI
{
    public class UIWindowSettings : SingletonMonoBehaviour<UIWindowSettings>
    {
        protected override bool dontDestroyOnLoad => false;

        [SerializeField]
        private Canvas dialogCanvas = null;

        [SerializeField]
        private Canvas modalWindowCanvas = null;

        public Canvas DialogCanvas => this.dialogCanvas;
        public Canvas ModalWindowCanvas => this.modalWindowCanvas;
    }
}
