using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Player {
    public class Sonar : IUpdatable, IDisposable {
        AvatarSettings settings;
        AvatarInput.PlayerActions input;
        Transform eyes;
        Func<Vector3> velocityCalculator;

        ParticleSystem particles;
        ParticleSystem.MainModule particlesMain;

        public Sonar(AvatarSettings settings, AvatarInput.PlayerActions input, Transform eyes, Func<Vector3> velocityCalculator) {
            this.settings = settings;
            this.input = input;
            this.eyes = eyes;
            this.velocityCalculator = velocityCalculator;

            RegisterInput();
            particles = UnityEngine.Object.Instantiate(settings.sonarPrefab, eyes);
            particlesMain = particles.main;
        }

        public void Dispose() {
            UnregisterInput();
            UnityEngine.Object.Destroy(particles.gameObject);
        }

        void RegisterInput() {
            input.Sonar.started += HandleSonarStart;
            input.Sonar.canceled += HandleSonarCancel;
        }

        void UnregisterInput() {
            input.Sonar.started -= HandleSonarStart;
            input.Sonar.canceled -= HandleSonarCancel;
        }

        public void Update(float deltaTime) {
        }
        void HandleSonarStart(InputAction.CallbackContext context) {
            particlesMain.emitterVelocity = 10 * velocityCalculator();
            particles.Emit(1000);
        }
        void HandleSonarCancel(InputAction.CallbackContext context) {
        }
    }
}