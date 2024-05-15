using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Box _boxPrefab;
    [SerializeField] private int _poolCount;
    [SerializeField] private Transform[] _positions;

    private CustomPool<Box> _boxCustomPool;
    private float _timerSpawn = 1.4f;

    private void Start()
    {
        _boxCustomPool = new CustomPool<Box>(_boxPrefab, _poolCount);
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        var wait = new WaitForSeconds(_timerSpawn);

        while (enabled)
        {
            var obj = _boxCustomPool.TakeObject();
            obj.transform.position = _positions[Random.Range(0, _positions.Length - 1)].position;
            obj.Disabled += AddPool;
            
            yield return wait;
        }
    }

    private void AddPool(Box box)
    {
        _boxCustomPool.PutPool(box);
        box.Disabled -= AddPool;
    }
}