using Agate.MVC.Base;
using Agate.MVC.Core;
using Jaddwal.Cell;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.PersonPiece
{
    public class PersonPieceController : ObjectController<PersonPieceController, PersonPieceModel, IPersonPieceModel, PersonPieceView>, ICellContent
    {
        public bool CompareContent(View another)
        {
            return _view == another;
        }

        public RectTransform GetRectTransform()
        {
            return _view.GetComponent<RectTransform>();
        }

        public Transform GetTransform()
        {
            return _view.transform;
        }
    }
}
