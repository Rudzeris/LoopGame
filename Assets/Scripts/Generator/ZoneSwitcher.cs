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
                zones[zones.Count - 1].GetComponent<Zone>().SetZoneActive(false);
            }

            if (zoneIndex + 1 < zones.Count)
            {
                SetZoneActive(zoneIndex + 1);
            }
            else
            {
                SetZoneActive(0);      
            }

            
        }

        private void SetZoneActive(int index)
        {
            zones[index].transform.position = zones[CurrentZoneIndex].transform.position + new Vector3(0, 0, Helper.zoneOffset);
            zones[index].GetComponent<Zone>().SetZoneActive(true); ;
        }

        private void LevelUpZones()
        {
            Debug.Log("LevelUP");

            zoneManager.LVLup();

            foreach(var zone in zones)
            {
                zone.GetComponent<Zone>().LvlUp();
            }
        }

        public GameObject GetCurrentZone()
        {
            return zones[currentZoneIndex];
        }

    }

}
