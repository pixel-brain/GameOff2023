using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject nodePrefab;
    public Transform nodeSpawnPoint;
    public TextMeshProUGUI remainingText;
    public Image remainingBackgroundImage;
    public Image mainImage;

    private int nodesRemaining;
    private GameObject currentNode;

    // Called when InventorySpawner creates the nodes
    public void Setup(InventorySlotData slotData, int count)
    {
        // Spawn node
        nodesRemaining = count;
        nodePrefab = slotData.spawnPrefab;
        currentNode = Instantiate(nodePrefab, nodeSpawnPoint.position, Quaternion.identity, transform);
        // Setup display
        remainingText.text = count.ToString();
        remainingText.color = slotData.remainingTextColor;
        remainingBackgroundImage.color = slotData.remainingBackgroundColor;
        mainImage.color = slotData.mainColor;
        mainImage.sprite = slotData.backgroundSprite;
    }

    // Called by nodes when they are placed in the node area
    public void NodeUsed(GameObject node)
    {
        if (node == currentNode)
        {
            nodesRemaining--;
            remainingText.text = nodesRemaining.ToString();
            if (nodesRemaining > 0)
            {
                currentNode = Instantiate(nodePrefab, nodeSpawnPoint.position, Quaternion.identity, transform);
            }
        }
    }
}
