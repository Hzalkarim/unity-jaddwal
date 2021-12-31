using Agate.MVC.Base;
using Agate.MVC.Core;
using Jaddwal.Cell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.SchedulerPiece
{
    public class SchedulerController : ObjectController<SchedulerController, SchedulerModel, ISchedulerModel, SchedulerView>, ICellContent
    {
        public void DecreaseCurrentValue(int value)
        {
            int newVal = _model.CurrentValue - value;
            _model.SetCurrentValue(newVal);
        }

        public void Destroy()
        {
            Object.Destroy(_view.gameObject);
        }

        public void SetMaxValue(int value)
        {
            _model.SetMaxValue(value);
        }

        public void SetCurrentValue(int value)
        {
            _model.SetCurrentValue(value);
        }

        public Transform GetTransform()
        {
            return _view.transform;
        }

        public RectTransform GetRectTransform()
        {
            return _view.GetComponent<RectTransform>();
        }

        public bool CompareContent(View another)
        {
            return _view == another;
        }
    }

}
