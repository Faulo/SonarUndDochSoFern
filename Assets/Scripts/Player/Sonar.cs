using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Runtime.Player {
    public class Sonar : IUpdatable, IDisposable {
        readonly IAvatar avatar;
        readonly AvatarSettings settings;
        readonly AvatarInput.PlayerActions input;
        readonly Transform eyes;
        readonly Vignette vignette;

        ParticleSystem paintParticles;
        ParticleSystem.MainModule paintMain;

        ParticleSystem burstParticles;
        ParticleSystem.MainModule burstMain;
        ParticleSystem.EmissionModule burstEmission;
        float normalizedAmmo => (float)paintParticles.particleCount / paintMain.maxParticles;

        public Sonar(IAvatar avatar, AvatarSettings settings, AvatarInput.PlayerActions input, Transform eyes, VolumeProfile volume) {
            this.avatar = avatar;
            this.settings = settings;
            this.input = input;
            this.eyes = eyes;
            volume.TryGet(out vignette);

            RegisterInput();
            SetupParticles();
        }

        public void Dispose() {
            UnregisterInput();
            DestroyParticles();
        }

        void SetupParticles() {
            paintParticles = UnityEngine.Object.Instantiate(settings.paintPrefab, eyes);
            paintMain = paintParticles.main;

            burstParticles = UnityEngine.Object.Instantiate(settings.burstPrefab, eyes);
            if (burstParticles.TryGetComponent<ParticleComponent>(out var particles)) {
                particles.paintSystem = paintParticles;
            }
            burstMain = burstParticles.main;
            burstEmission = burstParticles.emission;

            burstEmission.enabled = false;

            avatar.onAmmoCountChanged += UpdateAmmo;
            UpdateAmmo();
        }

        void UpdateAmmo() {
            paintMain.maxParticles = avatar.ammoCount * settings.paintPerAmmoCount;
        }

        void DestroyParticles() {
            UnityEngine.Object.Destroy(burstParticles.gameObject);
            UnityEngine.Object.Destroy(paintParticles.gameObject);
            avatar.onAmmoCountChanged -= UpdateAmmo;
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
            burstMain.emitterVelocity = avatar.velocity;
            vignette.intensity.Override(settings.vignetteOverAmmo.Evaluate(normalizedAmmo));
        }
        void HandleSonarStart(InputAction.CallbackContext context) {
            if (!avatar.hasBurst) {
                return;
            }
            burstEmission.enabled = true;
            burstParticles.Emit(settings.sonarBurstCount);
        }
        void HandleSonarCancel(InputAction.CallbackContext context) {
            burstEmission.enabled = false;
        }
        void HandleSpecialStart(InputAction.CallbackContext context) {
            if (!avatar.hasBomb) {
                return;
            }
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