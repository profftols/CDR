using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _radiusExpl;

    public event Action<Bomb> BackInPool;

    private int _minTimer = 2;
    private int _maxTimer = 6;
    private MeshRenderer _meshRenderer;
    private Color _startingColor;
    private float _fadeDuration = 2f;
    private float _targetAlpha = 0f;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _startingColor = _meshRenderer.material.color;
        _fadeDuration = Random.Range(_minTimer, _maxTimer);
    }

    public void StartBoom()
    {
        StartCoroutine(LaunchDeath());
    }

    private IEnumerator LaunchDeath()
    {
        float timer = 0;

        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            float step = timer / _fadeDuration;
            Color color = _startingColor;
            color.a = Mathf.Lerp(_startingColor.a, _targetAlpha, step);
            _meshRenderer.material.color = color;
            
            yield return null;
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

        BackInPool?.Invoke(this);
    }
}