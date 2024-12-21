using UnityEngine;

public class TeaWaitingProcess : WaitingProcess
{
    private Animator animator;
    public string ingredient;
    public bool isWaiting;
    private float cup_diff = 1.8f;
    new void Start()
    {
        base.Start();
        cupPosition.Add(new Vector2(transform.position.x, transform.position.y - cup_diff));
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("isPouring",false);
    }
    public string Ingredient()
    {  
        return ingredient;
    }
    public void BuilderStartWaiting()
    {
        isWaiting = true;
        if (animator != null)
        {
            animator.SetBool("isPouring",true);
        }
    }
    public void BuilderEndWaiting()
    {
        isWaiting = false;
        hadCup[0] = false;
        if (animator != null)
        {
            animator.SetBool("isPouring",false); 
        }
    }
}