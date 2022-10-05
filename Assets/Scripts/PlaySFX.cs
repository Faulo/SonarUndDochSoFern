using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.Audio;

namespace SonarUndDochSoFern {
    [CreateAssetMenu]
    sealed class PlaySFX : ScriptableObject {
        [SerializeField, Expandable]
        AudioSource prefab = default;
        [SerializeField, Expandable]
        AudioClip clip = default;
        [SerializeField, Expandable]
        AudioMixerGroup mixer = default;
        [SerializeField, Range(-10, 10)]
        float minPitch = 1;
        [SerializeField, Range(-10, 10)]
        float maxPitch = 1;

        public void Invoke(GameObject context) {
            var instance = Instantiate(prefab, context.transform.position, context.transform.rotation);
            instance.clip = clip;
            instance.outputAudioMixerGroup = mixer;
            instance.pitch = Random.Range(minPitch, maxPitch);
            instance.Play();
            Destroy(instance, Mathf.Abs(clip.length / instance.pitch));
        }
    }
}