using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    //              VARIABLES:

        //Enums
    public enum ecardClass
    {
        NONE,
        DAMAGE, //normal attack and fire for now - fire long attack short
        HEAL,
        BLOCK,
        NEXTTURNBOOST
    }

    public enum ecardElement
    {
        NONE,
        PHYSICAL,
        FIRE
    }

    public enum ecardName
    {
        NONE,
        ATTACKCARD,
        BLOCKCARD,
        FIRECARD,
        HEALCARD,
        OILCARD
    }

    //not sure how to do the image. Anne Senpai?



;

    //General variables
    public Sprite[] cardSpriteList;
    public ecardClass cardClass;
    public ecardElement cardElement;
    public ecardName cardName;       //In general we should just be able to use this to make a card. :)
    public Sprite cardSprite;
    public SpriteRenderer spriteRenderer;
    public string description;

    //public CombatManager combatManager; //Used to pass click infomation back up to where it's used.

    //Combat Stats
    public int tier;
    public int maxDamage;  
    public int minDamage;
    public bool IsUseableShortRange;
    public bool IsUseableLongRange;


    //          FUNCTIONS
    private void Start()
    {
        //spriteRenderer.sprite = cardSprite;
    }

    //Card creators - In general we will just use the name to create it.
    void CreateCard(int newTier, ecardClass newCardClass, ecardElement newCardElement, ecardName newCardName, Sprite newCardSprite, string newDescription, bool newIsUseableShortRange, bool newIsUseableLongRange)
    {
        tier = newTier;
        cardClass = newCardClass;
        cardElement = newCardElement;
        cardSprite = newCardSprite;
        description = newDescription;
        minDamage = maxDamage = 0;
        IsUseableShortRange = newIsUseableShortRange;
        IsUseableLongRange = newIsUseableLongRange;
        cardName = newCardName;


        //spriteRenderer.sprite = cardSprite;
    }
    void CreateCard(int newTier, ecardClass newCardClass, ecardElement newCardElement, ecardName newCardName, Sprite newCardSprite, string newDescription)
    {
        tier = newTier;
        cardClass = newCardClass;
        cardElement = newCardElement;
        cardSprite = newCardSprite;
        description = newDescription;
        minDamage = maxDamage = 0;
        cardName = newCardName;


        //spriteRenderer.sprite = cardSprite;
    }

    void CreateCard(int newTier, ecardClass newCardClass, ecardElement newCardElement, ecardName newCardName, Sprite newCardSprite, string newDescription, int damage, bool newIsUseableShortRange, bool newIsUseableLongRange)
    {
        tier = newTier;
        cardClass = newCardClass;
        cardElement = newCardElement;
        cardSprite = newCardSprite;
        description = newDescription;
        minDamage = damage;     //For now we do specific damage anyway.
        maxDamage = damage;
        IsUseableShortRange = newIsUseableShortRange;
        IsUseableLongRange = newIsUseableLongRange;
        cardName = newCardName;

       // spriteRenderer.sprite = cardSprite;

    }

    void CreateCard(int newTier, ecardClass newCardClass, ecardElement newCardElement, ecardName newCardName, Sprite newCardSprite, string newDescription, int damage)
    {
        tier = newTier;
        cardClass = newCardClass;
        cardElement = newCardElement;
        cardSprite = newCardSprite;
        description = newDescription;
        minDamage = damage;     //For now we do specific damage anyway.
        maxDamage = damage;
        cardName = newCardName;

        //spriteRenderer.sprite = cardSprite;
    }

    public void CreateCard(ecardName newCardName)
    {
        if (newCardName == ecardName.ATTACKCARD)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[0], "Somewhere a museum is probably looking for this.", 5, true, false);  
        }
        else if (newCardName == ecardName.BLOCKCARD)
        {
            CreateCard(1, ecardClass.BLOCK, ecardElement.PHYSICAL, newCardName, cardSpriteList[1], "Staying safe is a good plan of action.", true, true);
        }
        else if(newCardName == ecardName.HEALCARD)
        {
            CreateCard(1, ecardClass.HEAL, ecardElement.PHYSICAL, newCardName, cardSpriteList[2], "A heal a day keeps the doctor away.", 5, true, true);
        }
        else if (newCardName == ecardName.FIRECARD)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.FIRE, newCardName, cardSpriteList[3], "Did someone remember to pack the marshmellows?", 6, false, true);
        }
        else if(newCardName == ecardName.OILCARD)
        {
            CreateCard(1, ecardClass.NEXTTURNBOOST, ecardElement.PHYSICAL, newCardName, cardSpriteList[4], "Weakness to Fire for one Turn.\nWholesome vitamins to keep you healthy. Stay away from fire.", true, true);
        }
    }


    //GETTERS AND SETTERS
    ecardClass GetCardClass()
    {
        return cardClass;
    }

    void SetCardClass(ecardClass newCardClass)
    {
        cardClass = newCardClass;
    }

    ecardElement GetCardElement()
    {
        return cardElement;
    }

    void SetCardElement(ecardElement newCardElement)
    {
        cardElement = newCardElement;
    }
    int GetTier()
    {
        return tier;
    }

    void SetTier(int newTier)
    {
        tier = newTier;
    }

    string GetDescription()
    {
        return description;
    }

    void SetDescription(string newDescription)
    {
        description = newDescription;
    }

    int GetMinDamage()
    {
        return minDamage;
    }

    void SetMinDamage(int newMinDamage) //For now it changes both.
    {
        minDamage = newMinDamage;
        maxDamage = newMinDamage;
    }

    int GetMaxDamage()
    {
        return maxDamage;
    }

    void SetMaxDamage(int newMaxDamage)
    {
        minDamage = newMaxDamage;
        maxDamage = newMaxDamage;
    }
}
