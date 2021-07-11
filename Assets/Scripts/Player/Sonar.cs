using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Player {
    public class Sonar : IUpdatable, IDisposable {
        readonly IAvatar avatar;
        readonly AvatarSettings settings;
        readonly AvatarInput.PlayerActions input;
        readonly Transform eyes;

        ParticleSystem paintParticles;
        ParticleSystem burstParticles;
        ParticleSystem.MainModule particlesMain;
        ParticleSystem.EmissionModule particlesEmission;

        public Sonar(IAvatar avatar, AvatarSettings settings, AvatarInput.PlayerActions input, Transform eyes) {
            this.avatar = avatar;
            this.settings = settings;
            this.input = input;
            this.eyes = eyes;

            RegisterInput();
            SetupParticles();
        }

        public void Dispose() {
            UnregisterInput();
            DestroyParticles();
        }

        void SetupParticles() {
            paintParticles = UnityEngine.Object.Instantiate(settings.paintPrefab, eyes);
            burstParticles = UnityEngine.Object.Instantiate(settings.burstPrefab, eyes);
            if (burstParticles.TryGetComponent<ParticleComponent>(out var particles)) {
                particles.paintSystem = paintParticles;
            }
            particlesMain = burstParticles.main;
            particlesEmission = burstParticles.emission;

            particlesEmission.enabled = false;
        }
        void DestroyParticles() {
            UnityEngine.Object.Destroy(burstParticles.gameObject);
            UnityEngine.Object.Destroy(paintParticles.gameObject);
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
            burstParticles.Emit(settings.sonarBurstCount);
        }
        void HandleSonarCancel(InputAction.CallbackContext context) {
            particlesEmission.enabled = false;
        }
        void HandleSpecialStart(InputAction.CallbackContext context) {
            var special = UnityEngine.Object.Instantiate(settings.bombPrefab, avatar.position, avatar.rotation);
            if (special.TryGetComponent<ParticleComponent>(out var particles)) {
                particles.paintSystem = paintParticles;
            }
            if (special.TryGetComponent<Rigidbody>(out var rigidbody)) {
                rigidbody.velocity = avatar.velocity + (avatar.rotation * settings.specialEjectSpeed);
            }
        }
        void HandleSpecialCancel(InputAction.CallbackContext context) {
        }
    }
}