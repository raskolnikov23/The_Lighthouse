using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{


    public NavMeshAgent agent;


    // patrolling state


    private void Update()
    {
        agent.SetDestination(GameObject.Find("Player").transform.position);
        GetComponent<Rigidbody>().position= agent.transform.position;
    }

}
