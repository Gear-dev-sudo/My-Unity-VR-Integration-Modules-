
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace my_unity_integration
{

    public class MyTeleportationArea : BaseTeleportationInteractable
    {

        //Destroy the Collider Of the Reticle To prevent Interference
        protected override void Awake()
        {
            base.Awake();
            Collider cl=customReticle.GetComponent<Collider>();
             if (cl!=null)
            {
                Destroy(cl);
            }
        }


        /// <inheritdoc />
        protected override bool GenerateTeleportRequest(IXRInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
        {
            if (raycastHit.collider == null)
                return false;

            teleportRequest.destinationPosition = raycastHit.point;
            teleportRequest.destinationRotation = transform.rotation;
            return true;
        }
    }
}
