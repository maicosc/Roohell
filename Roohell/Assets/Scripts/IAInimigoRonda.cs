using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAInimigoRonda : MonoBehaviour {

	public GameObject inimigo; // referência ao inimigo a controlar

	public GameObject[] pontos; // vetor dos pontos de parada do inimigo

	//public Collider2D triggerAtaque;
	//public Collider2D espada;
	//public float inicioEspada = 1f;  // Tempo que leva entre pressionar o botão e realizar o ataque
	//public float duracaoEspada = 0.2f; // Duração do ataqur
	//private float tempoInicioEspada = 0f;    // Tempo de inicio do ataque atual
	// hora de "ligar" o collider da espada
	//private float tempoFimEspada = 0f;       // Tempo de final do ataque atual
	// hora de "desligar" o collider da espada

	public float velocidade = 5f; // velocidade do inimigo
	public float espera = 2f; // tempo de espera no ponto de parada

	public bool loop = true; // volta ao início após último ponto?
 

	private Transform transform;
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
        // Se há pontos e o inimigo está se movendo
        if (pontos.Length > 0 && seMovendo)
        {
            Vector3 destino = pontos[i].transform.position;

            // Flip (só se estiver se movendo no eixo X)
            float escalaX = Mathf.Abs(escalaOriginal.x);

            if (transformInimigo.position.x < destino.x)
                transformInimigo.localScale = new Vector3(escalaX, escalaOriginal.y, escalaOriginal.z);
            else
                transformInimigo.localScale = new Vector3(-escalaX, escalaOriginal.y, escalaOriginal.z);

            // Movimento
            transformInimigo.position = Vector3.MoveTowards(
                transformInimigo.position,
                destino,
                velocidade * Time.deltaTime
            );

            // Chegou no ponto?
            if (Vector3.Distance(transformInimigo.position, destino) <= 0.1f)
            {
                i++; // próximo ponto
                proxTempo = Time.time + espera;
                seMovendo = false;
            }

            // Se passou do último ponto
            if (i >= pontos.Length)
            {
                if (loop)
                    i = 0; // volta ao início
                else
                    seMovendo = false; // para
            }
        }

        // Se está esperando, verifica se é hora de voltar a andar
        if (!seMovendo && Time.time >= proxTempo)
        {
            seMovendo = true;
        }
    }

}

    

   

