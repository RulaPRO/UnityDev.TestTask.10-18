using UnityEngine;

public class JarController : QuestObject
{
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= 3f)
        {
            ActivateObject();
        }
    }

    private void OnCollisionEnter()
    {
        ActivateObject();
    }
}
