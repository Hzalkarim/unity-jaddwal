using Agate.MVC.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Jaddwal.Cell
{
    public delegate void CellViewEvent(PointerEventData eventData);

    public class CellView : ObjectView<ICellModel>, ICellView
    {

        public bool IsActivateEvent { get; set; }

        public event CellViewEvent OnPointerUpEvent;
        public event CellViewEvent OnPointerDownEvent;
        public event CellViewEvent OnPointerEnterEvent;
        public event CellViewEvent OnPointerExitEvent;

        [SerializeField]
        private Image _background;
        [SerializeField]
        private Image _edge;

        [SerializeField]
        private Transform _content;
        [SerializeField]
        private RectTransform _rect;

        protected override void InitRenderModel(ICellModel model)
        {

        }

        protected override void UpdateRenderModel(ICellModel model)
        {
            float l = model.Length;
            _rect.anchoredPosition = new Vector2(model.PosX * l, model.PosY * l);
        }

        #region SETTER
        public void SetBackgroundColor(Color color)
        {
            _background.color = color;
        }

        public void SetEgdeColor(Color color)
        {
            _edge.color = color;
        }

        public void SetBackgroundImage(Sprite sprite)
        {
            _background.sprite = sprite;
        }

        public void SetEdgeImage(Sprite sprite)
        {
            _edge.sprite = sprite;
        }
        #endregion

        #region GETTER

        public Transform GetContentTransform()
        {
            return _content;
        }
        #endregion

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsActivateEvent)
                OnPointerEnterEvent?.Invoke(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (IsActivateEvent)
                OnPointerDownEvent?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsActivateEvent)
                OnPointerExitEvent?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (IsActivateEvent)
                OnPointerUpEvent?.Invoke(eventData);
        }
    }

    public interface ICellView : IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {

    }

}
