using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jaddwal.SchedulerPiece.Container
{
    public class SchedulerContainerController : BaseController<SchedulerContainerController>
    {
        private List<SchedulerController> _schedulerPieces = new List<SchedulerController>();

        public void AddPiece(SchedulerController piece)
        {
            _schedulerPieces.Add(piece);
        }

        public void RemovePiece(SchedulerController piece)
        {
            _schedulerPieces.Remove(piece);
        }

        public void ClearPieces()
        {
            _schedulerPieces.Clear();
        }

        public List<SchedulerController> GetAllPieces()
        {
            return _schedulerPieces;
        }

    }

}
