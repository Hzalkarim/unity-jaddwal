using Agate.MVC.Base;
using Jaddwal.Board;
using Jaddwal.Cell;
using Jaddwal.Core.MVC;
using Jaddwal.SchedulerPiece.Container;
using Jaddwal.SchedulerPiece.Instantiator;
using Jaddwal.SchedulerPiece.TeamManager.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.SchedulerPiece.TeamManager
{
    public class SchedulerTeamController : ObjectController<SchedulerTeamController, SchedulerTeamView>, ISystemController<SchedulerTeamView>
    {
        private SchedulerInstantiatorController _instantiator;
        private SchedulerContainerController _container;
        private BoardController _board;

        private List<int> _schedulerTeamIndex;
        public List<int> SchedulerTeamIndex => _schedulerTeamIndex;

        public int ActiveTeamIndex
        {
            get
            {
                return _view.Data.activeIndex;
            }
            private set
            {
                _view.Data.activeIndex = value;
            }
        }

        public void SetActiveSystem(bool value)
        {
            _instantiator.IsActive = value;
        }

        public void SetActiveScheduler(int i)
        {
            if (i < _view.Data.prefabs.Count)
            {
                ActiveTeamIndex = i;
                _instantiator.Prefab = _view.Data.prefabs[i];
            }
        }

        public int GetTeamIndex(SchedulerController sch)
        {
            int i = _container.GetAllPieces().IndexOf(sch);
            return SchedulerTeamIndex[i];
        }

        public void AddPiece(SchedulerController piece, int teamIndex)
        {
            _container.AddPiece(piece);
            _schedulerTeamIndex.Add(teamIndex);
        }

        public void RemovePiece(SchedulerController piece)
        {
            int index = _container.GetAllPieces().IndexOf(piece);
            _container.RemovePiece(piece);
            _schedulerTeamIndex.RemoveAt(index);
        }

        public void RemovePiece(SchedulerController piece, out int teamIndex)
        {
            int index = _container.GetAllPieces().IndexOf(piece);
            _container.RemovePiece(piece);

            teamIndex = _schedulerTeamIndex[index];
            _schedulerTeamIndex.RemoveAt(index);
        }

        public IEnumerator OnInitSceneObject(SchedulerTeamView view)
        {
            SetView(view);
            _schedulerTeamIndex = new List<int>();
            yield return null;
        }

        public IEnumerator OnLaunchScene()
        {
            _view.UpdateEvent += ChangeSchedulerTurnByInput;
            yield return null;
            //var cells = _board.GetAllCells();
            //foreach (var cell in cells)
            //{
            //    cell.AddOnPointerUpHandler((_) =>
            //    {
            //        if (_instantiator.IsActive && cell.IsEmpty())
            //            OnPiecePlace(cell);
            //    });

            //    yield return null;
            //}
        }

        private void OnPiecePlace(CellController cell)
        {
            int val = _instantiator.Value;
            var piece = _instantiator.InstantiateSchedulerPiece(val);

            cell.AddContent(piece);
            AddPiece(piece, ActiveTeamIndex);
            Debug.Log($"Instantiate with TEAM: {ActiveTeamIndex}");
        }

        private void ChangeSchedulerTurnByInput()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SetActiveScheduler(0);
                Publish(new SchedulerTeamChangeMessage(0));
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                SetActiveScheduler(1);
                Publish(new SchedulerTeamChangeMessage(1));
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                var lastValue = _instantiator.IsActive;
                SetActiveSystem(!lastValue);
            }
        }
    }

}
