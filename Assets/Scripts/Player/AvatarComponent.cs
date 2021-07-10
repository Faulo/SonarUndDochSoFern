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
            SmartUpdate,
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

        public event Action<ControllerColliderHit> onControllerColliderHit;

        public Vector3 forward => eyes.forward;
        public Vector3 velocity => movement.currentVelocity;
        public Vector3 position => eyes.position;
        public Quaternion rotation => eyes.rotation;


        void Awake() {
            input = new AvatarInput();
            movement = new Movement(this, settings, input.Player, character);
            look = new Look(this, settings, input.Player, body, eyes);
            sonar = new Sonar(this, settings, input.Player, eyes);
        }
        void OnEnable() {
            input.Enable();
        }
        void OnDisable() {
            input.Disable();
        }
        void Update() {
            if (updateMethod == UpdateMethod.Update) {
                look.Update(Time.deltaTime);
                movement.Update(Time.deltaTime);
                sonar.Update(Time.deltaTime);
            }
        }
        void FixedUpdate() {
            if (updateMethod == UpdateMethod.FixedUpdate) {
                look.Update(Time.deltaTime);
                movement.Update(Time.deltaTime);
                sonar.Update(Time.deltaTime);
            }
        }
        void LateUpdate() {
            if (updateMethod == UpdateMethod.LateUpdate) {
                look.Update(Time.deltaTime);
                movement.Update(Time.deltaTime);
                sonar.Update(Time.deltaTime);
            }
        }
        void OnControllerColliderHit(ControllerColliderHit hit) {
            onControllerColliderHit?.Invoke(hit);
        }
    }
}