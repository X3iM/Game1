using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private Fruit _fruit;
    [SerializeField] private float _spawnDelay = 2f;
    [SerializeField] private float _minPoint;
    [SerializeField] private float _maxPoint;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);
        
        while (true)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(_minPoint, _maxPoint), transform.position.y);
            Instantiate(_fruit, spawnPosition, Quaternion.identity);
            yield return wait;
        }
    }
}