using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DapperDino.InputSystemTutorials
{
    public class RebindingDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputActionReference actionReference = null;
        [SerializeField] private PlayerController playerController = null;
        [SerializeField] private TMP_Text bindingDisplayNameText = null;
        [SerializeField] private GameObject startRebindObject = null;
        [SerializeField] private GameObject waitingForInputObject = null;

        private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

        public void StartRebinding()
        {
            startRebindObject.SetActive(false);
            waitingForInputObject.SetActive(true);

            playerController.PlayerInput.SwitchCurrentActionMap("Menu");

            rebindingOperation = actionReference.action.PerformInteractiveRebinding()
                .WithControlsExcluding("Mouse")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindCompleted())
                .Start();
        }

        private void RebindCompleted()
        {
            int controlBindingIndex = actionReference.action.GetBindingIndexForControl(actionReference.action.controls[0]);

            bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
                actionReference.action.bindings[controlBindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);

            rebindingOperation.Dispose();
            rebindingOperation = null;

            startRebindObject.SetActive(true);
            waitingForInputObject.SetActive(false);

            playerController.PlayerInput.SwitchCurrentActionMap("Gameplay");
        }
    }
}
