using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyWarpableItem : MonoBehaviour
{
    [SerializeField] private WarpToLevel warpComponent;
    [SerializeField] private InputAction action;

    private bool enabled = false;
    private bool activated = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enabled)
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
