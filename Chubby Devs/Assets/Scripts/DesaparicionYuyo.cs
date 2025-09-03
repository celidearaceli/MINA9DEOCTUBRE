using UnityEngine;

public class DesaparicionYuyo : MonoBehaviour
{
    public GameObject objetoADesaparecer;     // El objeto que desaparece
    public Camera camara;                     // La c�mara que se gira
    public MonoBehaviour controlDeCamara;     // El script de control de la c�mara (ej. MouseLook)
    public float velocidadRotacion = 2f;

    private int contadorEntradas = 0;
    private bool girarCamara = false;
    private Quaternion rotacionObjetivo;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            contadorEntradas++;

            if (contadorEntradas == 2) // Solo la segunda vez
            {
                if (objetoADesaparecer != null)
                    objetoADesaparecer.SetActive(false);

                if (controlDeCamara != null)
                    controlDeCamara.enabled = false;

                // Calcular la rotaci�n 180� en Y desde donde est� la c�mara
                rotacionObjetivo = Quaternion.Euler(0, camara.transform.eulerAngles.y + 180f, 0);
                girarCamara = true;
            }
        }
    }

    void Update()
    {
        if (girarCamara)
        {
            camara.transform.rotation = Quaternion.Slerp(camara.transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadRotacion);

            // Si ya casi lleg�, detenemos
            if (Quaternion.Angle(camara.transform.rotation, rotacionObjetivo) < 1f)
            {
                girarCamara = false;
                // Si quer�s reactivar el control de c�mara despu�s:
                if (controlDeCamara != null)
                    controlDeCamara.enabled = true;
            }
        }
    }
}


