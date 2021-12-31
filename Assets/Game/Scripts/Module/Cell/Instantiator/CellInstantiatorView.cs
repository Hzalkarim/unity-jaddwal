using Agate.MVC.Base;
using System;
using UnityEngine;

namespace Jaddwal.Cell.System
{
    public class CellInstantiatorView : BaseView
    {
        [SerializeField]
        private CellSystemViewData _data;
        public CellSystemViewData Data => _data;
    }

    [Serializable]
    public class CellSystemViewData
    {
        public CellView prefab;
        public Color background;
        public Color edge;
    }
}