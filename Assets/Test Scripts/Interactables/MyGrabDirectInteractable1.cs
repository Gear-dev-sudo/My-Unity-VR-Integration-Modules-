using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;

namespace my_unity_integration
{
    [RequireComponent(typeof(Collider))]
    public class MyGrabDirectInteractable1 : XRBaseInteractable
    {
        //Attach TransForms
        public Transform attachTransform;
        public Transform h_leftAttachTransform;
        public Transform h_rightAttachTransform;

        //Attach Offset
        public Vector3 attachPositionOffset;
        public Vector3 attachRotationOffset;

       

        private IXRInteractor _interactor;
        private Quaternion _attachInitialRotation;

        Transform attachPoint;
        public bool dualAttachCheckbox = false;
        public bool dynamicAttachCheckbox = false;
        public bool forceDetachGravityCheckbox = false;
        public bool OriginalGravityCheckbox = false;
        Rigidbody rb;
        protected override void Awake()
        {
            rb = GetComponent<Rigidbody>();
            base.Awake();
            if (dynamicAttachCheckbox)
            {
                this.attachTransform = null;
            }


            if (this.attachTransform == null && !dualAttachCheckbox)
            {
                GameObject newGO = new GameObject("Attach Transform of " + name);
                Transform newTransform = newGO.transform;
                newTransform.parent = gameObject.transform;
                newTransform.localPosition = Vector3.zero;
                this.attachTransform = newTransform;
                attachPoint = newTransform;
            }

            else if (this.attachTransform != null && !dualAttachCheckbox)
            {
                attachPoint = this.attachTransform;
            }


            if (this.attachTransform != null)
                _attachInitialRotation = attachTransform.localRotation;



        }

        protected override void OnEnable()
        {
            base.OnEnable();
            this.selectEntered.AddListener(XRSelectEnter);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            this.selectEntered.RemoveListener(XRSelectEnter);
        }
        public void XRSelectEnter(SelectEnterEventArgs selectEnterEventArgs)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            //Debug.LogWarning("XR SEL ENTER");
            if (!dualAttachCheckbox && dynamicAttachCheckbox)
            {
                attachPoint.position = selectEnterEventArgs.interactorObject.transform.position;

                attachPoint.rotation = selectEnterEventArgs.interactorObject.transform.rotation;
            }
            else if (dualAttachCheckbox && !dynamicAttachCheckbox)
            {
                if (selectEnterEventArgs.interactorObject.transform.CompareTag("Left Hand"))
                {
                    // Debug.LogWarning("Left Hand");
                    attachTransform = h_leftAttachTransform;
                    attachPoint = h_leftAttachTransform;
                    _attachInitialRotation = attachTransform.localRotation;
                   // Debug.LogWarning(_attachInitialRotation+"Left");
                }
                else if (selectEnterEventArgs.interactorObject.transform.CompareTag("Right Hand"))
                {
                    //Debug.LogWarning("Right Hand");
                    attachTransform = h_rightAttachTransform;
                    attachPoint = h_rightAttachTransform;
                    _attachInitialRotation = attachTransform.localRotation;
                  //  Debug.LogWarning(_attachInitialRotation+"Right");
                }

            }


        }
        Vector3 velo;
        private Vector3 angVelo;
        Vector3 lastTrans;
        Quaternion _lastRotation;
        void FixedUpdate()
        {

            velo = Vector3.ClampMagnitude((this.transform.position - lastTrans) /(1.5f * (Time.fixedDeltaTime)),50);

            Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(_lastRotation);
            deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
            angVelo = angle * Mathf.Deg2Rad * axis / Time.fixedDeltaTime;
            _lastRotation = transform.rotation;

           // Debug.LogWarning("update velo" + velo);
         //   Debug.LogWarning("update angvelo" + angVelo);
            lastTrans = this.transform.position;
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
           
            
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            base.OnSelectEntered(args);
            _interactor = args.interactorObject;
            
            if (!dynamicAttachCheckbox)
            {
                args.interactableObject.transform.position = (args.interactorObject).transform.position;

                args.interactableObject.transform.Translate(args.interactableObject.transform.position - attachPoint.position);

            }

            Attach();
        }

