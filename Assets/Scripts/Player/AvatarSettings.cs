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

        [Header("Jumping")]
        [SerializeField, Range(0, 100)]
        public float jumpStartSpeed = 10;
        [SerializeField, Range(0, 100)]
        public float jumpStopSpeed = 2;
        [SerializeField, Range(0, 1)]
        public float shortJumpInputDuration = 0.1f;
        [SerializeField, Range(0, 1)]
        public float mediumJumpInputDuration = 0.2f;
        [SerializeField, Range(0, 1)]
        public float shortJumpExecutionDuration = 0.25f;
        [SerializeField, Range(0, 1)]
        public float mediumJumpExecutionDuration = 0.5f;
        [SerializeField, Range(0, 1)]
        public float longJumpExecutionDuration = 0.75f;

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