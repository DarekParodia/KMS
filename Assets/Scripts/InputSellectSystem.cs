using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[ExecuteAlways]
public class GamepadSelection : MonoBehaviour
{
    public TMP_Dropdown Player1Dropdown;
    public TMP_Dropdown Player2Dropdown;
    private InputDevice Player1SelectedDevice;
    private InputDevice Player2SelectedDevice;

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        UpdateInputDeviceList();

        Player1Dropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(Player1Dropdown, 1); });
        Player2Dropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(Player2Dropdown, 2); });
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
        Player1Dropdown.onValueChanged.RemoveAllListeners();
        Player2Dropdown.onValueChanged.RemoveAllListeners();
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        UpdateInputDeviceList();
    }

    private void UpdateInputDeviceList()
    {
        var devices = new List<InputDevice>();
        foreach (var device in InputSystem.devices)
        {
            if (device is Gamepad || device is Keyboard)
            {
                devices.Add(device);
            }
        }
        UpdateDropdownOptions(devices.ToArray());
    }

    private void UpdateDropdownOptions(InputDevice[] devices)
    {
        Player1Dropdown.options.Clear();
        Player2Dropdown.options.Clear();

        TMP_Dropdown.OptionData placeholderOption = new TMP_Dropdown.OptionData("Wybierz opcje");
        Player1Dropdown.options.Add(placeholderOption);
        Player2Dropdown.options.Add(placeholderOption);

        foreach (var device in devices)
        {
            bool isSelected = device == Player1SelectedDevice || device == Player2SelectedDevice;
            if (!isSelected)
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData(device.displayName);
                Player1Dropdown.options.Add(optionData);
                Player2Dropdown.options.Add(optionData);
            }
        }

        Player1Dropdown.value = 0;
        Player2Dropdown.value = 0;
        Player1Dropdown.RefreshShownValue();
        Player2Dropdown.RefreshShownValue();
    }

    private void OnDropdownValueChanged(TMP_Dropdown dropdown, int playerIndex)
    {
        if (dropdown.value == 0) return; // Placeholder selected

        InputDevice selectedDevice = availableInputDevices[dropdown.value - 1];
        if (playerIndex == 1)
        {
            Player1SelectedDevice = selectedDevice;
            Player2SelectedDevice = null; // Deselect Player 2
        }
        else if (playerIndex == 2)
        {
            Player2SelectedDevice = selectedDevice;
            Player1SelectedDevice = null; // Deselect Player 1
        }

        UpdateInputDeviceList(); // Refresh the list to reflect the new selections
    }
}
