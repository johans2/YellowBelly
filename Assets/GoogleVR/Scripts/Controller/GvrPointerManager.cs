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

/// GvrPointerManager is a standard interface for
/// controlling which IGvrGazePointer is being used
/// for user input affordance.
///
/// Related to this, it is also used to control which camera
/// is used by any Canvases in the scene in conjunction with
/// GvrGazeCanvas. This is to make it easier to integrate
/// a pointer with unity's UI system.
///
public class GvrPointerManager : MonoBehaviour {
  private static GvrPointerManager instance;

  /// Delegate for callbacks that occur when the event camera changes.
  public delegate void PointerEventCameraChangedDelegate(Camera pointerEventCamera);

  /// Event for callbacks that occur when the event camera changes.
  public event PointerEventCameraChangedDelegate OnPointerEventCameraChanged;

  /// Returns the singleton instance associated with the GvrPointerManager.
  public static GvrPointerManager Instance
  {
    get {
      return instance;
    }
  }

  /// Change the IGvrGazePointer that is currently being used.
  public IGvrGazePointer Pointer
  {
    get {
      return pointer;
    }
    set {
      if (pointer == value) {
        return;
      }

      pointer = value;
      GazeInputModule.gazePointer = pointer;
    }
  }

  /// Change the event camera currently being used
  /// for Canvases.
  public Camera PointerEventCamera
  {
    get {
      return pointerEventCamera;
    }
    set {
      if (pointerEventCamera == value) {
        return;
      }

      pointerEventCamera = value;

      if (OnPointerEventCameraChanged != null) {
        OnPointerEventCameraChanged(PointerEventCamera);
      }
    }
  }

  /// GvrBaseGazePointer calls this when it is created.
  /// If a pointer hasn't already been assigned, it
  /// will assign the newly created one by default.
  ///
  /// This simplifies the common case of having only one
  /// GvrGazePointer so is can be automatically hooked up
  /// to the manager.  If multiple GvrGazePointers are in
  /// the scene, the app has to take responsibility for
  /// setting which one is active.
  public void OnPointerCreated(IGvrGazePointer createdPointer, Camera createdPointerEventCamera) {
    if (Pointer == null) {
      Pointer = createdPointer;
      PointerEventCamera = createdPointerEventCamera;
    }
  }

  private IGvrGazePointer pointer;
  private Camera pointerEventCamera;

  private void Awake() {
    if (instance != null) {
      Debug.LogError("More than one GvrPointerManager instance was found in your scene. "
        + "Ensure that there is only one GvrPointerManager.");
      this.enabled = false;
      return;
    }

    instance = this;
  }

  private void OnDestroy() {
    if (instance == this) {
      instance = null;
    }
  }
}
