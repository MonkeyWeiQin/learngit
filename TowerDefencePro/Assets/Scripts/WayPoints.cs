using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour {

    public static Transform[] positions;

    private void Awake()
    {
        positions = new Transform[this.transform.childCount];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = this.transform.GetChild(i);
        }
    }
}
