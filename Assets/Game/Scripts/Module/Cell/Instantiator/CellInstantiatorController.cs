using Agate.MVC.Base;
using Jaddwal.Core.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.Cell.System
{
    public class CellInstantiatorController : ObjectController<CellInstantiatorController, CellInstantiatorView>, ISystemController<CellInstantiatorView>
    {

        public CellController InstantiateCell(int posX, int posY, float length, Transform parent)
        {
            var controller = new CellController();
            controller.SetPosition(posX, posY, length);
            var view = Object.Instantiate(_view.Data.prefab);

            RectTransform rect = view.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(length, length);

            view.transform.SetParent(parent);
            controller.SetView(view);
            rect.localScale = Vector2.one;

            string template = "[{0,3}, {1,3}]";
            controller.SetName(string.Format(template, posX, posY));

            return controller;
        }

        public Color GetDefaultBackgroundColor()
        {
            return _view.Data.background;
        }

        public Color GetDefaultEdgeColor()
        {
            return _view.Data.edge;
        }

        public IEnumerator OnInitSceneObject(CellInstantiatorView view)
        {
            SetView(view);
            yield return null;
        }

        public IEnumerator OnLaunchScene()
        {
            yield return null;
        }
    }
}
