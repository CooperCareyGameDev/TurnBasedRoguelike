using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    public static DamagePopup Create(Vector3 position, int damageAmount, bool isCriticalHit)
    {
        //Debug.Log("Creating Popup");
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);
        

        return damagePopup;
    }

    public static DamagePopup CreateHealPopup(Vector3 position, int healAmount)
    {
        //Debug.Log("Heal Popup");
        Transform healPopupTransform = Instantiate(GameAssets.i.pfHealPopup, position, Quaternion.identity);
        DamagePopup healPopup = healPopupTransform.GetComponent<DamagePopup>();
        healPopup.SetupHealPopup(healAmount);
        return healPopup;
    }

    public static DamagePopup CreateShieldPopup(Vector3 position, int shieldAmount)
    {
        //Debug.Log("Shield Popup");
        Transform shieldPopupTransform = Instantiate(GameAssets.i.pfShieldPopup, position, Quaternion.identity);
        DamagePopup shieldPopup = shieldPopupTransform.GetComponent<DamagePopup>();
        shieldPopup.SetupShieldPopup(shieldAmount);
        return shieldPopup;
    }
    private static int sortingOrder; 

    private const float DISAPPEAR_TIMER_MAX = 1f; 
    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        textColor = textMesh.color;
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            textMesh.fontSize = 36;
            textColor = UtilsClass.GetColorFromString("FF6D00");
        }
        else
        {
            textMesh.fontSize = 45;
            textColor = UtilsClass.GetColorFromString("FF0005");
        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        moveVector = new Vector3(1, 1) * 60f; 

    }

    public void SetupHealPopup(int healAmount)
    {
        textMesh.SetText(healAmount.ToString());
        textMesh.fontSize = 36;
        textColor = UtilsClass.GetColorFromString("04FF00");
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        moveVector = new Vector3(1, 1) * 60f; 
    }

    public void SetupShieldPopup(int shieldAmount)
    {
        textMesh.SetText(shieldAmount.ToString());
        textMesh.fontSize = 36;
        textColor = UtilsClass.GetColorFromString("331F00");
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        moveVector = new Vector3(1, 1) * 60f;
    }
    private void Update()
    {
        float moveYSpeed = 20f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        //transform.position += moveVector * Time.deltaTime; 
        //moveVector -= moveVector * 8f * Time.deltaTime;
        disappearTimer -= Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime; 
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
        }
        if (textColor.a < 0)
        {
            Destroy(gameObject);
        }
    }
}
