using UnityEngine;

namespace SonarUndDochSoFern.Player {
    sealed class Upgrades : IUpdatable {
        readonly IAvatar avatar;
        readonly AvatarSettings settings;

        Collider[] colliders;
        int colliderCount;

        public Upgrades(IAvatar avatar, AvatarSettings settings) {
            this.avatar = avatar;
            this.settings = settings;

            colliders = new Collider[8];
        }

        public void Update(float deltaTime) {
            colliderCount = Physics.OverlapSphereNonAlloc(avatar.position, settings.powerMagnetRadius, colliders, settings.powerLayer);
            for (int i = 0; i < colliderCount; i++) {
                if (colliders[i].TryGetComponent<PowerUpComponent>(out var power)) {
                    if (Vector3.Distance(avatar.position, power.position) < settings.powerCollectRadius) {
                        power.Collect(avatar);
                    } else {
                        power.position = Vector3.SmoothDamp(power.position, avatar.position, ref power.velocity, settings.powerMagnetTime, settings.powerMagnetSpeed, deltaTime);
                    }
                }
            }
        }
    }
}