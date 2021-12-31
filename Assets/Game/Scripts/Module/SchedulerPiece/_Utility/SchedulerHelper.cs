using Jaddwal.Cell;
using Jaddwal.SchedulerPiece;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.Utility.Scheduler
{
    public static class SchedulerHelper
    {

        public static List<SchedulerController> Schedulers { get; private set; } = new List<SchedulerController>();

        public static void SetSchedulers(List<SchedulerController> schedulers)
        {
            Schedulers = schedulers;
        }

        public static SchedulerController GetScheduler(this CellController cell)
        {
            var schView = cell.GetCellContent<SchedulerView>();
            if (schView == null) return null;

            if (Schedulers.Count == 0) return null;

            var sch = Schedulers.Find(s => s.CompareContent(schView));

            return sch;
        }

        public static SchedulerController GetScheduler(this CellController cell, List<SchedulerController> schedulers)
        {
            var schView = cell.GetCellContent<SchedulerView>();
            if (schView == null) return null;

            if (schedulers.Count == 0) return null;

            var sch = schedulers.Find(s => s.CompareContent(schView));

            return sch;
        }
    }

}
