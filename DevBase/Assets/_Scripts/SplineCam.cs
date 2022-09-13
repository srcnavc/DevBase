using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;

public class SplineCam : MonoBehaviour
{
    public static SplineCam ins;
    public GameObject ToFollow;
    [Range(0f, 0.1f)] [SerializeField] double camOffset;
    [Range(0f, 10f)] [SerializeField] float positionFollowSpeed;
    [Range(0f, 10f)] [SerializeField] float rotationFollowSpeed;
    [SerializeField] SplineComputer computer;
    [SerializeField] bool useSimpleFollow;
    [SerializeField] bool syncRotationWithSpline;
    [SerializeField] bool syncRotation;
    
    [SerializeField] float RotationResetDuration;
    

    SplineFollower playerFollower;
    SplineFollower myFollower;
    SplineCamConfiguration config;
    double tempDouble;

    public float PositionFollowSpeed { get => positionFollowSpeed; set => positionFollowSpeed = value; }

    private void Awake()
    {
        if (ToFollow != null)
            transform.SetPositionAndRotation(ToFollow.transform.position, ToFollow.transform.rotation);
        else
            Debug.Log("Spline Cam : ToFollow is null.");

        if (ins == null)
            ins = this;
    }

    private void Start()
    {
        if (ToFollow != null)
        {
            playerFollower = ToFollow.GetComponent<SplineFollower>();
            config = ToFollow.GetComponent<SplineCamConfiguration>();
        }

        myFollower = GetComponent<SplineFollower>();

    }

    public void JumpToFollowPosition()
    {
        transform.position = ToFollow.transform.position;
    }

    public void SetToFollow(Transform follow)
    {
        if (ToFollow == null)
            return;
        ToFollow = follow.gameObject;
        config = ToFollow.GetComponent<SplineCamConfiguration>();
    }

    void SplineLerpPosition()
    {
        if((config == null && !useSimpleFollow) || (config != null && !config.UseSimpleFollow))
            myFollower.SetPercent(Mathf.Lerp((float)myFollower.result.percent, (float)playerFollower.result.percent, PositionFollowSpeed * Time.deltaTime));
        else
            SimpleFollow();
    }

    void SplineLerpRotation()
    {
        if (playerFollower.result.percent + camOffset > 1)
        {
            
            tempDouble = 1 - (playerFollower.result.percent + camOffset);
            if (tempDouble <= camOffset)
                camOffset -= tempDouble;
            else
                camOffset = 0;
        }

        transform.forward = computer.Evaluate(playerFollower.result.percent + camOffset).forward;
    }

    private void SimpleFollow()
    {
        if (myFollower!= null && myFollower.follow)
            myFollower.follow = false;

        transform.position = Vector3.Lerp(transform.position, ToFollow.transform.position, PositionFollowSpeed * Time.deltaTime);
    }

    private void SimpleLerpRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, ToFollow.transform.rotation, rotationFollowSpeed * Time.deltaTime);
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (ToFollow != null )
        {
            if (playerFollower != null)
                SplineLerpPosition();
            else
                SimpleFollow();

            if ((config == null && syncRotationWithSpline) || (config != null && config.SyncRotationWithSpline))
                SplineLerpRotation();
            else if ((config == null && syncRotation) || (config != null && config.SyncRotation))
                SimpleLerpRotation();
            
        }
    }

    public void ResetView()
    {
        transform.DORotate(Vector3.zero, RotationResetDuration);
    }

    private void OnValidate()
    {
        if (syncRotationWithSpline)
        {
            if (!syncRotation)
                syncRotation = true;
        }

    }
}


