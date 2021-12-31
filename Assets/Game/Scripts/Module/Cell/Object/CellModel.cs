using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.Cell
{
    public class CellModel : BaseModel, ICellModel
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public float Length { get; private set; }

        public void SetPosition(int x, int y, float l)
        {
            PosX = x;
            PosY = y;
            Length = l;
            SetDataAsDirty();
        }
    }

    public interface ICellModel : IBaseModel
    {
        int PosX { get; }
        int PosY { get; }
        float Length { get; }
    }
}
