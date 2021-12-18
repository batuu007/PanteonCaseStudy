using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentAI : MonoBehaviour
{
    public Transform target;
    public GameObject gameOverCanvas;

    private float endGameTime = 3f;

    NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        RunToTarget();
    }
    private void RunToTarget()
    {
        navMeshAgent.destination = target.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            StartCoroutine(EndTheGame());
        }
    }
     IEnumerator EndTheGame()
    {
        yield return new WaitForSeconds(endGameTime);

        gameOverCanvas.SetActive(true);
    }
}
