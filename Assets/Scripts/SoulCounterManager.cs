using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulCounterManager : MonoBehaviour
{
    public int soulCount = 0;
    public Text soulCounterText;

    private void Start()
    {
        UpdateSoulCounterUI();
    }

    public void IncreaseSoulCount()
    {
        soulCount++; // Increment the soul count.
        UpdateSoulCounterUI(); // Update the UI

        if (soulCount > 2) {
            GameManager.soulCountExceedsThreshold = true;
        }    
    }

    private void UpdateSoulCounterUI()
    {
        // Update the text of the soulCounterText UI element.
        soulCounterText.text = "Souls: " + soulCount;
    }
}
