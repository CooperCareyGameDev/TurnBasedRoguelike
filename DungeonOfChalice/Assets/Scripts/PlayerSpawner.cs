using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject knightPrefab;
    [SerializeField] GameObject barbarianPrefab;
    [SerializeField] GameObject magePrefab;
    [SerializeField] GameObject archerPrefab;
    [SerializeField] GameObject clericPrefab;
    [SerializeField] GameObject kingPrefab;
    [SerializeField] GameObject trapperPrefab;
    [SerializeField] GameObject paladinPrefab;

    public List<Transform> spawnLocations = new List<Transform>();

    private void Start()
    {
        switch (PlayerClassManger.firstPlayerClassName)
        {
            case "Knight": Instantiate(knightPrefab, spawnLocations[0].position, Quaternion.identity); break;
            case "Barbarian": Instantiate(barbarianPrefab, spawnLocations[0].position, Quaternion.identity); break;
            case "Mage": Instantiate(magePrefab, spawnLocations[0].position, Quaternion.identity); break;
            case "Archer": Instantiate(archerPrefab, spawnLocations[0].position, Quaternion.identity); break;
            case "Cleric": Instantiate(clericPrefab, spawnLocations[0].position, Quaternion.identity); break;
            case "King": Instantiate(kingPrefab, spawnLocations[0].position, Quaternion.identity); break;
            case "Trapper": Instantiate(trapperPrefab, spawnLocations[0].position, Quaternion.identity); break;
            case "Paladin": Instantiate(paladinPrefab, spawnLocations[0].position, Quaternion.identity); break;
        }
        switch (PlayerClassManger.secondPlayerClassName)
        {
            case "Knight": Instantiate(knightPrefab, spawnLocations[1].position, Quaternion.identity); break;
            case "Barbarian": Instantiate(barbarianPrefab, spawnLocations[1].position, Quaternion.identity); break;
            case "Mage": Instantiate(magePrefab, spawnLocations[1].position, Quaternion.identity); break;
            case "Archer": Instantiate(archerPrefab, spawnLocations[1].position, Quaternion.identity); break;
            case "Cleric": Instantiate(clericPrefab, spawnLocations[1].position, Quaternion.identity); break;
            case "King": Instantiate(kingPrefab, spawnLocations[1].position, Quaternion.identity); break;
            case "Trapper": Instantiate(trapperPrefab, spawnLocations[1].position, Quaternion.identity); break;
            case "Paladin": Instantiate(paladinPrefab, spawnLocations[1].position, Quaternion.identity); break;
        }
        switch (PlayerClassManger.thirdPlayerClassName)
        {
            case "Knight": Instantiate(knightPrefab, spawnLocations[2].position, Quaternion.identity); break;
            case "Barbarian": Instantiate(barbarianPrefab, spawnLocations[2].position, Quaternion.identity); break;
            case "Mage": Instantiate(magePrefab, spawnLocations[2].position, Quaternion.identity); break;
            case "Archer": Instantiate(archerPrefab, spawnLocations[2].position, Quaternion.identity); break;
            case "Cleric": Instantiate(clericPrefab, spawnLocations[2].position, Quaternion.identity); break;
            case "King": Instantiate(kingPrefab, spawnLocations[2].position, Quaternion.identity); break;
            case "Trapper": Instantiate(trapperPrefab, spawnLocations[2].position, Quaternion.identity); break;
            case "Paladin": Instantiate(paladinPrefab, spawnLocations[2].position, Quaternion.identity); break;
        }
        switch (PlayerClassManger.fourthPlayerClassName)
        {
            case "Knight": Instantiate(knightPrefab, spawnLocations[3].position, Quaternion.identity); break;
            case "Barbarian": Instantiate(barbarianPrefab, spawnLocations[3].position, Quaternion.identity); break;
            case "Mage": Instantiate(magePrefab, spawnLocations[3].position, Quaternion.identity); break;
            case "Archer": Instantiate(archerPrefab, spawnLocations[3].position, Quaternion.identity); break;
            case "Cleric": Instantiate(clericPrefab, spawnLocations[3].position, Quaternion.identity); break;
            case "King": Instantiate(kingPrefab, spawnLocations[3].position, Quaternion.identity); break;
            case "Trapper": Instantiate(trapperPrefab, spawnLocations[3].position, Quaternion.identity); break;
            case "Paladin": Instantiate(paladinPrefab, spawnLocations[3].position, Quaternion.identity); break;
        }
    }
}
