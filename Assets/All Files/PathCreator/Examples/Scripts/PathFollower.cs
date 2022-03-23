using UnityEngine;
using PathCreation;


// Moves along a path at constant speed.
// Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
public class PathFollower : Singleton<PathFollower>
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;

    public bool isMoving = false;

    void Start()
    {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
        else
        {
            //Debug.Log("yol bulamadım");
            //pathCreator = GameObject.FindGameObjectWithTag("Path").GetComponent<PathCreator>();
        }
    }

    void Update()
    {
        if (isMoving)
        {
            FollowThePath();
        }
    }

    void FollowThePath()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }
    void StartBooleans()
    {
        isMoving = true;
        pathCreator = GameObject.FindGameObjectWithTag("Path").GetComponent<PathCreator>();
    }

    void EndBooleans()
    {
        isMoving = false;
    }
    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += StartBooleans;
        GameManager.OnGameWin += EndBooleans;
        GameManager.OnGameLose += EndBooleans;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= StartBooleans;
        GameManager.OnGameWin -= EndBooleans;
        GameManager.OnGameLose -= EndBooleans;
    }

}

