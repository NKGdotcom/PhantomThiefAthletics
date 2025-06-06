using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyFallower : MonoBehaviour
{
    [Header("�ǔ��X�s�[�h")]
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
    /// �v���C���[���G�ꂽ�Ƃ��Ɉʒu�ƃ^�[�Q�b�g��ݒ�
    /// </summary>
    /// <param name="_player"></param>
    public void SetTarget(GameObject _player)
    {
        playerTarget = _player.transform;
        identityKeyPosition = playerTarget.position;
    }
    /// <summary>
    /// �����ǔ�
    /// </summary>
    private void FollowKey()
    {
        transform.position = Vector3.Lerp(transform.position, playerTarget.position, keyFallowSpeed * Time.deltaTime);
    }
}