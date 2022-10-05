using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SonarUndDochSoFern.Player {
    sealed class Look : IUpdatable, IDisposable {
        readonly IAvatar avatar;
        readonly AvatarSettings settings;
        readonly AvatarInput.PlayerActions input;
        readonly Transform body;
        readonly Transform eyes;
        readonly CinemachineVirtualCamera cinemachine;

        float horizontalAngle;
        float horizontalSpeed;

        float verticalAngle;
        float verticalSpeed;

        bool isLocked {
            get => Cursor.lockState == CursorLockMode.Locked;
            set => Cursor.lockState = value
                ? CursorLockMode.Locked
                : CursorLockMode.None;
        }

        public Look(IAvatar avatar, AvatarSettings settings, AvatarInput.PlayerActions input, Transform body, Transform eyes, CinemachineVirtualCamera cinemachine) {
            this.avatar = avatar;
            this.settings = settings;
            this.input = input;
            this.body = body;
            this.eyes = eyes;
            this.cinemachine = cinemachine;

            RegisterInput();
        }

        public void Dispose() {
            isLocked = false;
            UnregisterInput();
        }

        void RegisterInput() {
            input.Sonar.started += HandleSonarStart;
            input.Menu.started += HandleMenuStart;
        }

        void UnregisterInput() {
            input.Sonar.started -= HandleSonarStart;
            input.Menu.started -= HandleMenuStart;
        }

        public void Update(float deltaTime) {
            if (isLocked) {
                var deltaLook = input.Look.ReadValue<Vector2>() * settings.cameraSpeed;

                horizontalAngle = Mathf.SmoothDampAngle(
                    horizontalAngle,
                    horizontalAngle + deltaLook.x,
                    ref horizontalSpeed,
                    settings.cameraSmoothing,
                    float.PositiveInfinity,
                    deltaTime
                );
                body.localRotation = Quaternion.Euler(0, horizontalAngle, 0);

                verticalAngle = Mathf.SmoothDampAngle(
                    verticalAngle,
                    Mathf.Clamp(verticalAngle - deltaLook.y, settings.cameraMinX, settings.cameraMaxX),
                    ref verticalSpeed,
                    settings.cameraSmoothing,
                    float.PositiveInfinity,
                    deltaTime
                );
                eyes.localRotation = Quaternion.Euler(verticalAngle, 0, 0);
                UpdateFOV();
            }
        }

        float fieldOfView {
            get => cinemachine.m_Lens.FieldOfView;
            set => cinemachine.m_Lens.FieldOfView = value;
        }
        float fieldOfViewVelocity;
        void UpdateFOV() {
            fieldOfView = Mathf.SmoothDamp(
                fieldOfView,
                avatar.isRunning ? settings.runningFieldOfView : settings.defaultFieldOfView,
                ref fieldOfViewVelocity,
                settings.fieldOfViewSmoothingTime
            );
        }

        void HandleSonarStart(InputAction.CallbackContext context) {
            isLocked = true;
        }
        void HandleMenuStart(InputAction.CallbackContext context) {
            isLocked = false;
        }
    }
}