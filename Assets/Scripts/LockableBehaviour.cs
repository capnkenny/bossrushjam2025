using UnityEngine;

public class LockableBehaviour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer secondaryRenderer;
    [SerializeField] public bool Locked = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spriteRenderer != null)
        {
            spriteRenderer.material?.SetInt("_Locked", Locked ? 1 : 0);
        }
        if(secondaryRenderer != null)
        {
            secondaryRenderer.material?.SetInt("_Locked", Locked ? 1 : 0);
        }
    }
}
