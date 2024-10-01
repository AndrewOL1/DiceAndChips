using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BystanderNav : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform p;
    Vector3 lastPoint = new Vector3 (0,0,0);
    private Vector3 walkPoint;
    bool walking=true;
    private GameObject StandingPositions;
    private List<Vector3> StandingPos= new List<Vector3>();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        StandingPositions = GameObject.Find("StandingPositions");
        foreach(Transform t in StandingPositions.transform)
        {
            if (!StandingPos.Contains(t.position))
            {
                StandingPos.Add(t.position);
            }
        }
        StartCoroutine("MoveToPoint");

    }
   


    private IEnumerator idle()
    {
        Debug.Log("idle");
        yield return new WaitForSeconds(Random.Range(5.0f, 20.0f));
        StartCoroutine("MoveToPoint");

    }
    private IEnumerator MoveToPoint()
    {
        int temp;
        if (lastPoint == new Vector3(0, 0, 0))
        {
            temp = StandingPos.Count - 1;
            temp = Random.Range(0, temp);
            walkPoint = StandingPos[temp];
            lastPoint = StandingPos[temp];
        }
        else
        {
            temp = StandingPos.Count - 2;
            temp = Random.Range(0, temp);
            StandingPos.Remove(lastPoint);
            walkPoint = StandingPos[temp];
            lastPoint = StandingPos[temp];
            StandingPos.Add(lastPoint);
        }
        agent.SetDestination(walkPoint);
        Vector3 distToPoint= transform.position - walkPoint;
        while (distToPoint.magnitude > 1f)
        {
            distToPoint = transform.position - walkPoint;
            yield return null;
        }
        StartCoroutine("idle");
    }

}
