/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Character.Abilities.AI
{
    using UnityEngine;

    /// <summary>
    /// Base class for moving the character with a pathfinding implementation.
    /// </summary>
    public abstract class PathfindingMovement : Ability
    {
        [Tooltip("Can the character apply new accelerations while in the air?")]
        [SerializeField] protected bool m_AllowMovementInAir = true;

        public bool AllowMovementInAir { get { return m_AllowMovementInAir; } set { m_AllowMovementInAir = value; } }

        public override bool IsConcurrent { get { return true; } }

        /// <summary>
        /// Returns the desired input vector value. This will be used by the Ultimate Character Locomotion componnet.
        /// </summary>
        public abstract Vector2 InputVector { get; }
        /// <summary>
        /// Returns the desired rotation value. This will be used by the Ultimate Character Locomotion component.
        /// </summary>
        public abstract Vector3 DeltaRotation { get; }
        /// <summary>
        /// Returns if the agent has arrived at the destination.
        /// </summary>
        public abstract bool HasArrived { get; }

        /// <summary>
        /// Sets the destination of the pathfinding agent.
        /// </summary>
        /// <param name="target">The position to move towards.</param>
        /// <returns>True if the destination was set.</returns>
        public abstract bool SetDestination(Vector3 target);

        /// <summary>
        /// Returns the destination of the pathfinding agent.
        /// </summary>
        /// <returns>The destination of the pathfinding agent.</returns>
        public abstract Vector3 GetDestination();

        /// <summary>
        /// Sets the rotation of the agent after they have arrived at the destination.
        /// </summary>
        /// <param name="rotation">The destination rotation.</param>
        public virtual void SetDestinationRotation(Quaternion rotation) { }

        /// <summary>
        /// Updates the character's input values.
        /// </summary>
        public override void Update()
        {
            if (!m_AllowMovementInAir && !m_CharacterLocomotion.Grounded) {
                m_CharacterLocomotion.InputVector = Vector2.zero;
                return;
            }
            m_CharacterLocomotion.InputVector = InputVector;
        }

        /// <summary>
        /// Updates the character's rotation values.
        /// </summary>
        public override void UpdateRotation()
        {
            if (!m_AllowMovementInAir && !m_CharacterLocomotion.Grounded) {
                m_CharacterLocomotion.DeltaRotation = Vector3.zero;
                return;
            }
            m_CharacterLocomotion.DeltaRotation = DeltaRotation;
        }
    }
}