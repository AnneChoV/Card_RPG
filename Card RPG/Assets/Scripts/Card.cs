using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

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
        BLOCK,
        FIRE,
        POTION,
        OIL,
        DAGGERSTABI,
        DAGGERSTABII,
        DAGGERSTABIII,
        SWORDSLASHI,
        SWORDSLASHII,
        SWORDSLASHIII,
        MACESMASHI,
        MACESMASHII,
        MACESMASHIII,
        THROWI,
        THROWII,
        THROWIII,
        SHOOTI,
        SHOOTII,
        SHOOTIII,
        CATAPULTI,
        CATAPULTII,
        CATAPULTIII
    }


    //General variables
    public Sprite[] cardSpriteList;
    public ecardClass cardClass;
    public ecardElement cardElement;
    public ecardName cardName;       //In general we should just be able to use this to make a card. :)
    public Sprite cardSprite;

    public string description;

    public Text tCardName;
    public Image iCardImage;
    public Text tCardElement;
    public Image iElementImagePhysical;
    public Image iElementImageFire;
    public Image iElementImageRestore;
    public Image iElementImageCurse;
    public Image iRangeImageRanged;
    public Image iRangeImageMelee;
    public Text tCardDescription;
    public Text tCardTier;
    public Text tCardDamage;

    public CombatManager combatManager; //Used to pass click infomation back up to where it's used.

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

        tCardName.text = cardName.ToString();
        tCardDescription.text = newDescription;
        //tCardElement.text = newCardElement.ToString();
        tCardTier.text = newTier.ToString();
        //cardSprite = newCardSprite;
        tCardDamage.text = null;
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

        tCardName.text = cardName.ToString();
        tCardDescription.text = newDescription;
        tCardElement.text = newCardElement.ToString();
        tCardTier.text = newTier.ToString();
        //  cardSprite = newCardSprite;

        //  //spriteRenderer.sprite = cardSprite;
    }

    void CreateCard(int newTier, ecardClass newCardClass, ecardElement newCardElement, ecardName newCardName, Sprite newCardSprite, string newDescription, int newMinDamage, int newMaxDamage, bool newIsUseableShortRange, bool newIsUseableLongRange)
    {
        tier = newTier;
        cardClass = newCardClass;
        cardElement = newCardElement;
        cardSprite = newCardSprite;
        description = newDescription;
        minDamage = newMinDamage;     //For now we do specific damage anyway.
        maxDamage = newMaxDamage;
        IsUseableShortRange = newIsUseableShortRange;
        IsUseableLongRange = newIsUseableLongRange;
        cardName = newCardName;

        tCardName.text = newCardName.ToString();
        tCardDamage.text = newMinDamage.ToString();
        tCardDescription.text = newDescription;
        //tCardElement.text = newCardElement.ToString();
        tCardTier.text = newTier.ToString();
        cardSprite = newCardSprite;
        // spriteRenderer.sprite = cardSprite;

    }

    void CreateCard(int newTier, ecardClass newCardClass, ecardElement newCardElement, ecardName newCardName, Sprite newCardSprite, string newDescription, int newMinDamage, int newMaxDamage)
    {
        tier = newTier;
        cardClass = newCardClass;
        cardElement = newCardElement;
        cardSprite = newCardSprite;
        description = newDescription;
        minDamage = newMinDamage;     //For now we do specific damage anyway.
        maxDamage = newMaxDamage;
        cardName = newCardName;
        tCardName.text = cardName.ToString();
        tCardDamage.text = newMinDamage.ToString();
        tCardDescription.text = newDescription;
        //tCardElement.text = newCardElement.ToString();
        tCardTier.text = newTier.ToString();
        //    cardSprite = newCardSprite;
        //spriteRenderer.sprite = cardSprite;
    }

    public void CreateCard(ecardName newCardName)   //SRS BALANCE ISSUES.
    {
        if (newCardName == ecardName.BLOCK)
        {
            CreateCard(1, ecardClass.BLOCK, ecardElement.PHYSICAL, newCardName, cardSpriteList[0], "Blocks the next attack.", true, true);
        }
        else if(newCardName == ecardName.POTION)
        {
            CreateCard(1, ecardClass.HEAL, ecardElement.PHYSICAL, newCardName, cardSpriteList[1], "Heals 1.", 4, 4, true, true);
        }
        else if (newCardName == ecardName.FIRE)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.FIRE, newCardName, cardSpriteList[2], "1 Damage.", 1, 1, true, true);
        }
        else if(newCardName == ecardName.OIL)
        {
            CreateCard(1, ecardClass.NEXTTURNBOOST, ecardElement.PHYSICAL, newCardName, cardSpriteList[3], "Triples the damage of the next fire attack.", true, true);
        }
        else if (newCardName == ecardName.DAGGERSTABI)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[4], "1 Damage.\nHalf in BackLine.", 1, 1, true, false);
        }
        else if (newCardName == ecardName.DAGGERSTABII)
        {
            CreateCard(2, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[5], "2 Damage.\nHalf in BackLine..", 2, 2, true, false);
        }
        else if (newCardName == ecardName.DAGGERSTABIII)
        {
            CreateCard(3, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[6], "3 Damage\nHalf in BackLine.", 3, 3, true, false);
        }
        else if (newCardName == ecardName.SWORDSLASHI)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[7], "1-2 Damage.\nHalf in BackLine.", 1, 2, true, false);
        }
        else if (newCardName == ecardName.SWORDSLASHII)
        {
            CreateCard(2, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[8], "1-3 Damage.\nHalf in BackLine.", 1, 3, true, false);
        }
        else if (newCardName == ecardName.SWORDSLASHIII)
        {
            CreateCard(3, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[9], "2-4 Damage.\nHalf in BackLine.", 2, 4, true, false);
        }
        else if (newCardName == ecardName.MACESMASHI)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[10], "2-3 Damage.\nHalf in BackLine.", 2, 3, true, false);
        }
        else if (newCardName == ecardName.MACESMASHII)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[11], "2-4 Damage.\nHalf in BackLine.", 2, 4, true, false);
        }
        else if (newCardName == ecardName.MACESMASHIII)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[12], "3-5 Damage.\nHalf in BackLine.", 3, 5, true, false);
        }
        else if (newCardName == ecardName.THROWI)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[13], "1 Damage.\nHalf in FrontLine.", 1, 1, false, true);
        }
        else if (newCardName == ecardName.THROWII)
        {
            CreateCard(2, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[14], "2 Damage.\nHalf in FrontLine.", 2, 2, false, true);
        }
        else if (newCardName == ecardName.THROWIII)
        {
            CreateCard(3, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[15], "3 Damage.\nHalf in FrontLine.", 3, 3, false, true);
        }
        else if (newCardName == ecardName.SHOOTI)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[16], "1-2 Damage.\nHalf in FrontLine.", 1, 2, false, true);
        }
        else if (newCardName == ecardName.SHOOTII)
        {
            CreateCard(2, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[17], "1-3 Damage.\nHalf in FrontLine.", 1, 3, false, true);
        }
        else if (newCardName == ecardName.SHOOTIII)
        {
            CreateCard(3, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[18], "2-4 Damage.\nHalf in FrontLine.", 2, 4, false, true);
        }
        else if (newCardName == ecardName.CATAPULTI)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[19], "2-3 Damage.\nHalf in FrontLine.", 2, 3, false, true);
        }
        else if (newCardName == ecardName.CATAPULTII)
        {
            CreateCard(2, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[20], "2-4 Damage.\nHalf in FrontLine.", 2, 4, false, true);
        }
        else if (newCardName == ecardName.CATAPULTIII)
        {
            CreateCard(3, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[21], "3-5 Damage.\nHalf in FrontLine.", 3, 5, false, true);
        }
    }

    //These Three Functions Depend on EventTrigger in the cards.
    public void onCursorEnter()
    {
        combatManager.currentlySelectedCard = this;
    }

    public void onCursorClick()
    {
        combatManager.ProcessPlayerCardUsing();
    }

    public void onCursorExit()
    {
        combatManager.currentlySelectedCard = null;
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