        Vector3 InteractorLocalPosition;
        Quaternion InteractorLocalRotation;



        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            Rigidbody rb = GetComponent<Rigidbody>();
            

            if (rb != null && throwOnDetach)
            {
                if(forceDetachGravityCheckbox)
                    rb.useGravity = true;


                rb.velocity = velo;
                rb.angularVelocity = angVelo;

            
            }
            else if(!throwOnDetach)
                    {
                if (forceDetachGravityCheckbox)
                    rb.useGravity = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
          
        }

 
        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
    
            base.OnSelectExiting(args);
            Detach();

            _interactor = null;

            
        }

        private void Attach()
        {
            if ((_interactor is XRDirectInteractor))
            {
                kinematicSetting = transform.gameObject.GetComponent<Rigidbody>().isKinematic;
                // 将游戏对象附加到交互器上
                transform.SetParent(_interactor.transform);
                transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                transform.localPosition += attachPositionOffset;
                transform.localRotation = Quaternion.Euler(attachRotationOffset) * _attachInitialRotation;

            }
        }


        bool throwOnDetach=true;
        private bool kinematicSetting;

        /// <summary>
        /// 
        /// </summary>
        private void Detach()
        {


            if (_interactor is XRDirectInteractor)
            {
                // 将游戏对象从交互器上分离
                transform.SetParent(null);
                transform.gameObject.GetComponent<Rigidbody>().isKinematic =kinematicSetting;
                attachTransform.localRotation = _attachInitialRotation;


            }

        }










#if UNITY_EDITOR
        [CustomEditor(typeof(MyGrabDirectInteractable))]
        public class MyGrabInteractableEditor : UnityEditor.XR.Interaction.Toolkit.XRBaseInteractableEditor
        {
            //protected SerializedProperty m_InteractionLayers;
            public static readonly GUIContent interactionLayerSetting = EditorGUIUtility.TrTextContent("Interaction Layer Mask", "Allows interaction with Interactors whose Interaction Layer Mask overlaps with any Layer in this Interaction Layer Mask.");

            public override void OnInspectorGUI()
            {
                EditorGUILayout.LabelField("Interaction Layer", EditorStyles.boldLabel);
                m_InteractionLayers = serializedObject.FindProperty("m_InteractionLayers");
                serializedObject.Update();

                serializedObject.ApplyModifiedProperties();

                MyGrabDirectInteractable interactable = (MyGrabDirectInteractable)target;
                EditorGUILayout.PropertyField(m_InteractionLayers, interactionLayerSetting);
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Attach", EditorStyles.boldLabel);
                interactable.dualAttachCheckbox = EditorGUILayout.Toggle("Dual Attach", interactable.dualAttachCheckbox);
                
                interactable.forceDetachGravityCheckbox = EditorGUILayout.Toggle("Force Gravity On Detach", interactable.forceDetachGravityCheckbox);
                interactable.dynamicAttachCheckbox = EditorGUILayout.Toggle("Dynamic Attach", interactable.dynamicAttachCheckbox);
               
                if (interactable.dualAttachCheckbox)
                {
                    EditorGUILayout.LabelField("Left/Right Hand Attach Points", EditorStyles.boldLabel);
                    interactable.h_leftAttachTransform = (Transform)EditorGUILayout.ObjectField("Left Hand Attach Transform", interactable.h_leftAttachTransform, typeof(Transform), true);
                    interactable.h_rightAttachTransform = (Transform)EditorGUILayout.ObjectField("Right Hand Attach Transform", interactable.h_rightAttachTransform, typeof(Transform), true);
                    // Add other menu items here
                }
               
                    if (!interactable.dynamicAttachCheckbox)
                    {
                        if(!interactable.dualAttachCheckbox)
                        interactable.attachTransform = (Transform)EditorGUILayout.ObjectField("Attach Transform", interactable.attachTransform, typeof(Transform), true);

                        interactable.attachRotationOffset = EditorGUILayout.Vector3Field("Rotation Offset", interactable.attachRotationOffset);
                    }
                    interactable.attachPositionOffset = EditorGUILayout.Vector3Field("Position Offset", interactable.attachPositionOffset);
                
                DrawInteractableEvents();
                serializedObject.ApplyModifiedProperties();//Hook Into the Interactable Events

            }

        }
#endif
    }
}