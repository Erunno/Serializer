using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer
{
    public class SimpleTypeDescriptor<ParentType, TheType> : IDescriptor<ParentType>
    {
        private Func<ParentType, TheType> accessor;
        string typeName;

        public SimpleTypeDescriptor(string typeName, Func<ParentType, TheType> accessor)
        {
            this.typeName = typeName;
            this.accessor = accessor;
        }

        public string GenerateStringRepresentation(ParentType instatnce)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"<{typeName}>");
            result.AppendLine(accessor(instatnce).ToString());
            result.Append($"</{typeName}>");

            return result.ToString();
        }
    }
}
