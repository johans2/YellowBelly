using UnityEngine;
using System.Collections;
using System;
using CakewalkIoC.Signal;

public class PointerInteractor : MonoBehaviour {

    public static Signal<Vector3> MoveSignal = new Signal<Vector3>();

    public GameObject reticlePrefab;

    private GameObject reticle;

    private Vector3 outOfBoundsPosition = new Vector3(-999999, -999999, -99999); // hopefully not visible.
    private int interactionLayer = 1 << 8;

    private bool hasValidMoveTarget = false;

    void Awake() {
        reticle = (GameObject)Instantiate(reticlePrefab, transform.position, Quaternion.identity);
	}
    
	void Update () {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, interactionLayer)) {
            reticle.transform.position = hit.point + new Vector3(0,0.05f,0);
            reticle.transform.forward = -Vector3.up;
            hasValidMoveTarget = true;

            if(RGInput.Instance.ButtonWasPressed(RGInput.Button.Click)) {
                MoveSignal.Dispatch(hit.point);
            }
        }
        else {
            reticle.transform.position = outOfBoundsPosition;
            hasValidMoveTarget = false;
        }
        
    }
    
}
