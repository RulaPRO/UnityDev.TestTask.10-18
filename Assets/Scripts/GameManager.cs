using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public CameraController Camera;
    public float CameraSens = 3f;

    public UiManager UiManager;

    public GamePrefabs Prefabs;

    private PlayerController _player;
    public PlayerController Player
    {
        set { _player = value; }
    }

    private QuestObject[] _questObjects;
    private int[] _questObjectPriority;
    private const int QuestObjectCount = 3;
    private int _activatedQuestObjectCount;
    public int ActivatedQuestObjectCount
    {
        get { return _activatedQuestObjectCount; }
        set { _activatedQuestObjectCount = value; }
    }

    private bool _reset;
    public bool Reset
    {
        set { _reset = value; }
    }

    private float _resetTimer;

    void Start()
    {
        _questObjects = new QuestObject[QuestObjectCount];
        _questObjectPriority = new int[QuestObjectCount];
        _resetTimer = 3;
        
        CreateLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (_reset)
        {
            ResetObjectActivation();
        }

        if (_activatedQuestObjectCount == 3)
        {
            ResetLevel();
        }
    }

    private void CreateLevel()
    {
        OrderGeneration();
        
        CreatePlayer();
        CreateQuestObjects();

        CameraSetup(_player.transform);

        _activatedQuestObjectCount = 0;
    }

    private void CreatePlayer()
    {
        _player = Instantiate(Prefabs.Player, new Vector3(0, 0, 0), Quaternion.identity);
        _player.SensX = CameraSens;
    }

    private void CreateQuestObjects()
    {
        for (int i = 0; i < QuestObjectCount; i++)
        {
            QuestObject questObject = Instantiate(Prefabs.QuestsObjects[_questObjectPriority[i]].Prefab, new Vector3(Random.Range(-21, 20), 0, Random.Range(-21, 20)), Quaternion.identity);
            questObject.Player = _player;
            questObject.Text = UiManager.Text[i];
            questObject.ActivationOrder = i;
            questObject.GameManager = this;
            _questObjects[i] = questObject;
        }
    }

    public void ResetObjectActivation()
    {
        foreach (QuestObject questObject in _questObjects)
        {
            questObject.ResetActivation();
        }

        _reset = false;
    }

    private void CameraSetup(Transform target)
    {
        Camera.Target = target;
        Camera.Sensitivity = CameraSens;
    }

    private void OrderGeneration()
    {
        List<int> tempList = new List<int>(){0, 1, 2};
        for (int i = 0; i < 3; i++)
        {
            int random = Random.Range(0, tempList.Count);
            _questObjectPriority[i] = tempList[random];
            tempList.RemoveAt(random);
        }

        ResetUi();
    }

    private void ResetUi()
    {
        UiManager.GameOverPanel.SetActive(false);
        
        for (int i = 0; i < QuestObjectCount; i++)
        {
            UiManager.Icons[i].sprite = Prefabs.QuestsObjects[_questObjectPriority[i]].Icon;
            UiManager.Text[i].text = string.Empty;
        }
    }

    private void ResetLevel()
    {
        if (!UiManager.GameOverPanel.activeSelf)
        {
            UiManager.GameOverPanel.SetActive(true);
        }
        
        if (_resetTimer > 0f)
        {
            _resetTimer -= Time.deltaTime;
            UiManager.Timer.text = "restart after " + Math.Round(_resetTimer, 0) + " sec";
        }
        else
        {
            _reset = false;
            _resetTimer = 3;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
