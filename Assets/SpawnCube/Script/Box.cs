using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
public class Box : MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    private Spawner _spawner;
    private MeshRenderer _renderer;
    private int _defaultMaterial = 0;
    private int _minTimer = 2;
    private int _maxTimer = 6;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        ChangeColor(_materials[_defaultMaterial]);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Platform platform))
        {
            ChangeColor(_materials[_materials.Length-1]);
            StartCoroutine(LaunchDeath());
        }
    }
    
    public void SetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }

    private IEnumerator LaunchDeath()
    {
        var timer = new WaitForSeconds(Random.Range(_minTimer, _maxTimer));
        
        yield return timer;

        _spawner.AddPoolBox(this);
        ChangeColor(_materials[_defaultMaterial]);
    }

    private void ChangeColor(Material material)
    {
        _renderer.material = material;
    }
}