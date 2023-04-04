using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;


namespace my_unity_integration
{


    public class MyXRSocketInteractor : XRSocketInteractor
    {
         Matrix4x4 GetHoverMeshMatrix(IXRInteractable interactable, MeshFilter meshFilter, float hoverScale)
        {
            Debug.LogWarning("MemberHidden");
            var interactableAttachTransform = interactable.GetAttachTransform(this);

            var grabInteractable = interactable as XRGrabInteractable;

            // Get the "static" pose of the attach transform in world space.
            // While the interactable is selected, it may have a different pose than when released,
            // so this assumes it will be restored back to the original pose before a selection was made.
            Pose interactableAttachPose;
            if (interactable is IXRSelectInteractable selectable && selectable.isSelected &&
                interactableAttachTransform != interactable.transform &&
                interactableAttachTransform.IsChildOf(interactable.transform))
            {
                // The interactable's attach transform must not change parent Transform while selected
                // for the pose to be calculated correctly. This transforms the captured pose in local space
                // into the current pose in world space. If the pose of the attach transform was not modified
                // after being selected, this will be the same value as calculated in the else statement.
                var localAttachPose = selectable.GetLocalAttachPoseOnSelect(selectable.firstInteractorSelecting);
                var attachTransformParent = interactableAttachTransform.parent;
                interactableAttachPose =
                    new Pose(attachTransformParent.TransformPoint(localAttachPose.position),
                        attachTransformParent.rotation * localAttachPose.rotation);
            }
            else
            {
                interactableAttachPose = new Pose(interactableAttachTransform.position, interactableAttachTransform.rotation);
            }

            var attachOffset = meshFilter.transform.position - interactableAttachPose.position;
            var interactableLocalPosition = InverseTransformDirection(interactableAttachPose, attachOffset) * hoverScale;
            var interactableLocalRotation = Quaternion.Inverse(Quaternion.Inverse(meshFilter.transform.rotation) * interactableAttachPose.rotation);

            Vector3 position;
            Quaternion rotation;

            var interactorAttachTransform = GetAttachTransform(interactable);
            var interactorAttachPose = new Pose(interactorAttachTransform.position, interactorAttachTransform.rotation);
            if (grabInteractable == null || grabInteractable.trackRotation)
            {
                position = interactorAttachPose.rotation * interactableLocalPosition + interactorAttachPose.position;
                rotation = interactorAttachPose.rotation * interactableLocalRotation;
            }
            else
            {
                position = interactableAttachPose.rotation * interactableLocalPosition + interactorAttachPose.position;
                rotation = meshFilter.transform.rotation;
            }

            // Rare case that Track Position is disabled
            if (grabInteractable != null && !grabInteractable.trackPosition)
                position = meshFilter.transform.position;

            var scale = meshFilter.transform.lossyScale * hoverScale;

            return Matrix4x4.TRS(position, rotation, scale);
        }
        static Vector3 InverseTransformDirection(Pose pose, Vector3 direction)
        {
            return Quaternion.Inverse(pose.rotation) * direction;
        }


        protected override void OnHoverEntering(HoverEnterEventArgs args)
        {
            if (args.interactableObject is MyGrabDirectInteractable)
            {
                args.interactableObject.transform.rotation = attachTransform.rotation;
                base.OnHoverEntering(args);
                args.interactableObject.transform.parent = this.transform;
            }
        }

    }
}