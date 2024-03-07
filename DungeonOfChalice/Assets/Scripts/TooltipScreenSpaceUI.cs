using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipScreenSpaceUI : MonoBehaviour
{
    public static TooltipScreenSpaceUI Instance { get; private set; }



    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private float tooltipResetDelay = 1.5f; 
    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;

    private System.Func<string> getTooltipTextFunc;
    private void Awake()
    {
        Instance = this;
        Debug.Log(Instance);
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        textMeshPro = transform.Find("TooltipText").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();
        SetText("Placeholder the image should change to fit the text now");

        HideTooltip();
        
    }

    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }

    private void Update()
    {

        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }
        rectTransform.anchoredPosition = anchoredPosition;
        SetText(getTooltipTextFunc());
    }

    private void ShowTooltip(string tooltipText)
    {
        gameObject.SetActive(true);
        backgroundRectTransform.GetComponent<Image>().color = Color.black;
        SetText(tooltipText);
        ShowTooltip(() => tooltipText);
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipText)
    {
        Instance.ShowTooltip(tooltipText);

        
    }
    
    public static void ShowTooltipWarning_Static(string tooltipText)
    {
        Instance.ShowTooltipWarning(tooltipText);
        
    }

    private void ShowTooltipWarning(string tooltipText)
    {
        Instance.ShowTooltip(tooltipText);
        StartCoroutine(ShowWarning());
    }
    private IEnumerator ShowWarning()
    {
        Debug.Log("Coroutine Started");
        backgroundRectTransform.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(tooltipResetDelay);
        
        Instance.HideTooltip();

    }
    public static void ShowTooltip_Static(System.Func<string> getTooltipTestFunc)
    {
        Instance.ShowTooltip(getTooltipTestFunc);
    }

    private void ShowTooltip(System.Func<string> getTooltipTextFunc)
    {
        this.getTooltipTextFunc = getTooltipTextFunc;
        gameObject.SetActive(true);
        SetText(getTooltipTextFunc());
    }
    /*private void ShowTooltip(System.Func<string> getTooltipTextFunc)
    {
        this.getTooltipTextFunc = getTooltipTextFunc;
        gameObject.SetActive(true);
        SetText(getTooltipTextFunc());
    }*/
    /*public static void ShowTooltip_Static(System.Func<string> getTooltipTextFunc)
    {
        //Instance.ShowTooltip(getTooltipTextFunc);
    }*/
    public static void HideTooltip_Static()
    {
        Instance.HideTooltip();
    }
}
