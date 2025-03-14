using UnityEngine;

public class Expression : MonoBehaviour
{
    private HealthControl healthControl;
    private Animator animator;
    private float currentHealth = 0;
    // Start is called before the first frame update
    void Start()
    {
        healthControl = gameObject.GetComponent<HealthControl>();
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("over50", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (healthControl != null)
        {
            updateExpression();
        }
    }
    public void Served()
    {
        animator.SetTrigger("Served");
    }
    private void updateExpression()
    {
        currentHealth = healthControl.getHealth();
        if (currentHealth < 50)
        {
            animator.SetBool("over50",false);
        }  
    }
}
