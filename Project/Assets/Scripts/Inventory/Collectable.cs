using UnityEngine;
using System.Collections;

public enum CollectableType
{
    Shield,
    Impulse,
    Coin,
}

public class Collectable : MonoBehaviour {

    public SpriteRenderer collectableImg;
    public Sprite[] CollectableSprites;
    private CollectableType collectableType;
	bool isCollected = false;

	public void Show() {
		this.GetComponent<SpriteRenderer>().enabled = true;
		this.GetComponent<CircleCollider2D>().enabled = true;
		isCollected = false;
	}

	void Hide() {
		this.GetComponent<SpriteRenderer>().enabled = false;
		this.GetComponent<CircleCollider2D>().enabled = false;
	}


	void Collect() {

		isCollected = true;
		Hide();
        switch (collectableType)
        {
            case CollectableType.Shield:
                PlayerPrefs.SetInt("Shield", PlayerPrefs.GetInt("Shield") + 1);
                break;
            case CollectableType.Impulse:
                PlayerPrefs.SetInt("Impulse", PlayerPrefs.GetInt("Impulse") + 1);
                break;
            case CollectableType.Coin:
                GameManager.instance.ChangePlayerCoins(1);
                break;
            default:
                break;
        }
	}

    public void SetCollectableType(CollectableType ct)
    {
        collectableType = ct;
        switch (collectableType)
        {
            case CollectableType.Shield:
                collectableImg.sprite = CollectableSprites[0];
                break;
            case CollectableType.Impulse:
                collectableImg.sprite = CollectableSprites[1];
                break;
            case CollectableType.Coin:
                collectableImg.sprite = CollectableSprites[2];
                break;
            default:
                break;
        }
    }

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag == "Player") {
			Collect();
		}
	}	
}
