using UnityEngine;
using System.Collections;
using System;
using RGCommon;

public class Pet : MonoBehaviour {

    private NavMeshAgent navAgent;
    private Animator anim;
    private bool isMoving = false;
    private bool hasMovedOnce = false;

	void Awake() {
        navAgent = Find.ComponentOnGameObject<NavMeshAgent>(this);
        anim = Find.ComponentOnGameObject<Animator>(this);
        PointerInteractor.MoveSignal.AddListener(OnMoveSignal);
        
	}
    
	void Update () {
    }

    private void OnMoveSignal(Vector3 target) {
        anim.SetBool("Move", true);
        anim.SetBool("Idle", false);
        navAgent.SetDestination(target);
    }

}
