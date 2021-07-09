using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Runtime.Player {
    public class AvatarComponent : MonoBehaviour {
        [SerializeField, Expandable]
        AvatarSettings settings = new AvatarSettings();
        [SerializeField, Expandable]
        CharacterController character = default;

        Avatar avatar;
        void Awake() {
            avatar = new Avatar(settings, character);
        }

        void Update() {
            avatar.Update(Time.deltaTime);
        }
    }
}