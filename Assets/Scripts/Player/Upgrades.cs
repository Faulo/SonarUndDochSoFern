namespace Runtime.Player {
    public class Upgrades : IUpdatable {
        readonly IAvatar avatar;
        readonly AvatarSettings settings;
        readonly AvatarInput.PlayerActions input;

        public Upgrades(IAvatar avatar, AvatarSettings settings, AvatarInput.PlayerActions input) {
            this.avatar = avatar;
            this.settings = settings;
            this.input = input;
        }

        public void Update(float deltaTime) {

        }
    }
}