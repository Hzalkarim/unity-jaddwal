using Jaddwal.Board;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.Utility.Board
{
    public static class BoardHelper
    {
        public static int CellCount { get; private set; }
        public static int SizeX { get; private set; }
        public static int SizeY { get; private set; }

        public static void SetBoardProperties(BoardController board)
        {
            Vector2Int size = board.GetSize();
            SizeY = size.y;
            SizeX = size.x;
            CellCount = size.x * size.y;

        }

        public static int Up(this int pivot)
        {
            if (ValidateUp(pivot))
                return pivot + SizeX;
            else
                return -1;
        }

        public static int Up(this int pivot, int step)
        {
            if (step < 1) return -1;

            int newPivot = pivot;
            for (int i = 0; i < step; i++)
            {
                newPivot = newPivot.Up();
            }
            return newPivot;
        }

        public static int Down(this int pivot)
        {
            if (ValidateDown(pivot))
                return pivot - SizeX;
            else
                return -1;
        }
        public static int Down(this int pivot, int step)
        {
            if (step < 1) return -1;

            int newPivot = pivot;
            for (int i = 0; i < step; i++)
            {
                newPivot = newPivot.Down();
            }
            return newPivot;
        }

        public static int Right(this int pivot)
        {
            if (ValidateRight(pivot))
                return pivot + 1;
            else
                return -1;
        }
        public static int Right(this int pivot, int step)
        {
            if (step < 1) return -1;

            int newPivot = pivot;
            for (int i = 0; i < step; i++)
            {
                newPivot = newPivot.Right();
            }
            return newPivot;
        }

        public static int Left(this int pivot)
        {
            if (ValidateLeft(pivot))
                return pivot - 1;
            else
                return -1;
        }
        public static int Left(this int pivot, int step)
        {
            if (step < 1) return -1;

            int newPivot = pivot;
            for (int i = 0; i < step; i++)
            {
                newPivot = newPivot.Left();
            }
            return newPivot;
        }

        private static bool ValidateUp(int pivot)
        {
            if (pivot < 0)
                return false;
            int y = pivot / SizeX;
            return y < SizeY - 1;
        }

        private static bool ValidateDown(int pivot)
        {
            if (pivot < 0)
                return false;
            int y = pivot / SizeX;
            return y > 0;
        }

        private static bool ValidateRight(int pivot)
        {
            if (pivot < 0)
                return false;
            int x = pivot % SizeX;
            return x < SizeX - 1;
        }

        private static bool ValidateLeft(int pivot)
        {
            if (pivot < 0)
                return false;
            int x = pivot % SizeX;
            return x > 0;
        }
    }

}
