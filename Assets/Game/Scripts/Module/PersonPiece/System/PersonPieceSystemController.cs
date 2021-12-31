using Agate.MVC.Base;
using Jaddwal.Board;
using Jaddwal.Core.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.PersonPiece.System
{
    public class PersonPieceSystemController : ObjectController<PersonPieceSystemController, PersonPieceSystemView>, ISystemController<PersonPieceSystemView>
    {
        private BoardController _board;

        private PersonPieceController _piece;
        public PersonPieceController GetPiece() => _piece;

        public Vector2Int PiecePosition { get; private set; }

        public void SetPiecePosition(Vector2Int pos)
        {
            var cell = _board.GetCellAtPosition(pos.x, pos.y);
            cell.AddContent(_piece);
            PiecePosition = pos;
        }

        public IEnumerator OnInitSceneObject(PersonPieceSystemView view)
        {
            SetView(view);

            var piece = new PersonPieceController();
            var pieceView = Object.Instantiate(_view.Data.prefab);
            piece.SetView(pieceView);

            _piece = piece;
            yield return null;
        }

        public IEnumerator OnLaunchScene()
        {
            var pos = _view.Data.StartingPosition;
            SetPiecePosition(pos);

            PiecePosition = pos;

            yield return null;
        }
    }

}
