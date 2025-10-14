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
    [SerializeField] private List<ListMeshRenderers> _lightMeshRenderer;
    [SerializeField] private List<ListMeshRenderers> _pedestrianLightMeshRenderer;
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
            UpdatePedestrianTrafficLight(0, _lightsMaterials[0]);
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
            UpdatePedestrianTrafficLight(2, _lightsMaterials[2]);
            yield return new WaitForSeconds(_timeBetweenChanging - 3);
            UpdateTrafficLight(1, _lightsMaterials[1]);
            _pedestrianCrossing.SetActive(true);
            UpdateSurfaces();
            yield return new WaitForSeconds(3);
            UpdateTrafficLight(2, _lightsMaterials[2]);
            UpdatePedestrianTrafficLight(0, _lightsMaterials[0]);
            yield return new WaitForSeconds(_timeBetweenChanging - 3);
            UpdateTrafficLight(1, _lightsMaterials[1]);
            yield return new WaitForSeconds(3);
            _pedestrianCrossing.SetActive(false);
            UpdateSurfaces();
        }
    }

    private  void UpdateTrafficLight(int index, Material material)
    {
        foreach (var list in _lightMeshRenderer)
        {
            foreach (var el in list.renderers)
            {
                if (el != null)
                {
                    el.material = _defaultMaterial;
                }

                if (list.renderers[index] != null)
                {
                    list.renderers[index].material = material;
                }
            }
        }
    }

    private void UpdatePedestrianTrafficLight(int index, Material material)
    {
        foreach (var list in _pedestrianLightMeshRenderer)
        {
            foreach (var el in list.renderers)
            {
                if (el != null)
                {
                    el.material = _defaultMaterial;
                }

                if (list.renderers[index] != null)
                {
                    list.renderers[index].material = material;
                }
            }
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
