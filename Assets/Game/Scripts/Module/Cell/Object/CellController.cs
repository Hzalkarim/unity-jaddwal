using Agate.MVC.Base;
using Agate.MVC.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Jaddwal.Cell
{
    public class CellController : ObjectController<CellController, CellModel, ICellModel, CellView>
    {


        public bool IsActivateEvent
        {
            get
            {
                return _view.IsActivateEvent;
            }
            set
            {
                _view.IsActivateEvent = value;
            }
        }
        public Vector2Int Pos { get { return new Vector2Int(Model.PosX, Model.PosY); } }
        
        #region SETTER
        public void SetPosition(int posX, int posY, float length)
        {
            _model.SetPosition(posX, posY, length);
        }

        public void SetName(string name)
        {
            _view.gameObject.name = name;
        }

        public void SetBackgroundImage(Sprite sprite)
        {
            _view.SetBackgroundImage(sprite);
        }

        public void SetEdgeImage(Sprite sprite)
        {
            _view.SetEdgeImage(sprite);
        }

        public void SetBackgroundColor(Color color)
        {
            _view.SetBackgroundColor(color);
        }

        public void SetEdgeColor(Color color)
        {
            _view.SetEgdeColor(color);
        }
        #endregion

        public void AddContent(ICellContent content)
        {
            Transform contentTransform = content.GetTransform();
            contentTransform.SetParent(_view.GetContentTransform());

            RectTransform rectTrans = content.GetRectTransform();
            rectTrans.offsetMax = Vector2.one;
            rectTrans.offsetMin = Vector2.zero;
            rectTrans.localScale = Vector2.one;
        }

        public T GetCellContent<T>() where T : View
        {
            return _view.GetContentTransform().GetComponentInChildren<T>();
        }

        public bool IsEmpty()
        {
            return _view.GetContentTransform().childCount == 0;
        }

        public Transform GetTransform() => _view?.transform;

        #region EVENT HANDLER
        public void AddOnPointerUpHandler(CellViewEvent action)
        {
            _view.OnPointerUpEvent += action;
        }

        public void RemoveOnPointerUpHandler(CellViewEvent action)
        {
            _view.OnPointerUpEvent -= action;
        }

        public void AddOnPointerDownHandler(CellViewEvent action)
        {
            _view.OnPointerDownEvent += action;
        }

        public void RemoveOnPointerUDownHandler(CellViewEvent action)
        {
            _view.OnPointerDownEvent -= action;
        }

        public void AddOnPointerEnterHandler(CellViewEvent action)
        {
            _view.OnPointerEnterEvent += action;
        }

        public void RemoveOnPointerEnterHandler(CellViewEvent action)
        {
            _view.OnPointerEnterEvent -= action;
        }

        public void AddOnPointerExitHandler(CellViewEvent action)
        {
            _view.OnPointerExitEvent += action;
        }

        public void RemoveOnPointerExitHandler(CellViewEvent action)
        {
            _view.OnPointerExitEvent -= action;
        }
        #endregion

    }
}
