using Agate.MVC.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.PersonPiece.AI
{
    public class PersonPieceAIView : BaseView
    {
        [SerializeField]
        private PersonPieceAIViewData _data;
        public PersonPieceAIViewData Data => _data;

        public void SetShowScore(bool value)
        {
            _data.ShowScore = value;
        }
    }

    [Serializable]
    public class PersonPieceAIViewData
    {
        public bool ShowScore;
    }

}
