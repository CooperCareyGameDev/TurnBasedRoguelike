using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerClassManger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerText1;
    [SerializeField] TextMeshProUGUI playerText2;
    [SerializeField] TextMeshProUGUI playerText3;
    [SerializeField] TextMeshProUGUI playerText4;

    public static string firstPlayerClassName;
    public static string secondPlayerClassName;
    public static string thirdPlayerClassName;
    public static string fourthPlayerClassName;

    private void Update()
    {
        firstPlayerClassName = playerText1.text;
        secondPlayerClassName = playerText2.text;
        thirdPlayerClassName = playerText3.text;
        fourthPlayerClassName = playerText4.text;
    }
}
