using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    [SerializeField] private CharacterBattle target;
    [SerializeField] private BattleHandler battleHandler;
    private void Update()
    {
        
    }
    

    public void SetEnemyCharacterBattle()
    {
        battleHandler.enemyCharacterBattle = target;
        Debug.Log("Set enemy");
    }
}
