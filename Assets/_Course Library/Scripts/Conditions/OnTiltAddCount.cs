using System;
using UnityEngine;
using UnityEngine.Events;

public class OnTiltAddCount : MonoBehaviour
{
    [Tooltip("Tilt range, 0 - 180 degrees")]
    [Range(0, 1)] public float threshold = 0.0f;

    [Serializable] public class TiltEvent : UnityEvent<MonoBehaviour> { }

    // Threshold has been broken
    public TiltEvent OnBegin = new TiltEvent();

    // Threshold is no longer broken
    public TiltEvent OnEnd = new TiltEvent();

    [Tooltip("Time spent between OnBegin and OnEnd TiltEvents")]
   // public float timeBetweenEvents = 0.0f;

    private bool withinThreshold = false;


    public bool pouringWater = false;

    private void Update()
    {
        CheckOrientation();
    }

    private void CheckOrientation()
    {
        float similarity = Vector3.Dot(-transform.up, Vector3.up);
        similarity = Mathf.InverseLerp(-1, 1, similarity);

        bool thresholdCheck = similarity >= threshold;

        if (withinThreshold != thresholdCheck)
        {
            withinThreshold = thresholdCheck;

            if (withinThreshold)
            {
               
                OnBegin.Invoke(this);
                pouringWater = true;
            }
            else
            {
                pouringWater = false;
                OnEnd.Invoke(this);
            }
        }
    }
}
