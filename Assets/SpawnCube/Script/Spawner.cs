using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Box _box;
    [SerializeField] private int _poolBox;

    private Queue<Box> _boxes;
    private Transform[] _positions;
    private float _timerSpawn = 3f;

    private void Start()
    {
        _boxes = new Queue<Box>();

        for (int i = 0; i < _poolBox; i++)
        {
            var box = Instantiate(_box);
            box.SetSpawner(this);
            box.gameObject.SetActive(false);
            _boxes.Enqueue(box);
        }

        _positions = transform.GetComponentsInChildren<Transform>();

        StartCoroutine(SpawnBox());
    }

    public void AddPoolBox(Box box)
    {
        _boxes.Enqueue(box);
        box.gameObject.SetActive(false);
    }

    private IEnumerator SpawnBox()
    {
        var wait = new WaitForSeconds(_timerSpawn);

        while (enabled)
        {
            var box = _boxes.Dequeue();
            box.transform.position = _positions[Random.Range(0, _positions.Length)].position;
            box.gameObject.SetActive(true);

            yield return wait;
        }
    }
}