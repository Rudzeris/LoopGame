using Assets.Scripts.GameObjects.Entities;
using System;
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
        private ZoneManager zoneManager;

        public int CurrentZoneIndex { get => currentZoneIndex; set => currentZoneIndex = value; }

        public void Start()
        {
            zones = GetComponent<ZoneSpawner>().Zones;
            zoneManager = GetComponent<ZoneManager>();
        }

        public void SwithZone(int zoneIndex)
        {

            if (CurrentZoneIndex == zoneIndex)
                return;

            CurrentZoneIndex = zoneIndex;

            if (zoneIndex - 1 >= 0)
            {
                zones[zoneIndex - 1].GetComponent<Zone>().SetZoneActive(false);
            }
            else if (zoneIndex - 1 < 0)
            {
                LevelUpZones();

                zones[0].GetComponent<Zone>().ActivateActiveObstacles();
                zones[zones.Count - 1].GetComponent<Zone>().SetZoneActive(false);
            }

            if (zoneIndex + 1 < zones.Count)
            {
                SetZoneActive(zoneIndex + 1, Helper.zoneOffset);
            }
            else
            {
                float firstZonePlatform = 2;
                SetZoneActive(0, Helper.zoneOffset + firstZonePlatform);      
            }

            
        }

        private void SetZoneActive(int index, float Offset)
        {
            zones[index].transform.position = zones[CurrentZoneIndex].transform.position + new Vector3(0, 0, Offset);
            zones[index].GetComponent<Zone>().SetZoneActive(true); 
        }

        private void LevelUpZones()
        {
            Debug.Log("LevelUP");

            foreach (var zone in zones)
            {
                zone.GetComponent<Zone>().LvlUp();
            }

            zoneManager.LVLup();


        }

        public GameObject GetCurrentZone()
        {
            return zones[currentZoneIndex];
        }

    }

}
