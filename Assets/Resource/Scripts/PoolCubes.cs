using System.Collections.Generic;
using UnityEngine;

public class PoolCubes : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private ClickerExplosive _explosive;

    private int _cubesCount = 36;
    private int _minCube = 2;
    private int _maxCube = 7;
    private Cube[] _cubes;
    private List<Rigidbody> _cubesRb;

    private void Start()
    {
        _cubes = new Cube[_cubesCount];

        for (int i = 0; i < _cubesCount; i++)
        {
            _cubes[i] = Instantiate(_cubePrefab, transform);
            _cubes[i].GetComponent<Renderer>().material = _materials[Random.Range(0, _materials.Length)];
            _cubes[i].gameObject.SetActive(false);
        }
    }

    public void SpawnCubes(Transform oldTransform)
    {
        int count = Random.Range(_minCube, _maxCube);
        _cubesRb = new List<Rigidbody>(count);

        for (int i = 0; i < _cubes.Length; i++)
        {
            if (count > 0)
            {
                if (_cubes[i].gameObject.activeSelf == false)
                {
                    ActivateCube(_cubes[i], oldTransform);

                    if (_cubes[i].TryGetComponent(out Rigidbody _rigidbody))
                    {
                        _cubesRb.Add(_rigidbody);
                    }
                }
                
                count--;
            }
            else
            {
                break;
            }
        }

        _explosive.Explode(_cubesRb);
        _cubesRb = null;
    }
    
    private void ActivateCube(Cube cube, Transform transform)
    {
        cube.gameObject.SetActive(true);
        cube.transform.position = transform.position;
        cube.transform.localScale = transform.localScale;
    }
}