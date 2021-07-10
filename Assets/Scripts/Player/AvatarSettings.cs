using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Runtime.Player {
    [CreateAssetMenu]
    public class AvatarSettings : ScriptableObject {
        [Header("Movement")]
        [SerializeField, Range(0, 100)]
        public float walkingSpeed = 5;
        [SerializeField, Range(0, 100)]
        public float runningSpeed = 12;
        [SerializeField]
        public AnimationCurve speedOverForward = AnimationCurve.Constant(-1, 1, 1);
        [SerializeField, Range(0, 10)]
        public float smoothingTime = 1;

        [Header("Jumping")]
        [SerializeField]
        public Vector2 jumpStartSpeed = Vector2.one;
        [SerializeField]
        public Vector2 jumpStopSpeed = Vector2.one;
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