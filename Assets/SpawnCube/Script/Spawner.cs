using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Box _box;
    [SerializeField] private Bomb _bomb;
    [SerializeField] private int _poolObject;
    [SerializeField] private Transform[] _positions;
    
    private Queue<Box> _boxes;
    private Queue<Bomb> _bombs;
    private float _timerSpawn = 1.4f;

    private void Start()
    {
        _boxes = new Queue<Box>();
        _bombs = new Queue<Bomb>();

        for (int i = 0; i < _poolObject; i++)
        {
            var box = Instantiate(_box);
            var bomb = Instantiate(_bomb);
            box.gameObject.SetActive(false);
            bomb.gameObject.SetActive(false);
            _bombs.Enqueue(bomb);
            _boxes.Enqueue(box);
        }

        StartCoroutine(SpawnBox());
    }

    private void AddPoolBox(Box box)
    {
        _boxes.Enqueue(box);
        box.gameObject.SetActive(false);
        SpawnBomb(box.transform);
        box.BackInPool -= AddPoolBox;
    }

    private void AddPoolBomb(Bomb bomb)
    {
        bomb.gameObject.SetActive(false);
        bomb.BackInPool -= AddPoolBomb;
    }

    private void SpawnBomb(Transform boxPosition)
    {
        if (_bombs.TryDequeue(out Bomb bomb))
        {
            bomb.gameObject.SetActive(true);
        }
        else
        {
            bomb = Instantiate(_bomb);
        }

        bomb.transform.position = boxPosition.position;
        bomb.BackInPool += AddPoolBomb;
        bomb.StartBoom();
    }

    private IEnumerator SpawnBox()
    {
        var wait = new WaitForSeconds(_timerSpawn);

        while (enabled)
        {
            if (_boxes.TryDequeue(out Box box))
            {
                box.gameObject.SetActive(true);
            }
            else
            {
                box = Instantiate(_box);
            }
            
            box.transform.position = _positions[Random.Range(0, _positions.Length)].position;
            box.BackInPool += AddPoolBox;

            yield return wait;
        }
    }
}