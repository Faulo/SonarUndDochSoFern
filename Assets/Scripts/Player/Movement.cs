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

        IAvatar avatar;
        AvatarSettings settings;
        AvatarInput.PlayerActions input;
        CharacterController character;

        Vector3 targetVelocity;
        Vector2 targetMovement;
        public Vector3 currentVelocity;
        Vector3 acceleration;

        bool intendsToJump;
        JumpState jumpState;
        float jumpTimer;
        float currentSpeed => input.Sprint.phase == InputActionPhase.Started
            ? settings.runningSpeed
            : settings.walkingSpeed;

        public Movement(IAvatar avatar, AvatarSettings settings, AvatarInput.PlayerActions input, CharacterController character) {
            this.avatar = avatar;
            this.settings = settings;
            this.character = character;
            this.input = input;

            RegisterInput();
        }

        public void Dispose() {
            UnregisterInput();
        }

        void RegisterInput() {
        }

        void UnregisterInput() {
        }

        public void Update(float deltaTime) {
            CalculateTargetVelocity();
            ProcessJump();

            targetVelocity.y = currentVelocity.y;
            currentVelocity = Vector3.SmoothDamp(currentVelocity, targetVelocity, ref acceleration, settings.smoothingTime);

            float gravity = Physics.gravity.y * deltaTime;
            if (character.isGrounded && currentVelocity.y <= gravity) {
                currentVelocity.y = gravity;
            } else {
                currentVelocity.y += gravity;
            }

            character.Move(currentVelocity * deltaTime);
        }

        void CalculateTargetVelocity() {
            var movement = input.Movement.ReadValue<Vector2>();
            targetVelocity = character.transform.rotation * new Vector3(movement.x, 0, movement.y);
            targetVelocity *= currentSpeed;
            if (targetVelocity != Vector3.zero) {
                float forward = Vector3.Dot(targetVelocity.normalized, character.transform.forward);
                targetVelocity *= settings.speedOverForward.Evaluate(forward);
            }
            targetMovement = new Vector2(targetVelocity.x, targetVelocity.z);
            if (targetMovement != Vector2.zero) {
                targetMovement.Normalize();
            }
        }
        void ProcessJump() {
            switch (jumpState) {
                case JumpState.NotJumping:
                    if (character.isGrounded && input.Jump.phase == InputActionPhase.Started) {
                        jumpTimer = 0;
                        jumpState = JumpState.ShortJump;
                        currentVelocity.x += targetMovement.x * settings.jumpStartSpeed.x;
                        currentVelocity.y = settings.jumpStartSpeed.y;
                        currentVelocity.z += targetMovement.y * settings.jumpStartSpeed.x;
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
                    currentVelocity.x += targetMovement.x * settings.jumpStopSpeed.x;
                    currentVelocity.y = Math.Min(currentVelocity.y, settings.jumpStopSpeed.y);
                    currentVelocity.z += targetMovement.y * settings.jumpStopSpeed.x;
                }
            }
        }
    }
}