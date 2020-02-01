using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementCircle : MonoBehaviour {
    public GameObject prefab;
    private int numObjects = 50;

    public void DoRenderer(float radius) {


        float deltaTheta = (float)(2.0 * Mathf.PI) / numObjects;
        float theta = 0f;

        for (int i = 0; i < numObjects + 1; i++) {
            float x = radius * Mathf.Cos(theta) +transform.position.x;
            float z = radius * Mathf.Sin(theta) + transform.position.z;
            Vector3 pos = new Vector3(x, 0, z);
            GameObject temp = Instantiate(prefab, transform) as GameObject;
            temp.transform.position = pos;
            theta += deltaTheta;
        }
    }
}
