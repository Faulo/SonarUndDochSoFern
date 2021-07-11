using System;
using UnityEngine;

namespace Runtime.Player {
    public interface IAvatar {
        event Action<ControllerColliderHit> onControllerColliderHit;
        event Action onAmmoCountChanged;

        Vector3 forward { get; }
        Vector3 position { get; }
        Quaternion rotation { get; }
        Vector3 velocity { get; }
        bool hasBurst { get; set; }
        bool hasBomb { get; set; }
        int jumpCount { get; set; }
        int ammoCount { get; set; }
    }
}