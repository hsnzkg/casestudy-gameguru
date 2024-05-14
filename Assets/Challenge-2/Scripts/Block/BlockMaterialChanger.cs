using System.Collections.Generic;
using UnityEngine;

public class BlockMaterialChanger : MonoBehaviour
{
    [SerializeField] private List<Material> _mats = new List<Material>();
    [SerializeField] private MeshRenderer _meshRenderer;
    private Material _material;
    public Material material => _material;

    private void Awake()
    {
        SelectRandomMaterial();
    }

    public void SelectRandomMaterial()
    {
        _material = _mats[Random.Range(0, _mats.Count)];
        _meshRenderer.material = _material;
    }

    public void ChangeMaterial(Material mat)
    {
        _meshRenderer.material = mat;
    }
}
