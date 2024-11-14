using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject bateria, botella;
    public Transform[] spawnpoints;
    public int numBateria, numBotella;

    void Start()
    {
        SpawnObject(bateria, numBateria);
        SpawnObject(botella, numBotella);
    }

    void SpawnObject(GameObject _object, int _numObjects)
    {
        int _index = 0;
        int attempts = 0; // Intentos para evitar bucles infinitos

        while (_index < _numObjects && attempts < spawnpoints.Length * 2)
        {
            Transform _position = spawnpoints[Random.Range(0, spawnpoints.Length)];
            if (_position.childCount == 0)
            {
                Instantiate(_object, _position, false);
                _index++;
            }
            attempts++;
        }
    }

 
    public void NotifyObjectTaken(string objectTag)
    {
        if (objectTag == bateria.tag)
        {
            SpawnSingleObject(bateria);
        }
        else if (objectTag == botella.tag)
        {
            SpawnSingleObject(botella);
        }
    }

    void SpawnSingleObject(GameObject _object)
    {
        int attempts = 0;
        bool spawned = false;

        while (!spawned && attempts < spawnpoints.Length * 2)
        {
            Transform _position = spawnpoints[Random.Range(0, spawnpoints.Length)];
            if (_position.childCount == 0)
            {
                Instantiate(_object, _position, false);
                spawned = true;
            }
            attempts++;
        }
    }
}
