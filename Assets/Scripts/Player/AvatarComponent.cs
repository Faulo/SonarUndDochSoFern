using Cinemachine;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Runtime.Player {
    public class AvatarComponent : MonoBehaviour {
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

        AvatarInput input;
        Movement avatar;
        Look look;

        void Awake() {
            input = new AvatarInput();
            avatar = new Movement(settings, input.Player, character);
            look = new Look(settings, input.Player, body, eyes);
        }
        void OnEnable() {
            input.Enable();
        }
        void OnDisable() {
            input.Disable();
        }
        void FixedUpdate() {
            look.Update(Time.deltaTime);
            avatar.Update(Time.deltaTime);
        }
    }
}