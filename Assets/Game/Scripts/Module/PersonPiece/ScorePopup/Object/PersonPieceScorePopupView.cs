using Agate.MVC.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jaddwal.PersonPiece.ScorePopup
{
    public class PersonPieceScorePopupView : ObjectView<IPersonPieceScorePopupModel>
    {

        public TextMeshProUGUI score;
        public GameObject isMax;

        [SerializeField]
        private Animator _anim;
        public Animator Anim => _anim;

        public event Action OnHideEndedEvent;

        protected override void InitRenderModel(IPersonPieceScorePopupModel model)
        {
            
        }

        protected override void UpdateRenderModel(IPersonPieceScorePopupModel model)
        {
            score.text = model.Score.ToString();
            isMax.SetActive(model.IsMax);
        }

        public void OnHideEnded()
        {
            OnHideEndedEvent?.Invoke();
        }
    }

}
