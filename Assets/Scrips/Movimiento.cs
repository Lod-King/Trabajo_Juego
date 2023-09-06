using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorMovimiento : MonoBehaviour {
    public String nombre;
    public float velocidad;

    public float rotacion;
    // Para manipular un componente que le hemos agregado al GameObject, tenemos que crear
    // una propiedad del mismo del tipo del componente.. en este caso RigidBody
    // PASO 1: crear una variable del tipo del componente, PERO SIN INSTANCIARLO
    public Rigidbody fisicas;
    public float fuerzaSalto;

    // Start es llamada ANTES del primer Frame que se ejecutará
    void Start() {
        // Si cambiamos la position del GameObject, lo movemos en pantalla
        //transform.position = new Vector3(60, 1, 65);
        // PASO 2: Aquí SÍ CREAMOS EL OBJETO
        fisicas = GetComponent<Rigidbody>();
    }

    // Update es llamado una vez por cada Frame
    void Update() {
        //if (Input.GetKey(KeyCode.W)) {
            // El problema de usar position es que estás sobreescribiendo todos los ejes..
            // En este ejemplo solo quiero mover al GameObject en el eje Z, pero como estoy sobreescriendo TODA
            // la posición, tengo que darle los mismos valores sobre el eje X y el eje Y
            //    transform.position = new Vector3(
            //        transform.position.x, transform.position.y, transform.position.z + ejeZ);

            // Por eso, UNITY inventó un método para trasladar un GameObject
            //var movimiento = new Vector3(0, 0, ejeZ);
            //transform.Translate(movimiento);
        //}
        
        // Lo anterior era muy tedioso, así que UNITY inventó el InputManager, que lo que hace es:
        // darle un nombre a los ejes de movimiento.. así NO tienes que preocuparte de si pulsó
        // la tecla A o la D, porque ambas están consideradas dentro del eje de movimiento horizontal
        var horizontal = Input.GetAxis("Horizontal"); // esto retorna un valor entre -1 y 1
        var vertical = Input.GetAxis("Vertical");

        // Si dejamos simplemente así el cálculo del movimiento, se moverá el GameObject demasiado rápido
        // en un computador que mueva muchos FPS, y demasiado lento en los que muevan a pocos FPS
        // Para regularizar esto, debemos multiplicar por una propiedad inventada por UNITY
        var movimiento = new Vector3(horizontal, 0, vertical).normalized *
                                (velocidad * Time.deltaTime);
        // usamos la propiedad normalized de Vector3() para que el GameObject no se mueva más rápido si va en diagonal
        transform.Translate(movimiento);

        if (Input.GetKey(KeyCode.Space)) {
            var salto = new Vector3(0, fuerzaSalto, 0);
            fisicas.AddForce(salto);
        }

        var mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, mouseX * rotacion * Time.deltaTime, 0));
    }
}


