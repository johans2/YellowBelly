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
using UnityEngine.UI;

public class PointerDemoManager : MonoBehaviour {

  private const string DISCONNECTED_TEXT = "Controller disconnected.";
  private const string SCANNING_TEXT = "Controller scanning...";
  private const string CONNECTING_TEXT = "Controller connecting...";

  public GameObject messageCanvas;
  public Text messageText;

  void Update() {
    UpdatePointer();
    UpdateStatusMessage();
  }

  private void UpdateStatusMessage() {
    // This is an example of how to process the controller's state to display a status message.
    switch (GvrController.State) {
      case GvrConnectionState.Connected:
        messageCanvas.SetActive(false);
        break;
      case GvrConnectionState.Disconnected:
        messageText.text = DISCONNECTED_TEXT;
        messageText.color = Color.white;
        messageCanvas.SetActive(true);
        break;
      case GvrConnectionState.Scanning:
        messageText.text = SCANNING_TEXT;
        messageText.color = Color.cyan;
        messageCanvas.SetActive(true);
        break;
      case GvrConnectionState.Connecting:
        messageText.text = CONNECTING_TEXT;
        messageText.color = Color.yellow;
        messageCanvas.SetActive(true);
        break;
      case GvrConnectionState.Error:
        messageText.text = "ERROR: " + GvrController.ErrorDetails;
        messageText.color = Color.red;
        messageCanvas.SetActive(true);
        break;
      default:
        // Shouldn't happen.
        Debug.LogError("Invalid controller state: " + GvrController.State);
        break;
    }
  }

  private void UpdatePointer() {
    /// Cast the ray and apply selections.
    RaycastHit hitInfo;
    Vector3 rayOrigin = GvrArmModel.Instance.pointerPosition;
    Vector3 rayDirection = GvrArmModel.Instance.pointerRotation * Vector3.forward;
    if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo)) {
      if (hitInfo.collider && hitInfo.collider.gameObject) {
        SetSelectedObject(hitInfo.collider.gameObject);
      }
    }
  }

  private void SetSelectedObject(GameObject obj) {
    LaserHighlight laserHighlight = obj.GetComponent<LaserHighlight>();
    if (laserHighlight) {
      laserHighlight.rayHit = true;
    }
  }
}
