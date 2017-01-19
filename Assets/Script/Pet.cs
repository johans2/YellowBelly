using UnityEngine;
using System.Collections;
using System;
using RGCommon;

public class Pet : MonoBehaviour {

    private NavMeshAgent navAgent;

	void Awake() {
        navAgent = Find.ComponentOnGameObject<NavMeshAgent>(this);
        PointerInteractor.MoveSignal.AddListener(OnMoveSignal);
	}
    
	void Update () {

	}

    private void OnMoveSignal(Vector3 target) {
        navAgent.SetDestination(target);
    }

}
