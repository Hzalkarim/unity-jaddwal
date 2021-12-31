using Agate.MVC.Base;
using Jaddwal.GameplaySequence.Selector;
using Jaddwal.SchedulerPiece.Selector.Mono;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.SchedulerPiece.Selector
{
    public class SchedulerSelectionView : BaseView
    {
        [SerializeField]
        private SchedulerSelectionViewData _data;
        public SchedulerSelectionViewData Data => _data;
    }

    [System.Serializable]
    public class SchedulerSelectionViewData
    {
        public List<Color> SelectedColor;
        [Space]
        public Transform Parent;
        public SchedulerSelectionPickerView PrefabPicker;
    }

}
