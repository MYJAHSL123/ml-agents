using System;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Unity.MLAgents.Extensions.Runtime.Input
{
    public class InputActuatorComponent : ActuatorComponent
    {
        PlayerInput m_PlayerInput;
        BehaviorParameters m_BehaviorParameters;
        Agent m_Agent;
        void Awake()
        {
            FindNeededComponents();
        }

        void FindNeededComponents()
        {
            if (m_PlayerInput == null)
            {
                m_PlayerInput = GetComponent<PlayerInput>();
                Assert.IsNotNull(m_PlayerInput);
            }

            if (m_BehaviorParameters == null)
            {
                m_BehaviorParameters = GetComponent<BehaviorParameters>();
                Assert.IsNotNull(m_BehaviorParameters);
            }

            if (m_Agent == null)
            {
                m_Agent = GetComponent<Agent>();
                Assert.IsNotNull(m_Agent);
            }
        }

        public override IActuator CreateActuator()
        {
            FindNeededComponents();
            return new InputActuator(m_PlayerInput, m_BehaviorParameters, m_Agent);
        }

        public override ActionSpec ActionSpec => ActionSpec.MakeContinuous(0);
    }
}
