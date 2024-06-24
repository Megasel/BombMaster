using System;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;
    private bool isExploded;
    private Rigidbody rb;
    float explodeRadius = 0.4f;
    public LayerMask layer;
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out Chunk chunk))
        {
            Explode();
            Destroy(gameObject);
            //gameObject.SetActive(false);
            
        }
    }

    private void Start()
    {
        explodeRadius = GameManager.instance.explodeRadius;
        rb = GetComponent<Rigidbody>();
        Launch();
    }

    private void Explode()
    {
        if (isExploded)
            return;
        isExploded = true;
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        var colliders = Physics.OverlapSphere(transform.position, explodeRadius, layer);
        foreach (var item in colliders)
        {
            Chunk chunk = item.GetComponent<Chunk>();
            
                chunk.Explode();
            
        }

        //check if level ends
        GameManager.instance.voxelChunkManager.CheckIfLevelShouldEnds();
    }
    private void Launch()
    {
        rb.AddForce(transform.forward * 100, ForceMode.Impulse);
    }
}
