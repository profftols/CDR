using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Swing : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Rigidbody _rigidbody;
    
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Punch();
        }
    }

    private void Punch()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }
}
