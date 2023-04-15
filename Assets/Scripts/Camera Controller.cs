using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Selection selection; 
    [SerializeField] private UIControls uIControls;
    private CinemachineVirtualCamera virtualCamera;
    private Transform activeCamera;
    private GameObject suspension;
   
    // Start is called before the first frame update
    void Start()
    {
        selection.OnSelection += FocusOnSelection;
        uIControls.OnSelection += FocusOnButtonPress;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selection.ClearSelection();
            OnCameraSwitch();
        }
    }

    private void FocusOnSelection(object sender, Selection.selectionEventArgs selectionEventArgs)
    {
        OnCameraSwitch();
        if (selectionEventArgs.triggeringGameObject != null)
        {
            switch (selectionEventArgs.triggeringGameObject.name)
            {
                case "SkyCarBody":
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Button"));
                    virtualCamera = GameObject.Find("Virtual Camera Car Body").GetComponent<CinemachineVirtualCamera>();
                    break;

                case "SkyCarWheelRearLeft":
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Button (8)"));
                    virtualCamera = GameObject.Find("Virtual Camera Back Left Wheel").GetComponent<CinemachineVirtualCamera>();
                    break;

                case "SkyCarWheelRearRight":
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Button (7)"));
                    virtualCamera = GameObject.Find("Virtual Camera Back Right Wheel").GetComponent<CinemachineVirtualCamera>();
                    break;

                case "SkyCarSuspensionFrontRight":
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Button (5)"));
                    suspension = GameObject.Find("SkyCarWheelFrontRight");
                    suspension.GetComponent<MeshRenderer>().enabled = false;
                    suspension.GetComponent<MeshCollider>().enabled = false;
                    goto case "SkyCarWheelFrontRight";

                case "SkyCarWheelFrontRight":
                    if (suspension == null) EventSystem.current.SetSelectedGameObject(GameObject.Find("Button (1)"));
                    virtualCamera = GameObject.Find("Virtual Camera Front Right Suspension and Wheel").GetComponent<CinemachineVirtualCamera>();
                    break;

                case "SkyCarSuspensionFrontLeft":
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Button (6)"));
                    suspension = GameObject.Find("SkyCarWheelFrontLeft");
                    suspension.GetComponent<MeshRenderer>().enabled = false;
                    suspension.GetComponent<MeshCollider>().enabled = false;
                    goto case "SkyCarWheelFrontLeft";

                case "SkyCarWheelFrontLeft":
                    if (suspension == null) EventSystem.current.SetSelectedGameObject(GameObject.Find("Button (3)"));
                    virtualCamera = GameObject.Find("Virtual Camera Front Left Suspension and Wheel").GetComponent<CinemachineVirtualCamera>();
                    break;

                case "SkyCarMudGuardFrontLeft":
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Button (4)"));
                    virtualCamera = GameObject.Find("Virtual Camera Front Left Mud Guard").GetComponent<CinemachineVirtualCamera>();
                    break;

                case "SkyCarMudGuardFrontRight":
                    EventSystem.current.SetSelectedGameObject(GameObject.Find("Button (2)"));
                    virtualCamera = GameObject.Find("Virtual Camera Front Right Mud Guard").GetComponent<CinemachineVirtualCamera>();
                    break;
            }
            SwapCamera();
        }
    }

    private void FocusOnButtonPress(object sender, UIControls.selectionEventArgs selectionEventArgs)
    {
        OnCameraSwitch();
        GameObject.Find(selectionEventArgs.triggeringGameObject).GetComponent<Outline>().enabled = true;
        selection.SetSelection(selectionEventArgs.triggeringGameObject);
        switch (selectionEventArgs.triggeringGameObject)
        {
            case "SkyCarBody":
                virtualCamera = GameObject.Find("Virtual Camera Car Body").GetComponent<CinemachineVirtualCamera>();
                break;

            case "SkyCarWheelRearLeft":
                virtualCamera = GameObject.Find("Virtual Camera Back Left Wheel").GetComponent<CinemachineVirtualCamera>();
                break;

            case "SkyCarWheelRearRight":
                virtualCamera = GameObject.Find("Virtual Camera Back Right Wheel").GetComponent<CinemachineVirtualCamera>();
                break;

            case "SkyCarSuspensionFrontRight":
                suspension = GameObject.Find("SkyCarWheelFrontRight");
                suspension.GetComponent<MeshRenderer>().enabled = false;
                suspension.GetComponent<MeshCollider>().enabled = false;
                goto case "SkyCarWheelFrontRight";

            case "SkyCarWheelFrontRight":
                virtualCamera = GameObject.Find("Virtual Camera Front Right Suspension and Wheel").GetComponent<CinemachineVirtualCamera>();
                break;

            case "SkyCarSuspensionFrontLeft":
                suspension = GameObject.Find("SkyCarWheelFrontLeft");
                suspension.GetComponent<MeshRenderer>().enabled = false;
                suspension.GetComponent<MeshCollider>().enabled = false;
                goto case "SkyCarWheelFrontLeft";

            case "SkyCarWheelFrontLeft":
                virtualCamera = GameObject.Find("Virtual Camera Front Left Suspension and Wheel").GetComponent<CinemachineVirtualCamera>();
                break;

            case "SkyCarMudGuardFrontLeft":
                virtualCamera = GameObject.Find("Virtual Camera Front Left Mud Guard").GetComponent<CinemachineVirtualCamera>();
                break;

            case "SkyCarMudGuardFrontRight":
                virtualCamera = GameObject.Find("Virtual Camera Front Right Mud Guard").GetComponent<CinemachineVirtualCamera>();
                break;

        }
        SwapCamera();
    }

    private void SwapCamera()
    {
        activeCamera = virtualCamera.transform;
        virtualCamera.enabled = true;
    }

    private void OnCameraSwitch()
    {

        if (activeCamera != null)
        {
            activeCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
            activeCamera = null;
        }

        if (suspension != null)
        {
            suspension.GetComponent<MeshRenderer>().enabled = true;
            suspension.GetComponent<MeshCollider>().enabled = true;
            suspension = null;
        }
    }

}

