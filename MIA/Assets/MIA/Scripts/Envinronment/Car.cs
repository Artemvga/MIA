using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    [SerializeField] private GameObject _carPrefab;
    [SerializeField] private int _carsAmountPerMinute;
    [SerializeField] private List<Transform> _spawnPositions;
    [SerializeField] private List<Transform> _startedPoints;

    private IEnumerator Start()
    {
        var time = Mathf.RoundToInt(60 / _carsAmountPerMinute);
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(time - 1, time + 1));
            var index = Random.Range(0, _spawnPositions.Count);
            var position = /*_spawnPositions[index];*/ _spawnPositions[index];
            var carAiController = Instantiate(_carPrefab, position.position, position.rotation).GetComponent<CarAIController>();
            carAiController.nextCheckpoint = _spawnPositions[index];
        }
    }
}
