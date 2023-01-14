using UnityEngine;

public interface IMoveable
{
    public void HandleMovement(Vector3 targetPosition);
    public void StopMovement();
    public void HandleRotation(Vector3 targetPosition, float rotateSpeed);

}