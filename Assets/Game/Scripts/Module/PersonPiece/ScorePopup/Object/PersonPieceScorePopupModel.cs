using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.PersonPiece.ScorePopup
{
    public class PersonPieceScorePopupModel : BaseModel, IPersonPieceScorePopupModel
    {
        public int Score { get; private set; }
        public bool IsMax { get; private set; }

        public void SetScore(int value)
        {
            Score = value;
            SetDataAsDirty();
        }

        public void SetMax(bool value)
        {
            IsMax = value;
            SetDataAsDirty();
        }
    }

    public interface IPersonPieceScorePopupModel : IBaseModel
    {
        int Score { get; }
        bool IsMax { get; }
    }
}
