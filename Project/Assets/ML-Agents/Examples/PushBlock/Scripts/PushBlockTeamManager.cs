using System.Collections.Generic;
using Unity.MLAgents;
using System.Linq;
using UnityEngine;
using Unity.MLAgents.Extensions.Teams;
using Unity.MLAgents.Sensors;

public class PushBlockTeamManager : BaseTeamManager
{
    Dictionary<Agent, bool> m_AgentDoneState = new Dictionary<Agent, bool> { };


    public override void RegisterAgent(Agent agent)
    {
        m_AgentDoneState[agent] = false;
    }

    public override void OnAgentDone(Agent agent, Agent.DoneReason doneReason, List<ISensor> sensors)
    {
        Debug.Log($"Agent {agent.gameObject.GetInstanceID()} mark as done in manager");
        m_AgentDoneState[agent] = true;
    }

    public void OnTeamDone()
    {
        Debug.Log("Team is Done");
        foreach (var agent in m_AgentDoneState.Keys.ToList())
        {
            if (m_AgentDoneState[agent])
            {
                agent.SendDoneToTrainer();
                m_AgentDoneState[agent] = false;
            }
        }
    }

    public void AddTeamReward(float reward)
    {
        foreach (var agent in m_AgentDoneState.Keys)
        {
            if (m_AgentDoneState[agent])
            {
                Debug.Log($"Add pos reward to Agent {agent.gameObject.GetInstanceID()}");
                agent.AddRewardAfterDeath(reward);
            }
            else
            {
                Debug.Log($"Add normal reward to Agent {agent.gameObject.GetInstanceID()}");
                agent.AddReward(reward);
            }
        }
    }
}
