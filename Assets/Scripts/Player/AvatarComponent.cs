using Cinemachine;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Runtime.Player {
    public class AvatarComponent : MonoBehaviour {
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
        Movement avatar;
        Look look;
        Sonar sonar;

        void Awake() {
            input = new AvatarInput();
            avatar = new Movement(settings, input.Player, character);
            look = new Look(settings, input.Player, body, eyes);
            sonar = new Sonar(settings, input.Player, eyes, () => avatar.velocity);
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
                avatar.Update(Time.deltaTime);
                sonar.Update(Time.deltaTime);
            }
        }
        void FixedUpdate() {
            if (updateMethod == UpdateMethod.FixedUpdate) {
                look.Update(Time.deltaTime);
                avatar.Update(Time.deltaTime);
                sonar.Update(Time.deltaTime);
            }
        }
        void LateUpdate() {
            if (updateMethod == UpdateMethod.LateUpdate) {
                look.Update(Time.deltaTime);
                avatar.Update(Time.deltaTime);
                sonar.Update(Time.deltaTime);
            }
        }
    }
}