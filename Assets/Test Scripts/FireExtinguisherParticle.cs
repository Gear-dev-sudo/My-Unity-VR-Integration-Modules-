using UnityEngine;

public class FireExtinguisherParticle : MonoBehaviour
{
    [Tooltip("The particle system used for the fire extinguisher effect.")]
    public ParticleSystem particleSystem;

    [Tooltip("The duration of the particle effect.")]
    public float duration = 2f;

    [Tooltip("The force applied to the particles.")]
    public float force = 10f;

    [Tooltip("The radius of the explosion.")]
    public float radius = 1f;

    [Tooltip("The upward force applied to the particles.")]
    public float upwardsModifier = 1f;

    private void Start()
    {
        // Set the particle system to loop
        particleSystem.loop = true;

        // Set the duration of the particle effect
        particleSystem.Stop();
        particleSystem.playbackSpeed = 1 / duration;

        // Set the force applied to the particles
        var main = particleSystem.main;
        main.startSpeed = force;

        // Set the radius of the explosion
        var shape = particleSystem.shape;
        shape.radius = radius;

        // Set the upward force applied to the particles
        var forceOverLifetime = particleSystem.forceOverLifetime;
        forceOverLifetime.y = upwardsModifier;
    }

    // Play the particle effect
    public void Play()
    {
        particleSystem.Play();
    }

    // Stop the particle effect
    public void Stop()
    {
        particleSystem.Stop();
    }
}
