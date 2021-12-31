using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.SchedulerPiece.BoardFilter
{
    public class SchedulerBoardFilterView : BaseView
    {
        [SerializeField]
        private SchedulerBoardFilterViewData _data;
        public SchedulerBoardFilterViewData Data => _data;
    }

    [System.Serializable]
    public class SchedulerBoardFilterViewData
    {
        public Sprite FilteredSprite;
        public GameObject FilterBlue;
        public GameObject FilterGreen;
    }
}
