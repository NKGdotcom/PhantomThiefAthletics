using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public static ResultManager Instance { get; private set; }

    [SerializeField] private GameObject gameClearUI;
    [SerializeField] private GameObject gameOverUI;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameClear()
    {
        gameClearUI.SetActive(true);
        SoundManager.Instance.PlaySE(SESource.clear);
        ResultTextTrigger.Instance.SetActiveText().Forget();
    }
    public void GameOver()
    {
        GameState.Instance.StateResult();
        gameOverUI.SetActive(true);
        SoundManager.Instance.PlaySE(SESource.over);
        ResultTextTrigger.Instance.SetActiveText().Forget();
    }
}
