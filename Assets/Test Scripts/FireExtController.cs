using System.Collections.Generic;
using UnityEngine;
using System;


public class FireExtController:MonoBehaviour
{


    [SerializeField]
 ParticleSystem part;
    [SerializeField]
 public List<ParticleCollisionEvent> collisionEvents;

    void Awake()
    {
       


    }
void Start()
{
    
    collisionEvents = new List<ParticleCollisionEvent>();
}

void OnParticleCollision(GameObject other)
{
    int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
    int i = 0;
        if (other.CompareTag("Fire"))
            Debug.LogWarning("Fire Collided....."+other+"   "+ numCollisionEvents);
    while (i < numCollisionEvents)
    {
        if (other.CompareTag("Fire"))
       {
                Debug.LogWarning("Fire Collided....." + numCollisionEvents + "numCollisionEvents "+ "i"+i);
            other.SetActive(false);
      }
        i++;
    }
}}