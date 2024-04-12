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
        Vector3 direction = _targetPosition.position - transform.position;
        float distance = direction.magnitude;

        if (distance > _offset)
        {
            _rigidbody.MovePosition(transform.position + direction.normalized * (_speed * Time.deltaTime));
            transform.LookAt(_targetPosition);
        }
    }
}