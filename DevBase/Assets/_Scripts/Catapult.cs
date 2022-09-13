using UnityEngine;
using DG.Tweening;

public class Catapult : MonoBehaviour
{
    [SerializeField] string Tag;
    [SerializeField] PoolInfoWithPool projectile;
    [SerializeField] Vector2 RandomAngularSpeedRange;// => GameManager.Ins.gameSettingsSC.randomAngularSpeedRange;
    [SerializeField] float forwardDistance = 10f;
    [SerializeField] float jumpPower = 50f;
    [SerializeField] float jumpDuration = 1f;
    [SerializeField] float radiusMultiplier = 1f;
    [SerializeField] Transform targetLocation;
    
    Rigidbody tempRb;
    GameObject throwThis;
    Vector3 tempRandomVector3;
    
    
    public float ForwardDistance { get => forwardDistance; }
    public float JumpPower { get => jumpPower; }
    public float JumpDuration { get => jumpDuration; }

    public void Throw(GameObject go)
    {
        if (go != null)
        {
            throwThis = projectile.Fetch();


            SetProperties(throwThis);
        }
    }

    public void Throw()
    {
        if (projectile != null)
        {
            throwThis = projectile.Fetch();

            SetProperties(throwThis);
        }
    }

    

    private void SetProperties(GameObject go)
    {
        go.transform.position = transform.position;

        //tempRb = go.GetComponent<Rigidbody>();
        //tempRb.angularVelocity = RandomVector3();

        go.SetActive(true);

        /*Vector3 vec = Random.insideUnitCircle;
        vec.z = -Mathf.Abs(vec.z);

        tempRb.AddForce(vec * 50 +
           transform.root.up  * 20, ForceMode.Impulse);*/

        if (targetLocation == null)
        {
            tempVec3 = RandomLocation(go);
            go.transform.DOJump(tempVec3, JumpPower, 1, JumpDuration)
                .OnComplete(() => OnDestinationReached(go));
        }
        else
            go.transform.DOJump(targetLocation.position + RandomUnitCircle(), JumpPower, 1, JumpDuration)
                .OnComplete(() => OnDestinationReached(go));

        
        //go.transform.DOJump(transform.position + (Vector3.forward * forwardDistance), jumpPower, 1, jumpDuration);
    }
     Vector3 tempVec3;
    private static Vector3 RandomLocation(GameObject go)
    {
        return new Vector3(-4f + Random.Range(0f, 8f), go.transform.position.y,
                                go.transform.position.z + 13f + Random.Range(0f, 3f));
    }

    Vector2 tempVec2;
    private Vector3 RandomUnitCircle()
    {
        tempVec2 = Random.insideUnitCircle;
        tempVec3 = Vector3.zero;
        tempVec3.x = tempVec2.x;
        tempVec3.z = tempVec2.y;
        
        return tempVec3 * radiusMultiplier;
    }

    private void OnDestinationReached(GameObject go)
    {
        go.GetComponent<CurrencyCollectable>().IsAvailable = true;
    }

    private Vector3 RandomVector3()
    {
        tempRandomVector3.x = RandomFloat();
        tempRandomVector3.y = RandomFloat();
        tempRandomVector3.z = RandomFloat();

        return tempRandomVector3; 
    }

    private float RandomFloat()
    {
        return Random.Range(RandomAngularSpeedRange.x, RandomAngularSpeedRange.y);
    }
}
