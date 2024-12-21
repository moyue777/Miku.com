using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Ingredient : MonoBehaviour
{
    public string ingredientName; // 配料名称
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isDragging = false;
    private bool canDrag = true;
    private Vector2 offset; // 鼠标点击位置与初始位置的偏移量
    public Vector2 defaultPosition; // 初始位置
    
    public AudioClip dragSound;
    private AudioSource audioSource; 
    void Start()
    {
        defaultPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // 处理左键按下，拖动
        if (Input.GetMouseButtonDown(0) && canDrag)
        {
            
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = (Vector2)transform.position - mousePosition; // 计算偏移量
                
            }
        }

        // 处理左键释放
        if (Input.GetMouseButtonUp(0) && isDragging && canDrag)
        {
            Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
            foreach (var collider in colliders)
            {
                if (collider.GetComponentInParent<Builder>() != null) // 顾客的标签是"Customer"
                {
                    Builder target = collider.GetComponentInParent<Builder>();
                    if (target != null )
                    {
                        target.AddIngredient(ingredientName);
                        Debug.Log("added" + ingredientName);
                        break;
                    }
                }
            }

            transform.position = defaultPosition;
            isDragging = false;
        }

        // 如果正在拖动，更新位置
        if (isDragging && canDrag)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition + offset;
        }
    }
    
    
    void OnMouseDown()
    {
        PlayDragSound();
    }
    

    private void PlayDragSound()
    {
        if (dragSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(dragSound);
        }
    }
}






