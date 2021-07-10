using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Player {
    public class ParticleComponent : MonoBehaviour {
        [SerializeField]
        ParticleSystem sonarSystem = default;
        [SerializeField]
        ParticleSystem paintSystem = default;
        [SerializeField]
        new Renderer renderer = default;

        List<ParticleCollisionEvent> events = new List<ParticleCollisionEvent>(1024);
        void OnParticleCollision(GameObject other) {
            int eventCount = sonarSystem.GetCollisionEvents(other, events);
            for (int i = 0; i < eventCount; i++) {
                var emitParams = new ParticleSystem.EmitParams {
                    position = events[i].intersection,
                    rotation3D = Quaternion.LookRotation(events[i].normal, Vector3.up).eulerAngles,
                };
                paintSystem.Emit(emitParams, 1);
            }
        }
        void OnCollisionEnter(Collision collision) {
            if (TryGetComponent<Rigidbody>(out var rigidbody)) {
                rigidbody.isKinematic = true;
                sonarSystem.Play();
            }
            if (renderer) {
                renderer.enabled = false;
            }
        }
    }
}