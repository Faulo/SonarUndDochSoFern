using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Player {
    public class Movement : IUpdatable, IDisposable {
        enum JumpState {
            NotJumping,
            ShortJump,
            MediumJump,
            LongJump,
        }
        AvatarSettings settings;
        AvatarInput.PlayerActions input;
        CharacterController character;

        public Vector3 velocity;
        Vector3 acceleration;

        bool intendsToJump;
        JumpState jumpState;
        float jumpTimer;

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
            ProcessJump();

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
            /*
            if (character.isGrounded) {
                velocity.y = settings.jumpSpeed;
                jumpTimer = 0;
                jumpState = JumpState.ShortJump;
                intendsToJump = true;
            }
            //*/
        }
        void HandleJumpCancel(InputAction.CallbackContext context) {
            //intendsToJump = false;
            //jumpState = JumpState.NotJumping;
        }
        void ProcessJump() {
            switch (jumpState) {
                case JumpState.NotJumping:
                    if (character.isGrounded && input.Jump.phase == InputActionPhase.Started) {
                        jumpTimer = 0;
                        jumpState = JumpState.ShortJump;
                        velocity.y = settings.jumpStartSpeed;
                    }
                    break;
                case JumpState.ShortJump:
                    jumpTimer += Time.deltaTime;
                    if (jumpTimer >= settings.shortJumpInputDuration && input.Jump.phase == InputActionPhase.Started) {
                        jumpState = JumpState.MediumJump;
                    }
                    break;
                case JumpState.MediumJump:
                    jumpTimer += Time.deltaTime;
                    if (jumpTimer >= settings.mediumJumpInputDuration && input.Jump.phase == InputActionPhase.Started) {
                        jumpState = JumpState.LongJump;
                    }
                    break;
                case JumpState.LongJump:
                    jumpTimer += Time.deltaTime;
                    break;
                default:
                    break;
            }
            if (jumpState != JumpState.NotJumping) {
                float duration = jumpState switch {
                    JumpState.ShortJump => settings.shortJumpExecutionDuration,
                    JumpState.MediumJump => settings.mediumJumpExecutionDuration,
                    JumpState.LongJump => settings.longJumpExecutionDuration,
                    _ => throw new NotImplementedException(),
                };
                if (jumpTimer >= duration) {
                    jumpState = JumpState.NotJumping;
                    velocity.y = Math.Min(velocity.y, settings.jumpStopSpeed);
                }
            }
        }
        void HandleSonarStart(InputAction.CallbackContext context) {
        }
        void HandleSonarCancel(InputAction.CallbackContext context) {
        }
    }
}