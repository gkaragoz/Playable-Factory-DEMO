using UnityEngine;

public class Example : MonoBehaviour {

    public Material capMaterial;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
        Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * 5.0f);
        Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.forward * 5.0f);

        Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + -transform.up * 0.5f);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Wood") {
            GameObject victim = other.gameObject;
            Debug.Log(victim.name);
            GameObject[] woodPieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

            foreach (GameObject woodPiece in woodPieces) {
                woodPiece.tag = "Untagged";
            }

            Destroy(woodPieces[0].GetComponent<CapsuleCollider>());
            woodPieces[0].AddComponent<CapsuleCollider>();

            woodPieces[1].AddComponent<CapsuleCollider>();

            if (!woodPieces[1].GetComponent<Rigidbody>()) {
                woodPieces[1].AddComponent<Rigidbody>();
            }
        }
    }

}
