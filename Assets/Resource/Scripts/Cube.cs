using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private ClickerExplosive _clickerExplosive;
    [SerializeField] private PoolCubes _poolCubes;

    private int _division = 2;
    private int _minRandomChance = 1;
    private int _maxRandomChance = 100;

    public void Destroy()
    {
        if ((float)Random.Range(_minRandomChance, _maxRandomChance) / _maxRandomChance <= transform.localScale.x)
        {
            transform.localScale /= _division;
            _poolCubes.SpawnCubes(transform);
        }
        
        gameObject.SetActive(false);
    }
}
