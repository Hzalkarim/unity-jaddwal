using Agate.MVC.Base;
using Jaddwal.Core.MVC;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jaddwal.SchedulerPiece.TeamManager.Message;
using Jaddwal.Utility.Scheduler;
using Jaddwal.SchedulerPiece;

namespace Jaddwal.GameplaySequence.RoundManager
{
    public class RoundManagementController : ObjectController<RoundManagementController, RoundManagementView>, ISystemController<RoundManagementView>
    {
        private Board.BoardController _board;

        private PersonPiece.Selector.PersonPieceSelectionController _person;
        private PersonPiece.AI.PersonPieceAIController _personAI;

        private SchedulerPiece.Selector.SchedulerSelectionController _schedulerSelector;
        private SchedulerPiece.TeamManager.SchedulerTeamController _schedulerTeam;
        private SchedulerPiece.RoundManager.SchedulerRoundManagementController _schedulerRoundManager;
        private SchedulerPiece.ScoreCollector.SchedulerPieceScoreCollectorController _schedulerScoring;
        private SchedulerPiece.BoardFilter.SchedulerBoardFilterController _schedulerBoardFilter;

        private SchedulerPiece.Container.SchedulerContainerController _schedulerContainer;

        public Turn CurrentTurn { get; private set; }
        public bool IsRoundActive { get; private set; }

        private WaitForSeconds _waitHalfSecond = new WaitForSeconds(0.5f);
        private WaitForSeconds _waitOneTenthSecond = new WaitForSeconds(0.1f);

        public void StartRound()
        {
            _view.StartRound(Round());
        }

        public IEnumerator Round()
        {

            _person.SetActive(false);
            while (IsRoundActive)
            {
                yield return SchedulerTurn();

                yield return PersonAITurn();

                var pieces = _schedulerContainer.GetAllPieces();
                if (pieces.Count > 0)
                {
                    var zeros = pieces.Where(sch => sch.Model.CurrentValue == 0);
                    foreach (var zero in zeros)
                    {
                        _schedulerBoardFilter.ApplySchedulerFilter(zero);
                        yield return null;
                    }
                }

                yield return EndingRound();
                yield return _waitHalfSecond;
                yield return ApplyFilterBonusForOppositeTeam(1);
                yield return ApplyFilterBonusForOppositeTeam(0);

            }
            yield return null;
        }

        public void EndTurn(Turn turn)
        {
            switch (turn)
            {
                case Turn.SchedulerBlue:
                    CurrentTurn = Turn.SchedulerGreen;
                    break;
                case Turn.SchedulerGreen:
                    CurrentTurn = Turn.Person;
                    break;
                case Turn.Person:
                    CurrentTurn = Turn.Ending;
                    break;
                case Turn.Ending:
                    CurrentTurn = Turn.SchedulerBlue;
                    break;
                default:
                    break;
            }
        }

        public void SetRoundActive(bool value)
        {
            IsRoundActive = value;
        }

        public IEnumerator OnInitSceneObject(RoundManagementView view)
        {
            SetView(view);
            yield return null;
        }

        public IEnumerator OnLaunchScene()
        {
            IsRoundActive = true;
            StartRound();
            yield return null;
        }

        #region SCHEDULER
        private IEnumerator SchedulerTurn()
        {

            yield return BlueSchedulerTurn();
            yield return GreenSchedulerTurn();


        }

        private IEnumerator BlueSchedulerTurn()
        {
            _schedulerTeam.SetActiveScheduler(0);

            _schedulerBoardFilter.ActivateFilterCell(0);
            //_schedulerBoardFilter.HighlightFilteredCell(0);
            _schedulerSelector.SetActive(true);
            yield return new WaitUntil(() => CurrentTurn != Turn.SchedulerBlue);
            _schedulerSelector.SetActive(false);
            //_schedulerBoardFilter.UnhighlightFilteredCell(0);
            _schedulerBoardFilter.DeactivateFilterCell(0);
        }

        private IEnumerator GreenSchedulerTurn()
        {
            _schedulerTeam.SetActiveScheduler(1);
            _schedulerBoardFilter.ActivateFilterCell(1);
            //_schedulerBoardFilter.HighlightFilteredCell(1);
            _schedulerSelector.SetActive(true);
            yield return new WaitUntil(() => CurrentTurn != Turn.SchedulerGreen);
            _schedulerSelector.SetActive(false);
            //_schedulerBoardFilter.UnhighlightFilteredCell(1);
            _schedulerBoardFilter.DeactivateFilterCell(1);
        }
        #endregion

        #region PERSON

        private IEnumerator PersonTurn()
        {
            _person.SetActive(true);
            _person.GenerateValidMovementPlace(_person.IsZeroExist);
            yield return new WaitUntil(() => CurrentTurn != Turn.Person);
            _person.SetActive(false);
        }

        private IEnumerator PersonAITurn()
        {
            _person.SetActive(true);
            yield return _personAI.CaptureHighest();
            yield return new WaitUntil(() => CurrentTurn != Turn.Person);
            _person.SetActive(false);
        }
        #endregion

        private IEnumerator ApplyFilterBonusForOppositeTeam(int team)
        {
            var filterGreen = _schedulerBoardFilter.GetFilteredCells(team);
            if (filterGreen.Count > 0)
            {
                var cells = _board.GetAllCells();
                var pieces = _schedulerContainer.GetAllPieces();
                bool schExist = false;
                List<SchedulerController> schs = new List<SchedulerController>();
                foreach (int i in filterGreen)
                {
                    var sch = cells[i].GetScheduler(pieces);
                    if (sch != null && _schedulerTeam.GetTeamIndex(sch) != team)
                    {
                        schs.Add(sch);
                    }
                    yield return null;
                }

                if (schs.Count > 0)
                {
                    foreach (int i in filterGreen)
                    {
                        _schedulerBoardFilter.RemoveFilterMark(team, cells[i]);
                        yield return _waitOneTenthSecond;
                    }

                    schs.ForEach(s =>
                    {
                        s.SetMaxValue(s.Model.MaxValue * 2);
                        s.SetCurrentValue(s.Model.CurrentValue + 2);
                    });
                    _schedulerBoardFilter.ClearFilteredCells(team);
                }
            }
            yield return null;
        }


        private IEnumerator EndingRound()
        {
            yield return _waitHalfSecond;
            _schedulerRoundManager.DecreaseCurrentValue();
            yield return _waitHalfSecond;
            _schedulerScoring.CollectPointByEndRound();
            EndTurn(Turn.Ending);
        }
    }

    public enum Turn
    {
        SchedulerBlue, SchedulerGreen, Person, Ending
    }
}
