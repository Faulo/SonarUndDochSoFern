using Cinemachine;
using UnityEngine;

namespace SonarUndDochSoFern.Player {
    sealed class CinemachineAxisInput : MonoBehaviour, AxisState.IInputAxisProvider {
        public Vector2 input = Vector2.zero;
        public float GetAxisValue(int axis) {
            return input[axis];
        }
    }
}