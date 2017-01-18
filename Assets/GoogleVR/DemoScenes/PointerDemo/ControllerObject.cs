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

/// Provides visual feedback for the daydream controller.
public class ControllerObject : MonoBehaviour {

  /// Units are in meters
  private static readonly Vector3 TOUCHPAD_POINT_DIMENSIONS = new Vector3(0.01f, 0.0004f, 0.01f);
  private const float TOUCHPAD_RADIUS = 0.012f;
  private const float TOUCHPAD_POINT_Y_OFFSET = 0.035f;
  private const float TOUCHPAD_POINT_ELEVATION = 0.0025f;
  private const float TOUCHPAD_POINT_FILTER_STRENGTH = 0.8f;

  public GameObject touchPoint;
  public Material material_idle;
  public Material material_app;
  public Material material_system;
  public Material material_touchpad;
  public Material touchTransparent;
  public Material touchOpaque;

  void Update() {
    // Choose the appropriate material to render based on button states
    Renderer renderer = GetComponent<Renderer>();
    if (GvrController.ClickButton) {
      renderer.material = material_touchpad;
      touchPoint.SetActive(false);
      return;
    }

    // Change material to reflect button presses.
    if (GvrController.AppButton) {
      renderer.material = material_app;
    } else if (GvrController.Recentering) {
      renderer.material = material_system;
    } else {
      renderer.material = material_idle;
    }

    // Draw the touch point and animate the scale change.
    touchPoint.SetActive(true);
    if (GvrController.IsTouching) {
      float x = (GvrController.TouchPos.x - 0.5f) * 2.0f * TOUCHPAD_RADIUS;
      float y = (GvrController.TouchPos.y - 0.5f) * 2.0f * TOUCHPAD_RADIUS;
      touchPoint.transform.localScale = touchPoint.transform.localScale * TOUCHPAD_POINT_FILTER_STRENGTH +
                                        TOUCHPAD_POINT_DIMENSIONS * (1.0f - TOUCHPAD_POINT_FILTER_STRENGTH);
      touchPoint.transform.localPosition = new Vector3(-x, TOUCHPAD_POINT_Y_OFFSET - y, TOUCHPAD_POINT_ELEVATION);
    } else {
      touchPoint.transform.localScale = touchPoint.transform.localScale * TOUCHPAD_POINT_FILTER_STRENGTH;
    }

    //Adjust transparency
    float alpha = GvrArmModel.Instance.alphaValue;
    Color color = new Color(1.0f, 1.0f, 1.0f, alpha);
    renderer.material.color = color;
    Renderer touchRenderer = touchPoint.GetComponent<Renderer>();
    if (alpha < 1.0f) {
      touchRenderer.material = touchTransparent;
      touchRenderer.material.color = color;
    } else {
      touchRenderer.material = touchOpaque;
    }
  }
}
