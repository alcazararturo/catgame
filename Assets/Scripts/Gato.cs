using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gato : MonoBehaviour
{
   	public int turno;// 0 -> X y 1 -> O
    public int cuentaTurno; // Contador de numero de turnos del jugador
    public GameObject[] turnoIcons; // Muesta que turno es
    public Sprite[] iconsJuego; //0 -> X icon y 1 -> O icon
    public Button[] gatoEspacios; // 
    public int[] espacioUsado; // Id´s espacio usado por cada jugador
    public Text textoGanador;
    public GameObject[] lineaGanadora;
    public GameObject panelGanador;
    public int xJugadorMarcador;
    public int oJugadorMarcador;
    public Text xJugadorMarcadorTexto;
    public Text oJugadorMarcadorTexto;
    public Button xJugadorBoton;
    public Button oJugadorBoton;
    public GameObject gatoImagen;

    void Start()
    {
        GameSetup();
    } 

    void GameSetup()
    {
        turno       = 0;
        cuentaTurno = 0;
        turnoIcons[0].SetActive(true);
        turnoIcons[1].SetActive(false);
        for (int i = 0; i < gatoEspacios.Length; i++)
        {
            gatoEspacios[i].interactable = true;
            gatoEspacios[i].GetComponent<Image>().sprite = null;   
        }
        for (int i = 0; i < espacioUsado.Length; i++)
        {
            espacioUsado[i] = -100;
        }
    }

    public void BotonGato(int index)
    {
        xJugadorBoton.interactable = false;
        oJugadorBoton.interactable = false;
        gatoEspacios[index].image.sprite = iconsJuego[turno];
        gatoEspacios[index].interactable = false;
        espacioUsado[index] = turno + 1;
        cuentaTurno++;
        if (cuentaTurno > 4)
        {
            bool isGanador = Ganador();
            if (cuentaTurno == 9 && isGanador == false) 
            {
                GatoValida();
            }
        }

        if (turno == 0)
        {
            turno = 1;
            turnoIcons[0].SetActive(false);
            turnoIcons[1].SetActive(true);
        }
        else
        {
            turno = 0;
            turnoIcons[0].SetActive(true);
            turnoIcons[1].SetActive(false);
        }
    }

    bool Ganador()
    {
        int v1 = espacioUsado[0] + espacioUsado[1] + espacioUsado[2];
        int v2 = espacioUsado[3] + espacioUsado[4] + espacioUsado[5];
        int v3 = espacioUsado[6] + espacioUsado[7] + espacioUsado[8];
        int h1 = espacioUsado[0] + espacioUsado[3] + espacioUsado[6];
        int h2 = espacioUsado[1] + espacioUsado[4] + espacioUsado[7];
        int h3 = espacioUsado[2] + espacioUsado[5] + espacioUsado[8];
        int d1 = espacioUsado[0] + espacioUsado[4] + espacioUsado[8];
        int d2 = espacioUsado[2] + espacioUsado[4] + espacioUsado[6];
        var solucion = new int[] {v1, v2, v3, h1, h2, h3, d1, d2};
        for (int i = 0; i < solucion.Length; i++)
        {
            if (solucion[i] == 3 * (turno+1))
            {
                mensajeGanador(i);
                return true;
            }
        }
        return false;
    }

    void mensajeGanador(int indexIn)
    {
        panelGanador.gameObject.SetActive(true);
        if (turno == 0)
        {
            xJugadorMarcador++;
            xJugadorMarcadorTexto.text = xJugadorMarcador.ToString();
            textoGanador.text = "Jugador X Gano!!";
        } else if (turno == 1)
        {
            oJugadorMarcador++;
            oJugadorMarcadorTexto.text = oJugadorMarcador.ToString();
            textoGanador.text = "Jugador O Gano!!";
        }
        lineaGanadora[indexIn].SetActive(true);
    }

    public void Revancha()
    {
        GameSetup();
        for (int i = 0; i < lineaGanadora.Length; i++)
        {
            lineaGanadora[i].SetActive(false);
        }
        panelGanador.SetActive(false);
        xJugadorBoton.interactable = true;
        oJugadorBoton.interactable = true;
        gatoImagen.SetActive(false);
    }

    public void Restart()
    {
        Revancha();
        xJugadorMarcador           = 0;
        oJugadorMarcador           = 0;
        xJugadorMarcadorTexto.text = "0";
        oJugadorMarcadorTexto.text = "0";
    }

    public void OpcionJugador(int jugador)
    {
        if (jugador == 0)
        {
            turno = 0;
            turnoIcons[0].SetActive(true);
            turnoIcons[1].SetActive(false);
        }
        else if (jugador == 1)
        {
            turno = 1;
            turnoIcons[0].SetActive(false);
            turnoIcons[1].SetActive(true);
        }
    }

    void GatoValida()
    {
        panelGanador.SetActive(true);
        gatoImagen.SetActive(true);
        textoGanador.text = "Gato!!!";
    }

}
