using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasGameMng : MonoBehaviour
{
    public static CanvasGameMng Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    [SerializeField] GameObject[] paineis;
    public bool estaPausado;
    public bool comecouJogo;

    [Header("Config Player")]
    [SerializeField] Slider sldVidaPlayer;
    private float vidaPlayermaxima = 150;
    private float vidaPlayerAtual;

    [Header("Config Inimigo")]
    [SerializeField] Slider sldVidaInimigo;
    [SerializeField] TextMeshProUGUI txtDistancia;
    private float vidaInimigomaxima = 600;
    private float vidaInimigoAtual;

    private void Start()
    {
        ExibirPainel(0);

        estaPausado = false;
        comecouJogo = true;

        sldVidaPlayer.maxValue = vidaPlayermaxima;
        sldVidaPlayer.value = vidaPlayermaxima;
        vidaPlayerAtual = vidaPlayermaxima;

        sldVidaInimigo.maxValue = vidaInimigomaxima;
        sldVidaInimigo.value = vidaInimigomaxima;
        vidaInimigoAtual = vidaInimigomaxima;
    }

    private void Update()
    {
        if (comecouJogo == false) return;
        if (Input.GetKeyDown(KeyCode.P))
        {
            estaPausado = !estaPausado;
            if (estaPausado == true)
            {
                ExibirPainel(1);
            }
        }
        else
        {
            ExibirPainel(0);
        }
    }

    public void ExibirPainel(int index)
    {
        foreach (GameObject painel in paineis)
        {
            painel.SetActive(false);
        }

        paineis[index].SetActive(true);
    }


    public void VoltarMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinuarJogo()
    {
        estaPausado = false;
        ExibirPainel(0);
    }
    private void GameOver()
    {
        ExibirPainel(3);
        estaPausado = true;
        comecouJogo = false;

    }
    private void FimDeJogo()
    {
        ExibirPainel(2);
        estaPausado = true;
        comecouJogo = false;
    }
    public void AtualizarVidaPlayer(float valorDano)
    {
        vidaPlayerAtual -= valorDano;
        if (vidaPlayerAtual <= 0)
        {
            vidaPlayerAtual = 0;
            GameOver();
        }
        else
        {
            sldVidaPlayer.value = vidaPlayerAtual;
        }
    }

    public void AtualizarVidaInimigo(float dano)
    {
        vidaInimigoAtual -= dano;
        if (vidaInimigoAtual <= 0)
        {
            FimDeJogo();
        }
        else
        {
            sldVidaInimigo.value = vidaInimigoAtual;
        }
    }

    public void IniciarJogo()
    {
        SceneManager.LoadScene(1);
    }
    public void Sair()
    {
        Application.Quit();
    }
}