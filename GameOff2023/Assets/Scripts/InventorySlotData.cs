using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventorySlotData : ScriptableObject
{
    public GameObject spawnPrefab;
    public Sprite backgroundSprite;
    public Color32 mainColor;
    public Color32 remainingTextColor;
    public Color32 remainingBackgroundColor;
}
