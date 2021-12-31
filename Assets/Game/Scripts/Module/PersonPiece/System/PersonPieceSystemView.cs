using Agate.MVC.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.PersonPiece.System
{
    public class PersonPieceSystemView : BaseView
    {

        [SerializeField]
        private PersonPieceSystemViewData _data;
        public PersonPieceSystemViewData Data => _data;
    }

    [Serializable]
    public class PersonPieceSystemViewData
    {
        public PersonPieceView prefab;
        public Vector2Int StartingPosition;
    }
}
