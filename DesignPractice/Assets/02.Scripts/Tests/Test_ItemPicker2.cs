using DP.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Test_ItemPicker2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera _camera;
    [SerializeField] InputActionReference _pickAction;


    private void Start()
    {
        _camera = Camera.main; // 메인 카메라를 기준으로 마우스 감지를 할 예정
    }


    private void OnEnable()
    {
        _pickAction.action.started += OnPickStarted;
        _pickAction.action.performed += OnPickPerformed;
        _pickAction.action.canceled += OnPickCanceled;
        _pickAction.action.Enable();
    }


    private void OnDisable()
    {
        _pickAction.action.started -= OnPickStarted;
        _pickAction.action.performed -= OnPickPerformed;
        _pickAction.action.canceled -= OnPickCanceled;
        _pickAction.action.Disable();
    }


    private void OnPickStarted(InputAction.CallbackContext context)
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent(out ItemController itemController))
            {
                itemController.PickUp();  // 여기서 인벤토리로 이동
            }
        }

        Debug.Log("Pick started");
    }


    private void OnPickPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Pick performed");
    }


    private void OnPickCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Pick canceled");
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
