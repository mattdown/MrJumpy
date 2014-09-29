using UnityEngine;
using System.Collections;

public class EnemyModel : MonoBehaviour {

    public int health { get; set; }
    public string enemyType { get; set; }

    public bool isAlive { get; set; }

    public const string STRAIGHT_SHOOTER = "straight line";
    public const string AUTO_AIM = "auto aim";
    public const string HOMING_MISSILE = "homing missile";

    public string enemySide { get; set; }

    public static string LEFT = "left";
    public static string RIGHT = "right";


}
