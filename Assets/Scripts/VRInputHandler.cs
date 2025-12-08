using UnityEngine;
// Ensure you have the Input System package installed: com.unity.inputsystem
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class VRInputHandler : MonoBehaviour
{
    public VideoController videoController;

    [Header("Input Actions (New Input System)")]
    // These allow you to assign Input Actions from the Inspector (e.g. XRI Default Input)
#if ENABLE_INPUT_SYSTEM
    public InputActionProperty playPauseAction;
    public InputActionProperty seekRightAction;
    public InputActionProperty seekLeftAction;
#endif

    [Header("Fallback / Debug Keys")]
    public KeyCode debugPlayPause = KeyCode.Space;
    public KeyCode debugSeekFwd = KeyCode.RightArrow;
    public KeyCode debugSeekBack = KeyCode.LeftArrow;

    private void OnEnable()
    {
#if ENABLE_INPUT_SYSTEM
        playPauseAction.action?.Enable();
        seekRightAction.action?.Enable();
        seekLeftAction.action?.Enable();
#endif
    }

    private void Update()
    {
        if (videoController == null) return;

        // Check Input System Actions
#if ENABLE_INPUT_SYSTEM
        if (playPauseAction.action != null && playPauseAction.action.WasPerformedThisFrame())
        {
            videoController.TogglePlayPause();
        }
        
        if (seekRightAction.action != null && seekRightAction.action.WasPerformedThisFrame())
        {
            videoController.SeekForward();
        }
        
        if (seekLeftAction.action != null && seekLeftAction.action.WasPerformedThisFrame())
        {
            videoController.SeekBackward();
        }
#endif

        // Check Fallback Keys
        if (Input.GetKeyDown(debugPlayPause))
        {
            videoController.TogglePlayPause();
        }
        if (Input.GetKeyDown(debugSeekFwd))
        {
            videoController.SeekForward();
        }
        if (Input.GetKeyDown(debugSeekBack))
        {
            videoController.SeekBackward();
        }
    }
}
