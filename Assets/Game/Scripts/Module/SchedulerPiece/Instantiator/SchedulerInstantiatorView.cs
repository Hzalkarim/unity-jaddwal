using Agate.MVC.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.SchedulerPiece.Instantiator
{
    public class SchedulerInstantiatorView : BaseView
    {
        [SerializeField]
        private SchedulerPieceInstantiatorViewData _data;
        public SchedulerPieceInstantiatorViewData Data => _data;
    }

    [Serializable]
    public class SchedulerPieceInstantiatorViewData
    {
        public SchedulerView prefab;
        public bool isActive;
        public int value;
    }

}
