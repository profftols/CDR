using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    
    private float ChanceDestruction => transform.localScale.x;
    private float RandomChance => Random.Range(_minRandomChance, _maxRandomChance);
    
    private float _multiplier = 0.5f;
    private float _minRandomChance = 0.01f;
    private float _maxRandomChance = 1f;
    private int _minCube = 2;
    private int _maxCube = 7;

    public void Destroy()
    {
        int decimalPlaces = 2;
        float randomChance = Mathf.Round(RandomChance * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);
        
        if (randomChance <= ChanceDestruction)
        {
            transform.localScale *= _multiplier;
            Crumble();
        }
        
        gameObject.SetActive(false);
    }

    private void Crumble()
    {
        int count = Random.Range(_minCube, _maxCube);
        
        for (int i = 0; i < count; i++)
        {
           var crumble = Instantiate(this, transform.position, Quaternion.identity);
           
           if (crumble.TryGetComponent(out Renderer rendere))
           {
               rendere.material = _materials[Random.Range(0, _materials.Length)];
           }
        }
    }
}
