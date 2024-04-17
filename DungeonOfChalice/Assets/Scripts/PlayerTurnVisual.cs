using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnVisual : MonoBehaviour
{
    [SerializeField] GameObject hasTurnLeftIndicator;

    private void Update()
    {
        if (GetComponent<CharacterBattle>().hasDoneTurn)
        {
            hasTurnLeftIndicator.SetActive(false);
        }
        else
        {
            hasTurnLeftIndicator.SetActive(true);
        }
    }
}
