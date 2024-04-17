using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetVisual : MonoBehaviour
{
    BattleHandler battleHandler;
    [SerializeField] GameObject targetIndicator; 
    private void Start()
    {
        battleHandler = FindFirstObjectByType<BattleHandler>();
    }

    private void Update()
    {
        if (battleHandler.enemyCharacterBattle == GetComponent<CharacterBattle>())
        {
            targetIndicator.SetActive(true);
        }
        else
        {
            targetIndicator.SetActive(false);
        }
    }
}
