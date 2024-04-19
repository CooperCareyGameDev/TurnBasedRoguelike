using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum CurrentArea
    {
        Forest,
        Cave,
        Dungeon
    }
    [Header("Forest Enemies")]
    [SerializeField] GameObject smallMushroomEnemy;
    [SerializeField] GameObject goldVineEnemy;
    [SerializeField] GameObject tallMushroomEnemy;
    [SerializeField] GameObject swampMage;
    [Header("CaveEnemies")]
    [SerializeField] GameObject clawEnemy;
    [SerializeField] GameObject bigEyeEnemy;
    [SerializeField] GameObject spearEnemy;
    [SerializeField] GameObject multiEyeEnemy;
    [Header("DungeonEnemies")]
    [SerializeField] GameObject medusaEnemy;
    [SerializeField] GameObject lobsterEnemy;
    [SerializeField] GameObject armoredEnemy;
    [SerializeField] GameObject pillarEnemy;
    [SerializeField] GameObject tentacleEnemy;
    [SerializeField] GameObject sunriseEnemy;

    public List<Transform> spawnLocations = new List<Transform>();

    private void Awake()
    {
        Instantiate(smallMushroomEnemy, spawnLocations[0].position, Quaternion.identity);
        Instantiate(smallMushroomEnemy, spawnLocations[1].position, Quaternion.identity);
        Instantiate(smallMushroomEnemy, spawnLocations[2].position, Quaternion.identity);
        Instantiate(smallMushroomEnemy, spawnLocations[3].position, Quaternion.identity);
        Instantiate(smallMushroomEnemy, spawnLocations[4].position, Quaternion.identity);
    }

}
