using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Car : MonoBehaviour
{
    [SerializeField] private List<GameObject> _carsPrefabs;
    [SerializeField] private int _carsAmountPerMinute;
    [SerializeField] private List<Transform> _spawnPositions;

    private IEnumerator Start()
    {
        var time = Mathf.RoundToInt(60 / _carsAmountPerMinute);
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(time - 1, time + 1));
            var index = Random.Range(0, _spawnPositions.Count);
            var position = _spawnPositions[index];
            var carPrefab = _carsPrefabs[Random.Range(0, _carsPrefabs.Count)];
            var carAiController = Instantiate(carPrefab, position.position, position.rotation).GetComponent<CarAIController>();
            carAiController.nextCheckpoint = _spawnPositions[index];
        }
    }
}
