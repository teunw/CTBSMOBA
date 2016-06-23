using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Skills
{
    public interface Action
    {
        string Name { get; }
        Color actionColor { get; }
    }
}
