using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.Board
{
    public class BoardView : BaseView
    {
        [SerializeField]
        private BoardViewData _data;
        public BoardViewData Data => _data;
    }

    [System.Serializable]
    public class BoardViewData
    {
        public int sizeX;
        public int sizeY;
        public float cellLength;
    }

}
