using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrail : MonoBehaviour
{
    public GameObject tip;
    public GameObject start;
    public GameObject trail;

    public int trailFrameLength;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private int frameCount;
    private Vector3 prevFrameTipPos;
    private Vector3 prevFrameBasePos;

    private const int numVertices = 12;

    void Start()
    {
        mesh = new Mesh();
        trail.GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[trailFrameLength * numVertices];
        triangles = new int[vertices.Length];

        prevFrameTipPos = tip.transform.position;
        prevFrameBasePos = tip.transform.position;
    }

    void Update()
    {
        if(frameCount == (trailFrameLength * numVertices)) 
        {
            frameCount = 0;
        }

        vertices[frameCount] = start.transform.position;
        vertices[frameCount + 1] = tip.transform.position;
        vertices[frameCount + 2] = prevFrameTipPos;

        vertices[frameCount + 3] = start.transform.position;
        vertices[frameCount + 4] = prevFrameTipPos;
        vertices[frameCount + 5] = tip.transform.position;

        vertices[frameCount + 6] = prevFrameTipPos;
        vertices[frameCount + 7] = start.transform.position;
        vertices[frameCount + 8] = prevFrameBasePos;

        vertices[frameCount + 9] = prevFrameTipPos;
        vertices[frameCount + 10] = prevFrameBasePos;
        vertices[frameCount + 11] = start.transform.position;

        triangles[frameCount] = frameCount;
        triangles[frameCount + 1] = frameCount + 1;
        triangles[frameCount + 2] = frameCount + 2;
        triangles[frameCount + 3] = frameCount + 3;
        triangles[frameCount + 4] = frameCount + 4;
        triangles[frameCount + 5] = frameCount + 5;
        triangles[frameCount + 6] = frameCount + 6;
        triangles[frameCount + 7] = frameCount + 7;
        triangles[frameCount + 8] = frameCount + 8;
        triangles[frameCount + 9] = frameCount + 9;
        triangles[frameCount + 10] = frameCount + 10;
        triangles[frameCount + 11] = frameCount + 11;

        mesh.vertices = vertices; 
        mesh.triangles = triangles;

        trail.GetComponent<MeshFilter>().mesh = mesh;

        prevFrameTipPos = tip.transform.position;
        prevFrameBasePos = start.transform.position;

        frameCount += numVertices;


    }
}
