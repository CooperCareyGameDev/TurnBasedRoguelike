using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI testText;

    public void DropdownSample(int index)
    {
        switch (index)
        {
            case 0: testText.text = "Knight"; break;
            case 1: testText.text = "Barbarian"; break;
            case 2: testText.text = "Mage"; break;
            case 3: testText.text = "Archer"; break; 
        }
    }
}
