using Agate.MVC.Base;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jaddwal.Core.MVC;

namespace Jaddwal.SchedulerPiece.RoundManager
{
    public class SchedulerRoundManagementController : ObjectController<SchedulerRoundManagementController, SchedulerRoundManagementView>, ISystemController<SchedulerRoundManagementView>
    {
        private Container.SchedulerContainerController _container;
        private TeamManager.SchedulerTeamController _teamManager;

        public void RemoveAllZero()
        {
            var schedulers = _container.GetAllPieces();
            if (schedulers.Count == 0) return;

            var team = _teamManager.SchedulerTeamIndex;
            var zeroSch = schedulers.FindAll(sch => sch.Model.CurrentValue == 0);
            foreach (var sch in zeroSch)
            {
                team.RemoveAt(schedulers.IndexOf(sch));
                schedulers.Remove(sch);
                sch.Destroy();
            }
        }

        public void DecreaseCurrentValue()
        {
            var schedulers = _container.GetAllPieces();
            if (schedulers.Count == 0) return;

            if (schedulers.Any(sch => sch.Model.CurrentValue == 0))
                RemoveAllZero();

            schedulers.ForEach(sch => sch.DecreaseCurrentValue(1));
        }

        public IEnumerator OnInitSceneObject(SchedulerRoundManagementView view)
        {
            SetView(view);
            yield return null;
        }

        public IEnumerator OnLaunchScene()
        {
            _view.OnUpdateEvent += () =>
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    DecreaseCurrentValue();
                }
            };
            yield return null;
        }
    }

}
