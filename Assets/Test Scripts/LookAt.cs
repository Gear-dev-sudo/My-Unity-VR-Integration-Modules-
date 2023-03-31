using UnityEngine;
using System.Collections;
using UnityEditor;

namespace my_unity_integration
{
    public class LookAt : MonoBehaviour
    {
        public Transform target;
        void Awake()
        {
            //deprecated...
            //target=GameObject.Find("XR Origin").transform; 
        }
        

        [Tooltip("the offset to the target to look at, for example")]
        public Vector3 positionOffset = new Vector3(0, 1.5f, 0);
        public float moveSpeedFactor = 3.0f;

        void Update()
        {

            Vector3 relativePos = (target.position + positionOffset) - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            Quaternion current = transform.localRotation;

            transform.localRotation = Quaternion.Slerp(current, rotation, Time.fixedDeltaTime);

        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Sets up the interface for the CopyTransform script.
    /// </summary>
    [CustomEditor(typeof(LookAt))]
    public class LookAtEditor : Editor
    {
       
        public override void OnInspectorGUI()
        {
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
            var rbf = target as LookAt;
            
            if (rbf.target == null)
            {
                EditorGUILayout.HelpBox(
                    "Please assign a target to Look at.", MessageType.Error);
            }
            rbf.target = (Transform)EditorGUILayout.ObjectField("Target", rbf.target, typeof(Transform), true);
            EditorGUILayout.Space();
            rbf.positionOffset = EditorGUILayout.Vector3Field(new GUIContent("PositionOffset", "set the Position offset to the place to look at."), rbf.positionOffset);
        }


    }

#endif
}