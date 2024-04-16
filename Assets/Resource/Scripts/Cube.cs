using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private ClickerExplosive _clicker;

    private List<Rigidbody> _bodyExplosions;
    private Renderer _renderer;
    private float _multiplier = 0.5f;
    private float _minRandomChance = 0.01f;
    private float _maxRandomChance = 1f;
    private int _minCube = 2;
    private int _maxCube = 7;
    
    private float ChanceDestruction => transform.localScale.x;
    private float RandomChance => Random.Range(_minRandomChance, _maxRandomChance);

    private void Start()
    {
        _bodyExplosions = new List<Rigidbody>();
    }

    public void Destroy()
    {
        int decimalPlaces = 2;
        float randomChance = Mathf.Round(RandomChance * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);
        
        if (randomChance <= ChanceDestruction)
        {
            Crumble();
            _clicker.Explode(_bodyExplosions);
        }
        
        Destroy(gameObject);
    }

    private void Crumble()
    {
        int count = Random.Range(_minCube, _maxCube);
        
        for (int i = 0; i < count; i++)
        {
           var crumble = Instantiate(this, transform.position, Quaternion.identity);
           Init(crumble);
           
           if (crumble.TryGetComponent(out Rigidbody rb))
           {
               _bodyExplosions.Add(rb);
           }
        }
    }

    private void Init(Cube cube)
    {
        if (cube.TryGetComponent(out MeshRenderer render))
        {
            render.material = _materials[Random.Range(0, _materials.Length)];
        }
        
        cube.transform.localScale *= _multiplier;
    }
}
