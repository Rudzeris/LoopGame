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

            foreach (var zone in Zones) 
            {
                zone.GetComponent<Zone>().InitializeObstacle();

                zone.GetComponent<Zone>().SetZoneActive(false);

                zone.transform.position -= new Vector3(100, 0, 0);

            }

            StartCoroutine(ActivateFirstZones());
            
        }

        private IEnumerator ActivateFirstZones()
        {
            yield return new WaitForSeconds(0.6f);

            for (int i = 0; i < 2; i++)
            {
                zones[i].GetComponent<Zone>().SetZoneActive(true);
            }
        }
    }
}

