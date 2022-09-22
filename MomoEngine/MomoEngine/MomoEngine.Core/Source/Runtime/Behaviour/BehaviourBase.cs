using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomoEngine.Core
{
    public class BehaviourBase
    {
        public enum BehaviourType
        {
            Awake,
            Start,
            FixedUpdate,
            Update,
            LateUpdate,
            OnDestory,
            OnApplicationQuit
        }
    }
}
