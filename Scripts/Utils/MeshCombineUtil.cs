using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombineUtil : MonoBehaviour
{
    // Start is called before the first frame update
   
        void Start()
        {
            CombineMesh();
        }

        public void CombineMesh()
        {
            MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            int i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                var chunk = meshFilters[i].AddComponent<Chunk>();
               // chunk._meshCombineUtil = this;
                var col =  meshFilters[i].AddComponent<BoxCollider>();
                col.isTrigger = true;
                var rb = meshFilters[i].AddComponent<Rigidbody>();
                rb.isKinematic = true;
                meshFilters[i].gameObject.SetActive(false);

                i++;
            }

            var meshFilter = transform.GetComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
            meshFilter.mesh.indexFormat = IndexFormat.UInt32;
            meshFilter.mesh.CombineMeshes(combine,true,true);
            meshFilter.mesh.Optimize();
            // meshFilter.mesh.RecalculateNormals();
            // meshFilter.mesh.RecalculateTangents();
            transform.gameObject.SetActive(true);
            
            transform.localScale = Vector3.one;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;

        }
    
}
