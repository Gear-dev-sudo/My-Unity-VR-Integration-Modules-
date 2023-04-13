using UnityEngine;

public class MoveAlongX : MonoBehaviour
{
    [Tooltip("The speed at which the object moves along its local x-axis.")]
    public float speed = 2f;

    private void Start()
    {
        // Move the object along its local x-axis by the specified speed
        transform.Translate(Vector3.forward*speed);
    }
}
