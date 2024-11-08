using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject bateria,botella;
    public Transform[] spawnpoints;
    public int numBateria,numBotella;

    void Start()
    {
        SpawnObject(bateria, numBateria);
        SpawnObject(botella, numBotella);
    }

  
    void Update()
    {
        
    }

    void SpawnObject(GameObject _object, int _numObjects)
    {
        int _index = 0;
        while(_index<_numObjects)
        {
            Transform _position = spawnpoints[Random.Range(0,spawnpoints.Length)];
            if (_position.childCount == 0)
            {
                Instantiate(_object, _position, false);
                _index++;
            }
        }
    }
}
