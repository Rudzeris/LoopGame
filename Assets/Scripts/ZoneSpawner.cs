using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown.Generator
{
    public class ZoneSpawner : MonoBehaviour
    {
        [SerializeField] private float zoneOffset = 29;
        [SerializeField] private float zoneCount = 3;
        private List<GameObject> zones = new List<GameObject>();

        [SerializeField] private GameObject zonePrefab;
        void Start()
        {
            for (int i = 0; i < zoneCount; i++)
            {
                var zone = Instantiate(zonePrefab, new Vector3(100, 0, zoneOffset * i), Quaternion.identity);
                zones.Add(zone);
            }

           StartCoroutine(prepareZones()); 
        }

        private IEnumerator prepareZones()
        {
            yield return new WaitForSeconds(1);
            foreach(var zone in zones)
            {
                zone.transform.position -= new Vector3(100,0,0);
            }
        }
    }
}

