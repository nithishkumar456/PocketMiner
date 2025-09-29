using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class ClickToMove : MonoBehaviour
{
    public Tilemap groundTilemap;
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool moving = false;
    private Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
        targetPosition = transform.position;
    }

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
                moving = false;
        }
    }

    // This will be called by Player Input when Click action is performed
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 screenPos = Mouse.current.position.ReadValue();
            Vector3 worldPos = mainCam.ScreenToWorldPoint(screenPos);
            Vector3Int cellPos = groundTilemap.WorldToCell(worldPos);

            if (groundTilemap.HasTile(cellPos))
            {
                targetPosition = groundTilemap.GetCellCenterWorld(cellPos);
                moving = true;
            }
        }
    }
}
