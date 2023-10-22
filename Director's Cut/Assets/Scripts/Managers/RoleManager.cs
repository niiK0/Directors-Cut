using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class RoleManager : MonoBehaviour
{
    //Padrão singleton
    public static RoleManager Instance;

    //Lista que é atualizada com os PlayerManagers dos jogadores
    [SerializeField] private List<PlayerManager> players;
    
    public List<PlayerManager> GetPlayerList()
    {
        return players;
    }

    //Número de diretores. Há de ser alterado com uma função futura, relacionada às configurações da partida
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

        //Sorteia <nº de Diretores> números aleatórios de 0 a <nº de Jogadores>. Esses serão os diretores
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

        //Torna diretores aqueles cujos índices foram sorteados
        for (int i = 0; i < nOfPlayers; i++)
        {
            if (chosenDirectors.Contains(i))
            {
                players[i].IsDirector = true;
                //Debug.Log("Role de diretor atríbuída");
            }
        }
    }

    //Função a ser chamada ao iniciar do jogo. Define as roles. Ainda não é chamada por ninguém, mas já está pronta para utilização.
    public void SetUpRoles()
    {
        GetPlayers();
        AssignRoles();
        //Debug.Log("Setup concluded!");
    }
}
