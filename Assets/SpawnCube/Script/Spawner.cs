using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Box _box;
    [SerializeField] private int _poolBox;
    [SerializeField] private Transform[] _positions;

    private Queue<Box> _boxes;
    private float _timerSpawn = 1.4f;

    private void Start()
    {
        _boxes = new Queue<Box>();

        for (int i = 0; i < _poolBox; i++)
        {
            var box = Instantiate(_box);
            box.gameObject.SetActive(false);
            _boxes.Enqueue(box);
        }

        StartCoroutine(SpawnBox());
    }

    private void AddPoolBox(Box box)
    {
        _boxes.Enqueue(box);
        box.gameObject.SetActive(false);
        box.BackInPool -= AddPoolBox;
    }

    private IEnumerator SpawnBox()
    {
        var wait = new WaitForSeconds(_timerSpawn);

        while (enabled)
        {
            var box = _boxes.Dequeue();
            box.transform.position = _positions[Random.Range(0, _positions.Length)].position;
            box.gameObject.SetActive(true);
            box.BackInPool += AddPoolBox;

            yield return wait;
        }
    }
}