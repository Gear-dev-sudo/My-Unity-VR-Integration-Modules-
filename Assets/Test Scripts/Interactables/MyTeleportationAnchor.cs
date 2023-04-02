using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace my_unity_integration
{

    public class MyTeleportationAnchor : BaseTeleportationInteractable
    {
        [SerializeField]
        [Tooltip("The Transform that represents the teleportation destination.")]
        public Transform teleportAnchorTransform;


        protected void OnValidate()
        {
            if (teleportAnchorTransform == null)
                teleportAnchorTransform = transform;

            Collider cl = customReticle.GetComponent<Collider>();
            if (cl != null)
            {
                Destroy(cl);
            }
        }

        /// <inheritdoc />
        protected override void Reset()
        {
            base.Reset();
            teleportAnchorTransform = transform;
        }


        /// <inheritdoc />
        protected override bool GenerateTeleportRequest(IXRInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
        {
            if (teleportAnchorTransform == null)
                return false;

            teleportRequest.destinationPosition = teleportAnchorTransform.position;
            teleportRequest.destinationRotation = teleportAnchorTransform.rotation;
            return true;
        }
    }
}
