using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Seftali.StateMachine {
    /// <summary>
    /// Class for Finite-State Machine.
    /// </summary>
    public class StateMachine : MonoBehaviour {

        /// <summary>
        /// List of StateObjects.
        /// </summary>
        public List<State> StateObjects = new List<State>();

        /// <summary>
        /// Events that registered to this invokes when state changed.
        /// </summary>
        public UnityEvent OnStateChange;

        /// <summary>
        /// StateMachine Controls the GameObject Active status if true.
        /// </summary>
        public bool ControlGameObject = false;

        /// <summary>
        /// Current StateObject that is on CurrentIndex.
        /// </summary>
        public State CurrentObject => this.StateObjects[this.CurrentIndex];
        public int CurrentIndex => this.m_CurrentIndex;
        public int Count => this.StateObjects.Count;
        public State this[int index] {
            get => this.StateObjects[index];
            set => this.StateObjects[index] = value;
        }

        private int m_CurrentIndex = 0;


        public void Start() {
            this.ResetState();
        }

        /// <summary>
        /// Changes state to given index.
        /// <para>
        /// When called Invokes OnStateChange event first. 
        /// If ControlGameObject enabled, changes GameObject Active to false, then invokes OnExit event on CurrentObject.
        /// CurrentIndex will be the new index and CurrentObject is changed to StateObject in new index.
        /// After invoking OnEnter event on new state, changes GameObject Active to true if ControlGameObject is true.
        /// </para>
        /// See also:
        /// <seealso cref="OnStateChange"/>
        /// <seealso cref="State.OnEnter"/>,
        /// <seealso cref="State.OnExit"/>
        /// </summary>
        /// <param name="index">Index to set.</param>
        public void ChangeTo(int index) {
            this.OnStateChange.Invoke();

            if(this.CurrentObject != null) {
                if(ControlGameObject) {
                    this.CurrentObject.gameObject.SetActive(false);
                }
                this.CurrentObject.InvokeOnExit();
            }

            this.m_CurrentIndex = Mathf.Clamp(index, 0, this.Count - 1);

            if(this.CurrentObject != null) {
                this.CurrentObject.InvokeOnEnter();
                if(ControlGameObject) {
                    this.CurrentObject.gameObject.SetActive(true);
                }
            }
        }

        /// <summary>
        /// Changes state to StateObject on previous index.
        /// See <see cref="ChangeTo(int)"/> for more info.
        /// </summary>
        [ContextMenu("Previous")]
        public void Previous() => this.ChangeTo(this.m_CurrentIndex - 1);

        /// <summary>
        /// Changes state to StateObject on next index.
        /// See <see cref="ChangeTo(int)"/> for more info.
        /// </summary>
        [ContextMenu("Next")]
        public void Next() => this.ChangeTo(this.m_CurrentIndex + 1);

        /// <summary>
        /// Changes state to StateObject on index 0.
        /// <para> 
        /// If ControlGameObject enabled, calls SetActive(false) on every GameObject that StateObject in.
        /// Changes CurrentState to StateObject that in the index 0.
        /// See <see cref="ChangeTo(int)"/> for more info.
        /// </para>
        /// </summary>
        [ContextMenu("Reset")]
        public void ResetState() {
            if(ControlGameObject) {
                foreach(var objec in StateObjects) {
                    objec.gameObject.SetActive(false);
                }
            }

            this.m_CurrentIndex = 0;

            if(this.CurrentObject != null) {
                this.CurrentObject.InvokeOnEnter();
                if(ControlGameObject) {
                    this.CurrentObject.gameObject.SetActive(true);
                }
            }
        }
    }
}
