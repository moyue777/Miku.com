using UnityEngine;

public class BottomAddForce : MonoBehaviour
{
    [SerializeField]
    private float force;
    public bool isActive = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive && collision.gameObject.tag == "PhysicPerform")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, force),ForceMode2D.Impulse);
        }
    }
}