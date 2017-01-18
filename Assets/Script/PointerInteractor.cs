using UnityEngine;
using System.Collections;

public class PointerInteractor : MonoBehaviour {

    public GameObject reticlePrefab;

    private GameObject reticle;

    private Vector3 outOfBoundsPosition = new Vector3(-999999, -999999, -99999); // hopefully not visible.
    private int interactionLayer = 1 << 8;


    void Awake() {
        reticle = (GameObject)Instantiate(reticlePrefab, transform.position, Quaternion.identity);
	}
    
	void Update () {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, interactionLayer)) {
            reticle.transform.position = hit.point;
        }
        else {
            reticle.transform.position = outOfBoundsPosition;
        }
        
    }
}
