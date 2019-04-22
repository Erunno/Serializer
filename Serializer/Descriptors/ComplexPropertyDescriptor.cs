using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Serializer
{
    public class ComplexPropertyDescriptor<ParentType, InnerType> : IDescriptor<ParentType>
    {
        private RootDescriptor<InnerType> desc;
        private Func<ParentType, InnerType> accessor;
        string typeName;

        public ComplexPropertyDescriptor(string typeName,RootDescriptor<InnerType> innerTypeDesc, Func<ParentType, InnerType> accessor)
        {
            this.accessor = accessor;
            this.desc = innerTypeDesc;
            this.typeName = typeName;
        }

        public string GenerateStringRepresentation(ParentType instance)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"<{typeName}>");
            result.AppendLine(desc.GenerateStringRepresentation(accessor(instance)));
            result.Append($"</{typeName}>");

            return result.ToString();
        }
    }
}
