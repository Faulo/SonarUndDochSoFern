using UnityEngine;

namespace Runtime.Player {
    public class Avatar {
        AvatarSettings settings;
        CharacterController character;
        public Avatar(AvatarSettings settings, CharacterController character) {
            this.settings = settings;
            this.character = character;
        }

        public void Update(float deltaTime) {
        }
    }
}