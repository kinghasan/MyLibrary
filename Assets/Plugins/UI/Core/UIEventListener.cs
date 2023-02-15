using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Aya.UI
{
    public class UIEventListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler,
        IPointerUpHandler, ISelectHandler, IUpdateSelectedHandler, IDeselectHandler, IDropHandler, IMoveHandler,
        IBeginDragHandler, IInitializePotentialDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
    {
        public delegate void UIPointerEventDelegate(GameObject go, PointerEventData data);
        public delegate void UIBaseEventDelegate(GameObject go, BaseEventData data);
        public delegate void UIAxisEventDelegate(GameObject go, AxisEventData data);

        public UIPointerEventDelegate onClick = delegate { };
        public UIPointerEventDelegate onDoubleClick = delegate { };
        public UIPointerEventDelegate onDown = delegate { };
        public UIPointerEventDelegate onEnter = delegate { };
        public UIPointerEventDelegate onExit = delegate { };
        public UIPointerEventDelegate onUp = delegate { };
        public UIBaseEventDelegate onSelect = delegate { };
        public UIBaseEventDelegate onUpdateSelect = delegate { };
        public UIBaseEventDelegate onDeSelect = delegate { };
        public UIPointerEventDelegate onDrag = delegate { };
        public UIPointerEventDelegate onInitializePotentialDrag = delegate { };
        public UIPointerEventDelegate onBeginDrag = delegate { };
        public UIPointerEventDelegate onEndDrag = delegate { };
        public UIPointerEventDelegate onDrop = delegate { };
        public UIPointerEventDelegate onScroll = delegate { };
        public UIAxisEventDelegate onMove = delegate { };

        /// <summary>
        /// UIEventListener »º´æ×Öµä£¬¼õÉÙ GetComponent µ÷ÓÃ
        /// </summary>
        protected static Dictionary<GameObject, UIEventListener> UIEventListenerCacheDic = new Dictionary<GameObject, UIEventListener>();

        public static UIEventListener Get(GameObject uiGameObject)
        {
            if (UIEventListenerCacheDic.TryGetValue(uiGameObject, out var ret))
            {
                return ret;
            }

            ret = uiGameObject.GetComponent<UIEventListener>();
            if (ret == null)
            {
                ret = uiGameObject.AddComponent<UIEventListener>();
            }

            UIEventListenerCacheDic.Add(uiGameObject, ret);

            return ret;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag(gameObject, eventData);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            onDeSelect(gameObject, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag(gameObject, eventData);
        }

        public void OnDrop(PointerEventData eventData)
        {
            onDrop(gameObject, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag(gameObject, eventData);
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            onInitializePotentialDrag(gameObject, eventData);
        }

        public void OnMove(AxisEventData eventData)
        {
            onMove(gameObject, eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick(gameObject, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onDown(gameObject, eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onEnter(gameObject, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onExit(gameObject, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onUp(gameObject, eventData);
        }

        public void OnScroll(PointerEventData eventData)
        {
            onScroll(gameObject, eventData);
        }

        public void OnSelect(BaseEventData eventData)
        {
            onSelect(gameObject, eventData);
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            onUpdateSelect(gameObject, eventData);
        }
    }
}
