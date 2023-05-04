using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace my_unity_integration
{
    public class MyXRSocketInteractor : XRSocketInteractor
    {
        [Tooltip("The duration in seconds that the socket will be disabled after an object is selected.")]
        [SerializeField] private float disableDuration = 2f;

        [Tooltip("The hover mesh transform where the interactable will be moved.")]
        [SerializeField] private Transform hoverMeshTransform;

        private bool isDisabled = false;

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            if (!isDisabled)
            {
                StartCoroutine(DisableSocket());
            }
            base.OnSelectEntered(args);
        }

        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            args.interactableObject.transform.position = hoverMeshTransform.position;
            args.interactableObject.transform.rotation = hoverMeshTransform.rotation;
        }

        private IEnumerator DisableSocket()
        {
            isDisabled = true;
            this.enabled = false;
            yield return new WaitForSeconds(disableDuration);
            this.enabled = true;
            isDisabled = false;
        }

    }
}
