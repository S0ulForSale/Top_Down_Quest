using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PressRelease : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler
{
    [HideInInspector] public UnityEvent<bool> press;

    public void OnPointerDown(PointerEventData eventData)
    {
        press.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        press.Invoke(false);
    }

    public void OnDisable()
    {
        press.Invoke(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        press.Invoke(false);
    }
}
