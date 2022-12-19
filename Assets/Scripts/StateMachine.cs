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
    private Dijkstra Pathfinder;
    public float maxChaseDistance = 10f;
    List<Node> path = new List<Node>();
    public LayerMask mask;
    public float radiusDetection = 1000;
    float distanceToChange = 1f;
    public Vector3 targetPosition;
    Vector3 towardsTarget;
    Transform currentTarget;

    void Start()
    {
        Pathfinder = GameObject.Find("Dijkstra").GetComponent<Dijkstra>();
        ChooseTarget();
        GetPath();
        StartCoroutine(FSM());
        Debug.Log("Current State -> " + currentState);
    }

    void ChooseTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, radiusDetection, mask);

        Debug.Log("Amount of targets: " + targets.Length);
        if (targets.Length == 0) return;
        int nearestTarget = 0;
        float minDistance = Vector3.Distance(transform.position, targets[0].transform.position);
        for(int i = 1; i < targets.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, targets[i].transform.position);
            if (distance < minDistance)
            {
                nearestTarget = i;
                minDistance = distance;
            }
        }
        targetPosition = targets[nearestTarget].transform.position;
        Debug.Log(targetPosition);
    }
    void GetPath()
    {
        path = Pathfinder.Algorithm(transform.position, targetPosition);
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
        currentState = state.AtacarEnemigo;
    }

    IEnumerator Normal()
    {
        while (currentState == state.Normal && path.Count > 0)
        {
            targetPosition = path[0].position;
            towardsTarget = targetPosition - transform.position;
            MoveTowards(towardsTarget.normalized);

            if (towardsTarget.magnitude < 1f)
                path.RemoveAt(0);

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
