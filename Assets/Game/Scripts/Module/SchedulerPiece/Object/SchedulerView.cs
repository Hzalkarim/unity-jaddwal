using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jaddwal.SchedulerPiece
{
    public class SchedulerView : ObjectView<ISchedulerModel>
    {
        [SerializeField]
        private Text _maxValue;
        [SerializeField]
        private Text _currentValue;
        [SerializeField]
        private Image _image;

        protected override void InitRenderModel(ISchedulerModel model)
        {
        }

        protected override void UpdateRenderModel(ISchedulerModel model)
        {
            _maxValue.text = model.MaxValue.ToString();
            _currentValue.text = model.CurrentValue.ToString();
            _image.fillAmount = (float)model.CurrentValue / model.MaxValue;
        }
    }
}
