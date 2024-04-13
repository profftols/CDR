using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private PoolCubes _poolCubes;

    private float _chanceDestruction => transform.localScale.x / _division;
    private float _randomChance => (float)Random.Range(_minRandomChance, _maxRandomChance) / _maxRandomChance;
    private int _division = 2;
    private int _minRandomChance = 1;
    private int _maxRandomChance = 100;

    public void Destroy()
    {
        if (_randomChance <= _chanceDestruction)
        {
            transform.localScale /= _division;
            _poolCubes.SpawnCubes(transform);
        }
        
        gameObject.SetActive(false);
    }
}
