using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyWarpableItem : MonoBehaviour
{
    [SerializeField] private WarpToLevel warpComponent;
    [SerializeField] private LockableBehaviour lockable;
    private InputAction action;

    public bool Enabled = false;
    private bool activated = false;
    private bool locked = true;
    private bool lockableFound = false;

    void Awake()
    {
        action = InputSystem.actions.FindAction("Attack");
        if (lockable == null)
            locked = false;
        else
            lockableFound = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(lockableFound)
            locked = lockable.Locked;

        if(Enabled && !locked)
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
            Enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Enabled = true;
        }
    }
}
