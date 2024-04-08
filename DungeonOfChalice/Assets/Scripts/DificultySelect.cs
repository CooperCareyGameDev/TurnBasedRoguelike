using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DificultySelect : MonoBehaviour
{
    public static bool isEasy = false;
    public static bool isNormal = true;
    public static bool isHard = false; 


    public void SetEasy()
    {
        isEasy = true;
        isNormal = false;
        isHard = false;
    }

    public void SetNormal()
    {
        isEasy = false;
        isNormal = true;
        isHard = false;
    }

    public void SetHard()
    {
        isEasy = false; 
        isNormal = false;
        isHard = true;
    }
}
