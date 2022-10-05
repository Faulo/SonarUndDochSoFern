using MyBox;
using UnityEngine;

namespace SonarUndDochSoFern {
    [CreateAssetMenu]
    sealed class LoadScene : ScriptableObject {
        [SerializeField]
        SceneReference scene = default;

        public void Invoke() {
            scene.LoadScene();
        }
    }
}