# Unity OpenXR VR Video Viewer

This project is a 360/180/2D VR Video Viewer built with Unity and OpenXR.

## Prerequisites

- **Unity Hub**: Installed.
- **Unity Editor**: Recommended version 2021.3 LTS or 2022.3 LTS. Ensure **Android Build Support** (with OpenJDK & SDK) and **Windows Build Support** are installed.

## Project Setup Guide

### 1. Create a New Project
1. Open Unity Hub.
2. Click **New Project**.
3. Select **3D (URP)** or **3D (Core)**. URP is recommended for better performance on standalone headsets.
4. Name your project (e.g., `VRVideoViewer`) and create it.

### 2. Install OpenXR Plugin
1. In Unity, go to **Window > Package Manager**.
2. Click the `+` icon in the top left or ensure "Packages: Unity Registry" is selected.
3. Search for **OpenXR Plugin** and click **Install**.
4. If prompted to enable the new Input System, click **Yes** and let the editor restart.

### 3. Configure XR Plug-in Management
1. Go to **Edit > Project Settings > XR Plug-in Management**.
2. **PC Tab**: Check **OpenXR**.
3. **Android Tab**: Check **OpenXR**.
   - Under **OpenXR > Interaction Profiles**, add **Oculus Touch Controller Profile** (or others as needed, e.g., HTC Vive, Valve Index).
   - Resolve any warnings/errors that appear in the validation check (yellow/red icons).

### 4. Scene Setup
1. Open your main scene (e.g., `SampleScene`).
2. Delete the default **Main Camera**.
3. Right-click in Hierarchy > **XR > XR Origin (VR)**. (If you don't see this, install the **XR Interaction Toolkit** from Package Manager > Unity Registry, or just create a Game Object with a Camera and "Tracked Pose Driver").
   - *Note: Using XR Interaction Toolkit is the easiest way.*
4. **Video Sphere**:
   - Create a **3D Object > Sphere**.
   - Scale it to `(-100, 100, 100)` (Negative scale flips normals so you see it from inside).
   - Set its Position to `(0, 0, 0)`.
   - Remove the **Sphere Collider** (optional, prevents raycast blocks).
   - Create a new Material (Right-click Project > Create > Material), set Shader to **Universal Render Pipeline/Unlit** (or Standard > Unlit/Texture), and assign it to the Sphere.
5. **Video Player**:
   - Add a **Video Player** component to the Sphere.
   - Set **Source** to URL or Video Clip.
   - Set **Render Mode** to **Material Override** (specifically `BaseMap` or `_MainTex`).
   - Drag your video file into `Assets` and assign it, or use a URL.

### 5. Scripts Setup
1. Copy the provided scripts (`VideoController.cs`, `VRInputHandler.cs`) into `Assets/Scripts/`.
2. Attach `VideoController` to the **Sphere** (or wherever the Video Player is).
3. Attach `VRInputHandler` to the **XR Origin** or a functional Manager object.
4. Link the `VideoController` reference in the `VRInputHandler` inspector.
5. Map your inputs (if using Input System, create an Input Action Asset or use direct button references).

## Usage
- **Play/Pause**: Press the designated Trigger/Button `(A)`.
- **Seek**: Use the Joystick (Left/Right).
- **Recenter**: System default or implemented via `(B)` button.

## Troubleshooting
- **Black Screen**: Check if the video URL is valid or if the codec is supported on the target platform (H.264/H.265 usually work best for Android XR).
- **Inverted Video**: Ensure the Sphere scale is negative (`-100, 100, 100`).

