using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class LevelDesignSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabs;

    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeZ;

    [SerializeField] private Transform _parent;

    [SerializeField] private int _spawnCount;

    [SerializeField] private List<GameObject> _instances;
    [ContextMenu("SpawnObjects")]
    public void SpawnObjects()
    {
        Clear();
        
        for (int i = 0; i < _spawnCount; i++)
        {
            var randObject = Random.Range(0, _prefabs.Count);
            
            var randPosX = Random.Range(transform.position.x, transform.position.x + _sizeX);
            var randPosZ =  Random.Range(transform.position.z, transform.position.z + _sizeZ);
            
            var randRot = new Vector3(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));
            
            var randPos = new Vector3(randPosX, 100, randPosZ);
            
            var instance = Instantiate(_prefabs[randObject], randPos, Quaternion.Euler(randRot));

            RaycastHit hit;
            
            if(Physics.Raycast(instance.transform.position, Vector3.down, out hit))

                if (hit.collider.CompareTag("TeleportArea"))
                {
                    instance.transform.position = hit.point;
                    instance.transform.SetParent(this.transform);
                    
                    _instances.Add(instance);
                }
                else
                {
                    DestroyImmediate(instance.gameObject);
                }
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        if (_instances.Count != 0)
        {
            foreach (var instance in _instances)
            {
                DestroyImmediate(instance);
            }
        }
    }
}