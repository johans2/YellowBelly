// Copyright 2016 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;

/// Allows the bones to be visualized for debugging purposes.
public class BoneScript : MonoBehaviour {

  private LineRenderer lineRenderer;

  public enum Joint {
    Pointer,
    Wrist,
    Shoulder,
    Elbow
  }
  public Joint joint;

  public GameObject debugDrawTo;

  void Awake() {
    lineRenderer = gameObject.GetComponent<LineRenderer>();
  }

  void LateUpdate() {
    Vector3 jointPosition;
    Quaternion jointRotation;

    switch (joint) {
      case Joint.Pointer:
        jointPosition = GvrArmModel.Instance.pointerPosition;
        jointRotation = GvrArmModel.Instance.pointerRotation;
        break;
      case Joint.Wrist:
        jointPosition = GvrArmModel.Instance.wristPosition;
        jointRotation = GvrArmModel.Instance.wristRotation;
        break;
      case Joint.Elbow:
        jointPosition = GvrArmModel.Instance.elbowPosition;
        jointRotation = GvrArmModel.Instance.elbowRotation;
        break;
      case Joint.Shoulder:
        jointPosition = GvrArmModel.Instance.shoulderPosition;
        jointRotation = GvrArmModel.Instance.shoulderRotation;
        break;
      default:
        throw new System.Exception("Invalid FromJoint.");
    }

    transform.localPosition = jointPosition;
    transform.localRotation = jointRotation;

    DrawDebugLine();
  }

  private void DrawDebugLine() {
    if (lineRenderer == null || debugDrawTo == null) {
      return;
    }
    if (lineRenderer.useWorldSpace) {
      lineRenderer.SetPosition(0, transform.position);
      lineRenderer.SetPosition(1, debugDrawTo.transform.position);
    } else {
      lineRenderer.SetPosition(0, Vector3.zero);
      lineRenderer.SetPosition(1, transform.InverseTransformPoint(debugDrawTo.transform.position));
    }
  }
}
