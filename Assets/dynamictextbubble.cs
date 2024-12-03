using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; 

    public void UpdateText(string newText)
    {
        textMeshPro.text = newText;
    }
}
