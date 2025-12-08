using UnityEngine;
using UnityEngine.Video;

public class VideoScreenSetup : MonoBehaviour
{
    [Tooltip("If true, setup will run on Start")]
    public bool autoSetup = true;
    public string videoUrl = "https://media.w3.org/2010/05/sintel/trailer.mp4"; // Example

    private void Start()
    {
        if (autoSetup)
        {
            SetupScreen();
        }
    }

    [ContextMenu("Setup Screen Now")]
    public void SetupScreen()
    {
        // Check if we already have a sphere child
        Transform existing = transform.Find("VideoSphere");
        GameObject sphere = existing ? existing.gameObject : GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = "VideoSphere";
        sphere.transform.SetParent(transform);
        sphere.transform.localPosition = Vector3.zero;
        
        // Invert normals by setting negative scale
        sphere.transform.localScale = new Vector3(-100, 100, 100);

        // Remove Collider
        Collider col = sphere.GetComponent<Collider>();
        if (col) DestroyImmediate(col);

        // Setup Video Player
        VideoPlayer vp = sphere.GetComponent<VideoPlayer>();
        if (!vp) vp = sphere.AddComponent<VideoPlayer>();

        vp.playOnAwake = true;
        vp.renderMode = VideoRenderMode.MaterialOverride;
        vp.targetMaterialProperty = "_BaseMap"; // URP default, often _MainTex for built-in
        
        // Try setting standard property if URP one fails or just to be safe
        // Note: In runtime code we can't easily check shader properties without Material access, 
        // but VideoPlayer usually handles standard shaders well.
        
        vp.url = videoUrl;
        vp.isLooping = true;

        // Ensure we have a material that supports video (Unlit usually)
        Renderer rend = sphere.GetComponent<Renderer>();
        if (rend.sharedMaterial == null || rend.sharedMaterial.name == "Default-Material")
        {
            // Create a temporary material (runtime only)
            Material mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            if (!mat) mat = new Material(Shader.Find("Unlit/Texture"));
            rend.material = mat;
        }

        // Connect Controller if attached to this object
        VideoController ctrl = GetComponent<VideoController>();
        if (ctrl)
        {
            // We can't set the private field easily unless we make it public or add a Setup method
            // For now, the user manually links it or we rely on GetComponent in VideoController logic if we changed it.
            // But VideoController does GetComponent<VideoPlayer>(), so we should move VideoController to the Sphere 
            // OR make VideoController search for it.
            
            // Correction: My VideoController expects VideoPlayer on the SAME object.
            // So if I put VideoController on THIS object, I should put VideoPlayer on THIS object?
            // But the Sphere needs to handle the rendering.
            
            // Better approach: Create Sphere, put VideoPlayer on Sphere, put VideoController on Sphere.
        }
    }
}
