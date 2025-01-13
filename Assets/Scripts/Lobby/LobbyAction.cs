using UnityEngine;

public class LobbyAction : MonoBehaviour
{
    [SerializeField] public string ActionName;
    public virtual void PerformAction()
    {}

}