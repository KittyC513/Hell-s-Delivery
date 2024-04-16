using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundbank : MonoBehaviour
{
    [Header ("General")]
    [SerializeField] public AK.Wwise.Event steps;
    [SerializeField] public AK.Wwise.Event land;
    [SerializeField] public AK.Wwise.Event jump;
    [SerializeField] public AK.Wwise.Event die;
    [SerializeField] public AK.Wwise.Event parachuteOpen;
    [SerializeField] public AK.Wwise.Event parachuteClose;
    [SerializeField] public AK.Wwise.Event metalStep;
    [SerializeField] public AK.Wwise.Event woodStep;
    [SerializeField] public AK.Wwise.Event metalLand;
    [SerializeField] public AK.Wwise.Event woodLand;
    [SerializeField] public AK.Wwise.Event packagePick;
    [SerializeField] public AK.Wwise.Event packageToss;
    [SerializeField] public AK.Wwise.Event windCatch;
    [SerializeField] public AK.Wwise.Event windExit;

    [Space, Header("Shmink")]
    [SerializeField] public AK.Wwise.Event shminkJump;
    [SerializeField] public AK.Wwise.Event shminkPushed;
    [SerializeField] public AK.Wwise.Event shminkThrow;
    [SerializeField] public AK.Wwise.Event shminkDeath;

    [Space, Header ("Shmonk")]
    [SerializeField] public AK.Wwise.Event shmonkJump;
    [SerializeField] public AK.Wwise.Event shmonkPushed;
    [SerializeField] public AK.Wwise.Event shmonkThrow;
    [SerializeField] public AK.Wwise.Event shmonkDeath;
}
