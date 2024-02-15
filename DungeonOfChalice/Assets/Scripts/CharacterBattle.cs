using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CharacterBattle : MonoBehaviour
{
    Image image;
    [SerializeField] private GameObject turnIndicator;



    private void Awake()
    {
        HideTurnIndicator();
    }
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void Attack(CharacterBattle targetCharacterBattle, Action onAttackComplete)
    {
        Vector3 attackDir = (targetCharacterBattle.GetPosition() - GetPosition()).normalized;
        Debug.Log("Attacked");
        image.color = Color.yellow;
        onAttackComplete();
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void HideTurnIndicator()
    {
        turnIndicator.SetActive(false);
    }

    public void ShowTurnIndicator()
    {
        turnIndicator.SetActive(true);
    }
}
