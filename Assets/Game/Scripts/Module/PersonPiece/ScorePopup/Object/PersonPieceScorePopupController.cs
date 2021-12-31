using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.PersonPiece.ScorePopup
{
    public class PersonPieceScorePopupController : ObjectController<PersonPieceScorePopupController, PersonPieceScorePopupModel, IPersonPieceScorePopupModel, PersonPieceScorePopupView>
    {
        public void SetScore(int value)
        {
            _model.SetScore(value);
        }

        public void SetMax(bool value)
        {
            _model.SetMax(value);
        }

        public void InstantiateObject(PersonPieceScorePopupView prefabView, Vector3 position, Transform parent)
        {
            var view = Object.Instantiate(prefabView, position, Quaternion.identity, parent);
            SetView(view);
            _view.OnHideEndedEvent += DestroyObject;
        }

        public void HideAndDestroy()
        {
            _view.Anim.SetBool("Hide", true);
        }

        public void DestroyObject()
        {
            Object.Destroy(_view.gameObject);
        }
    }

}
