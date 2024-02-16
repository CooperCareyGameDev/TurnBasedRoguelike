using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public Transform healthBarPrefab;

    private void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);

        Transform healthBarTransform = Instantiate(healthBarPrefab, new Vector3(0, 10), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
    }
}
