using Agate.MVC.Base;
using Jaddwal.Board;
using Jaddwal.Core.MVC;
using Jaddwal.SchedulerPiece.TeamManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Jaddwal.Utility.Board;


namespace Jaddwal.GameplaySequence.Selector
{
    public class SelectionController : BaseController<SelectionController>
    {
        private List<int> _selectedIndex = new List<int>();

        public void SetSelectedIndex(int[] index)
        {
            ClearSelectedIndex();
            _selectedIndex.AddRange(index);
        }

        public List<int> GetSelectedIndex()
        {
            return _selectedIndex;
        }

        public void ClearSelectedIndex()
        {
            _selectedIndex.Clear();
        }
    }

}
