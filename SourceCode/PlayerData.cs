using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float JumpPower { get => jumpPower; }
    [Header("ƒWƒƒƒ“ƒv‚Ì—Í")]
    [SerializeField] private float jumpPower;
}