using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InputIcons;
using UnityEngine.InputSystem;

public class II_PlayerUIDisplayBehaviour : MonoBehaviour
{
    public TextMeshProUGUI deviceNameDisplayText;
    public Image deviceDisplayIcon;

    private II_Player player => GetComponent<II_Player>();

    [Header("Device Display Settings")]
    public InputIconSetConfiguratorSO iconSetConfigurator;


    private void OnEnable()
    {
        InputIconsManagerSO.onControlsChanged += UpdateUIVisuals;
        player.DeviceLostEvent += SetDisconnectedDeviceVisuals;

        UpdateUIVisuals(null);
    }

    private void OnDisable()
    {
        InputIconsManagerSO.onControlsChanged -= UpdateUIVisuals;
        player.DeviceLostEvent -= SetDisconnectedDeviceVisuals;
    }

    public void UpdateUIVisuals(PlayerInput playerInput)
    {
        Color deviceColor = InputIconSetConfiguratorSO.GetCurrentIconSet().deviceDisplayColor;
        deviceDisplayIcon.color = deviceColor;
        deviceNameDisplayText.text = "Device: " + InputIconSetConfiguratorSO.GetCurrentIconSet().deviceDisplayName;
    }

    public void SetDisconnectedDeviceVisuals()
    {

        Color disconnectedColor = InputIconSetConfiguratorSO.GetDisconnectedColor();
        deviceDisplayIcon.color = disconnectedColor;
        deviceNameDisplayText.text = "Device: " + InputIconSetConfiguratorSO.GetDisconnectedName();
    }
}
