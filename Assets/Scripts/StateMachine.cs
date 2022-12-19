using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MovingEntity
{
    public enum state
    {
        Normal,
        AtacarEnemigo,
        AtacarTorre
    }

    public state currentState;
    public Dijkstra Pathfinder;
    public float maxChaseDistance = 10f;

    private Vector3 investigationPosition;

    float distanceToChange = 1f;
    Vector3 targetPosition;
    Vector3 towardsTarget;
    Transform currentTarget;
    float secondsWaiting;

    void Start()
    {
        StartCoroutine(FSM());
        Debug.Log("Current State -> " + currentState);
    }

    IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(currentState.ToString());
        }
    }

    void ChangeState(state nextState)
    {
        Debug.Log(currentState + "->" + nextState);
        currentState = nextState;
    }

    public void ChangeState(state nextState, Transform destination)
    {
        Debug.Log(currentState + "->" + nextState);
        currentTarget = destination;
        currentState = nextState;
    }

    public void ChangeToAttack(Vector3 iposition)
    {
        Debug.Log(currentState + "->" + state.AtacarEnemigo);
        investigationPosition = iposition;
        currentState = state.AtacarEnemigo;
    }

    IEnumerator Normal()
    {
        int i = 0;
        while (currentState == state.Normal)
        {
            // targetPosition = Waypoints[i].position;
            towardsTarget = targetPosition - transform.position;
            MoveTowards(towardsTarget.normalized);

            if (towardsTarget.magnitude < 0.25f)
                i++;

            Debug.DrawLine(transform.position, targetPosition, Color.green);
            yield return 0;
        }
    }

    IEnumerator AtacarEnemigo()
    {
        List<Node> path = Pathfinder.Algorithm(transform.position, currentTarget.position);
        Node current = path[1];
        Vector3 prevCurrentTarget = currentTarget.position;

        while (currentState == state.AtacarEnemigo)
        {
            if (currentTarget.position != prevCurrentTarget)
                path = Pathfinder.Algorithm(transform.position, currentTarget.position);

            // towardsTarget = current.position - transform.position;
            MoveTowards(towardsTarget);
            yield return 0;
        }
    }

    IEnumerator AtacarTorre()
    {
        List<Node> path = Pathfinder.Algorithm(transform.position, currentTarget.position);
        Node current = path[1];
        Vector3 prevCurrentTarget = currentTarget.position;

        while (currentState == state.AtacarTorre)
        {
            if (currentTarget.position != prevCurrentTarget)
                path = Pathfinder.Algorithm(transform.position, currentTarget.position);
            towardsTarget = current.position - transform.position;
            MoveTowards(towardsTarget);

            // si la distancia al objetivo es menor que la maxima establecida
            if (towardsTarget.magnitude < distanceToChange && path.Count > 1)
            {
                current = path[1];
                path.RemoveAt(0);
            }

            if ((currentTarget.position - transform.position).magnitude > maxChaseDistance)
                ChangeState(state.Normal);
            prevCurrentTarget = currentTarget.position;
            yield return 0;
        }
    }

    // Collider para enemigos si corresponde
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentTarget = other.transform;
            ChangeState(state.AtacarEnemigo);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, maxChaseDistance);
    }
}
