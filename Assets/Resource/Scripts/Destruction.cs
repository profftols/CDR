using UnityEngine;

public class Destruction : MonoBehaviour
{
    [SerializeField] private ClickerExplosive _clickerExplosive;
    [SerializeField] private Cube _cube;
    [SerializeField] private Material[] _materials;

    private int _cubesCount = 36;
    private Cube[] _cubes;
    
    private void Start()
    {
        _cubes = new Cube[_cubesCount];
        
        for (int i = 0; i < _cubesCount; i++)
        {
            _cubes[i] = Instantiate(_cube, transform);
            _cubes[i].GetComponent<Renderer>().material = _materials[i % _materials.Length];
        }
    }
    
    
}
