using System;
using Cinemachine;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Runtime.Player {
    public class AvatarComponent : MonoBehaviour, IAvatar {
        public enum UpdateMethod {
            Update,
            FixedUpdate,
            LateUpdate,
        };
        [Header("MonoBehaviour Configuration")]
        [SerializeField, Expandable]
        AvatarSettings settings = default;
        [SerializeField, Expandable]
        CharacterController character = default;
        [SerializeField, Expandable]
        Transform body = default;
        [SerializeField, Expandable]
        Transform eyes = default;
        [SerializeField, Expandable]
        Camera unityCamera = default;
        [SerializeField, Expandable]
        CinemachineVirtualCamera cinemachineCamera = default;

        [Header("Unity Configuration")]
        [SerializeField]
        UpdateMethod updateMethod = UpdateMethod.FixedUpdate;

        AvatarInput input;
        Movement movement;
        Look look;
        Sonar sonar;
        Upgrades upgrades;

        public event Action<ControllerColliderHit> onControllerColliderHit;
        public event Action onAmmoCountChanged;

        public Vector3 forward => eyes.forward;
        public Vector3 velocity => movement.currentVelocity;
        public Vector3 position => eyes.position;
        public Quaternion rotation => eyes.rotation;
        public bool hasBurst { get; set; } = true;
        public bool hasBomb { get; set; } = true;
        public int jumpCount { get; set; } = 2;
        public int ammoCount {
            get => m_ammoCount;
            set {
                if (m_ammoCount != value) {
                    m_ammoCount = value;
                    onAmmoCountChanged?.Invoke();
                }
            }
        }
        [SerializeField]
        int m_ammoCount = 1;


        void Awake() {
            input = new AvatarInput();
            movement = new Movement(this, settings, input.Player, character);
            look = new Look(this, settings, input.Player, body, eyes);
            sonar = new Sonar(this, settings, input.Player, eyes);
            upgrades = new Upgrades(this, settings, input.Player);
        }
        void OnEnable() {
            input.Enable();
        }
        void OnDisable() {
            input.Disable();
        }
        void Update() {
            if (updateMethod == UpdateMethod.Update) {
                UpdateAvatar(Time.deltaTime);
            }
        }
        void FixedUpdate() {
            if (updateMethod == UpdateMethod.FixedUpdate) {
                UpdateAvatar(Time.deltaTime);
            }
        }
        void LateUpdate() {
            if (updateMethod == UpdateMethod.LateUpdate) {
                UpdateAvatar(Time.deltaTime);
            }
        }
        void UpdateAvatar(float deltaTime) {
            upgrades.Update(deltaTime);
            look.Update(deltaTime);
            movement.Update(deltaTime);
            sonar.Update(deltaTime);
        }
        void OnControllerColliderHit(ControllerColliderHit hit) {
            onControllerColliderHit?.Invoke(hit);
        }
    }
}