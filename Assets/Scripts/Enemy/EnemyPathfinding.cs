using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private float distanceThreshold = 0.5f;

    NavMeshAgent navMeshAgent;

    private bool reachedDestination = false;

    void Start() {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent ==  null) {
            Debug.LogError("NavMesh not found");
        } else {
            SetDestination();
        }
    }

    void Update() {
        reachedDestination = CheckDestination();
        if (!reachedDestination) {
            SetDestination();
        }
    }

    private void SetDestination() {
        if (destination != null) {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

    private bool CheckDestination() {
        return Vector3.Distance(transform.position, destination.position) < distanceThreshold;
    }
}
