using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPRectTransformToPanel : MonoBehaviour
{
    public RectTransform panelRectTransform; // Assign the Panel's RectTransform
    public TextMeshProUGUI tmpText; // Assign the TMP Text component

    void Start()
    {
        if (panelRectTransform != null && tmpText != null)
        {
            RectTransform tmpRectTransform = tmpText.GetComponent<RectTransform>();
            if (tmpRectTransform != null)
            {
                // Copy size and position from panel to TMP
                tmpRectTransform.anchorMin = panelRectTransform.anchorMin;
                tmpRectTransform.anchorMax = panelRectTransform.anchorMax;
                tmpRectTransform.anchoredPosition = panelRectTransform.anchoredPosition;
                tmpRectTransform.sizeDelta = panelRectTransform.sizeDelta;
            }
        }
        else
        {
            Debug.LogError("Assign the RectTransform of the panel and TMP Text object in the Inspector.");
        }
    }
}
