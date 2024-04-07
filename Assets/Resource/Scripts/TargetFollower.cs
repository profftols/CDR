using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _offset;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 position = _targetPosition.position;
        _rigidbody.velocity =
            transform.TransformDirection(new Vector3(position.x, position.y * Time.deltaTime, position.z + _offset) -
                                         transform.position) * (_speed * Time.deltaTime);
    }
}