using UnityEngine;

public interface IMovementBehavior
{
    float GetSqrMagnitudeAngularVelocity();
    float GetSqrMagnitudeVelocity();

    void Movement(Vector3 speedVector);
    void AddTorque(Vector3 torqueVector);
    void ApplyImpulse(Vector3 impulse);

    void ResetTorqueParams();
    void ResetForceParams();

    bool CheckGround();
}
