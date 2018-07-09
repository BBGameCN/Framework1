using UnityEngine;
using System.Collections;

namespace Framework
{
    public abstract class ReusbleObject : MonoBehaviour, IReusable
    {
        public abstract void OnSpawn();

        public abstract void OnUnspawn();
    }
}

