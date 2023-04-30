using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter");
        GetComponent<RectTransform>().transform.localScale *= 4;
        gameObject.transform.SetAsLastSibling();
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        GetComponent<RectTransform>().transform.localScale /= 4;
        isOver = false;
    }
}