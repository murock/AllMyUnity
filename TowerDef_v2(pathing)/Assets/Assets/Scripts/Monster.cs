using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Stack<Node> path;

    public Point GridPosition { get; set; }

    private Vector3 destination;

    public bool IsActive { get; set; }

    private void Update()
    {
        Move();
    }

    public void Spawn()
    {
        transform.position = LevelManager.Instance.SpawnPortal.transform.position;  //get spawn portals positions

        StartCoroutine(Scale(new Vector3(1f, 1f), new Vector3(2.2f, 2.2f)));

        SetPath(LevelManager.Instance.Path);
    }

    public IEnumerator Scale(Vector3 from, Vector3 to)  //Monster changing size on spawn/despawn
    {
        IsActive = false;

        float progress = 0;

        while (progress <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);

            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;

        IsActive = true;
    }

    private void Move()
    {
        if (IsActive)   //allows time to grow animation
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);    //move from current pos -> destination at speed*deltaTime

            if (transform.position == destination)
            {
                if (path != null && path.Count > 0) //still tiles to travel
                {
                    GridPosition = path.Peek().GridPosition;
                    destination = path.Pop().WorldPosition;
                }
            }
        }

    }

    //sets intial path
    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            this.path = newPath;
            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
                
        }
    }
}
