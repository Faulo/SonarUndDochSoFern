using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Player {
    public class Sonar : IUpdatable, IDisposable {
        readonly IAvatar avatar;
        readonly AvatarSettings settings;
        readonly AvatarInput.PlayerActions input;
        readonly Transform eyes;

        ParticleSystem particles;
        ParticleSystem.MainModule particlesMain;
        ParticleSystem.EmissionModule particlesEmission;

        public Sonar(IAvatar avatar, AvatarSettings settings, AvatarInput.PlayerActions input, Transform eyes) {
            this.avatar = avatar;
            this.settings = settings;
            this.input = input;
            this.eyes = eyes;

            RegisterInput();
            particles = UnityEngine.Object.Instantiate(settings.sonarPrefab, eyes);
            particlesMain = particles.main;
            particlesEmission = particles.emission;

            particlesEmission.enabled = false;
        }

        public void Dispose() {
            UnregisterInput();
            UnityEngine.Object.Destroy(particles.gameObject);
        }

        void RegisterInput() {
            input.Sonar.started += HandleSonarStart;
            input.Sonar.canceled += HandleSonarCancel;
            input.Special.started += HandleSpecialStart;
            input.Special.canceled += HandleSpecialCancel;
        }

        void UnregisterInput() {
            input.Sonar.started -= HandleSonarStart;
            input.Sonar.canceled -= HandleSonarCancel;
            input.Special.started -= HandleSpecialStart;
            input.Special.canceled -= HandleSpecialCancel;
        }

        public void Update(float deltaTime) {
            particlesMain.emitterVelocity = avatar.velocity;
        }
        void HandleSonarStart(InputAction.CallbackContext context) {
            particlesEmission.enabled = true;
            particles.Emit(settings.sonarBurstCount);
        }
        void HandleSonarCancel(InputAction.CallbackContext context) {
            particlesEmission.enabled = false;
        }
        void HandleSpecialStart(InputAction.CallbackContext context) {
            var special = UnityEngine.Object.Instantiate(settings.specialPrefab, avatar.position, avatar.rotation);
            if (special.TryGetComponent<Rigidbody>(out var rigidbody)) {
                rigidbody.velocity = avatar.velocity + (avatar.rotation * settings.specialEjectSpeed);
            }
        }
        void HandleSpecialCancel(InputAction.CallbackContext context) {
        }
    }
}