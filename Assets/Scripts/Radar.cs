using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject
{
    public Image icon { get; set; }
    public GameObject owner { get; set; }
}

public class Radar : MonoBehaviour
{
    private Transform playerPos;
    [SerializeField]
    float mapScale = 2.0f;

    public static List<RadarObject> radarObjects = new List<RadarObject>();

    private void Start() {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public static void RegisterRadarObjects(GameObject o, Image i)
    {
        Image radar_icon = Instantiate(i);
        radarObjects.Add(new RadarObject() { owner = o, icon = radar_icon});
    }

    public static void RemoveRadarObject(GameObject o)
    {
        List<RadarObject> newList = new List<RadarObject>();

        for (int i = 0; i < radarObjects.Count; i++)
        {
            if (radarObjects[i].owner == o)
            {
                Destroy(radarObjects[i].icon);
                continue;
            }
            else
            {
                newList.Add(radarObjects[i]);
            }
        }

        radarObjects.RemoveRange(0, radarObjects.Count);
        radarObjects.AddRange(newList);
    }

    void DrawRadarDots()
    {
        foreach (RadarObject ro in radarObjects)
        {
            Vector3 radarPos = (ro.owner.transform.position - playerPos.position);
            float distToObject = Vector3.Distance(playerPos.position, ro.owner.transform.position) * mapScale;
            float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - playerPos.eulerAngles.y;
            radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad);
            radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);

            ro.icon.transform.SetParent(this.transform);
            ro.icon.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + this.transform.position;
        }
    }

    private void Update()
    {
        DrawRadarDots();
    }
}
