using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class EnemySpawner : MonoBehaviour
{
    public enum CurrentArea
    {
        Forest,
        Cave,
        Dungeon
    }
    [SerializeField] private int currentWave = 1;
    [SerializeField] private TextMeshProUGUI waveText; 
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

    BattleHandler battleHandler;
    private bool once2 = true, once3 = true, once4 = true, once5 = true, once6 = true, once7 = true, once8 = true, once9 = true, once10 = true, once11 = true, once12 = true;
    int randomNo2, randomNo3, randomNo4, randomNo5, randomNo6, randomNo7, randomNo8, randomNo9, randomNo10, randomNo11, randomNo12;
    private void Awake()
    {
        randomNo2 = Random.Range(1, 3); randomNo3 = Random.Range(1, 4); randomNo4 = Random.Range(1, 4); randomNo5 = Random.Range(1, 4); randomNo6 = Random.Range(1, 4);
        randomNo7 = Random.Range(1, 4); randomNo8 = Random.Range(1, 4); randomNo9 = Random.Range(1, 4); randomNo10 = Random.Range(1, 4);  randomNo11 = Random.Range(1, 4); randomNo12 = Random.Range(1, 4);
    battleHandler = FindFirstObjectByType<BattleHandler>();
        //Instantiate(smallMushroomEnemy, spawnLocations[0].position, Quaternion.identity);
        Instantiate(smallMushroomEnemy, spawnLocations[1].position, Quaternion.identity);
        Instantiate(smallMushroomEnemy, spawnLocations[2].position, Quaternion.identity);
        Instantiate(smallMushroomEnemy, spawnLocations[3].position, Quaternion.identity);
        //Instantiate(smallMushroomEnemy, spawnLocations[4].position, Quaternion.identity);
    }

    private void Update()
    {
        waveText.text = $"Current Wave: {currentWave}";
        //Debug.Log(Random.Range(1, 4));
        
        if (currentWave == 2 && once2)
        {

            foreach (GameObject player in battleHandler.players)
            {
                player.GetComponent<CharacterBattle>().hasDoneTurn = false;
            }
            if (randomNo2 == 1)
            {
                Instantiate(smallMushroomEnemy, spawnLocations[1].position, Quaternion.identity);
                Instantiate(goldVineEnemy, spawnLocations[2].position, Quaternion.identity);
                Instantiate(goldVineEnemy, spawnLocations[3].position, Quaternion.identity);
                Instantiate(smallMushroomEnemy, spawnLocations[4].position , Quaternion.identity);

            }
            else if (randomNo2 == 2)
            {
                Instantiate(smallMushroomEnemy, spawnLocations[1].position, Quaternion.identity);
                Instantiate(goldVineEnemy, spawnLocations[2].position, Quaternion.identity);
                Instantiate(goldVineEnemy, spawnLocations[3].position, Quaternion.identity);
            }
            once2 = false;
            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyArray)
            {
                battleHandler.enemies.Add(enemy);
            }
        }
        else if (currentWave == 3 && once3)
        {

            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyArray)
            {
                battleHandler.enemies.Add(enemy);
            }
        }
        else if (currentWave == 4)
        {

            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyArray)
            {
                battleHandler.enemies.Add(enemy);
            }
        }
        else if (currentWave == 5)
        {

            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyArray)
            {
                battleHandler.enemies.Add(enemy);
            }
        }
        else if (currentWave == 6)
        {

            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyArray)
            {
                battleHandler.enemies.Add(enemy);
            }
        }
        else if (currentWave == 7)
        {

            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyArray)
            {
                battleHandler.enemies.Add(enemy);
            }
        }
        else if (currentWave == 8)
        {

            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyArray)
            {
                battleHandler.enemies.Add(enemy);
            }
        }
        else if (currentWave == 9)
        {

            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyArray)
            {
                battleHandler.enemies.Add(enemy);
            }
        }
        else if (currentWave == 10)
        {

            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyArray)
            {
                battleHandler.enemies.Add(enemy);
            }
        }
        else if (currentWave == 11)
        {

            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyArray)
            {
                battleHandler.enemies.Add(enemy);
            }
        }
        else if (currentWave == 12)
        {

            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyArray)
            {
                battleHandler.enemies.Add(enemy);
            }
        }
    }

    public void IncrementWave()
    {
        currentWave++;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
