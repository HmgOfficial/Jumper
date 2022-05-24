using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum PlatformType
{
    Basic,
    Cloud,
    Wood,
    Grass,
    Jump,
    Glass
}

[System.Serializable]
public class PlatformData
{
    [Header("General Info")]
    public string PlatformName;
    public PlatformType PlatformType;
    public Sprite PlatformSprite;
    [Header("Stats")]
    public float PlatformMovementSpeed;
    public float PlatformJumpForce;
    public int playerMaxCollisions = 99;
    [Header("BC Size")]
    public int X;
    public int Y;
    public int OffsetY;
}

public class Platform : MonoBehaviour
{
    private BoxCollider2D platformBC;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collectable collectable;

    public PlatformType platformType = PlatformType.Basic;

    public Vector3 nextPlatformPos
    {
        get
        {
            return nextPlatformPos;
        }
        set
        {
            nextPlatformPos += Vector3.up * 4;
        }
    }

    public bool move;
    public float movementSpeed;
    public float jumpForce;

    public int playerNCollisions;
    public int playerMaxCollisions = 99;

    private CollectableType collectableType;

    private List<RaycastResult> results = new List<RaycastResult>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Get the components
        rb = GetComponent<Rigidbody2D>();
        platformBC = GetComponentInChildren<BoxCollider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        collectable = GetComponentInChildren<Collectable>();

        // Init variables
        nextPlatformPos = (transform.localPosition + Vector3.up * 4);

        // The 20% of the platforms have a coin
        if (HaveItem())
        {
            GetComponentInChildren<Collectable>().Show();
        }
        // The platform moves? ----------------------------------------------------
        if (MovePlatform())
        {
            rb.velocity = new Vector2(movementSpeed, 0);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckPlayer();
        /*if ((Player.Instance.transform.position - transform.position).magnitude > 0)
        {
            platformBC.isTrigger = false;
        }
        else
        {
            platformBC.isTrigger = true;
        }*/
    }

    public void CheckPlayer()
    {

        if (Player.Instance.transform.position.y >= transform.position.y)
        {
            platformBC.isTrigger = false;
        }
        /*else if (Player.Instance.transform.position.y <= transform.position.y - 0.1f)
        {
            platformBC.isTrigger = true;
        }*/
        else
        {
            platformBC.isTrigger = true;
        }
    }

    public void CheckWalls()
    {
        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(transform.position, Vector2.right, Mathf.Infinity);
        for (int i = 0; i < hit.Length; i++)
        {
            print(hit[i].collider.name);
            //ChangeDir();
        }
        //if ((hit.collider != null && hit.collider.tag.Equals("Wall")) || (hit2.collider != null && hit2.collider.tag.Equals("Wall")))
        //{
        //    ChangeDir();
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            Player.Instance.Jump(jumpForce);
            playerNCollisions++;
            //if (Foo())
            //{
            //    RemoveThisPlatform();
            //}
        }
    }

    public void ChangeDir()
    {
        rb.velocity *= -1;
    }

    /// <summary>
    /// Some platforms have a special condition to being destroyed
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool RemoveThisPlatform(PlatformType type)
    {
        if (type == PlatformType.Cloud)
            return false;
        else if (type == PlatformType.Glass)
        {
            int a = Random.Range(0, 2);
            if (a == 0)
                return true;
            else
                return false;
        }
        else if (type == PlatformType.Wood && playerNCollisions >= playerMaxCollisions)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Returns if the platform has a collectable or not
    /// </summary>
    /// <returns></returns>
    bool HaveItem()
    {
        int a = Random.Range(0, 10);
        if (a <= 1)
        {
            SetCollectableType();
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Asign a random collectable
    /// </summary>
    private void SetCollectableType()
    {
        int a = Random.Range(0, 100);
        if (a < 33)
            collectableType = CollectableType.Shield;
        else if (a < 66)
            collectableType = CollectableType.Impulse;
        else
            collectableType = CollectableType.Coin;
    }

    /// <summary>
    /// Return if the platform has movement or not
    /// </summary>
    /// <returns></returns>
    bool MovePlatform()
    {
        return false;
    }

    /// <summary>
    /// Removes the cloud platform
    /// </summary>
    /// <returns></returns>
    IEnumerator CloudPlatformRemoveCo()
    {
        if (sr != null)
        {
            platformBC.enabled = false;
            while (sr.color.a > 0f)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (float)sr.color.a - Time.deltaTime * 2);
                yield return 0;
            }
            PlatformSpawner.Instance.RemovePlatform(this);
        }
        else
            Debug.LogWarning("The platform doesn't have the component SpriteRenderer");
    }
}

