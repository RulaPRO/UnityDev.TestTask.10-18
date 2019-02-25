using UnityEngine;

public class SquareController : QuestObject
{
    private Vector3 _previousPosition;

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= 3f)
        {
            ActivateObject();
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        Debug.Log("CollisionStay");
        if (_previousPosition == other.transform.position)
        {
            ActivateObject();
        }
        else
        {
            SavePosition(other);
        }
            
    }

    private void SavePosition(Collision obj)
    {
        _previousPosition = obj.transform.position;
    }
}
