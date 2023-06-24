using QuestBase.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGG.UI
{
    public class UICommonDialog : UIDialogBase
    {
        private UICommonDialogView view = null;

        public void Init(LangTextLabel titleTextLabel, LangTextLabel bodyTextLabel, Action onClickOK = null, Action onClickNo = null)
        {
            view = viewObj.GetComponent<UICommonDialogView>();
            view.TitleText.SetText(titleTextLabel);
            view.BodyText.SetText(bodyTextLabel);
            view.OkButton.onClick.AddListener(() => onClickOK?.Invoke());
            view.NoButton.onClick.AddListener(() => onClickNo?.Invoke());
        }

        public void Init(string titleText, string bodyText, Action onClickOk = null, Action onClickNo = null)
        {
            view = viewObj.GetComponent<UICommonDialogView>();
            view.TitleText.SetText(titleText);
            view.BodyText.SetText(bodyText);
            view.OkButton.onClick.AddListener(() => onClickOk?.Invoke());
            view.NoButton.onClick.AddListener(() => onClickNo?.Invoke());

            if (onClickOk == null)
            {
                view.OkButton.gameObject.SetActive(false);
            }

            if (onClickNo == null)
            {
                view.NoButton.gameObject.SetActive(false);
            }
        }

        protected override IEnumerator OnBeforeOpen()
        {
            yield break;
        }

        protected override IEnumerator OnAfterOpen()
        {
            view.OkButton.Select();
            yield break;
        }

        public override void Dispose()
        {
        }
    }
}
