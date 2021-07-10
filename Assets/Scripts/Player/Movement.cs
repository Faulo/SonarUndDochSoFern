using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Player {
    public class Movement : IUpdatable, IDisposable {
        AvatarSettings settings;
        AvatarInput.PlayerActions input;
        CharacterController character;

        public Vector3 velocity;
        Vector3 acceleration;

        public Movement(AvatarSettings settings, AvatarInput.PlayerActions input, CharacterController character) {
            this.settings = settings;
            this.character = character;
            this.input = input;

            RegisterInput();
        }

        public void Dispose() {
            UnregisterInput();
        }

        void RegisterInput() {
            input.Jump.started += HandleJumpStart;
            input.Jump.canceled += HandleJumpCancel;
            input.Sonar.started += HandleSonarStart;
            input.Sonar.canceled += HandleSonarCancel;
        }

        void UnregisterInput() {
            input.Jump.started -= HandleJumpStart;
            input.Jump.canceled -= HandleJumpCancel;
            input.Sonar.started -= HandleSonarStart;
            input.Sonar.canceled -= HandleSonarCancel;
        }

        public void Update(float deltaTime) {
            var movement = input.Movement.ReadValue<Vector2>();
            var targetVelocity = new Vector3(movement.x * settings.maxSpeed, velocity.y, movement.y * settings.maxSpeed);
            targetVelocity = character.transform.rotation * targetVelocity;
            velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref acceleration, settings.smoothingTime);

            float gravity = Physics.gravity.y * deltaTime;
            if (character.isGrounded && velocity.y <= gravity) {
                velocity.y = gravity;
            } else {
                velocity.y += gravity;
            }

            character.Move(velocity * deltaTime);
        }

        void HandleJumpStart(InputAction.CallbackContext context) {
            if (character.isGrounded) {
                velocity.y = settings.jumpSpeed;
            }
        }
        void HandleJumpCancel(InputAction.CallbackContext context) {
        }
        void HandleSonarStart(InputAction.CallbackContext context) {
        }
        void HandleSonarCancel(InputAction.CallbackContext context) {
        }
    }
}