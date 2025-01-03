using Playroom;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private PlayroomKit _roomKit = new();

    // Called when MonoBehaviour is first instantiated before Start()
    void Awake()
    {
        //Don't get used to this pls.
        DontDestroyOnLoad(gameObject);    
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
