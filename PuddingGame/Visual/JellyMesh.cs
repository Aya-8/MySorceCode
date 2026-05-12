using UnityEngine;
using UnityEngine.UIElements;

public class JellyMesh : MonoBehaviour
{
    public float Intensity = 1f;
    public float Mass = 1f;
    public float stiffness = 1f;
    public float damping = 0.75f;
    
    private Mesh OriginalMesh, MeshClones;
    private MeshRenderer Renderer;
    private JellyVertex[] jv;　　　　　　　　//頂点の配列
    private Vector3[] vertexArray;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OriginalMesh = GetComponent<MeshFilter>().sharedMesh; //元のメッシュを参照
        MeshClones = Instantiate(OriginalMesh);　　　　　　　 //複製をつくる

        //サブメッシュ情報をコピー
        MeshClones.subMeshCount = OriginalMesh.subMeshCount;
        for(int i = 0; i < OriginalMesh.subMeshCount; i++)
        {
            MeshClones.SetTriangles(OriginalMesh.GetTriangles(i), i);
        }


        GetComponent<MeshFilter>().sharedMesh = MeshClones;　 //自分のメッシュに複製のメッシュを入れる
        Renderer = GetComponent<MeshRenderer>();　　　　　　  

        jv = new JellyVertex[MeshClones.vertices.Length];
        for (int i = 0; i < MeshClones.vertices.Length; i++)
            jv[i] = new JellyVertex(i, transform.TransformPoint(MeshClones.vertices[i]));  //オブジェクト座標をワールド座標に変換
    }

    private void FixedUpdate()
    {
        vertexArray = OriginalMesh.vertices;
        for(int i = 0; i < jv.Length; i++)
        {
            Vector3 target = transform.TransformPoint(vertexArray[jv[i].ID]);
            float intensity = (1 - (Renderer.bounds.max.y - target.y) / Renderer.bounds.size.y) * Intensity;　//高さに応じて揺れやすさを決める
            jv[i].Shake(target, Mass, stiffness, damping);
            target = transform.InverseTransformPoint(jv[i].Position);
            vertexArray[jv[i].ID] = Vector3.Lerp(vertexArray[jv[i].ID], target, intensity);
        }
        MeshClones.vertices = vertexArray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //頂点を揺らす機能のクラス
    public class JellyVertex
    {
        public int ID;
        public Vector3 velocity, Force;
        public Vector3 Position;
        public JellyVertex(int id,Vector3 pos)
        {
            ID = id;
            Position = pos;
        }

        public void Shake(Vector3 target, float m, float s, float d)
        {
            Force = (target - Position) * s;
            velocity = (velocity + Force / m) * d;
            Position += velocity;
            if ((velocity + Force / m).magnitude < 0.001f)
                Position = target;
        }
    }

   
}
