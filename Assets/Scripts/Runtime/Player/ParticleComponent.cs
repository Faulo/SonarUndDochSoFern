using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace SonarUndDochSoFern.Player {
    sealed class ParticleComponent : MonoBehaviour {
        [SerializeField]
        public ParticleSystem sonarSystem = default;
        [SerializeField]
        public ParticleSystem paintSystem = default;
        [SerializeField, FormerlySerializedAs("renderer")]
        Renderer attachedRenderer = default;
        [SerializeField]
        Vector3 paintOffset = Vector3.zero;
        [SerializeField]
        float killY = 0;
        [SerializeField]
        UnityEvent<GameObject> onCollision = new();

        List<ParticleCollisionEvent> events = new(1024);
        void OnParticleCollision(GameObject other) {
            int eventCount = sonarSystem.GetCollisionEvents(other, events);
            for (int i = 0; i < eventCount; i++) {
                var rotation = Quaternion.LookRotation(events[i].normal);
                rotation *= Quaternion.Euler(0, 2 * Mathf.Rad2Deg * Mathf.Asin(events[i].normal.z), 0);
                var emitParams = new ParticleSystem.EmitParams {
                    position = events[i].intersection + (rotation * paintOffset),
                    rotation3D = rotation.eulerAngles,
                };
                paintSystem.Emit(emitParams, 1);
            }
        }
        void OnCollisionEnter(Collision collision) {
            Collide();
        }
        void Update() {
            if (transform.position.y < killY) {
                Destroy(gameObject);
            }
        }
        void Collide() {
            if (TryGetComponent<Rigidbody>(out var rigidbody)) {
                var main = sonarSystem.main;
                main.emitterVelocity = rigidbody.velocity;
                sonarSystem.Play();
                rigidbody.isKinematic = true;
            }

            if (attachedRenderer) {
                attachedRenderer.enabled = false;
            }

            onCollision.Invoke(gameObject);
        }
    }
}