using System;
using UnityEngine;


public abstract class AbstractAIActor : MonoBehaviour, IAgent, IActor
{
	public abstract void RunAI();
}

