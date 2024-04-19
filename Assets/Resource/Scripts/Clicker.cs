using System;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 10f;

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