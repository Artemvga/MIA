using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    [SerializeField] private bool _enableOnStart;
    [SerializeField] private int _timeBetweenChanging;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private List<Material> _lightsMaterials;
    [SerializeField] private List<MeshRenderer> _lightMeshRenderer;
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
            UpdateTrafficLight(2, _lightsMaterials[2]);
            _pedestrianCrossing.SetActive(true);
            UpdateSurfaces();
            yield return new WaitForSeconds(_timeBetweenChanging - 3);
            UpdateTrafficLight(1, _lightsMaterials[1]);
            yield return new WaitForSeconds(3);
        }
        _pedestrianCrossing.SetActive(false);
        UpdateSurfaces();
        while (true)
        {
            UpdateTrafficLight(0, _lightsMaterials[0]);
            yield return new WaitForSeconds(_timeBetweenChanging - 3);
            UpdateTrafficLight(1, _lightsMaterials[1]);
            _pedestrianCrossing.SetActive(true);
            UpdateSurfaces();
            yield return new WaitForSeconds(3);
            UpdateTrafficLight(2, _lightsMaterials[2]);
            yield return new WaitForSeconds(_timeBetweenChanging - 3);
            UpdateTrafficLight(1, _lightsMaterials[1]);
            yield return new WaitForSeconds(3);
            _pedestrianCrossing.SetActive(false);
            UpdateSurfaces();
        }
    }

    private  void UpdateTrafficLight(int index, Material material)
    {
        foreach (var el in _lightMeshRenderer)
        {
            el.material = _defaultMaterial;
        }
        _lightMeshRenderer[index].material = material;
    }

    private void UpdateSurfaces()
    {
        foreach (var sur in _surfaces)
        {
            sur.BuildNavMesh();
        }
    }
}
