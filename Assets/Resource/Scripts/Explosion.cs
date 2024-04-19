using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _radius;
    
    private float _originalState = 1f;
    private float _multiplierForce = 3000f;
    private float _multiplierRadius = 7500f;

    public void Scatter(Rigidbody rb)
    {
        Detonate(rb, _force, _radius);
    }

    public void CalculateNeighborhood()
    {
        float force = (_originalState - transform.localScale.x) * _multiplierForce;
        float radius = (_originalState - transform.localScale.x) * _multiplierRadius;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hit in colliders)
        {
            if (hit.TryGetComponent(out Rigidbody rb))
            {
                Detonate(rb, force, radius);
            }
        }
    }

    private void Detonate(Rigidbody cube, float force, float radius)
    {
        cube.AddExplosionForce(force, transform.position, radius);
    }
}