using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Runtime.Player {
    [CreateAssetMenu]
    public class AvatarSettings : ScriptableObject {
        [Header("Movement")]
        [SerializeField, Range(0, 100)]
        public float maxSpeed = 10;
        [SerializeField, Range(0, 10)]
        public float smoothingTime = 1;
        [SerializeField, Range(0, 100)]
        public float jumpSpeed = 10;

        [Header("Look")]
        [SerializeField]
        public Vector2 cameraSpeed = Vector2.one;
        [SerializeField, Range(0, 10)]
        public float cameraSmoothing = 1;
        [SerializeField, Range(-180, 180)]
        public float cameraMinX = -90;
        [SerializeField, Range(-180, 180)]
        public float cameraMaxX = 90;

        [Header("Sonar")]
        [SerializeField, Expandable]
        public ParticleSystem sonarPrefab = default;
    }
}