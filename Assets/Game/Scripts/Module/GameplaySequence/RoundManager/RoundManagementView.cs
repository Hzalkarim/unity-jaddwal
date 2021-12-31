using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.GameplaySequence.RoundManager
{
    public class RoundManagementView : BaseView
    {
        public void StartRound(IEnumerator round)
        {
            StartCoroutine(round);
        }

        public void StopRound()
        {
            StopAllCoroutines();
        }
    }

}
