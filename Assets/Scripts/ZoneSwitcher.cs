using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDown.Generator
{
    [RequireComponent(typeof(ZoneSpawner))]
    public class ZoneSwitcher : MonoBehaviour
    {
        private List<GameObject> zones = new List<GameObject>();
        private int currentZoneIndex = 0;

        public void Start()
        {
            zones = GetComponent<ZoneSpawner>().Zones;
        }

        public void SwithZone(int zoneIndex)
        {

            if (currentZoneIndex == zoneIndex)
                return;

            currentZoneIndex = zoneIndex;

            if (zoneIndex + 1 < zones.Count)
            {
                SetZoneActive(zoneIndex + 1);
            }
            else
            {
                SetZoneActive(0);
            }

            if (zoneIndex - 1 > 0)
            {
                zones[zoneIndex - 1].SetActive(false);
            }
            else if(zoneIndex - 1 == 0)
            {
                zones[0].SetActive(false);
                zones[zones.Count - 1].SetActive(false);
            }

            
        }

        private void SetZoneActive(int index)
        {
            zones[index].transform.position = zones[currentZoneIndex].transform.position + new Vector3(0, 0, Helper.zoneOffset);
            zones[index].SetActive(true);
        }

    }

}
