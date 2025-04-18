using DP.Controllers;
using UnityEngine;

public class Test_ItemPicker : MonoBehaviour
{
    Camera _camera;


    private void Start()
    {
        _camera = Camera.main; // 메인 카메라를 기준으로 마우스 감지를 할 예정
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out ItemController itemController))
                {
                    itemController.PickUp();
                }
            }
        }
    }
}
