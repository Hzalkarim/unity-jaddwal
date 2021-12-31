using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.SchedulerPiece.ScoreCollector
{
    public class SchedulerPieceScoreCollectorModel : BaseModel, ISchedulerPieceScoreCollectorModel
    {
        public List<int> Scores { get; private set; }

        public SchedulerPieceScoreCollectorModel()
        {
            Scores = new List<int>(2);
            Scores.Add(0);
            Scores.Add(0);
        }

        public void AddScore(int index, int score)
        {
            Scores[index] += score;
            SetDataAsDirty();
        }
    }

    public interface ISchedulerPieceScoreCollectorModel : IBaseModel
    {
        List<int> Scores { get; }
    }
}
