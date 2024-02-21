using UnityEngine;

namespace SonarUndDochSoFern {
    [CreateAssetMenu]
    sealed class InstantiateObject : ScriptableObject {
        [SerializeField]
        GameObject prefab = default;
        [SerializeField]
        bool asChild = false;
        [SerializeField]
        bool destroyLater = false;
        [SerializeField, Range(0, 100)]
        float destructionTimeout = 1;
        public void Invoke(GameObject context) {
            if (!prefab) {
                return;
            }

            var instance = asChild
                ? Instantiate(prefab, context.transform)
                : Instantiate(prefab, context.transform.position, context.transform.rotation);
            if (destroyLater) {
                Destroy(instance, destructionTimeout);
            }
        }
    }
}