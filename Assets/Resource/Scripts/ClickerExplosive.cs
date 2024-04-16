using System;
using System.Collections.Generic;
using UnityEngine;

public class ClickerExplosive : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private float _explosionForce = 5000f;
    [SerializeField] private float _explosionRadius = 500f;

    private Camera _camera;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }
    
    public void Explode(List<Rigidbody> bodyExplosions)
    {
        if (bodyExplosions == null)
        {
            return;
        }

        foreach (var hit in bodyExplosions)
        {
            hit.AddExplosionForce(_explosionForce, hit.transform.position, _explosionRadius);
        }
    }
    
    private void Shoot()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
        {
            if (hit.collider.TryGetComponent(out Cube cube))
            {
                cube.Destroy();
            }
        }
    }
}