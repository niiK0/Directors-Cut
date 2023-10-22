using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class RoleManager : MonoBehaviour
{
    //Padr�o singleton
    public static RoleManager Instance;

    //Lista que � atualizada com os PlayerManagers dos jogadores
    [SerializeField] private List<PlayerManager> players;
    
    public List<PlayerManager> GetPlayerList()
    {
        return players;
    }

    //N�mero de diretores. H� de ser alterado com uma fun��o futura, relacionada �s configura��es da partida
    private int nOfDirectors = 1;

    private void Awake()
    {
        Instance = this;
    }

    //Encontra os PlayerManagers na cena e os adiciona a lista de players
    private void GetPlayers()
    {
        PlayerManager[] playersArray = FindObjectsOfType<PlayerManager>();
        foreach (PlayerManager playerManager in playersArray)
        {
            players.Add(playerManager);
        }
        //Debug.Log("Players gotten: n of players: " + players.Count);
    }

    //Atribui aos PlayerManagers na lista de players a role de Diretor, se tiverem sido sorteados
    private void AssignRoles()
    {
        int nOfPlayers = players.Count;
        int[] chosenDirectors = new int[nOfDirectors];

        //Debug.Log("Numero de diretores:" + nOfDirectors);

        int aux = -1;

        //Sorteia <n� de Diretores> n�meros aleat�rios de 0 a <n� de Jogadores>. Esses ser�o os diretores
        for (int i = 0; i < nOfDirectors; i++)
        {
            int n = Random.Range(0, nOfPlayers);
            if (n != aux)
            {
                chosenDirectors[i] = n;
                aux = n;
                //Debug.Log("O diretor sera o numero" + n);
            } else
            {
                continue;
            }
        }

        //Torna diretores aqueles cujos �ndices foram sorteados
        for (int i = 0; i < nOfPlayers; i++)
        {
            if (chosenDirectors.Contains(i))
            {
                players[i].IsDirector = true;
                //Debug.Log("Role de diretor atr�bu�da");
            }
        }
    }

    //Fun��o a ser chamada ao iniciar do jogo. Define as roles. Ainda n�o � chamada por ningu�m, mas j� est� pronta para utiliza��o.
    public void SetUpRoles()
    {
        GetPlayers();
        AssignRoles();
        //Debug.Log("Setup concluded!");
    }
}
