using UnityEngine;

[CreateAssetMenu( menuName = "Character/CharacterData", fileName = "CharacterData")]
public class CharacterData : ScriptableObject
{

#region Variables

    [SerializeField] private float turnSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float slipeForce;

#endregion

#region  Properties

    public float TrunSpeed {get {return turnSpeed;} private set {turnSpeed = value;}}
    public float JumpForce {get {return jumpForce;} private set {jumpForce = value;}}
    public float SlipForce {get {return slipeForce;} private set {slipeForce = value;}}

#endregion

}
