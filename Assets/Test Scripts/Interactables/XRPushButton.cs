using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace my_unity_integration
{
    // Require BoxCollider component to be attached to the same GameObject
    [RequireComponent(typeof(BoxCollider))]
    public class XRPushButton : XRBaseInteractable
    {
        // Customizable event to be invoked when the button is pushed
        public UnityEvent OnPushed;

        // Minimum depth the button can be pushed to
        [SerializeField, Tooltip("Minimum depth the button can be pushed to")]
        private float minimalPushDepth;

        // Maximum depth the button can be pushed to
        [SerializeField, Tooltip("Maximum depth the button can be pushed to")]
        private float maximumPushDepth;

        // The interactor that is currently pushing the button
        private XRBaseInteractor pushInteractor = null;

        // Flag to keep track of whether the button was previously pushed
        private bool previouslyPushed = false;

        // The old position of the button when it was last pushed
        private float oldPushPosition;

        protected override void OnEnable()
        {
            base.OnEnable();

            // Add listeners for when the interactor hovers over the button
            hoverEntered.AddListener(StartPush);
            hoverExited.AddListener(EndPush);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            // Remove listeners for when the interactor hovers over the button
            hoverEntered.RemoveListener(StartPush);
            hoverExited.RemoveListener(EndPush);
        }

        private void Start()
        {
            // Get the BoxCollider component attached to the same GameObject
            BoxCollider boxCollider = GetComponent<BoxCollider>();

            // Set the minimum and maximum push depths based on the button's initial position and size
            minimalPushDepth = transform.localPosition.y;
            maximumPushDepth = transform.localPosition.y - (boxCollider.bounds.size.y * 0.55f);
        }

        private void StartPush(HoverEnterEventArgs arg0)
        {
            // Set the pushInteractor to the interactor that is currently pushing the button
            pushInteractor = (XRBaseInteractor)arg0.interactorObject;

            // Set the oldPushPosition to the current local Y position of the interactor
            oldPushPosition = GetLocalYPosition(((XRBaseInteractor)arg0.interactorObject).transform.position);
        }

        private void EndPush(HoverExitEventArgs arg0)
        {
            // Reset the pushInteractor, oldPushPosition, and previouslyPushed flags
            pushInteractor = null;
            oldPushPosition = 0.0f;
            previouslyPushed = false;

            // Reset the button's position to the minimum push depth
            SetYPosition(minimalPushDepth);
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            if (pushInteractor)
            {
                // Get the new local Y position of the interactor
                float newPushPosition = GetLocalYPosition(pushInteractor.transform.position);

                // Calculate the difference between the old and new local Y positions of the interactor
                float pushDifference = oldPushPosition - newPushPosition;

                // Update the oldPushPosition to the newPushPosition
                oldPushPosition = newPushPosition;

                // Calculate the new local Y position of the button based on the pushDifference
                float newPosition = transform.localPosition.y - pushDifference;

                // Set the new local Y position of the button, clamped between the minimum and maximum push depths
                SetYPosition(newPosition);

                // Check if the button is pressed within a specified range
                CheckPress();
            }
        }

        // Get the local Y position of the interactor relative to the button
        private float GetLocalYPosition(Vector3 interactorPosition)
        {
            return transform.root.InverseTransformDirection(interactorPosition).y;
        }

        // Set the Y position of the button, clamped between the minimum and maximum push depths
        private void SetYPosition(float yPos)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.y = Mathf.Clamp(yPos, maximumPushDepth, minimalPushDepth);

            transform.localPosition = newPosition;
        }

        // Check if the button is pressed within a specified range and invoke the OnPushed event if it is
        private void CheckPress()
        {
            float inRange = Mathf.Clamp(transform.localPosition.y, maximumPushDepth, maximumPushDepth + 0.01f);
            bool isPushedDown = transform.localPosition.y == inRange;

            if (isPushedDown && !previouslyPushed)
                OnPushed.Invoke();

            previouslyPushed = isPushedDown;
        }

        // Toggle the enabled state of the XRPushButton component
        public void TogglePushbuttonState()
        {
            this.enabled = !this.enabled;
        }
    }
}