using UnityEngine;

namespace SonarUndDochSoFern {
    sealed class ExitGameComponent : MonoBehaviour {
        public void Invoke() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}