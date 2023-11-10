using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;

public class PlayerManager : MonoBehaviour
{
    private List<PlayerInput> players = new List<PlayerInput>();

    [SerializeField]
    private List<Transform> startingPoints;
    [SerializeField]
    private List<LayerMask> playerLayers;
    [SerializeField]
    private List<LayerMask> itemContainer;
    [SerializeField]
    private List<LayerMask> playerColliderLayers;
    [SerializeField]
    private List<LayerMask> playerRespawnLayer;
    [SerializeField]
    private List<LayerMask> playerCameraLayer;

    private PlayerInputManager playerInputManager;


    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }
    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddPlayer(PlayerInput player)
    {
        players.Add(player);

        Transform playerParent = player.transform.parent;
        playerParent.position = startingPoints[players.Count - 1].position;

        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);
        int colliderLayerToAdd = (int)Mathf.Log(playerColliderLayers[players.Count - 1].value, 2);
        int itemContainerLayerToAdd = (int)Mathf.Log(itemContainer[players.Count - 1].value, 2);
        int playerRespawnLayerToAdd = (int)Mathf.Log(playerRespawnLayer[players.Count - 1].value, 2);
        //int playerCombatCameraLayerToAdd = (int)Mathf.Log(playerCameraLayer[players.Count - 1].value, 2);

        playerParent.GetComponentInChildren<CinemachineFreeLook>().gameObject.layer = layerToAdd;

        //playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = playerCombatCameraLayerToAdd;
        playerParent.GetComponentInChildren<CapsuleCollider>().gameObject.layer =colliderLayerToAdd;
        playerParent.GetComponentInChildren<BoxCollider>().gameObject.layer = itemContainerLayerToAdd;
        playerParent.GetComponentInChildren<RespawnControl>().gameObject.layer = playerRespawnLayerToAdd;
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << colliderLayerToAdd;
        playerParent.GetComponentInChildren<InputHandler>().horizontal = player.actions.FindAction("CameraLook");


    }
}
