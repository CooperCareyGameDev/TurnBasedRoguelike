using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Tooltip : MonoBehaviour
{

    [SerializeField ]private Camera uiCamera;
    private TextMeshProUGUI tooltipText;
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("TooltipText").GetComponent<TextMeshProUGUI>();

        ShowTooltip("Testing tooltip with tons of different words to see how it reacts to having tons of words.");
    }

    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f; 
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + (textPaddingSize * 2f), tooltipText.preferredHeight + (textPaddingSize * 2f));
        backgroundRectTransform.sizeDelta = backgroundSize; 
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        Debug.Log(localPoint);
        transform.localPosition = localPoint;
    }
}
