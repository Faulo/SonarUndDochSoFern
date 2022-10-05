using System;
using UnityEngine;

namespace SonarUndDochSoFern.Player {
    public interface IAvatar {
        event Action<ControllerColliderHit> onControllerColliderHit;
        event Action onAmmoCountChanged;
        GameObject gameObject { get; }
        Vector3 forward { get; }
        Vector3 position { get; }
        Quaternion rotation { get; }
        Vector3 velocity { get; }
        bool hasBurst { get; set; }
        bool hasBomb { get; set; }
        int jumpCount { get; set; }
        int ammoCount { get; set; }
        bool isRunning { get; }
    }
}