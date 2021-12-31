using Agate.MVC.Base;
using Jaddwal.Core.MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaddwal.SchedulerPiece.Selector
{
    public class SchedulerSelectionPickerController : ObjectController<SchedulerSelectionPickerController, SchedulerSelectionPickerView>
    {

        public void SetPosition(Vector3 pos)
        {
            _view.transform.position = pos;
        }

        public void SetParent(Transform parent)
        {
            _view.transform.SetParent(parent);
            _view.GetComponent<RectTransform>().localScale = Vector2.one;
        }

        public void SetActive(bool value)
        {
            _view.gameObject.SetActive(value);
        }

        public Transform GetTransform() => _view.transform;

        public void AddOnButtonClickHandler(int i, Action action)
        {
            _view.AddOnButtonClickHandler(i, action);
        }
    }

}
