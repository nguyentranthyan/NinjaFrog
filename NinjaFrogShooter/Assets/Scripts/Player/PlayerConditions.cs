using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions : MonoBehaviour
{
    /// <summary>
    /// Check Collider
    /// </summary>
    public bool IsCollidingBellow { get; set; }
    public bool IsCollidingAbove { get; set; }
    public bool IsCollidingLeft { get; set; }
    public bool IsCollidingRight { get; set; }
    public bool IsFalling { get; set; }
    public bool IsWallCling { get; set; }
    public bool IsJetpacking { get; set; }
    public bool IsJumping { get; set; }

    public void Reset()
    {
        IsCollidingBellow = false;
        IsCollidingAbove = false;
        IsCollidingLeft = false;
        IsCollidingRight = false;
        IsFalling = false;
    }
}
