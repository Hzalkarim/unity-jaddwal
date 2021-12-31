using Agate.MVC.Base;
using Jaddwal.Board;
using Jaddwal.Cell;
using Jaddwal.Core.MVC;
using Jaddwal.Utility.Board;
using Jaddwal.Utility.Scheduler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Jaddwal.SchedulerPiece.BoardFilter
{
    public class SchedulerBoardFilterController : ObjectController<SchedulerBoardFilterController, SchedulerBoardFilterView>, ISystemController<SchedulerBoardFilterView>
    {
        private BoardController _board;
        private TeamManager.SchedulerTeamController _teamManager;
        private Selector.SchedulerSelectionController _selector;
        private Container.SchedulerContainerController _container;

        private List<List<int>> _filteredCellIndex;

        private List<CellViewEvent> _inhibitors = new List<CellViewEvent>();
        private List<CellViewEvent> _uninhibitors = new List<CellViewEvent>();

        public void InitDummy()
        {
            _filteredCellIndex = new List<List<int>>();

            _filteredCellIndex.Add(new List<int>() { 1, 2, 5, 10 });
            _filteredCellIndex.Add(new List<int>() { 10, 20, 30, 40 });
        }

        public List<int> GetFilteredCells(int teamIndex)
        {
            return _filteredCellIndex[teamIndex];
        }

        public void SetFilteredCells(int teamIndex, List<int> cellsIndex)
        {
            _filteredCellIndex[teamIndex] = cellsIndex;
        }

        public void ClearFilteredCells(int teamIndex)
        {
            _filteredCellIndex[teamIndex].Clear();
        }

        public void HighlightFilteredCell(int team)
        {
            var cells = _board.GetAllCells();
            foreach (int i in _filteredCellIndex[team])
            {
                cells[i].SetBackgroundImage(_view.Data.FilteredSprite);
            }
        }

        public void AddFilterMark(int team, CellController cell)
        {
            for (int i = 0; i < 2; i++)
            {
                string name = team == 0 ? "FilterBlue" : "FilterGreen";
                if (cell.GetTransform().GetChild(i).name == name)
                    return;
            }

            GameObject filter = null;
            if (team == 0)
            {
                filter = Object.Instantiate(_view.Data.FilterBlue);
                filter.name = "FilterBlue";
            }
            else if (team == 1)
            {
                filter = Object.Instantiate(_view.Data.FilterGreen);
                filter.name = "FilterGreen";
            }

            filter.transform.SetParent(cell.GetTransform());
            filter.transform.SetAsFirstSibling();

            var rect = filter.GetComponent<RectTransform>();
            rect.offsetMax = Vector2.zero;
            rect.offsetMin = Vector2.zero;
            rect.localScale = Vector3.one;
        }

        public void RemoveFilterMark(int team, CellController cell)
        {
            for (int i = 0; i < 2; i++)
            {
                string name = team == 0 ? "FilterBlue" : "FilterGreen";
                if (cell.GetTransform().GetChild(i).name == name)
                {
                    Object.Destroy(cell.GetTransform().GetChild(i).gameObject);
                }
            }
        }

        public void UnhighlightFilteredCell(int team)
        {
            var cells = _board.GetAllCells();
            foreach (int i in _filteredCellIndex[team])
            {
                cells[i].SetBackgroundImage(null);
            }
        }

        public void ActivateFilterCell(int teamIndex)
        {
            var cells = _board.GetAllCells();

            _inhibitors.Clear();
            _uninhibitors.Clear();
            int j = 0;
            foreach (int i in _filteredCellIndex[teamIndex])
            {
                _inhibitors.Add(_ => InhibitSchedulerSelector(teamIndex, cells[i]));
                cells[i].AddOnPointerEnterHandler(_inhibitors[j]);

                _uninhibitors.Add(_ => UninhibitSchedulerSelector(teamIndex, cells[i]));
                cells[i].AddOnPointerExitHandler(_uninhibitors[j]);
                j++;
            }
            Debug.Log($"Activate Filter Cell Team:{teamIndex}");
        }

        public void DeactivateFilterCell(int teamIndex)
        {
            var cells = _board.GetAllCells();

            int j = 0;
            foreach (int i in _filteredCellIndex[teamIndex])
            {
                cells[i].RemoveOnPointerEnterHandler(_inhibitors[j]);

                cells[i].RemoveOnPointerExitHandler(_uninhibitors[j]);

                j++;
            }
            Debug.Log($"Deactivate Filter Cell Team:{teamIndex}");
        }

        public void ApplySchedulerFilter(SchedulerController scheduler)
        {
            var pieces = _container.GetAllPieces();
            if (pieces.Count == 0) return;

            BoardHelper.SetBoardProperties(_board);

            var cells = _board.GetAllCells();
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].IsEmpty()) continue;

                var sch = cells[i].GetScheduler(pieces);
                if (sch != null && sch == scheduler)
                {
                    int team = _teamManager.GetTeamIndex(sch);

                    switch (sch.Model.MaxValue)
                    {
                        case 2:
                            AddFilter(team, Own(i));
                            break;
                        case 4:
                            AddFilter(team, PlusShape(i));
                            break;
                        case 8:
                            AddFilter(team, OneCluster(i));
                            break;
                        case 16:
                            AddFilter(team, DiamondShape(i));
                            break;
                        default:
                            AddFilter(team, TwoCluster(i));
                            break;
                    }

                    break;
                }
            }

            UpdateFilterMark();
        }

        private void UpdateFilterMark()
        {
            var cells = _board.GetAllCells();

            foreach (int i in _filteredCellIndex[0])
            {
                AddFilterMark(0, cells[i]);
            }

            foreach (int i in _filteredCellIndex[1])
            {
                AddFilterMark(1, cells[i]);
            }
        }

        private void InhibitSchedulerSelector(int teamIndex, CellController cell)
        {
            if (_teamManager.ActiveTeamIndex == teamIndex)
            {
                _selector.SetActive(false);
                cell.SetBackgroundColor(Color.red);
            }
        }

        private void UninhibitSchedulerSelector(int teamIndex, CellController cell)
        {
            if (_teamManager.ActiveTeamIndex == teamIndex)
            {
                cell.SetBackgroundColor(Color.white);
                _selector.SetActive(true);
            }
        }

        private void AddFilter(int team, int[] shape)
        {
            var filter = _filteredCellIndex[team];
            filter.AddRange(shape);
            _filteredCellIndex[team] = filter.Distinct().Where(num => num >= 0).ToList();
        }

        private int[] TwoCluster(int pivot) => new int[]
        {
            pivot.Up(2).Left(2), pivot.Up(2).Left(), pivot.Up(2), pivot.Up(2).Right(), pivot.Up(2).Right(2),
            pivot.Up().Left(2), pivot.Up().Left(), pivot.Up(), pivot.Up().Right(), pivot.Up().Right(2),
            pivot.Left(2), pivot.Left(),pivot, pivot.Right(), pivot.Right(2),
            pivot.Down().Left(2), pivot.Down().Left(), pivot.Down(), pivot.Down().Right(), pivot.Down().Right(2),
            pivot.Down(2).Left(2), pivot.Down(2).Left(), pivot.Down(2), pivot.Down(2).Right(), pivot.Down(2).Right(2),
        };

        private int[] DiamondShape(int pivot) => new int[]
        {
            pivot.Up(2),
            pivot.Up().Left(), pivot.Up(), pivot.Up().Right(),
            pivot.Left(2), pivot.Left(),pivot, pivot.Right(), pivot.Right(2),
            pivot.Down().Left(), pivot.Down(), pivot.Down().Right(),
            pivot.Down(2)
        };

        private int[] OneCluster(int pivot) => new int[] 
        {
            pivot.Up().Left(), pivot.Up(), pivot.Up().Right(),
            pivot.Left(),pivot, pivot.Right(),
            pivot.Down().Left(), pivot.Down(), pivot.Down().Right()
        };

        private int[] PlusShape(int pivot) => new int[]
        {
            pivot.Up(),
            pivot.Left(), pivot, pivot.Right(),
            pivot.Down(),
        };

        private int[] Own(int pivot) => new int[]
        {
            pivot
        };

        public IEnumerator OnLaunchScene()
        {
            _filteredCellIndex = new List<List<int>>();

            int size = _board.GetSize().x * _board.GetSize().y;

            //List<int> blueFilter = Enumerable.Range(0, size).Where(i => i % 2 == 0).ToList();
            //List<int> greenFilter = Enumerable.Range(0, size).Where(i => i % 2 == 1).ToList();

            List<int> blueFilter = new List<int>();
            List<int> greenFilter = new List<int>();

            _filteredCellIndex.Add(blueFilter);
            _filteredCellIndex.Add(greenFilter);

            yield return null;
        }

        public IEnumerator OnInitSceneObject(SchedulerBoardFilterView view)
        {
            SetView(view);
            yield return null;
        }
    }

}
