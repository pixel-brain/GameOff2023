using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    private Canvas canvas;
    private RectTransform draggableArea, rectTransform;
    private Vector3 clickOffset;

    private void Start()
    {
        canvas = transform.root.GetComponent<Canvas>();
        draggableArea = transform.parent.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        Vector3 pointerPosition = GetPointerPosition(eventData);

        transform.SetAsLastSibling();
        clickOffset = transform.position - pointerPosition;
    }

    public void OnDrag(BaseEventData eventData)
    {
        Vector3 pointerPosition = GetPointerPosition(eventData);

        Vector3 dragPosition = pointerPosition + clickOffset;

        transform.position = dragPosition;
        RestrictToArea(draggableArea);
    }

    public void OnDrop(BaseEventData eventData)
    {

    }

    private void RestrictToArea(RectTransform areaRectTransform)
    {
        Vector2 bounds = new Vector2(
            (Mathf.Abs(areaRectTransform.sizeDelta.x) - rectTransform.sizeDelta.x) * 0.5f,
            (Mathf.Abs(areaRectTransform.sizeDelta.y) - rectTransform.sizeDelta.y) * 0.5f
            );

        rectTransform.localPosition = new Vector3(
            Mathf.Clamp(rectTransform.localPosition.x, -bounds.x, bounds.x), 
            Mathf.Clamp(rectTransform.localPosition.y, -bounds.y, bounds.y)
            );
    }

    private Vector3 GetPointerPosition(BaseEventData eventData)
    {
        PointerEventData pointerData = (PointerEventData)eventData;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out Vector2 pointerPosition);
        return canvas.transform.TransformPoint(pointerPosition);
    }
}
