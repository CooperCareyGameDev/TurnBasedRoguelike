using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; 

public class Testing : MonoBehaviour
{

    private void Start()
    {
        //DamagePopup.Create(Vector3.zero, 15);


    }

    public void DebugButton()
    {
        Debug.Log("Successfull Press");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && false)
        {
            bool isCriticalHit = Random.Range(0, 100) < 30;
            DamagePopup.Create(UtilsClass.GetMouseWorldPosition(), 15, isCriticalHit);
        }
    }
}
