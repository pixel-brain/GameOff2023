using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

public class InventorySpawner : MonoBehaviour
{
    public SlotDataIntDictionary inventorySlots; // Custom Serializable Dictionary
    public GameObject inventorySlotPrefab;
    public Vector2 slotStartPosition;
    public float verticalSpacing;
    public Transform nodeArea;

    private RectTransform rectTransform;
    [Header("For Gizmos Node Spawn Guide Only")]
    public Vector2 estimatedSlotBounds;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        Vector2 spawnPos = FirstNodeSpawnPosition();
        foreach (var slotPair in inventorySlots)
        {
            if (slotPair.Value > 0)
            {
                GameObject spawnedSlot = Instantiate(inventorySlotPrefab, spawnPos, Quaternion.identity, nodeArea);
                spawnedSlot.GetComponent<InventorySlot>().Setup(slotPair.Key, slotPair.Value);
            }
            spawnPos.y -= verticalSpacing;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        rectTransform = GetComponent<RectTransform>();
        Vector2 spawnPos = FirstNodeSpawnPosition();
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Gizmos.DrawWireCube(spawnPos, estimatedSlotBounds);
            spawnPos.y -= verticalSpacing;
        }
    }

    private Vector2 FirstNodeSpawnPosition()
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        return new Vector2(rectTransform.position.x + slotStartPosition.x, rectTransform.position.y + Mathf.Abs(corners[0].y) - slotStartPosition.y);
    }
}
