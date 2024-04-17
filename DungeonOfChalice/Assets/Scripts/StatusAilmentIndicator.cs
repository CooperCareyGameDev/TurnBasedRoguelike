using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAilmentIndicator : MonoBehaviour
{
    CharacterBattle characterBattle;
    [Header("Buffs")]
    [SerializeField] GameObject buffedIndicator;
    [SerializeField] GameObject magicSpikeIndicator;
    [SerializeField] GameObject trapIndicator;
    [SerializeField] GameObject evasiveIndicator;
    [Header("Debuffs")]
    [SerializeField] GameObject poisonIndicator;
    [SerializeField] GameObject weakenedIndicator;
    [SerializeField] GameObject bleedIndicator;
    //[SerializeField] GameObject bleedIndicator2;

    private void Awake()
    {
        characterBattle = GetComponent<CharacterBattle>();
    }
    private void Update()
    {
        if (characterBattle.isBuffed)
        {
            buffedIndicator.SetActive(true);
        }
        else
        {
            buffedIndicator.SetActive(false);
        }
        if (characterBattle.hasMagicSpike)
        {
            magicSpikeIndicator.SetActive(true);
        }
        else
        {
            magicSpikeIndicator.SetActive(false);
        }
        if (characterBattle.hasTrap)
        {
            trapIndicator.SetActive(true);
        }
        else
        {
            trapIndicator.SetActive(false);
        }
        if (characterBattle.hasEvasive)
        {
            evasiveIndicator.SetActive(true);
        }
        else
        {
            evasiveIndicator.SetActive(false);
        }
        if (characterBattle.isPoisoned)
        {
            poisonIndicator.SetActive(true);
        }
        else
        {
            poisonIndicator.SetActive(false);
        }
        if (characterBattle.isWeakened)
        {
            weakenedIndicator.SetActive(true);
        }
        else
        {
            weakenedIndicator.SetActive(false);
        }
        if (characterBattle.currentBleed == 1)
        {
            bleedIndicator.SetActive(true);
        }
        else
        {
            bleedIndicator.SetActive(false);
        }
    }
}
