using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.Core
{
    public class SceneLauncher : BaseMain<SceneLauncher>, IMain
    {
        protected override IConnector[] GetMainConnectors()
        {
            return null;
        }

        protected override IController[] GetSystemDependencies()
        {
            return null;
        }

        protected override IEnumerator InitSystem()
        {
            return null;
        }
    }
}
