using System.Collections.Generic;
using UnityEngine;

public class DisableCollidingFireParticles : MonoBehaviour
{
    [Tooltip("The tag of the particles to disable when colliding with this particle system.")]
    public string fireTag = "Fire";

    private ParticleSystem particleSystem;
    public List<ParticleCollisionEvent> collisionEvents;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

        int i = 0;
        while (i<numCollisionEvents)
        {
            if(other.CompareTag("Fire"))
            {
                StartCoroutine(DisableAfterDelay(other, 2f));
            }

            i++;
        }
    }

    private System.Collections.IEnumerator DisableAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
