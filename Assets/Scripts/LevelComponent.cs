using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Runtime {
    public class LevelComponent : MonoBehaviour {
        [SerializeField, Layer]
        int runtimeLayer = 0;
        void Awake() {
            var renderers = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++) {
                renderers[i].gameObject.layer = runtimeLayer;
            }
        }
    }
}