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

public class LaserHighlight : MonoBehaviour {

  private Material material;
  private float hsvHue = 0.6f;

  public bool rayHit;

  void Start() {
    material = GetComponent<Renderer>().material;
  }

  void Update() {
    // Continually move the scale closer to 1 (for scaling animation).
    transform.localScale = (transform.localScale + Vector3.one) * 0.5f;

    // Check for an intersection with the object and change color if so.
    float hsvValue = 0.5f;
    if (rayHit) {
      if (GvrController.ClickButtonDown) {
        hsvHue = Random.Range(0.0f, 1.0f);
        transform.localScale *= 1.2f;
      }
      hsvValue = 0.8f;
      rayHit = false;
    }
    material.color = Color.HSVToRGB(hsvHue, 0.8f, hsvValue);
  }
}

