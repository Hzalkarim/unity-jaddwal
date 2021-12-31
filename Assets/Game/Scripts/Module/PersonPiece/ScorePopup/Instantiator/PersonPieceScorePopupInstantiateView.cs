using Agate.MVC.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.PersonPiece.ScorePopup
{
    public class PersonPieceScorePopupInstantiateView : BaseView
    {
        [SerializeField]
        private PersonPieceScorePopupInstantiateViewData _data;
        public PersonPieceScorePopupInstantiateViewData Data => _data;
    }

    [Serializable]
    public class PersonPieceScorePopupInstantiateViewData
    {
        public PersonPieceScorePopupView Prefab;
        public Transform Parent;
    }
}
