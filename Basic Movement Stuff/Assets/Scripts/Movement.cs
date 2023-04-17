using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float velocityDefault; // Velocidad que nunca cambia

    public float velocity; // Velocidad que mueve al jugador
    public float runVelocity; // Velocidad hecha para que el jugador pueda "Correr"

    public float rotSpeed; // Velocidad de rotación

    public float jumpForce; // Fuerza de salto

    public Rigidbody rb; // Referencia para el Rigidbody
    public Animator anim; // Referencia para el Animator

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Se usa para encontrar y colocar de manera automática el RigidBody
        anim = GetComponent<Animator>(); // Se usa para encontrar y colocar de manera automática el Animator

        velocityDefault = velocity; // Aquí hacemos que la velocidad que nunca cambia sea igual a la de caminar, se usa para poder volver a la misma despues de correr
    }

    private void Update()
    {
        float v = Input.GetAxis("Vertical") * velocity * Time.deltaTime; // Input Vertical
        float h = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime; // Input horizontal

        transform.Translate(0, 0 , v); // El jugador se mueve para delante y para atrás
        transform.Rotate(0, h, 0); // El jugador rota hacia la izquierda y derecha

        Inputs();
    }

    void Inputs()
    {
        #region W and S Animator Input
        if (Input.GetKey(KeyCode.W)) // Si apretas la W hace esto
        {
            anim.SetFloat("Walk", 1); // Le pone el float en 1 al Walk del Animator cuando se presione la W
        }
        else if (Input.GetKey(KeyCode.S)) // Si apretas la S hace esto
        {
            anim.SetFloat("Walk", -1); // Le pone el float en 1 al Walk del Animator cuando se presione la S
        }
        else // Si no apretas nada hace esto
        {
            anim.SetFloat("Walk", 0); // Le pone el float en 0 al Walk del Animator cuando no se presione ni la W ni la S
        }
        #endregion

        #region Run And Animator Input
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) // Si apretas la tecla LShift + la W hace esto
        {
            velocity = runVelocity; // La velocidad de caminar cambia a la de correr
            anim.SetBool("Run", true); // Le pone verdadero al bool Run del Animator
        }
        else // Cuando se deja de presionar LShift hace esto
        {
            velocity = velocityDefault; // La velocidad vuelve a ser la de caminar
            anim.SetBool("Run", false); // Le pone falso al bool Run del Animator
        }
        #endregion

        #region Jump And Animator Input
        if (Input.GetKeyDown(KeyCode.Space)) // Si apretas Espacio hace esto
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Le añade una fuerza de impulso hacia arriba al RigidBody, haciendo que el jugador salte
            anim.SetTrigger("Jump"); // Activa el trigger de Jump del Animator
        }
        #endregion
    }
}
