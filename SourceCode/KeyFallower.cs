using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyFallower : MonoBehaviour
{
    [Header("追尾スピード")]
    [SerializeField] private float keyFallowSpeed;

    private Transform playerTarget;
    private Vector3 identityKeyPosition;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (playerTarget != null)
        {
            FollowKey();
        }
    }
    /// <summary>
    /// プレイヤーが触れたときに位置とターゲットを設定
    /// </summary>
    /// <param name="_player"></param>
    public void SetTarget(GameObject _player)
    {
        playerTarget = _player.transform;
        identityKeyPosition = playerTarget.position;
    }
    /// <summary>
    /// 鍵が追尾
    /// </summary>
    private void FollowKey()
    {
        transform.position = Vector3.Lerp(transform.position, playerTarget.position, keyFallowSpeed * Time.deltaTime);
    }
}