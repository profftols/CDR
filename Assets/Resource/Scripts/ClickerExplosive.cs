using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ClickerExplosive : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private float _explosionForce = 5000f;
    [SerializeField] private float _explosionRadius = 500f;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, _maxDistance);
        Cube point = hit.collider.GetComponent<Cube>();
        
        if (point)
        {
            point.Destroy();
        }

        Explode(hit.transform.position);
    }

    private void Explode(Vector3 point)
    {
        Collider[] colliders = Physics.OverlapSphere(point, _explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_explosionForce, point, _explosionRadius);
            }
        }
    }
}