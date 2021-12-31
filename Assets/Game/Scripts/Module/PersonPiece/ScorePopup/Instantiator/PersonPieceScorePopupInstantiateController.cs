using Agate.MVC.Base;
using Jaddwal.Core.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.PersonPiece.ScorePopup
{
    public class PersonPieceScorePopupInstantiateController : ObjectController<PersonPieceScorePopupInstantiateController, PersonPieceScorePopupInstantiateView>, IInitializableOnScene<PersonPieceScorePopupInstantiateView>
    {
        private List<PersonPieceScorePopupController> _scorePopups = new List<PersonPieceScorePopupController>();

        public bool IsActive { get; set; }
        private WaitForSeconds _waitOneTenthSecond = new WaitForSeconds(.1f);

        public void InstantiateScorePopup(int score, bool isMax, Vector3 position)
        {
            if (!IsActive) return;

            var popup = new PersonPieceScorePopupController();
            popup.SetScore(score);
            popup.SetMax(isMax);
            popup.InstantiateObject(_view.Data.Prefab, position, _view.Data.Parent);

            if (isMax)
            {
                _scorePopups.ForEach(sp => sp.SetMax(false));
            }
            _scorePopups.Add(popup);
        }

        public IEnumerator InstantiateScorePopupEnum(int score, bool isMax, Vector3 position)
        {
            if (!IsActive)
                yield break;
            InstantiateScorePopup(score, isMax, position);
            yield return _waitOneTenthSecond;
        }

        public List<PersonPieceScorePopupController> GetAllScorePopups() => _scorePopups;

        public void DestroyAllScorePopup()
        {
            _scorePopups.ForEach(sp => sp.HideAndDestroy());
            _scorePopups.Clear();
        }


        public IEnumerator OnInitSceneObject(PersonPieceScorePopupInstantiateView view)
        {
            SetView(view);
            yield return null;
        }
    }

}
