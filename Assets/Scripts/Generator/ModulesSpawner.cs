using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace TopDown.Generator
{
    [RequireComponent(typeof(Collider))]
    public class ModulesSpawner : MonoBehaviour
    {
        [SerializeField] private int spawnThreshold = 100;
        [SerializeField] private List<GameObject> modulesList = new List<GameObject>();
        [SerializeField] private List<GameObject> fillModulesList = new List<GameObject>();
        private GameObject zoneParent;
        private bool readyToFill = false;
        private bool bGenerated = false;

        public void Start()
        {
            zoneParent = transform.parent.gameObject;
        }

        public void Update()
        {
            if(bGenerated)
                return;

            if(readyToFill)
            {
                SpawnFiller();
            }
            else
            {
                SpawnModules();
            }
        }

        private void SpawnFiller()
        {
            int modulesPerFrame = 5;
            for (int i = 0; i < modulesPerFrame; i++)
            {
                {
                    if ((spawnThreshold > 0))
                    {
                        spawnThreshold--;
                        var spawned = Instantiate(GetRandomFiller(), GetRandomPositionInCollider(GetComponent<Collider>()), Quaternion.identity);
                        spawned.transform.SetParent(zoneParent.transform);
                    }
                    else
                    {
                        bGenerated = true;
                    }
                }
            }
        }

        Vector3 GetRandomPositionInCollider(Collider collider)
        {
            if (collider == null)
            {
                Debug.LogError("No collider found on the object.");
                return Vector3.zero;
            }

            Bounds bounds = collider.bounds;

            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);

            return new Vector3(randomX, 0, randomZ);
        }

        private void SpawnModules()
        {
            int modulesPerFrame = 1;
            for (int i = 0; i < modulesPerFrame; i++)
            {
                {
                    if ((spawnThreshold > 0) && (modulesList.Count > 0))
                    {
                        GameObject randomModule = GetRandomModule();

                        spawnThreshold--;
                        if (randomModule != null)
                        {
                            var spawned = Instantiate(randomModule, GetRandomPositionInCollider(GetComponent<Collider>()), Quaternion.identity);
                            spawned.transform.SetParent(zoneParent.transform);

                            if (randomModule != null)
                            {
                                modulesList.Remove(randomModule);
                            }
                        }
                    }
                    else
                    {
                        readyToFill = true;
                    }
                }
            }
        }

        private GameObject GetRandomModule()
        {
            int randomIndex = Random.Range(0, modulesList.Count);
            var module = modulesList[randomIndex];
            return module;
        }

        private GameObject GetRandomFiller()
        {
            int randomIndex = Random.Range(0, fillModulesList.Count);
            var module = fillModulesList[randomIndex];
            return module;
        }
    }
}
