using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPulse : MonoBehaviour {
    Light light;
    [Range (0, 3)]
    public float duration = 2.0f;
    // Use this for initialization
    void Start () {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        float phi = Time.time / duration * 2 * Mathf.PI;
        float amplitude = Mathf.Cos(phi) * 0.5f + 2f;
        light.intensity = amplitude ;
    }
}
