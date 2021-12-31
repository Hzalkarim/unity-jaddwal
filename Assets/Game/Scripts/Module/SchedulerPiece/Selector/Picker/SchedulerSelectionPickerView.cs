using Agate.MVC.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jaddwal.SchedulerPiece.Selector
{
    public class SchedulerSelectionPickerView : BaseView
    {
        [SerializeField]
        private List<Button> _button;

        public void AddOnButtonClickHandler(int i, Action action)
        {
            _button[i].onClick.AddListener(() => action());
        }
    }
}
