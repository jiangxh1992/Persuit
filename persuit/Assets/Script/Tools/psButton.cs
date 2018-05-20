using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class psButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name == "btn_left")
        {
            InputEventControlller.Ins.OnLeftDown();
        }
        else if (gameObject.name == "btn_right")
        {
            InputEventControlller.Ins.OnRightDown();
        }
        else if (gameObject.name == "btn_jump")
        {
            InputEventControlller.Ins.OnUpArrowDown();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (gameObject.name == "btn_left")
        {
            InputEventControlller.Ins.OnLeftUp();
        }
        else if (gameObject.name == "btn_right")
        {
            InputEventControlller.Ins.OnRightUp();
        }
        else if (gameObject.name == "btn_jump")
        {
            InputEventControlller.Ins.OnUpArrowUp();
        }
    }
}
