using UnityEngine;

[RequireComponent(typeof(SpringJoint))]
public class Catapult : MonoBehaviour
{
    [SerializeField] private Missile _prefab;
    [SerializeField] private Transform _spawnPoint;
    
    private SpringJoint _springJoint;
    private Rigidbody _rb;
    private bool _isLoaded;
    private float _shootPower = 8f;
    
    private void Start()
    {
        _springJoint = GetComponent<SpringJoint>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isLoaded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Instantiate(_prefab, _spawnPoint.position, _spawnPoint.rotation);
                _springJoint.spring = _shootPower;
                _isLoaded = false;
            }
        }
        else
        {
            _springJoint.spring -= Time.deltaTime;
            _rb.WakeUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bucket bucket))
        {
            _isLoaded = true;
        }
    }
}
