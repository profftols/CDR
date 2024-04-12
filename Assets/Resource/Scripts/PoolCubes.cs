using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCubes : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private Cube _cube;
    
    private int _cubesCount = 36;
    private int _minCube = 2;
    private int _maxCube = 7;
    private Cube[] _cubes;
    
    private void Start()
    {
        _cubes = new Cube[_cubesCount];
        
        for (int i = 0; i < _cubesCount; i++)
        {
            _cubes[i] = Instantiate(_cube, transform);
            _cubes[i].GetComponent<Renderer>().material = _materials[Random.Range(0, _materials.Length)];
            _cubes[i].gameObject.SetActive(false);
        }
    }
    
    public void SpawnCubes(Transform oldTransform)
    {
        int count = Random.Range(_minCube, _maxCube);

        for (int i = 0; i < count; i++)
        {
            if (_cubes[i].gameObject.activeSelf == false)
            {
                _cubes[i].gameObject.SetActive(true);
                _cubes[i].transform.position = oldTransform.position;
                _cubes[i].transform.localScale = oldTransform.localScale;
            }
        }
    }
}
