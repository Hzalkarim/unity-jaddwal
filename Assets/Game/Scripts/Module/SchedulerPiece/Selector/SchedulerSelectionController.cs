using Agate.MVC.Base;
using Jaddwal.Core.MVC;
using Jaddwal.GameplaySequence.Selector;
using Jaddwal.SchedulerPiece.Container;
using Jaddwal.SchedulerPiece.Instantiator;
using Jaddwal.SchedulerPiece.TeamManager;
using Jaddwal.Utility.Board;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using Jaddwal.Cell;
using Jaddwal.Board;
using Jaddwal.GameplaySequence.RoundManager;
using Jaddwal.GameplaySequence.Message;
using Jaddwal.Utility.Scheduler;

namespace Jaddwal.SchedulerPiece.Selector
{
    public class SchedulerSelectionController : ObjectController<SchedulerSelectionController, SchedulerSelectionView> , ISystemController<SchedulerSelectionView>
    {
        private SchedulerInstantiatorController _instantiator;
        private SchedulerContainerController _container;
        private SchedulerTeamController _teamManager;

        private SelectionController _selector;
        private BoardController _board;

        private Cell.System.CellInstantiatorController _cellInstantiator;
        protected Color DefaultCellColor => _cellInstantiator.GetDefaultBackgroundColor();

        private int[] _values = new int[3] { 2, 4, 8 };

        private CellController _selectedCell;
        private SchedulerSelectionPickerController _picker;

        public bool IsActive { get; private set; }

        public void SetActive(bool value)
        {
            IsActive = value;
        }

        public void ChangeHighlightColor(int i)
        {
            Color c = GetHighlightColor(i);
            ChangeHighlightColor(c);
        }

        public void ChangeHighlightColor(Color color)
        {
            var cells = _board.GetAllCells();
            Vector2Int size = _board.GetSize();
            int maxIndex = size.x * size.y;
            foreach (int i in _selector.GetSelectedIndex())
            {
                if (i >= maxIndex || i < 0) continue;

                cells[i].SetBackgroundColor(color);
            }
        }

        public Color GetHighlightColor(int i)
        {
            return _view.Data.SelectedColor[i];
        }

        public IEnumerator OnInitSceneObject(SchedulerSelectionView view)
        {
            SetView(view);
            var picker = new SchedulerSelectionPickerController();
            var pickerView = UnityEngine.Object.Instantiate(_view.Data.PrefabPicker);
            picker.SetView(pickerView);
            picker.SetParent(_view.Data.Parent);
            picker.SetActive(false);

            _picker = picker;

            _selector = new SelectionController();
            yield return null;
        }

        public IEnumerator OnLaunchScene()
        {
            var cells = _board.GetAllCells();

            int i = 0;
            foreach (int val in _values)
            {
                _picker.AddOnButtonClickHandler(i++, () =>
                {
                    if (_selectedCell == null) return;

                    _instantiator.Value = val;
                    var piece = _instantiator.InstantiateSchedulerPiece(val);
                    _selectedCell.AddContent(piece);

                    _selectedCell = null;
                    _picker.SetActive(false);

                    _teamManager.AddPiece(piece, _teamManager.ActiveTeamIndex);
                    EndTurn();
                });
            }

            BoardHelper.SetBoardProperties(_board);
            cells.ForEach(cell =>
            {
                int index = cells.IndexOf(cell);
                //int[] toColor = new int[]
                //{
                //    index.Up().Up(),
                //    index.Up().Left(), index.Up(), index.Up().Right(),
                //    index.Left().Left(), index.Left(), index, index.Right(), index.Right().Right(),
                //    index.Down().Left(), index.Down(), index.Down().Right(),
                //    index.Down().Down()
                //};
                int[] toColor = new int[1] { index };

                cell.AddOnPointerEnterHandler(_ =>
                {
                    if (!IsActive) return;

                    bool isEmpty = cell.IsEmpty();
                    if (isEmpty)
                    {
                        //highlight when empty
                        _selector.SetSelectedIndex(toColor);
                        ChangeHighlightColor(_teamManager.ActiveTeamIndex);
                        _instantiator.Value = 5;
                    }
                    else
                    {
                        //Include own scheduler
                        var sch = cell.GetScheduler(_container.GetAllPieces());
                        if (sch != null){
                            int index = _container.GetAllPieces().IndexOf(sch);
                            if (_teamManager.SchedulerTeamIndex[index] == _teamManager.ActiveTeamIndex)
                            {
                                _selector.SetSelectedIndex(toColor);
                                ChangeHighlightColor(_teamManager.ActiveTeamIndex);
                                _instantiator.Value = -1;
                            }
                        }
                    }
                });

                cell.AddOnPointerExitHandler(_ =>
                {
                    if (!IsActive) return;

                    ChangeHighlightColor(DefaultCellColor);
                });

                cell.AddOnPointerUpHandler(_ =>
                {
                    if (!IsActive) return;

                    //When clicked cell contains own scheduler
                    if (!cell.IsEmpty())
                    {
                        var sch = cell.GetScheduler(_container.GetAllPieces());
                        if (sch == null) return;

                        if (_teamManager.GetTeamIndex(sch) != _teamManager.ActiveTeamIndex || sch.Model.CurrentValue == 0)
                            return;
                        
                        sch.DecreaseCurrentValue(1);
                        EndTurn();
                        return;
                    }

                    //When clicked cell is empty
                    if (_selectedCell == null)
                    {
                        _selectedCell = cell;
                        _picker.SetActive(true);
                        _picker.SetPosition(cell.GetTransform().position);
                    }
                    else if (_selectedCell != cell)
                    {
                        _selectedCell = cell;
                        _picker.SetPosition(cell.GetTransform().position);
                    }
                    else
                    {
                        _picker.SetActive(false);
                        _selectedCell = null;
                    }
                });
            });

            _picker.GetTransform().SetAsLastSibling();
            yield return null;
        }

        private void EndTurn()
        {
            ChangeHighlightColor(DefaultCellColor);
            switch (_teamManager.ActiveTeamIndex)
            {
                case 0:
                    Publish(new GameplayDoneActionMessage(Turn.SchedulerBlue));
                    break;
                case 1:
                    Publish(new GameplayDoneActionMessage(Turn.SchedulerGreen));
                    break;
                default:
                    break;
            }
        }

        private bool TryGetContent(SchedulerView view, out SchedulerController controller)
        {
            if (view == null)
            {
                controller = null;
                return false;
            }

            foreach (var sch in _container.GetAllPieces())
            {
                if (sch.CompareContent(view))
                {
                    controller = sch;
                    return true;
                }
            }

            controller = null;
            return false;
        }
    }

}
