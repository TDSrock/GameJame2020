using UnityEngine;
public interface IInteractable
{
    void OnInteract();
    Vector3 GetPosition();
    string GetObjectName();
    GameObject GameObject { get; }
}
