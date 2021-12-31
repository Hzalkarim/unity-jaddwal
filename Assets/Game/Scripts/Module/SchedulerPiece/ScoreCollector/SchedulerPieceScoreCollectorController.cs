using Agate.MVC.Base;
using Jaddwal.Core.MVC;
using Jaddwal.SchedulerPiece.Container;
using Jaddwal.SchedulerPiece.TeamManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.SchedulerPiece.ScoreCollector
{
    public class SchedulerPieceScoreCollectorController : ObjectController<SchedulerPieceScoreCollectorController, SchedulerPieceScoreCollectorModel, ISchedulerPieceScoreCollectorModel ,SchedulerPieceScoreCollectorView>, ISystemController<SchedulerPieceScoreCollectorView>
    {
        private SchedulerContainerController _container;
        private SchedulerTeamController _teamManager;

        private List<int> _score = new List<int>(2);

        public void CollectPointByPerson(SchedulerController scheduler)
        {
            _teamManager.RemovePiece(scheduler, out int team);

            int score = GetScore(scheduler);
            _score[team] += score;
            scheduler.Destroy();

            _model.AddScore(team, score);
            Debug.Log($"BLUE [{_score[0]}] - [{_score[1]}] GREEN");
        }

        public void CollectPointByEndRound()
        {
            var schedulers = _container.GetAllPieces();
            int[] score = new int[2] { 0, 0 };
            foreach (var sch in schedulers)
            {
                int team = _teamManager.GetTeamIndex(sch);
                score[team] += 1;
            }
            _model.AddScore(0, score[0]);
            _model.AddScore(1, score[1]);
        }

        public int GetScore(SchedulerController scheduler)
        {
            if (scheduler.Model.CurrentValue == 0)
            {
                return scheduler.Model.MaxValue * 2;
            }
            else
            {
                return scheduler.Model.MaxValue - scheduler.Model.CurrentValue;
            }
        }

        public IEnumerator OnLaunchScene()
        {
            _score.Add(0);
            _score.Add(0);
            yield return null;
        }

        public IEnumerator OnInitSceneObject(SchedulerPieceScoreCollectorView view)
        {
            SetView(view);
            yield return null;
        }
    }

}
