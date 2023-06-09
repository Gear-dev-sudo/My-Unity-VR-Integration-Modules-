﻿using UnityEngine;

/// <summary>
/// This script creates a trail at the location of a gameobject with a particular width and color.
/// </summary>

namespace my_unity_integration
{


    public class CreateTrail : MonoBehaviour
    {
        
        public GameObject trailPrefab = null;

        public float width = 0.05f;
        public Color color = Color.white;

        public GameObject currentTrail = null;
        public  void  Start()
        {
            if (trailPrefab != null)
            {
                TrailRenderer trailRenderer = trailPrefab.GetComponent<TrailRenderer>();
                if (trailRenderer == null)
                {
                    Debug.LogError("Field does not have a TrailRenderer component!");
                }
                if(trailPrefab.TryGetComponent<Collider>(out Collider collider))
                {
                    Debug.LogWarning("Trail Prefab Collider Will Interfere with Interactor");
                    Debug.LogWarning("Trail Prefab Collider Deleting");
                    DestroyImmediate(collider,true);
                }
            }
        }
        public void StartTrail()
        {
            if (!currentTrail)
            {
                currentTrail = Instantiate(trailPrefab, transform.position, transform.rotation, transform);
                ApplySettings(currentTrail);
            }
        }

        private void ApplySettings(GameObject trailObject)
        {
            TrailRenderer trailRenderer = trailObject.GetComponent<TrailRenderer>();
            trailRenderer.widthMultiplier = width;
            trailRenderer.startColor = color;
            trailRenderer.endColor = color;
        }

        public void EndTrail()
        {
            if (currentTrail)
            {
                currentTrail.transform.parent = null;
                currentTrail = null;
            }
        }

        public void SetWidth(float value)
        {
            width = value;
        }

        public void SetColor(Color value)
        {
            color = value;
        }
    }
}