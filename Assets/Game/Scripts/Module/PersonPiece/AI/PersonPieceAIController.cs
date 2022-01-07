using Agate.MVC.Base;
using Jaddwal.GameplaySequence.Message;
using Jaddwal.GameplaySequence.RoundManager;
using Jaddwal.SchedulerPiece;
using Jaddwal.Utility.Scheduler;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jaddwal.Utility.Board;
using Jaddwal.Core.MVC;

namespace Jaddwal.PersonPiece.AI
{
    public class PersonPieceAIController : ObjectController<PersonPieceAIController, PersonPieceAIView>, IInitializableOnScene<PersonPieceAIView>
    {
        private Board.BoardController _board;

        private ScorePopup.PersonPieceScorePopupInstantiateController _scorePopupInstantiator;
        
        private SchedulerPiece.Container.SchedulerContainerController _schedulerContainer;
        private SchedulerPiece.ScoreCollector.SchedulerPieceScoreCollectorController _schedulerScoring;

        private System.PersonPieceSystemController _system;
        private Selector.PersonPieceSelectionController _selector;

        private WaitForSeconds _waitHalfSecond = new WaitForSeconds(0.5f);
        private WaitForSeconds _waitOneTenthSecond = new WaitForSeconds(0.1f);

        public bool IsShowScore => _view.Data.ShowScore;

        public IEnumerator CaptureHighest()
        {
            _scorePopupInstantiator.IsActive = IsShowScore;

            _selector.GenerateValidMovementPlace(_selector.IsZeroExist, removePrevHighlight: false);
            yield return _waitHalfSecond;

            var validMovementIndex = _selector.GetValidMovementPlace().FindAll(i => i >= 0);

            //Check board for existing scheduler
            var cells = _board.GetAllCells();
            var schs = _schedulerContainer.GetAllPieces();
            if (schs.Count == 0)
            {
                //Move randomly if no scheduler exist
                RandomMove(validMovementIndex);
                yield return _waitHalfSecond;
                _selector.RemoveHighlight();
                Publish(new GameplayDoneActionMessage(Turn.Person));
                yield break;
            }

            //Search highest score in every cells within capture area
            int maxValue = -1;
            int maxIndex = -1;
            SchedulerController schMax = null;
            var zeros = new Dictionary<int, SchedulerController>();
            bool schExist = false;
            foreach (var i in validMovementIndex)
            {

                //Check whether cell contains a scheduler
                var sch = cells[i].GetScheduler(schs);
                if (sch == null)
                    continue;

                schExist = true;
                //Collect all zeros
                if (sch.Model.CurrentValue == 0)
                {
                    zeros.Add(i, sch);
                }

                //if (zeros.Count > 0)
                //    continue;

                //If exist
                //Compare score with current max score
                int score = _schedulerScoring.GetScore(sch);
                if (score > maxValue)
                {
                    maxValue = score;
                    maxIndex = i;
                    schMax = sch;
                }

                if (sch.Model.CurrentValue != 0)
                    yield return _scorePopupInstantiator.InstantiateScorePopupEnum(score, score == maxValue, cells[i].GetTransform().position);
            }



            //If Zero exist, make priority
            if (zeros.Count > 0)
            {
                maxIndex = -1;
                maxValue = -1;
                schMax = null;
                foreach (int i in zeros.Keys)
                {
                    int max = zeros[i].Model.MaxValue;
                    if (max > maxValue)
                    {
                        maxValue = max;
                        maxIndex = i;
                        schMax = zeros[i];
                    }

                    yield return _scorePopupInstantiator.InstantiateScorePopupEnum(max * 2, max == maxValue, cells[i].GetTransform().position, max != maxValue);
                }
            }

            if (schExist && _scorePopupInstantiator.IsActive)
            {
                yield return new WaitForSeconds(2f);
            }

            if (schMax != null)
            {
                //Capture highest score
                _schedulerScoring.CollectPointByPerson(schMax);
                _system.SetPiecePosition(cells[maxIndex].Pos);
            }
            else
            {
                //Move towards the highest score scheduler
                yield return ChaseHighest();
            }

            _scorePopupInstantiator.DestroyAllScorePopup();
            yield return _waitHalfSecond;
            _selector.RemoveHighlight();
            Publish(new GameplayDoneActionMessage(Turn.Person));

        }

        public IEnumerator OnInitSceneObject(PersonPieceAIView view)
        {
            SetView(view);
            yield return null;
        }

        public void SetShowScore(bool value)
        {
            _view.Data.ShowScore = value;
        }

        private IEnumerator ChaseHighest()
        {

            var schs = _schedulerContainer.GetAllPieces();
            var scores = schs.Select(s => _schedulerScoring.GetScore(s)).ToList();

            int maxVal = -1;
            int maxIndex = -1;
            for (int i = 0; i < schs.Count; i++)
            {
                if (scores[i] > maxVal)
                {
                    maxVal = scores[i];
                    maxIndex = i;
                }
            }

            yield return OneStepToScheduler(schs[maxIndex]);
            if (_selector.IsZeroExist)
            {
                yield return OneStepToScheduler(schs[maxIndex]);
            }
        }

        private IEnumerator OneStepToScheduler(SchedulerController sch)
        {
            var piecePos = _system.GetPiece().GetTransform().position;
            var schPos = sch.GetTransform().position;

            var dir = (schPos - piecePos).normalized;

            var pos = _system.PiecePosition;
            if (dir.x > 0)
            {
                pos.Set(pos.x + 1, pos.y);
            }
            else if (dir.x < 0)
            {
                pos.Set(pos.x - 1, pos.y);
            }

            if (dir.y > 0)
            {
                pos.Set(pos.x, pos.y + 1);
            }
            else if (dir.y < 0)
            {
                pos.Set(pos.x, pos.y - 1);
            }

            if (_board.IsPositionValid(pos.x, pos.y))
            {
                _system.SetPiecePosition(pos);
                yield return _waitOneTenthSecond;
            }
        }

        private void RandomMove(List<int> index)
        {
            int r = Random.Range(0, index.Count);

            _system.SetPiecePosition(_board.GetAllCells()[index[r]].Pos);
        }
    }

}
