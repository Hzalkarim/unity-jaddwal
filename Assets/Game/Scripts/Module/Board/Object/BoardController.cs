using Agate.MVC.Base;
using Jaddwal.Core.MVC;
using Jaddwal.Cell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jaddwal.Cell.System;

namespace Jaddwal.Board
{
    public class BoardController : ObjectController<BoardController, BoardView>, ISystemController<BoardView>
    {
        private Vector2Int _size = new Vector2Int();
        private List<CellController> _cells;

        private CellInstantiatorController _cellSystem;

        public bool IsSizeMatch { get; private set; }

        public void SetSize(int sizeX, int sizeY)
        {
            var lastSize = _size;
            _size = new Vector2Int(sizeX, sizeY);

            if (lastSize != _size)
                IsSizeMatch = false;
        }

        public Vector2Int GetSize()
        {
            return _size;
        }

        public CellController GetCellAtPosition(float x, float y)
        {
            if (IsPositionValid(x, y))
                return _cells.Find(c => c.Model.PosX == x && c.Model.PosY == y);
            else
                return null;
        }

        public List<CellController> GetAllCells()
        {
            return _cells;
        }

        public bool IsPositionValid(float x, float y)
        {
            if (IsSizeMatch)
            {
                return _size.x >= Mathf.Abs(x) && _size.y >= Mathf.Abs(y);
            }
            else
            {
                return false;
            }
        }

        public void SetIsActivateEvent(bool value)
        {
            _cells.ForEach(cell => cell.IsActivateEvent = value);
        }

        public void GenerateBoard(int sizeX, int sizeY)
        {
            SetSize(sizeX, sizeY);
            GenerateBoard();
        }

        public void GenerateBoard()
        {
            float l = _view.Data.cellLength;
            float centerX = _size.x * l / -2 + .5f * l;
            float centerY = _size.y * l / -2 + .5f * l;
            for (int i = 0; i < _size.y; i++)
            {
                for (int j = 0; j < _size.x; j++)
                {
                    var cell = _cellSystem.InstantiateCell(j , i, l, _view.transform);
                    //cell.AddOnPointerUpHandler((_) => { Debug.Log($"Cell:{cell.Model.PosX}-{cell.Model.PosY}"); });
                    _cells.Add(cell);
                }
            }

            _view.GetComponent<RectTransform>().anchoredPosition = new Vector2(centerX, centerY);
            //_view.transform.position = new Vector3(centerX, centerY);
            IsSizeMatch = true;
        }

        public IEnumerator OnInitSceneObject(BoardView view)
        {
            _cells = new List<CellController>(_size.x * _size.y);
            SetView(view);
            yield return null;
        }

        public IEnumerator OnLaunchScene()
        {
            var data = _view.Data;
            GenerateBoard(data.sizeX, data.sizeY);
            yield return null;
        }
    }
}
