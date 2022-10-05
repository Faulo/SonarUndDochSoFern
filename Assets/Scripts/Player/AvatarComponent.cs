using System;
using Cinemachine;
using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.Rendering;

namespace SonarUndDochSoFern.Player {
    sealed class AvatarComponent : MonoBehaviour, IAvatar {
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
        CinemachineVirtualCamera cinemachineCamera = default;
        [SerializeField, Expandable]
        VolumeProfile globalVolume = default;
        [SerializeField, Expandable]
        AudioSource sonarAudio = default;

        [Header("Unity Configuration")]
        [SerializeField]
        UpdateMethod updateMethod = UpdateMethod.FixedUpdate;

        AvatarInput input;
        Movement movement;
        Look look;
        Sonar sonar;
        Upgrades upgrades;

        public event Action<ControllerColliderHit> onControllerColliderHit;
        public event Action onGainBurst;
        public event Action onGainBomb;
        public event Action onJumpCountChanged;
        public event Action onAmmoCountChanged;

        public Vector3 forward => eyes.forward;
        public Vector3 velocity => movement.currentVelocity;
        public Vector3 position => eyes.position;
        public Quaternion rotation => eyes.rotation;
        public bool isRunning => movement.isRunning;
        public bool hasBurst {
            get => m_hasBurst;
            set {
                if (m_hasBurst != value) {
                    m_hasBurst = value;
                    if (value) {
                        onGainBurst?.Invoke();
                    }
                }
            }
        }
        [SerializeField]
        bool m_hasBurst = false;
        public bool hasBomb {
            get => m_hasBomb;
            set {
                if (m_hasBomb != value) {
                    m_hasBomb = value;
                    if (value) {
                        onGainBomb?.Invoke();
                    }
                }
            }
        }
        [SerializeField]
        bool m_hasBomb = false;
        public int jumpCount {
            get => m_jumpCount;
            set {
                if (m_jumpCount != value) {
                    m_jumpCount = value;
                    onJumpCountChanged?.Invoke();
                }
            }
        }
        [SerializeField]
        int m_jumpCount = 1;
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
            look = new Look(this, settings, input.Player, body, eyes, cinemachineCamera);
            sonar = new Sonar(this, settings, input.Player, eyes, globalVolume, sonarAudio);
            upgrades = new Upgrades(this, settings, input.Player);

            onGainBurst += () => settings.onGainBurst.Invoke(gameObject);
            onGainBomb += () => settings.onGainBomb.Invoke(gameObject);
            onJumpCountChanged += () => settings.onJumpCountChanged.Invoke(gameObject);
            onAmmoCountChanged += () => settings.onAmmoCountChanged.Invoke(gameObject);
        }
        void OnEnable() {
            input.Enable();
        }
        void OnDisable() {
            input.Disable();
        }
        void OnDestroy() {
            movement.Dispose();
            look.Dispose();
            sonar.Dispose();
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
            if (hit.collider.gameObject.layer == settings.goalLayer) {
                settings.onTouchGoal.Invoke(gameObject);
            }
        }
    }
}