using UnityEngine;

public class SquareController : QuestObject
{
    private const float ActivationDistance = 2.5f;
    private Vector3 _previousPosition;

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= ActivationDistance)
        {
            if (_previousPosition == Player.transform.position)
            {
                ActivateObject();
            }
            else
            {
                _previousPosition = Player.transform.position;
            }  
        }
    }
}
