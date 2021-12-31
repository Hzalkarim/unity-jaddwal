using Agate.MVC.Base;
using System;

namespace Jaddwal.SchedulerPiece.RoundManager
{
    public class SchedulerRoundManagementView : BaseView
    {
        public event Action OnUpdateEvent;

        private void Update()
        {
            OnUpdateEvent?.Invoke();
        }

    }

}
