using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShoppingListManager : MonoBehaviour
{
    public TextMeshProUGUI [] itemTexts; // Array of Text elements representing the items in the menu

    private List<string> collectedItems; // List of collected item names

    void Start()
    {
        // Initialize the collectedItems list
        collectedItems = new List<string>();

        // Update the menu UI at the start
        UpdateShoppingListUI();
    }

    // Update the menu UI to reflect the collected items
    void UpdateShoppingListUI()
    {
        foreach (TextMeshProUGUI itemText in itemTexts)
        {
            // If the item is collected, cross it off
            if (collectedItems.Contains(itemText.text))
            {
                itemText.text = "<s>" + itemText.text + "</s>";
            }
        }
    }

    // Method to collect an item
    public void CollectItem(string itemName)
    {
        collectedItems.Add(itemName);
        UpdateShoppingListUI();
    }

    //Undo changes if restart game
    public void ResetShoppingList()
    {
        // Clear the collected items list
        collectedItems.Clear();

        // Reset the text of all items (remove strikethrough formatting)
        foreach (TextMeshProUGUI itemText in itemTexts)
        {
            itemText.text = itemText.text.Replace("<s>", "").Replace("</s>", "");
        }
    }
}

