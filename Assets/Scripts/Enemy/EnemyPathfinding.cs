using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float distanceThreshold = 0.5f;
    [SerializeField] private float walkRadius = 10f;
    [SerializeField] private float roamSpeed = 2f;
    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private Light visionLight;
    [SerializeField] private FieldOfView fieldOfView;
    public UnityAction OnPlayerReached;
    private NavMeshAgent navMeshAgent;

    private Transform playerTransform;

    Vector3 destination;
    private float stopTimer = 0f;

    private bool wasPlayerInSight = false;

    // Used when attacking, stop the enemy from moving while attacking
    public void StopMovement(float duration) {
        SetDestination(transform.position);
        stopTimer = duration;
    }
    private bool IsStopped() {
        return stopTimer > 0f;
    }

    void Awake() {
        if (visionLight.type != LightType.Spot) {
            Debug.LogError("Incompatible light type used");
        }
    }

    void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;

        visionLight.spotAngle = fieldOfView.viewAngle + 20f;
        visionLight.range = fieldOfView.viewRadius;

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (navMeshAgent ==  null) {
            Debug.LogError("NavMesh not found");
        }
        Roam();
    }

    void Update() {
        if (IsStopped()) {
            stopTimer -= Time.deltaTime;
        } else {
            if (fieldOfView.visibleTarget != null) { // If can see player
                if (CheckDestination(playerTransform.position)) {   // Arrived at player position
                    ReachedPlayer();
                } else {
                    SetSpeed(chaseSpeed);
                    SetDestination(playerTransform.position);
                }
                wasPlayerInSight = true;
            } else {    // Player is out of sight
                SetSpeed(roamSpeed);
                if (wasPlayerInSight) { // If we just lost sight of player, find a new destination
                    Roam();
                } else {
                    if (CheckDestination(destination)) {    // If reached destination, find a new destination
                        Roam();
                    } else {
                        // Still moving towards old destination
                    }
                }
                wasPlayerInSight = false;
            }
        }
    }

    private void SetDestination(Vector3 newDestination) {
        if (newDestination != null) {
            destination = newDestination;
            navMeshAgent.SetDestination(newDestination);
        }
    }

    private void SetSpeed(float speed) {
        navMeshAgent.speed = speed;
    }

    private void ReachedPlayer() {
        navMeshAgent.SetDestination(transform.position);
        OnPlayerReached.Invoke();
    }

    // Returns true if arrived at destination
    private bool CheckDestination(Vector3 destination) {
        return Vector3.Distance(transform.position, destination) < distanceThreshold;
    }

    private void Roam() {
        Vector3 newDestination = transform.position;
        for (int i = 0; i < 30; i++) {
            Vector3 randomPosition = UnityEngine.Random.insideUnitSphere * walkRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position + randomPosition, out hit, walkRadius, NavMesh.AllAreas)) {
                newDestination = hit.position;
            }
        }
        SetDestination(newDestination);
    }
}
