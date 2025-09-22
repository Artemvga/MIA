using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    [SerializeField] private bool _hasTimeBefore;
    [SerializeField] private int _timeBetweenChanging;
    [SerializeField] private List<Material> _lightsMaterials;
    [SerializeField] private MeshRenderer _lightMeshRenderer;
    [SerializeField] private GameObject _pedestrianCrossing;
    [SerializeField] private List<NavMeshSurface> _surfaces;

    private void Start()
    {
        StartCoroutine(ManageTrafficLight());
    }

    private IEnumerator ManageTrafficLight()
    {
        if (_hasTimeBefore)
        {
            _lightMeshRenderer.material = _lightsMaterials[2];
            _pedestrianCrossing.SetActive(true);
            UpdateSurfaces();
            yield return new WaitForSeconds(_timeBetweenChanging - 1);
            _lightMeshRenderer.material = _lightsMaterials[1];
            yield return new WaitForSeconds(1);
        }
        while (true)
        {
            _pedestrianCrossing.SetActive(false);
            UpdateSurfaces();
            _lightMeshRenderer.material = _lightsMaterials[0];
            yield return new WaitForSeconds(_timeBetweenChanging - 1);
            _lightMeshRenderer.material = _lightsMaterials[1];
            yield return new WaitForSeconds(1);
            _lightMeshRenderer.material = _lightsMaterials[2];
            _pedestrianCrossing.SetActive(true);
            UpdateSurfaces();
            yield return new WaitForSeconds(_timeBetweenChanging - 1);
            _lightMeshRenderer.material = _lightsMaterials[1];
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateSurfaces()
    {
        foreach (var sur in _surfaces)
        {
            sur.BuildNavMesh();
        }
    }
}
