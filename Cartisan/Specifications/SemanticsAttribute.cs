using System;

namespace Cartisan.Specifications {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SemanticsAttribute: Attribute {
        public Semantics Type { get; set; }

        public SemanticsAttribute(Semantics type) {
            this.Type = type;
        }
    }
}