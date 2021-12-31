using Agate.MVC.Base;
using Jaddwal.Board;
using Jaddwal.Cell;
using Jaddwal.Core.MVC;
using Jaddwal.GameplaySequence.Message;
using Jaddwal.GameplaySequence.RoundManager;
using Jaddwal.GameplaySequence.Selector;
using Jaddwal.PersonPiece.System;
using Jaddwal.Utility.Board;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jaddwal.SchedulerPiece;
using Jaddwal.Utility.Scheduler;

namespace Jaddwal.PersonPiece.Selector
{
    public class PersonPieceSelectionController : ObjectController<PersonPieceSelectionController, PersonPieceSelectionView>, ISystemController<PersonPieceSelectionView>
    {
        private Cell.System.CellInstantiatorController _cellInstantiator;

        private SchedulerPiece.Container.SchedulerContainerController _schedulerContainer;
        private SchedulerPiece.ScoreCollector.SchedulerPieceScoreCollectorController _schedulerScoring;
        private PersonPieceSystemController _pieceSystem;
        private BoardController _board;

        private SelectionController _selector;

        public bool IsZeroExist { get => _schedulerContainer.GetAllPieces().Any(sch => sch.Model.CurrentValue == 0); }

        public bool IsActive { get; private set; }
        public void SetActive(bool value)
        {
            IsActive = value;
        }

        public void GenerateValidMovementPlace(bool zeroExist, bool highlightPlace = true, bool removePrevHighlight = true)
        {
            if (!IsActive) return;

            var cells = _board.GetAllCells();

            var piecePos = _pieceSystem.PiecePosition;
            var cell = _board.GetCellAtPosition(piecePos.x, piecePos.y);
            int cellIndex = cells.IndexOf(cell);

            BoardHelper.SetBoardProperties(_board);
            int[] validMoveCellIndex = zeroExist ? TwoSurroundingCluster(cellIndex) : OneSurroundingCluster(cellIndex);

            if (removePrevHighlight)
            {
                RemoveHighlight(cells);
            }

            _selector.SetSelectedIndex(validMoveCellIndex);

            if (highlightPlace)
            {
                ShowHighlight(cells);
            }
        }

        public List<int> GetValidMovementPlace() => _selector.GetSelectedIndex();

        public void ShowHighlight()
        {
            ShowHighlight(_board.GetAllCells());
        }

        public void RemoveHighlight()
        {
            RemoveHighlight(_board.GetAllCells());
        }

        private void ShowHighlight(List<CellController> cells)
        {
            foreach (int i in _selector.GetSelectedIndex())
            {
                if (i < 0) continue;
                cells[i].SetBackgroundColor(_view.Data.SelectedColor);
            }
        }

        private void RemoveHighlight(List<CellController> cells)
        {
            foreach (int i in _selector.GetSelectedIndex())
            {
                if (i < 0) continue;
                cells[i].SetBackgroundColor(_cellInstantiator.GetDefaultBackgroundColor());
            }
        }

        public IEnumerator OnInitSceneObject(PersonPieceSelectionView view)
        {
            SetView(view);
            _selector = new SelectionController();
            yield return null;
        }

        public IEnumerator OnLaunchScene()
        {
            IsActive = true;
            GenerateValidMovementPlace(IsZeroExist, false, false);

            var cells = _board.GetAllCells();

            cells.ForEach(cell =>
            {
                cell.AddOnPointerUpHandler(_ =>
                {
                    if (!IsActive) return;

                    int index = cells.IndexOf(cell);
                    if (_selector.GetSelectedIndex().Contains(index))
                    {
                        _pieceSystem.SetPiecePosition(cell.Pos);

                        GenerateValidMovementPlace(IsZeroExist, highlightPlace:false, removePrevHighlight:true);

                        var sch = cell.GetScheduler(_schedulerContainer.GetAllPieces());
                        if (sch != null)
                        {
                            _schedulerScoring.CollectPointByPerson(sch);
                        }

                        Publish(new GameplayDoneActionMessage(Turn.Person));
                    }
                });
            });

            yield return null;
        }

        private int[] TwoSurroundingCluster(int cellIndex) => new int[]
            {
                cellIndex.Up(2).Left(2), cellIndex.Up(2).Left(), cellIndex.Up(2), cellIndex.Up(2).Right(), cellIndex.Up(2).Right(2),
                cellIndex.Up().Left(2), cellIndex.Up().Left(), cellIndex.Up(), cellIndex.Up().Right(), cellIndex.Up().Right(2),
                cellIndex.Left(2), cellIndex.Left(), cellIndex.Right(), cellIndex.Right(2),
                cellIndex.Down().Left(2), cellIndex.Down().Left(), cellIndex.Down(), cellIndex.Down().Right(), cellIndex.Down().Right(2),
                cellIndex.Down(2).Left(2), cellIndex.Down(2).Left(), cellIndex.Down(2), cellIndex.Down(2).Right(), cellIndex.Down(2).Right(2)
            };

        private int [] OneSurroundingCluster(int cellIndex) => new int[]
            {
                cellIndex.Up().Left(), cellIndex.Up(), cellIndex.Up().Right(),
                cellIndex.Left(), cellIndex.Right(),
                cellIndex.Down().Left(), cellIndex.Down(), cellIndex.Down().Right(),
            };
    }

}
