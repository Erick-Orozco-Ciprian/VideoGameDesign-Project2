using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneItemManager : MonoBehaviour
{
    void Start()
    {
        // Check if the item represented by this GameObject was collected
        gameObject.SetActive(ShoppingListManager.collectedItems.Contains(gameObject.name));
    }
}
