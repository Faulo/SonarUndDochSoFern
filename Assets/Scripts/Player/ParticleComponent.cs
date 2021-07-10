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
                var rotation = Quaternion.LookRotation(events[i].normal);
                rotation *= Quaternion.Euler(0, 2 * Mathf.Rad2Deg * Mathf.Asin(events[i].normal.z), 0);
                var emitParams = new ParticleSystem.EmitParams {
                    position = events[i].intersection,
                    rotation3D = rotation.eulerAngles,//new Vector3(Mathf.Rad2Deg * Mathf.Asin(events[i].normal.y), 0, 0),
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