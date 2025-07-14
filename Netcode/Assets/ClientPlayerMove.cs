using StarterAssets;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClientPlayerMove : NetworkBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private StarterAssetsInputs _starterAssetInputs;
    [SerializeField] private ThirdPersonController _thirdPersonController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _playerInput.enabled = false;
        _starterAssetInputs.enabled = false;
        _thirdPersonController.enabled = false;
    }

    // Update is called once per frame
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            _playerInput.enabled = true;
            _starterAssetInputs.enabled = true;
        }

        if (IsServer)
        {
            _thirdPersonController.enabled = true;
        }
    }

    private void Update()
    {
        UpdatePlayerInputRpc(_starterAssetInputs.move, _starterAssetInputs.look, _starterAssetInputs.jump, _starterAssetInputs.sprint);
    }

    [Rpc(SendTo.Server)]
    private void UpdatePlayerInputRpc(Vector2 move, Vector2 look, bool jump, bool sprint)
    {
        _starterAssetInputs.MoveInput(move);
        _starterAssetInputs.LookInput(look);
        _starterAssetInputs.JumpInput(jump);
        _starterAssetInputs.SprintInput(sprint);
    }
}