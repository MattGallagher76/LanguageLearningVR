using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Drag your TextMeshPro object here in the Inspector

    public void UpdateText(string newText)
    {
        textMeshPro.text = newText;
    }
}
