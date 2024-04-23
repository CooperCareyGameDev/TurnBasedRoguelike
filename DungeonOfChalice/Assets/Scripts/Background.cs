using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    EnemySpawner enemySpawner; 

    [SerializeField] Sprite forestBackground;
    [SerializeField] Sprite caveBackground;
    [SerializeField] Sprite dungeonBackground;

    Image image; 
    private void Awake()
    {
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (enemySpawner.GetCurrentWave() == 1 || enemySpawner.GetCurrentWave() == 2 || enemySpawner.GetCurrentWave() == 3 || enemySpawner.GetCurrentWave() == 4)
        {
            image.sprite = forestBackground; 
        }
        else if (enemySpawner.GetCurrentWave() == 5 || enemySpawner.GetCurrentWave() == 6 || enemySpawner.GetCurrentWave() == 7 || enemySpawner.GetCurrentWave() == 8) {
            image.sprite = caveBackground;
        }
        else if (enemySpawner.GetCurrentWave() == 9 || enemySpawner.GetCurrentWave() == 10 || enemySpawner.GetCurrentWave() == 11 || enemySpawner.GetCurrentWave() == 12)
        {
            image.sprite = dungeonBackground;
        }
    }
}
