using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jaddwal.SchedulerPiece.Selector.Mono
{
    public class SchedulerSelectionViewMono : MonoBehaviour
    {
        public event Action<int> OnValueSelectedEvent;

        public List<Button> Buttons;

        public void OnValueSelected(int value)
        {
            OnValueSelectedEvent?.Invoke(value);
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }
        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
            var rect = GetComponent<RectTransform>();
            rect.localScale = Vector2.one;
        }
    }
}
