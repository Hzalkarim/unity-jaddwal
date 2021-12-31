using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.Cell
{
    public interface ICellContent
    {
        public Transform GetTransform();
        public RectTransform GetRectTransform();

        public bool CompareContent(View another);
    }

}
