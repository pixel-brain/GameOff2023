using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableNode : Draggable
{
    private RectTransform draggableArea;
    private bool usedFromInventory;
    private bool inDraggableArea;
    private Vector3 startPosition;
    private InventorySlot inventorySlot;

    protected override void Start()
    {
        base.Start();
        // Setup
        draggableArea = GameObject.FindWithTag("NodeArea").GetComponent<RectTransform>();
        inventorySlot = transform.parent.GetComponent<InventorySlot>();
        Vector3 startingPosition = transform.position;
        transform.SetParent(draggableArea, false);
        transform.position = startingPosition;
        startPosition = transform.position;
    }

    public override void OnPointerDown(BaseEventData eventData)
    {
        base.OnPointerDown(eventData);
        // Move node on top
        transform.SetAsLastSibling();
    }

    public override void OnDrag(BaseEventData eventData)
    {
        base.OnDrag(eventData);
        if (!usedFromInventory)
        {
            // "Use" the node from the inventory once the pointer enters the draggable area
            Vector3 pointerPosition = GetPointerPosition(eventData);
            if (draggableArea.rect.Contains(draggableArea.InverseTransformPoint(pointerPosition)))
            {
                usedFromInventory = true;
                inventorySlot.NodeUsed(gameObject);
            }
        }
        else
        {
            // Lock node inside draggable area only after it has completely entered
            if (!inDraggableArea)
            {
                inDraggableArea = IsInArea(draggableArea);
            }
            else
            {
                RestrictToArea(draggableArea);
            }
        }
    }

    public override void OnPointerUp(BaseEventData eventData)
    {
        base.OnPointerUp(eventData);
        // If used, snap node inside the draggable area
        if (usedFromInventory)
        {
            inDraggableArea = true;
            RestrictToArea(draggableArea);
        }
        // Else, return it to its spot in the inventory
        else
        {
            transform.position = startPosition;
        }
    }
}
