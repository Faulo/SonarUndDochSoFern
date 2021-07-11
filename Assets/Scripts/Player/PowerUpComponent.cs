using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Player {
    public class PowerUpComponent : MonoBehaviour {
        enum Type {
            Nothing,
            Burst,
            Bomb,
            DoubleJump,
            AmmoCount,
        }

        [SerializeField]
        Type type = default;

        [SerializeField]
        UnityEvent onCollect = new UnityEvent();

        public Vector3 position {
            get => transform.position;
            set => transform.position = value;
        }
        public Vector3 velocity;

        public void Collect(IAvatar avatar) {
            switch (type) {
                case Type.Nothing:
                    break;
                case Type.Burst:
                    avatar.hasBurst = true;
                    break;
                case Type.Bomb:
                    avatar.hasBomb = true;
                    break;
                case Type.DoubleJump:
                    avatar.jumpCount++;
                    break;
                case Type.AmmoCount:
                    avatar.ammoCount++;
                    break;
                default:
                    throw new NotImplementedException();
            }
            onCollect.Invoke();
            Destroy(gameObject);
        }
    }
}