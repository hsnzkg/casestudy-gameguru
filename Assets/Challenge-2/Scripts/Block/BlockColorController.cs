using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColorController : MonoBehaviour
{
    [SerializeField] private List<Material> _materials = new List<Material>();
    [SerializeField] private MeshRenderer _meshRenderer;

    private void Awake()
    {
        SelectRandomMaterial();
    }

    public void SelectRandomMaterial()
    {
        _meshRenderer.material = _materials[Random.Range(0,_materials.Count)];
    }
}
