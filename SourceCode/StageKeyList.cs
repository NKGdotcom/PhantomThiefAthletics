using System.Collections.Generic;
using UnityEngine;

public class StageKey : MonoBehaviour
{
    private List<GameObject> stageKeyList = new List<GameObject>();
    private List<GameObject> haveKeyList = new List<GameObject>();
    private const int _firstNum = 0;
    public int HaveKeyCount => haveKeyList.Count;
    public bool HasKey => haveKeyList.Count > 0;
    void Start()
    {
        stageKeyList.AddRange(GameObject.FindGameObjectsWithTag("Key"));
    }

    private void Update()
    {
        //Debug.Log(HasKey);
    }
    /// <summary>
    /// Œ»İ‚Á‚Ä‚¢‚éŒ®ƒŠƒXƒg‚É’Ç‰Á
    /// </summary>
    /// <param name="_key"></param>
    public void AddListHaveKey(GameObject _key)
    {
        if (haveKeyList.Contains(_key)) return;
        SoundManager.Instance.PlaySE(SESource.getItem);
        stageKeyList.Remove(_key);
        haveKeyList.Add(_key);
    }
    /// <summary>
    /// Œ®‚ğg‚¤
    /// </summary>
    public void UseKeys()
    {
        if (!HasKey) return;
        haveKeyList[_firstNum].SetActive(false);
        haveKeyList.RemoveAt(_firstNum);
    }
}

