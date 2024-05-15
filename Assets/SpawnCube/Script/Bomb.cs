using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Bomb : Subject
{
    [SerializeField] private float _explosionForce = 750f;
    [SerializeField] private float _radiusExpl = 75f;
    
    public event Action<Bomb> Disabled;
    
    private MeshRenderer _meshRenderer;
    private Color _startingColor;
    private float _fadeDuration = 1f;
    private float _maxTimer = 5f;
    private float _targetAlpha = 0f;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _startingColor = _meshRenderer.material.color;
    }
    
    public void StartBoom()
    {
        StartCoroutine(LaunchDeath());
    }

    protected override IEnumerator LaunchDeath()
    {
        var wait = new WaitForSeconds(_fadeDuration);
        float timer = 0;

        while (timer < _maxTimer)
        {
            timer += _fadeDuration;
            float step = timer / _maxTimer;
            Color color = _startingColor;
            color.a = Mathf.Lerp(_startingColor.a, _targetAlpha, step);
            _meshRenderer.material.color = color;
            
            yield return wait;
        }
        
        Explode();
    }

    private void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, _radiusExpl);

        foreach (var hit in colliders)
        {
            if (hit.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(_explosionForce, transform.position, _radiusExpl);
            }
        }
        
        Disabled?.Invoke(this);
    }
}