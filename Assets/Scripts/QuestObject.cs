using UnityEngine;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour
{
    public GameManager GameManager;

    private Transform _player;
    public Transform Player
    {
        get { return _player; }
        set { _player = value; }
    }
    
    private Text _text;
    public Text Text
    {
        set { _text = value; }
    }
    
    private GameObject _activationEffect;
    public GameObject ActivationEffect
    {
        set { _activationEffect = value; }
    }

    private int _activationOrder;
    public int ActivationOrder
    {
        set { _activationOrder = value; }
    }

    public void ActivateObject()
    {
        gameObject.SetActive(false);

        if (_activationOrder == GameManager.ActivatedQuestObjectCount)
        {
            _activationEffect = Instantiate(GameManager.Prefabs.Success, transform.position, Quaternion.identity);

            _text.color = Color.green;
            _text.text = "SUCCESS";
            
            GameManager.ActivatedQuestObjectCount++;
        }
        else
        {
            _activationEffect = Instantiate(GameManager.Prefabs.Fail, transform.position, Quaternion.identity);
            
            _text.color = Color.red;
            _text.text = "FAIL";
            
            Invoke("StartReset", 3f);
        }
    }

    public void ResetActivation()
    {
        gameObject.SetActive(true);
        Destroy(_activationEffect);
        _text.text = string.Empty;
    }

    private void StartReset()
    {
        GameManager.Reset = true;
    }
}
