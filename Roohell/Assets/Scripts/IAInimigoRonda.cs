using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAInimigoRonda : MonoBehaviour {

	public GameObject inimigo; // referência ao inimigo a controlar

	public GameObject[] pontos; // vetor dos pontos de parada do inimigo

	public float velocidade = 5f; // velocidade do inimigo
	public float espera = 2f; // tempo de espera no ponto de parada

	public bool loop = true; // volta ao início após último ponto?
 


	int i = 0;		// indice do vetor pontos
	float proxTempo;  // tempo do próximo movimento
	bool seMovendo = true;
	
	private Animator animator;
    private Transform transformInimigo;
    private Vector3 escalaOriginal;

    void Start()
    {
        transformInimigo = inimigo.transform;
        escalaOriginal = transformInimigo.localScale; 
        proxTempo = 0f;
        seMovendo = true;
        animator = GetComponent<Animator>();
    }


    void Update () {
		movimenta();
	}

    void movimenta()
    {
        if (pontos == null || pontos.Length == 0)
            return;

        // Verifica se i ainda é um índice válido
        if (i >= pontos.Length)
        {
            if (loop)
                i = 0;
            else
            {
                seMovendo = false;
                return;
            }
        }

        // Agora é seguro acessar pontos[i]
        Vector3 destino = pontos[i].transform.position;

        // Flip do inimigo
        float escalaX = Mathf.Abs(escalaOriginal.x);
        if (transformInimigo.position.x < destino.x)
            transformInimigo.localScale = new Vector3(-escalaX, escalaOriginal.y, escalaOriginal.z);
        else
            transformInimigo.localScale = new Vector3(escalaX, escalaOriginal.y, escalaOriginal.z);

        // Movimento
        transformInimigo.position = Vector3.MoveTowards(
            transformInimigo.position,
            destino,
            velocidade * Time.deltaTime
        );

        // Chegou no ponto
        if (Vector3.Distance(transformInimigo.position, destino) <= 0.1f)
        {
            i++;
            proxTempo = Time.time + espera;
            seMovendo = false;
        }

        // Retomar movimento depois do tempo de espera
        if (!seMovendo && Time.time >= proxTempo)
        {
            seMovendo = true;
        }
    }


}





