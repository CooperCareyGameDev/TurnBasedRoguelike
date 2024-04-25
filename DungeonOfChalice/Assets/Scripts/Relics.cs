using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro; 
public class Relics : MonoBehaviour
{
    // Rage Fills Faster
    public static bool rageBuff = false;
    // Gives shield at beginning of turn;
    public static bool shieldBuff = false;
    // When one party member dies, they are revived at full health once
    public static bool secondChanceBuff = false;
    // Increased crit chance
    public static bool critChanceBuff = false;
    // Heal slightly after each battle
    public static bool healingBuff = false;

    // Test Variables
    [SerializeField] TextMeshProUGUI relicText; 

    [SerializeField] bool localRageBuff = false;
    [SerializeField] bool localShieldBuff = false;
    [SerializeField] bool localSecondChanceBuff = false;
    [SerializeField] bool localCritChanceBuff = false;
    [SerializeField] bool localHealingBuff = false;
    [SerializeField] bool localHasSecondChance = false; 

    public static bool hasSecondChance = false; 
    private void Awake()
    {
        // Setting Test Variables
        //rageBuff = localRageBuff;
        //shieldBuff = localShieldBuff;
        //secondChanceBuff = localSecondChanceBuff;
        //critChanceBuff = localCritChanceBuff;
        //healingBuff = localHealingBuff;
        //hasSecondChance = localHasSecondChance;
    }


    private void Update()
    {
        /*Debug.Log("Rage: " + rageBuff);
        Debug.Log("Shield: " +  shieldBuff);
        Debug.Log("Second Chance: " + secondChanceBuff);
        Debug.Log("Crit: " + critChanceBuff);
        Debug.Log("Healing: " +  healingBuff); */  
        if (rageBuff)
        {
            relicText.text = "Current Relic: Rage";
        }
        else if (shieldBuff)
        {
            relicText.text = "Current Relic: Shield";
        }
        else if (secondChanceBuff)
        {
            relicText.text = "Current Relic: Second Chance";
        }
        else if (critChanceBuff)
        {
            relicText.text = "Current Relic: Crit Chance";
        }
        else if (healingBuff)
        {
            relicText.text = "Current Relic: Healing";
        }
    }

    public void ActivateRageBuff()
    {
        rageBuff = true;
        shieldBuff = false;
        secondChanceBuff = false;
        critChanceBuff = false;
        healingBuff = false;
        hasSecondChance = false;
    }

    public void ActivateShieldBuff()
    {
        rageBuff = false;
        shieldBuff = true;
        secondChanceBuff = false;
        critChanceBuff = false;
        healingBuff = false;
        hasSecondChance = false;
        Debug.Log("Active shield " + shieldBuff);
    }

    public void ActivateSecondChanceBuff()
    {
        rageBuff = false;
        shieldBuff = false;
        secondChanceBuff = true;
        critChanceBuff = false;
        healingBuff = false;
        hasSecondChance = true;
    }

    public void ActivateCritChanceBuff()
    {
        rageBuff = false;
        shieldBuff = false;
        secondChanceBuff = false;
        critChanceBuff = true;
        healingBuff = false;
        hasSecondChance = false;
    }

    public void ActivateHealingBuff()
    {
        rageBuff = false;
        shieldBuff = false;
        secondChanceBuff = false;
        critChanceBuff = false;
        healingBuff = true;
        hasSecondChance = false;
    }
}
