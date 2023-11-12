using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Draggable : MonoBehaviour
{
    protected Canvas canvas;
    protected RectTransform rectTransform;
    protected Vector3 clickOffset;

    protected virtual void Start()
    {
        canvas = GameObject.FindWithTag("GameBoardCanvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void OnPointerDown(BaseEventData eventData)
    {
        Vector3 pointerPosition = GetPointerPosition(eventData);
        clickOffset = transform.position - pointerPosition;
    }

    public virtual void OnDrag(BaseEventData eventData)
    {
        Vector3 pointerPosition = GetPointerPosition(eventData);
        Vector3 dragPosition = pointerPosition + clickOffset;
        transform.position = dragPosition;
    }

    public virtual void OnPointerUp(BaseEventData eventData)
    {

    }

    protected void RestrictToArea(RectTransform areaRectTransform)
    {
        Vector2 bounds = Bounds(areaRectTransform);
        rectTransform.localPosition = new Vector3(
            Mathf.Clamp(rectTransform.localPosition.x, -bounds.x, bounds.x), 
            Mathf.Clamp(rectTransform.localPosition.y, -bounds.y, bounds.y)
            );
    }

    protected bool IsInArea(RectTransform areaRectTransform)
    {
        Vector2 bounds = Bounds(areaRectTransform);
        return (Mathf.Abs(rectTransform.localPosition.x) < bounds.x && Mathf.Abs(rectTransform.localPosition.y) < bounds.y);
    }

    protected Vector2 Bounds(RectTransform areaRectTransform)
    {
        return new Vector2(
            (Mathf.Abs(areaRectTransform.sizeDelta.x) - rectTransform.sizeDelta.x) * 0.5f,
            (Mathf.Abs(areaRectTransform.sizeDelta.y) - rectTransform.sizeDelta.y) * 0.5f
            );
    }

    protected Vector3 GetPointerPosition(BaseEventData eventData)
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
