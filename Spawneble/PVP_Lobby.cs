using Mirror;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class PVP_Lobby : NetworkBehaviour
{

    [SerializeField] List<Transform> spawnPvp;

    [SerializeField] List<Transform> spawnWaithPvp;
    [SerializeField] Transform lookAt;

    [SerializeField] Transform returnToMainLobby;

     SyncList <Player> pvp_Players = new SyncList<Player>();
     SyncList <Player> pvp_WaightPlayers = new SyncList<Player>();

    [SerializeField] int min_PlayersForStart = 2;
    List<Transform> spawnPoints;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Character_Player")
        {
            if (other.gameObject.GetComponent<Player>().isLocalPlayer)
            {
                var player = other.gameObject.GetComponent<Player>();

                    if (pvp_Players.Count < min_PlayersForStart){

                        spawnPoints = spawnPvp;
                        AddPlayerPVP(player);
                        player.Container.OnPVP_area = true;
                        player.Container.AllowMove = false;
                        player.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position;
                        player.transform.rotation = spawnPoints[0].rotation;
                        player.Container.Game_CameraGM.transform.rotation = player.transform.rotation;
                        player.Container.Main_Camera.transform.LookAt(lookAt);
                        player.Container.Pvp = this;
                    }
                    else{
                        AddPlayerWaight(player);
                        spawnPoints = spawnWaithPvp;
                        player.Container.OnPVP_area = true;
                        player.Container.AllowMove = false;
                        player.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position;
                        player.transform.rotation = spawnPoints[0].rotation;
                        player.Container.Game_CameraGM.transform.rotation = player.transform.rotation;
                        player.Container.Main_Camera.transform.LookAt(lookAt);
                        player.Container.Pvp = this;
                    }
              
            }
            else {
                var player = other.gameObject.GetComponent<Player>();

                if (pvp_Players.Count < min_PlayersForStart){

                    Debug.Log("Count  sync/list" + pvp_Players.Count);
                    spawnPoints = spawnPvp;
                    AddPlayerPVP(player);
                    player.Container.OnPVP_area = true;
                    player.Container.AllowMove = false;
                    player.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position;
                    player.transform.rotation = spawnPoints[0].rotation;
                    player.Container.Game_CameraGM.transform.rotation = player.transform.rotation;
                    player.Container.Main_Camera.transform.LookAt(lookAt);
                    player.Container.Pvp = this;
                }
                else{

                    AddPlayerWaight(player);
                    spawnPoints = spawnWaithPvp;
                    player.Container.OnPVP_area = true;
                    player.Container.AllowMove = false;
                    player.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position;
                    player.transform.rotation = spawnPoints[0].rotation;
                    player.Container.Game_CameraGM.transform.rotation = player.transform.rotation;
                    player.Container.Main_Camera.transform.LookAt(lookAt);
                    player.Container.Pvp = this;
                }
            }
        }
    }

    [Command(requiresAuthority = false)]
    void AddPlayerPVP(Player player){

        pvp_Players.Add(player);
        Debug.Log("Count  sync/list" + pvp_Players.Count);
    }
    
    [Command(requiresAuthority = false)]
    void AddPlayerWaight(Player player) => pvp_WaightPlayers.Add(player);

    [Command(requiresAuthority = false)]
    void RemovePlayer(Player player){
        if(pvp_WaightPlayers.Contains(player))pvp_WaightPlayers.Remove(player);
        else if(pvp_Players.Contains(player)) pvp_Players.Remove(player);

        Debug.Log("Count  sync/list" + pvp_Players.Count);
    }
    [Command(requiresAuthority = false)]
    void RemoveAllPlayers(){
        pvp_Players.Clear();

    }

    [Client]
    public void BackToMainLobby(Player player){

        player.Container.AllowMove = true;
        RemovePlayer(player);
        player.transform.position = returnToMainLobby.position;
        player.transform.rotation = returnToMainLobby.rotation;
    }
}
