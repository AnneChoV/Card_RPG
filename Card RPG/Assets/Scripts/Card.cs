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
        FIRE,
        CURSE, 
        RESTORATION
    }

    public enum ecardName
    {
        NONE,
        BLOCK,
        FIRE,
        POTION,
        OIL,
        ICE_PICK,
        KITCHEN_KNIFE,
        SCIMITAR,
        SHORT_SWORD,
        VORPAL_BLADE,
        EXCALIBUR,
        BIG_ASS_SWORD,
        GREATSWORD_OF_IVAN,
        DOOMFIST,
        STONE,
        METAL_DONKEY,
        COW_TREBUCHET,
        SLINGSHOT,
        LONGBOW,
        WRATH_OF_PAT,
        NO_MANS_GUN,
        RAILHAMMER,
        ARMAGEDDON
    }

    //General variables
    public ParticleSystem clickParticle;

    public Sprite[] cardSpriteList;
    public ecardClass cardClass;
    public ecardElement cardElement;
    public ecardName cardName;       //In general we should just be able to use this to make a card. :)
    public Image iElementImagePhysicalPrefab;
    public Image iElementImageFirePrefab;
    public Image iElementImageRestorePrefab;
    public Image iElementImageCursePrefab;
    public Image iRangeImageRangedPrefab;
    public Image iRangeImageMeleePrefab;
    public Image iRangeImageMeleeAndRangedPrefab;
    public Image iOilImageHackyPrefab;


    public Sprite cardSprite;
    public Image iElementImage;
    public Image iRangeImage;

    public string description;

    public Text tCardName;
    public Image iCardImage;
    public Image iCardImageElement;
    public Image iCardImageRange;

    public Text tCardDescription;
    public Text tCardTier;
    public Text tCardDamage;

    public CardColouring cardColouring;

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
        tCardTier.text = null;
        if (newCardElement == ecardElement.PHYSICAL)
        {
            iElementImage.sprite = iElementImagePhysicalPrefab.sprite;
            cardColouring.TypeID = 3;
        }
        else if (newCardElement == ecardElement.FIRE)
        {
            iElementImage.sprite = iElementImageFirePrefab.sprite;
            cardColouring.TypeID = 1;
        }
        else if (newCardElement == ecardElement.CURSE)
        {
            iElementImage.sprite = iElementImageCursePrefab.sprite;
            cardColouring.TypeID = 0;
        }
        else if (newCardElement == ecardElement.RESTORATION)
        {
            iElementImage.sprite = iElementImageRestorePrefab.sprite;
            cardColouring.TypeID = 2;
        }
        for (int i = 0; i < newTier; i++)
        {
            tCardTier.text = tCardTier.text + "I";
        }
        if (IsUseableLongRange == true && IsUseableShortRange == true)
        {
            //FREAKOUTCAUSE WE DONT HAVE A CARD FOR THIS
            iRangeImage.sprite = iRangeImageMeleePrefab.sprite;
        }
        else if (IsUseableLongRange == false && IsUseableShortRange == true)
        {
            iRangeImage.sprite = iRangeImageMeleePrefab.sprite;
        }
        else if (IsUseableLongRange == true && IsUseableShortRange == false)
        {
            iRangeImage.sprite = iRangeImageRangedPrefab.sprite;
        }

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
        tCardTier.text = null;
        if (newCardElement == ecardElement.PHYSICAL)
        {
            iElementImage.sprite = iElementImagePhysicalPrefab.sprite;
            cardColouring.TypeID = 3;
        }
        else if (newCardElement == ecardElement.FIRE)
        {
            iElementImage.sprite = iElementImageFirePrefab.sprite;
            cardColouring.TypeID = 1;
        }
        else if (newCardElement == ecardElement.CURSE)
        {
            iElementImage.sprite = iElementImageCursePrefab.sprite;
            cardColouring.TypeID = 0;
        }
        else if (newCardElement == ecardElement.RESTORATION)
        {
            iElementImage.sprite = iElementImageRestorePrefab.sprite;
            cardColouring.TypeID = 2;
        }
        for (int i = 0; i < newTier; i++)
        {
            tCardTier.text = tCardTier.text + "I";
        }
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
        cardSprite = newCardSprite;
        if (newCardElement == ecardElement.PHYSICAL)
        {
            iElementImage.sprite = iElementImagePhysicalPrefab.sprite;
            cardColouring.TypeID = 3;
        }
        else if (newCardElement == ecardElement.FIRE)
        {
            iElementImage.sprite = iElementImageFirePrefab.sprite;
            cardColouring.TypeID = 1;
        }
        else if (newCardElement == ecardElement.CURSE)
        {
            iElementImage.sprite = iElementImageCursePrefab.sprite;
            cardColouring.TypeID = 0;
        }
        else if (newCardElement == ecardElement.RESTORATION)
        {
            iElementImage.sprite = iElementImageRestorePrefab.sprite;
            cardColouring.TypeID = 2;
        }

        if (IsUseableLongRange == true && IsUseableShortRange == true)
        {
            //FREAKOUTCAUSE WE DONT HAVE A CARD FOR THIS
            if (cardName == ecardName.OIL)
            {
                iRangeImage.sprite = iOilImageHackyPrefab.sprite;
            }
            else
            {
                iRangeImage.sprite = iRangeImageMeleeAndRangedPrefab.sprite;
            }
        }
        else if (IsUseableLongRange == false && IsUseableShortRange == true)
        {
            iRangeImage.sprite = iRangeImageMeleePrefab.sprite;
        }
        else if (IsUseableLongRange == true && IsUseableShortRange == false)
        {
            iRangeImage.sprite = iRangeImageRangedPrefab.sprite;
        }

        tCardTier.text = null;
        for (int i = 0; i < newTier; i++)
        {
            tCardTier.text = tCardTier.text + "I";
        }
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
        tCardTier.text = null;
        if (newCardElement == ecardElement.PHYSICAL)
        {
            iElementImage.sprite = iElementImagePhysicalPrefab.sprite;
            cardColouring.TypeID = 3;
        }
        else if (newCardElement == ecardElement.FIRE)
        {
            iElementImage.sprite = iElementImageFirePrefab.sprite;
            cardColouring.TypeID = 1;
        }
        else if (newCardElement == ecardElement.CURSE)
        {
            iElementImage.sprite = iElementImageCursePrefab.sprite;
            cardColouring.TypeID = 0;
        }
        else if (newCardElement == ecardElement.RESTORATION)
        {
            iElementImage.sprite = iElementImageRestorePrefab.sprite;
            cardColouring.TypeID = 2;
        }

        for (int i = 0; i < newTier; i++)
        {
            tCardTier.text = tCardTier.text + "I";
        }
        //    cardSprite = newCardSprite;
        //spriteRenderer.sprite = cardSprite;
    }

    public void CreateCard(ecardName newCardName)   //SRS BALANCE ISSUES.
    {
        if (newCardName == ecardName.BLOCK)
        {
            CreateCard(1, ecardClass.BLOCK, ecardElement.CURSE, newCardName, cardSpriteList[0], "Blocks the next attack.", true, true);
        }
        else if(newCardName == ecardName.POTION)
        {
            CreateCard(1, ecardClass.HEAL, ecardElement.RESTORATION, newCardName, cardSpriteList[1], "Heals 1.", 4, 4, true, true);
        }
        else if (newCardName == ecardName.FIRE)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.FIRE, newCardName, cardSpriteList[2], "Ignites Oil", 1, 1, true, true);
        }
        else if(newCardName == ecardName.OIL)
        {
            CreateCard(1, ecardClass.NEXTTURNBOOST, ecardElement.CURSE, newCardName, cardSpriteList[3], "Triples the damage of the next fire attack.", true, true);
        }
        else if (newCardName == ecardName.ICE_PICK)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[4], "Accurate.\nHalf in BackLine.", 1, 1, true, false);
        }
        else if (newCardName == ecardName.KITCHEN_KNIFE)
        {
            CreateCard(2, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[5], "Accurate.\nHalf in BackLine..", 2, 2, true, false);
        }
        else if (newCardName == ecardName.SCIMITAR)
        {
            CreateCard(3, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[6], "Accurate\nHalf in BackLine.", 3, 3, true, false);
        }
        else if (newCardName == ecardName.SHORT_SWORD)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[7], "Innaccurate.\nHalf in BackLine.", 1, 2, true, false);
        }
        else if (newCardName == ecardName.VORPAL_BLADE)
        {
            CreateCard(2, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[8], "Highly Innaccurate.\nHalf in BackLine.", 1, 3, true, false);
        }
        else if (newCardName == ecardName.EXCALIBUR)
        {
            CreateCard(3, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[9], "Highly Innaccurate.\nHalf in BackLine.", 2, 4, true, false);
        }
        else if (newCardName == ecardName.BIG_ASS_SWORD)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[10], "Innaccurate.\nHalf in BackLine.", 2, 3, true, false);
        }
        else if (newCardName == ecardName.GREATSWORD_OF_IVAN)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[11], "Highly Innaccurate.\nHalf in BackLine.", 2, 4, true, false);
        }
        else if (newCardName == ecardName.DOOMFIST)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[12], "Highly Innaccurate.\nHalf in BackLine.", 3, 5, true, false);
        }
        else if (newCardName == ecardName.STONE)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[13], "Accurate.\nHalf in FrontLine.", 1, 1, false, true);
        }
        else if (newCardName == ecardName.METAL_DONKEY)
        {
            CreateCard(2, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[14], "Accurate.\nHalf in FrontLine.", 2, 2, false, true);
        }
        else if (newCardName == ecardName.COW_TREBUCHET)
        {
            CreateCard(3, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[15], "Accurate.\nHalf in FrontLine.", 3, 3, false, true);
        }
        else if (newCardName == ecardName.SLINGSHOT)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[16], "Innaccurate.\nHalf in FrontLine.", 1, 2, false, true);
        }
        else if (newCardName == ecardName.LONGBOW)
        {
            CreateCard(2, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[17], "Highly Innaccurate.\nHalf in FrontLine.", 1, 3, false, true);
        }
        else if (newCardName == ecardName.WRATH_OF_PAT)
        {
            CreateCard(3, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[18], "Highly Innaccurate.\nHalf in FrontLine.", 2, 4, false, true);
        }
        else if (newCardName == ecardName.NO_MANS_GUN)
        {
            CreateCard(1, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[19], "Innaccurate.\nHalf in FrontLine.", 2, 3, false, true);
        }
        else if (newCardName == ecardName.RAILHAMMER)
        {
            CreateCard(2, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[20], "Highly Innaccurate.\nHalf in FrontLine.", 2, 4, false, true);
        }
        else if (newCardName == ecardName.ARMAGEDDON)
        {
            CreateCard(3, ecardClass.DAMAGE, ecardElement.PHYSICAL, newCardName, cardSpriteList[21], "Highly Innaccurate.\nHalf in FrontLine.", 3, 5, false, true);
        }
    }

    //These Three Functions Depend on EventTrigger in the cards.
    public void onCursorEnter()
    {
        combatManager.currentlySelectedCard = this;
    }

    public void onCursorClick()
    {

        combatManager.ProcessPlayerCardUsing(clickParticle);
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
