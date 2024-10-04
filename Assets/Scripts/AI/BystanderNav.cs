using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BystanderNav : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform p;
    Vector2 lastPoint = new Vector2 (0,0);
    private Vector2 walkPoint;
    bool walking=true;
    private GameObject StandingPositions;
    private List<Vector2> StandingPos= new List<Vector2>();
    private List<StandingPos> occupiedList= new List<StandingPos>();
    float yV;

    private void Awake()
    {
        yV = transform.position.y;
        agent = GetComponent<NavMeshAgent>();
        StandingPositions = GameObject.Find("StandingPositions");
        foreach(Transform t in StandingPositions.transform)
        {
            if (!StandingPos.Contains(new Vector2(t.position.x,t.position.z)))
            {
                StandingPos.Add(new Vector2(t.position.x, t.position.z));
                occupiedList.Add(t.GetComponent<StandingPos>());
            }
        }
        StartCoroutine("MoveToPoint");

    }
   


    private IEnumerator idle(int pos)
    {
        Debug.Log("idle");
        yield return new WaitForSeconds(Random.Range(5.0f, 20.0f));
        occupiedList[pos].occupied = false;
        StartCoroutine("MoveToPoint");

    }
    private IEnumerator MoveToPoint()
    {
        Debug.Log("walking");
        int temp;
        int count;
            count = StandingPos.Count;
            temp = Random.Range(0, count);
            while (occupiedList[temp].occupied == true || lastPoint == StandingPos[temp])
            {
                temp = Random.Range(0, count);
            }
            walkPoint = StandingPos[temp];
            lastPoint = StandingPos[temp];
            occupiedList[temp].occupied = true;
        agent.SetDestination(new Vector3 (walkPoint.x,yV,walkPoint.y));
        Vector2 distToPoint= new Vector2 (transform.position.x, transform.position.z) - walkPoint;
        while (distToPoint.magnitude > 1f)
        {
            distToPoint = new Vector2(transform.position.x, transform.position.z) - walkPoint;
            yield return null;
        }
        StartCoroutine("idle",temp);
    }

}
