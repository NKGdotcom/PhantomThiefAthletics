using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float JumpPower { get => jumpPower; }
    [Header("�W�����v�̗�")]
    [SerializeField] private float jumpPower;
}