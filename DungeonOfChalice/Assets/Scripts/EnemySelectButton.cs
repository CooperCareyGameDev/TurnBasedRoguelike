using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    [SerializeField] private CharacterBattle target;
    private BattleHandler battleHandler;
    private void Update()
    {
        
    }

    private void Awake()
    {
        battleHandler = FindFirstObjectByType<BattleHandler>();
    }
    public void SetEnemyCharacterBattle()
    {
        battleHandler.enemyCharacterBattle = target;
        Debug.Log("Set enemy");
    }
}
