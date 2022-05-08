using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientObject : MonoBehaviour
{
    [System.NonSerialized] public Vector3 originalScale;

    public string ingredient;

    public Sprite preparedSprite;
    public bool isPrepared = false;

    public Sprite cookedSprite;
    public bool isCooked = false;
    public float cookTime = 0.0f;
    public Vector3 cookSpriteScale;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
