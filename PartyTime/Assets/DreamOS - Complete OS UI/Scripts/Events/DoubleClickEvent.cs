﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Michsky.DreamOS
{
    [AddComponentMenu("DreamOS/Events/Double Click Event")]
    public class DoubleClickEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        // Settings
        public bool enableSingleClick;
        public float timeFactor = 0.3f;

        // Events
        public UnityEvent doubleClickEvents;
        public UnityEvent singleClickEvents;

        bool active;
        bool oneClick = false;
        float timerForDoubleClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (enableSingleClick == true)
                singleClickEvents.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            active = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            active = false;
        }

        void Update()
        {
            if (active == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (oneClick == false)
                    {
                        oneClick = true;
                        timerForDoubleClick = Time.time;
                    }

                    else
                    {
                        oneClick = false;
                        doubleClickEvents.Invoke();
                    }
                }

                else if (oneClick == true && (Time.time - timerForDoubleClick) > timeFactor)
                    oneClick = false;
            }
        }
    }
}