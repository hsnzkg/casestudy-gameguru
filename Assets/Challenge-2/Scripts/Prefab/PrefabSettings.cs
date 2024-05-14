using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings/Prefab Setting", order = 1)]
[Serializable]
public class PrefabSettings : ScriptableObject
{
    public GameObject BlockPrefab;
    public GameObject DroppingBlockPrefab;
    public GameObject CharacterPrefab;
    public GameObject FinishBlockPrefab;
    public GameObject CMPrefab;
}
