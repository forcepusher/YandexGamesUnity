using UnityEngine;
using UnityEngine.EventSystems;

public class WebInputModule : StandaloneInputModule
{
    //public override void UpdateModule()
    //{
    //    Debug.Log(nameof(UpdateModule));
    //    base.UpdateModule();
    //}

    //public override bool IsModuleSupported()
    //{
    //    Debug.Log(nameof(IsModuleSupported));
    //    return base.IsModuleSupported();
    //}

    //public override bool ShouldActivateModule()
    //{
    //    Debug.Log(nameof(ShouldActivateModule));
    //    return base.ShouldActivateModule();
    //}

    //public override void ActivateModule()
    //{
    //    Debug.Log(nameof(ActivateModule));
    //    base.ActivateModule();
    //}

    //public override void DeactivateModule()
    //{
    //    Debug.Log(nameof(DeactivateModule));
    //    base.DeactivateModule();
    //}

    public override void Process()
    {
        Debug.Log($"Process 0 focused={eventSystem.isFocused} shouldIgnore={ShouldIgnoreEventsOnNoFocus()}");

        if (!eventSystem.isFocused && ShouldIgnoreEventsOnNoFocus())
            return;

        Debug.Log("Process 1");

        bool usedEvent = SendUpdateEventToSelectedObject();

        // case 1004066 - touch / mouse events should be processed before navigation events in case
        // they change the current selected gameobject and the submit button is a touch / mouse button.

        // touch needs to take precedence because of the mouse emulation layer
        if (!ProcessTouchEvents() && input.mousePresent)
        {
            Debug.Log("Process 2");
            ProcessMouseEvent();
        }

        if (eventSystem.sendNavigationEvents)
        {
            Debug.Log("Process 3");

            if (!usedEvent)
                usedEvent |= SendMoveEventToSelectedObject();

            if (!usedEvent)
                SendSubmitEventToSelectedObject();
        }
    }

    private bool ProcessTouchEvents()
    {
        Debug.Log("Real TouchCount=" + Input.touchCount + " Module TouchCount=" + input.touchCount);

        for (int i = 0; i < input.touchCount; ++i)
        {
            Debug.Log("Touch 0");

            Touch touch = input.GetTouch(i);

            if (touch.type == TouchType.Indirect)
                continue;

            Debug.Log("Touch 1");

            bool released;
            bool pressed;
            var pointer = GetTouchPointerEventData(touch, out pressed, out released);

            ProcessTouchPress(pointer, pressed, released);

            if (!released)
            {
                Debug.Log("Touch 2");

                ProcessMove(pointer);
                ProcessDrag(pointer);
            }
            else
                RemovePointerData(pointer);
        }
        return input.touchCount > 0;
    }

    private bool ShouldIgnoreEventsOnNoFocus()
    {
#if UNITY_EDITOR
        return !UnityEditor.EditorApplication.isRemoteConnected;
#else
            return true;
#endif
    }
}
