using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ShipEngineRoom : MonoBehaviour
{
    private Vector3 endPosition;
    private bool Move;
    private NavMeshAgent ShipAgent;
    // Start is called before the first frame update
    void Start()
    {
        Move = false;
        ShipAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShipAgent.isStopped)
        {
            Move = false;
        }
        if (Move)
        {
            ShipAgent.SetDestination(endPosition);
            Debug.Log(gameObject.transform.name + "正航向:" + endPosition);
        }
    }
    public void SetEndPosition(Vector3 position)
    {
        endPosition = position;   
        Move = true;
    }
}
