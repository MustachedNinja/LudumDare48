using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


public class EnemyBehaviour : MonoBehaviour {
    public float DistanceThreshold = 0.5f;

    public Transform PlayerTransform;
    public NavMeshAgent NavMeshAgent;
    public UnityEvent OnPlayerReachedEvent = null;

    void Start() {
        PlayerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update() {
        Vector3 PlayerPos = PlayerTransform.position;

        bool destinationReached = Vector3.Distance(transform.position, PlayerPos) < DistanceThreshold;

        if(!destinationReached) {
            Vector3 PlayerDir = (PlayerPos - transform.position).normalized;

            if(Physics.Raycast(this.transform.position, PlayerDir, out var hitinfo)) {
                if(hitinfo.collider.CompareTag("Player")) {
                    NavMeshAgent.SetDestination(PlayerPos);
                    Debug.Log("player in sight");
                } else {
                    MoveAround();
                }
            }
        } else {
            NavMeshAgent.SetDestination(this.transform.position);
            OnPlayerReachedEvent.Invoke();
            Debug.Log("destination Reached");
        }


    }
    public void MoveAround() {
        //Debug.Log("Moving around");
    }
}
