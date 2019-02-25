using UnityEngine;

public class JarController : QuestObject
{
    private void OnCollisionStay(Collision other)
    {
        if (Player.Attack && other.gameObject.name.Equals("Sword"))
        {
            ActivateObject();
        }
    }
}
