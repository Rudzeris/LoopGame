using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown.Generator
{
    public class ZoneSpawner : MonoBehaviour
    {
        [SerializeField] private float zoneCount = 3;
        private List<GameObject> zones = new List<GameObject>();

        [SerializeField] private GameObject zonePrefab;

        public List<GameObject> Zones { get => zones; set => zones = value; }

        void Start()
        {
            for (int i = 0; i < zoneCount; i++)
            {
                var zone = Instantiate(zonePrefab, new Vector3(100, 0, Helper.zoneOffset * i), Quaternion.identity);
                zone.GetComponent<Zone>().index = i;
                Zones.Add(zone);
            }

            StartCoroutine(prepareZones()); 
        }

        private IEnumerator prepareZones()
        {
            yield return new WaitForSeconds(1);

            for (int i = 2; i < zones.Count; i++)
            {
                zones[i].SetActive(false);
            }

            foreach (var zone in Zones)
            {
                zone.transform.position -= new Vector3(100, 0, 0);
                zone.GetComponent<Zone>().InitializeObstacle();
            }


        }
    }
}

