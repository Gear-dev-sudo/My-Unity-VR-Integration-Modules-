using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace my_unity_integration
{
    public class ClimbGravityController : MonoBehaviour
    {

        private bool _isClimbActive = false;

        private CharacterController _characterController;

        void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            ClimbProvider.ClimbActive += ClimbActive;
            ClimbProvider.ClimbInActive += ClimbInactive;

        }
        void OnDestroy()
        {
            ClimbProvider.ClimbActive -= ClimbActive;
            ClimbProvider.ClimbInActive -= ClimbInactive;
        }


        void FixedUpdate()
        {
            if (!_characterController.isGrounded && !_isClimbActive)
            {
                Debug.LogWarning("Dropping");

                _characterController.SimpleMove(new Vector3());
            }
        }

        private void ClimbActive()
        {
            _isClimbActive = true;

        }
        private void ClimbInactive()
        {
            _isClimbActive = false;

        }




    }
}