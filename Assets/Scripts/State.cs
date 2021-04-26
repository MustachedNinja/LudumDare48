using UnityEngine;
using UnityEngine.Events;

namespace Seftali.StateMachine {
    /// <summary>
    /// StateMachine State class.
    /// See <seealso cref="StateMachine"/> for more info.
    /// </summary>
    public class State : MonoBehaviour {
        /// <summary>
        /// Invokes when state machine switched to this.
        /// </summary>
        public UnityEvent OnEnter;

        /// <summary>
        /// Invokes when state machine switched from this.
        /// </summary>
        public UnityEvent OnExit;
        /// <summary>
        /// Returns true if this is the CurrentObject on StateMachine.
        /// </summary>
        public bool IsCurrent => _IsCurrent;

        private bool _IsCurrent = false;

        public void InvokeOnEnter() {
            _IsCurrent = true;
            OnEnter.Invoke();
        }

        public void InvokeOnExit() {
            _IsCurrent = false;
            OnExit.Invoke();
        }
    }
}
