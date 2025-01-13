using UnityEngine;

public class LockableBehaviour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer secondaryRenderer;
    [SerializeField] public bool Locked = true;
    [SerializeField] private GameObject[] objectsToHide;

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

        if(objectsToHide != null && objectsToHide.Length > 0)
        {
            foreach(GameObject o in objectsToHide)
            {
                o.SetActive(!Locked);   //If locked, we'll disable the gameobject
            }
        }
    }
}
