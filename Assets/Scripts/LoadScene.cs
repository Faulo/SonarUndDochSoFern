using MyBox;
using UnityEngine;

namespace Runtime {
    [CreateAssetMenu]
    public class LoadScene : ScriptableObject {
        [SerializeField]
        SceneReference scene = default;

        public void Invoke() {
            scene.LoadScene();
        }
    }
}