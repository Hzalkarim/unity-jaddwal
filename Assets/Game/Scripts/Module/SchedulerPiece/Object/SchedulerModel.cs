using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.SchedulerPiece
{
    public class SchedulerModel : BaseModel, ISchedulerModel
    {
        public int MaxValue { get; private set; }
        public int CurrentValue { get; private set; }

        public void SetMaxValue(int value)
        {
            MaxValue = value;
            SetDataAsDirty();
        }

        public void SetCurrentValue(int value)
        {
            CurrentValue = value;
            SetDataAsDirty();
        }
    }

    public interface ISchedulerModel : IBaseModel
    {
        int MaxValue { get; }
        int CurrentValue { get; }
    }
}
