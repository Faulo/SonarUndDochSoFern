using System;
using UnityEngine;

namespace Runtime.Player {
    public interface IAvatar {
        public event Action<ControllerColliderHit> onControllerColliderHit;

        public Vector3 forward { get; }
        public Vector3 position { get; }
        public Quaternion rotation { get; }
        public Vector3 velocity { get; }
    }
}