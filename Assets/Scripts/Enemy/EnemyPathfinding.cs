using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[Serializable]
public class OnPlayerReachedEvent : UnityEvent<bool> {}


public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float distanceThreshold = 0.5f;
    public OnPlayerReachedEvent onPlayerReachedEvent = null;

    NavMeshAgent navMeshAgent;

    private bool reachedDestination = false;

    Vector3 destination;

    void Start() {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent ==  null) {
            Debug.LogError("NavMesh not found");
        }
    }

    void FindPlayerLocation() {
        destination = GameObject.FindWithTag("Player").transform.position;
    }

    void Update() {
        destination = GameObject.FindWithTag("Player").transform.position;
        reachedDestination = CheckDestination();
        if (!reachedDestination) {
            SetDestination();
        } else {
            ReachedDestination();
        }
    }

    private void SetDestination() {
        if (destination != null) {
            navMeshAgent.SetDestination(destination);
        }
    }

    private void ReachedDestination() {
        if (destination != null) {
            navMeshAgent.SetDestination(transform.position);
            onPlayerReachedEvent.Invoke(true);
        }
    }

    private bool CheckDestination() {
        return Vector3.Distance(transform.position, destination) < distanceThreshold;
    }
}
