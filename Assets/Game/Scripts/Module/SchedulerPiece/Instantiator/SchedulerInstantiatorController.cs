using Agate.MVC.Base;
using Jaddwal.Board;
using Jaddwal.Cell;
using Jaddwal.Core.MVC;
using Jaddwal.SchedulerPiece.Container;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.SchedulerPiece.Instantiator
{
    public class SchedulerInstantiatorController : ObjectController<SchedulerInstantiatorController, SchedulerInstantiatorView>, ISystemController<SchedulerInstantiatorView>
    {
        private BoardController _board;
        private SchedulerContainerController _schedulerPieceContainer;

        public int Value
        {
            get
            {
                return _view.Data.value;
            }
            set
            {
                _view.Data.value = value;
            }
        }
        public bool IsActive
        {
            get
            {
                return _view.Data.isActive;
            }
            set
            {
                _view.Data.isActive = value;
            }
        }
        public SchedulerView Prefab
        {
            get
            {
                return _view.Data.prefab;
            }
            set
            {
                _view.Data.prefab = value;
            }
        }

        public SchedulerController InstantiateSchedulerPiece(int value)
        {
            var piece = new SchedulerController();
            piece.SetMaxValue(value);
            piece.SetCurrentValue(value);

            var pieceView = Object.Instantiate(Prefab);
            piece.SetView(pieceView);

            return piece;
        }

        public IEnumerator OnInitSceneObject(SchedulerInstantiatorView view)
        {
            SetView(view);
            yield return null;
        }

        public IEnumerator OnLaunchScene()
        {
            var cells = _board.GetAllCells();
            int i = 0;
            foreach (var cell in cells)
            {
                cell.AddContent(InstantiateSchedulerPiece(i++));
                yield return null;
            }
        }

        private void OnPlacePiece(CellController cell)
        {
            if (IsActive && cell.IsEmpty())
            {
                var piece = InstantiateSchedulerPiece(Value);
                cell.AddContent(piece);
                _schedulerPieceContainer.AddPiece(piece);
            }
        }
    }

}
