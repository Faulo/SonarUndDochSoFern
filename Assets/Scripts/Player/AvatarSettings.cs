using UnityEngine;

namespace Runtime.Player {
    [CreateAssetMenu]
    public class AvatarSettings : ScriptableObject {
        [SerializeField, Range(0, 100)]
        public float maxSpeed = 10;
        [SerializeField, Range(0, 10)]
        public float accelerationDuration = 1;
    }
}