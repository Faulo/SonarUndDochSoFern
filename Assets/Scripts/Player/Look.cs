using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Player {
    public class Look : IDisposable {
        AvatarSettings settings;
        AvatarInput.PlayerActions input;
        Transform body;
        Transform eyes;

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

        public Look(AvatarSettings settings, AvatarInput.PlayerActions input, Transform body, Transform eyes) {
            this.settings = settings;
            this.input = input;
            this.body = body;
            this.eyes = eyes;

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
            }
        }

        void HandleSonarStart(InputAction.CallbackContext context) {
            isLocked = true;
        }
        void HandleMenuStart(InputAction.CallbackContext context) {
            isLocked = false;
        }
    }
}