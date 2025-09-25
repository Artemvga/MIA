using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    [SerializeField] private bool _enableOnStart;
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
        if (_enableOnStart)
        {
            _lightMeshRenderer.material = _lightsMaterials[2];
            _pedestrianCrossing.SetActive(true);
            UpdateSurfaces();
            yield return new WaitForSeconds(_timeBetweenChanging - 2);
            _lightMeshRenderer.material = _lightsMaterials[1];
            yield return new WaitForSeconds(2);
        }
        _pedestrianCrossing.SetActive(false);
        UpdateSurfaces();
        while (true)
        {
            _lightMeshRenderer.material = _lightsMaterials[0];
            yield return new WaitForSeconds(_timeBetweenChanging - 2);
            _lightMeshRenderer.material = _lightsMaterials[1];
            _pedestrianCrossing.SetActive(true);
            UpdateSurfaces();
            yield return new WaitForSeconds(2);
            _lightMeshRenderer.material = _lightsMaterials[2];
            yield return new WaitForSeconds(_timeBetweenChanging - 2);
            _lightMeshRenderer.material = _lightsMaterials[1];
            yield return new WaitForSeconds(2);
            _pedestrianCrossing.SetActive(false);
            UpdateSurfaces();
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
