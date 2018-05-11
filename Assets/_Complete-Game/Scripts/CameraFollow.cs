using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject Player;
    public float followSpeed;
    public Vector3 Playerposition;
    public Vector3 currentPos;
    public Vector3 Offset;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        currentPos = transform.position;
        Playerposition = Player.transform.position;
       transform.position = Vector3.Lerp(currentPos, Playerposition + Offset, followSpeed*Time.deltaTime);

	}
}
