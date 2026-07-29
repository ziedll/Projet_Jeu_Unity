using UnityEngine;

public class deplacement : MonoBehaviour
{
    public float vitesse = 6f;
    public float vitesseRotation = 10f;
    public Animator animator;

    void Update()
    {
        float x = 0f;
        float z = 0f;

        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) z = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) z = -1f;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 1f;

        Vector3 dir = new Vector3(x, 0f, z).normalized;

        if (dir.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * vitesseRotation);

            transform.Translate(dir * vitesse * Time.deltaTime, Space.World);
            
            if (animator != null) animator.SetBool("IsRunning", true);
        }
        else
        {
            if (animator != null) animator.SetBool("IsRunning", false);
        }
    }
}