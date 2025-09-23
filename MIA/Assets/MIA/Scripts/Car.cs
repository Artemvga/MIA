using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    public GameObject car;
    public List<Transform> positions;
    public List<Transform> neededPositions;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            var index = Random.Range(0, positions.Count);
            var pos = positions[index];
            var navMesh = Instantiate(car, pos.position, pos.rotation).GetComponent<NavMeshAgent>();
            navMesh.SetDestination(neededPositions[index].position);
        }
    }
}
