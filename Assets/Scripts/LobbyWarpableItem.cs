using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyWarpableItem : MonoBehaviour
{
    [SerializeField] private WarpToLevel warpComponent;
    [SerializeField] private LockableBehaviour lockable;
    private InputAction action;

    private bool enabled = false;
    private bool activated = false;
    private bool locked = true;

    void Awake()
    {
        action = InputSystem.actions.FindAction("Attack");
        if (lockable == null)
            locked = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(enabled && !locked)
        {
            if(action.IsPressed())
            {
                activated = true;
                return; //Wait one frame before warping.
            }
        }
        if(activated)
        {
            activated = false;
            warpComponent.Warp();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("LWI col: "+ this.gameObject.name + " / " + other.gameObject.name);
        if(other.gameObject.tag == "Player")
        {
            enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            enabled = true;
        }
    }
}
