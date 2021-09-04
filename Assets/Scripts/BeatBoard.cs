using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBoard : MonoBehaviour
{
    public GameObject Panel;
    public Beat BeatPrefab;

    public Transform StartPos;
    public Transform TargetPos;


    private List<Beat> _beats = new List<Beat>();

    void Start()
    {

    }

    void FixedUpdate()
    {
        float deltaTime = Time.fixedDeltaTime;

        //print(StartPos.position);
        //print(TargetPos.position);

        foreach (Beat beat in _beats)
        {

        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            SpawnBeat();
        }
    }

    void SpawnBeat()
    {
        Beat beat = Instantiate(BeatPrefab, Panel.transform);
        beat.StartMove(StartPos.position, TargetPos.position, 3);
        _beats.Add(beat);
    }
}
