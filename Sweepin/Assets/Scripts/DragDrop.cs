using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 itemsOriginalVector2;
    private MouseHovering mouseHovering;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        mouseHovering = gameObject.GetComponent<MouseHovering>();
    }
    public void OnDrag(PointerEventData eventData)
    {
       //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
        //print("hej");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseHovering.hoverItemCanvasGameObject.SetActive(false);
        mouseHovering.enabled = false;
        //print($"Pointer pressed at {rectTransform.anchoredPosition}");
        itemsOriginalVector2 = rectTransform.anchoredPosition;
        gameObject.transform.parent = canvas.transform;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //print($"Pointer released at {rectTransform.anchoredPosition}");
        rectTransform.anchoredPosition = itemsOriginalVector2;
        mouseHovering.enabled = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
