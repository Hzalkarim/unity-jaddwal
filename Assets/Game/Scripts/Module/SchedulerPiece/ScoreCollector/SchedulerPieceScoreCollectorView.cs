using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jaddwal.SchedulerPiece.ScoreCollector
{
    public class SchedulerPieceScoreCollectorView : ObjectView<ISchedulerPieceScoreCollectorModel>
    {
        [SerializeField]
        private List<Text> _scores;
        private List<string> _teams = new List<string>() { "BLUE", "GREEN" };

        protected override void InitRenderModel(ISchedulerPieceScoreCollectorModel model)
        {
        }

        protected override void UpdateRenderModel(ISchedulerPieceScoreCollectorModel model)
        {
            _scores[0].text = $"{_teams[0]} - {model.Scores[0]}";
            _scores[1].text = $"{model.Scores[1]} - {_teams[1]}";
        }
    }

}
