using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
public class Box : Subject
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Material[] _materials;

    public event Action<Box> Disabled;

    private CustomPool<Bomb> _bombCustomPool;
    private MeshRenderer _renderer;
    private int _defaultMaterial = 0;
    private int _countBomb = 2;
    
    private void Awake()
    {
        _bombCustomPool = new CustomPool<Bomb>(_bombPrefab, _countBomb);
        _renderer = GetComponent<MeshRenderer>();
        ChangeColor(_materials[_defaultMaterial]);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Platform platform))
        {
            ChangeColor(_materials[_materials.Length - 1]);
            StartCoroutine(LaunchDeath());
        }
    }

    private void OnDisable()
    {
        _renderer.material = _materials[_defaultMaterial];
    }
    
    protected override IEnumerator LaunchDeath()
    {
        var timer = new WaitForSeconds(Random.Range(MinTimer, MaxTimer));
        
        yield return timer;
        
        var bomb = _bombCustomPool.TakeObject();
        bomb.transform.position = transform.position;
        bomb.Disabled += AddPool;
        bomb.StartBoom();
        Disabled?.Invoke(this);
    }

    private void ChangeColor(Material material)
    {
        _renderer.material = material;
    }

    private void AddPool(Bomb bomb)
    {
        _bombCustomPool.PutPool(bomb);
        bomb.Disabled -= AddPool;
    }
}