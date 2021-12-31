using Agate.MVC.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.SchedulerPiece.TeamManager
{
    public class SchedulerTeamView : BaseView
    {
        [SerializeField]
        private SchedulerPieceChangerViewData _data;
        public SchedulerPieceChangerViewData Data => _data;

        public event Action UpdateEvent;

        private void Update()
        {
            UpdateEvent?.Invoke();
        }
    }

    [Serializable]
    public class SchedulerPieceChangerViewData
    {
        public int activeIndex = 0;
        public List<SchedulerView> prefabs;
    }
}
