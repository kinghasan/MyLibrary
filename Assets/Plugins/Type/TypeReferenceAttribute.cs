using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Types
{
    public class TypeReferenceAttribute : Attribute
    {
        public Type Type;

        public TypeReferenceAttribute(Type type)
        {
            Type = type;
        }
    }
}
