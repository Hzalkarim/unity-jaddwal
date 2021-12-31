using Agate.MVC.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.PersonPiece.Selector
{
    public class PersonPieceSelectionView : BaseView
    {
        [SerializeField]
        private PersonPieceSelectionViewData _data;
        public PersonPieceSelectionViewData Data => _data;
    }

    [Serializable]
    public class PersonPieceSelectionViewData
    {
        public Color SelectedColor;
    }

}
