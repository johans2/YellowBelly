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
using UnityEngine.UI;
using UnityEngine.VR;

[ExecuteInEditMode]
public class GvrToolTips : MonoBehaviour {

  private bool bAnimate = false;

  private GameObject TouchPadText;
  private GameObject AppButtonText;
  private GameObject SwipePadText;
  private GameObject MenuButtonText;

  private GameObject TouchPad;
  private GameObject AppButton;
  private GameObject SwipePad;
  private GameObject MenuButton;

  void OnEnable() {
    // Find the reference for the GameObjects.
    TouchPadText = transform.FindChild("TouchPad").transform.FindChild("TouchPadShadow").transform.FindChild("TouchPadText").gameObject;
    AppButtonText = transform.FindChild("AppButton").transform.FindChild("AppButtonShadow").transform.FindChild("AppButtonText").gameObject;
    SwipePadText = transform.FindChild("SwipePad").transform.FindChild("SwipePadShadow").transform.FindChild("SwipePadText").gameObject;
    MenuButtonText = transform.FindChild("MenuButton").transform.FindChild("MenuButtonShadow").transform.FindChild("MenuButtonText").gameObject;

    TouchPad = transform.FindChild("TouchPad").gameObject;
    AppButton = transform.FindChild("AppButton").gameObject;
    SwipePad = transform.FindChild("SwipePad").gameObject;
    MenuButton = transform.FindChild("MenuButton").gameObject;
  }

  private Vector3 GetHeadForward() {
#if UNITY_EDITOR
    return GvrViewer.Instance.HeadPose.Orientation * Vector3.forward;
#else
    return InputTracking.GetLocalRotation(VRNode.Head) * Vector3.forward;
#endif // UNITY_EDITOR
  }

  void Update () {
    // Place Tooltips on the right/left of the controller.
    if (!Application.isPlaying) {
      ShowRightLeft();
    } else {
      // Show tooltips if the controller is in the FOV or if the controller angle is high enough.
      float controllerAngleToFront = Vector3.Angle(GvrController.Orientation * Vector3.down, GetHeadForward());
      bAnimate = (controllerAngleToFront < 50.0f);

      // Force the tooltips off if the controller fades out or is twisted backwards.
      if (GvrArmModel.Instance.alphaValue < 1.0f) {
        bAnimate = false;
      }

      // Updating the transition boolean for animation.
      GetComponent<Animator>().SetBool("visible", bAnimate);

      // Reseting the transition variable.
      bAnimate = false;
    }
  }

  public void ShowRightLeft() {
    // Place the pivot on the center.
    TouchPadText.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
    AppButtonText.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
    SwipePadText.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
    MenuButtonText.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

    // Place the tooltips for right hand.
    if (GvrSettings.Handedness == GvrSettings.UserPrefsHandedness.Right) {
      TouchPad.transform.localScale = new Vector3(1f, 1f, 1f);
      TouchPadText.transform.localScale = new Vector3(1f, 1f, 1f);
      TouchPadText.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

      AppButton.transform.localScale = new Vector3(1f, 1f, 1f);
      AppButtonText.transform.localScale = new Vector3(1f, 1f, 1f);
      AppButtonText.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

      SwipePad.transform.localScale = new Vector3(-1f, 1f, 1f);
      SwipePadText.transform.localScale = new Vector3(-1f, 1f, 1f);
      SwipePadText.GetComponent<Text>().alignment = TextAnchor.MiddleRight;

      MenuButton.transform.localScale = new Vector3(-1f, 1f, 1f);
      MenuButtonText.transform.localScale = new Vector3(-1f, 1f, 1f);
      MenuButtonText.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
    } else {
      // Place the tooltips for left hand.
      AppButton.transform.localScale = new Vector3(-1f,1f,1f);
      AppButtonText.transform.localScale = new Vector3(-1f,1f,1f);
      AppButtonText.GetComponent<Text>().alignment = TextAnchor.MiddleRight;

      TouchPad.transform.localScale = new Vector3(-1f,1f,1f);
      TouchPadText.transform.localScale = new Vector3(-1f,1f,1f);
      TouchPadText.GetComponent<Text>().alignment = TextAnchor.MiddleRight;

      SwipePad.transform.localScale = new Vector3(1f, 1f, 1f);
      SwipePadText.transform.localScale = new Vector3(1f, 1f, 1f);
      SwipePadText.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;

      MenuButton.transform.localScale = new Vector3(1f, 1f, 1f);
      MenuButtonText.transform.localScale = new Vector3(1f, 1f, 1f);
      MenuButtonText.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
    }
  }
}
