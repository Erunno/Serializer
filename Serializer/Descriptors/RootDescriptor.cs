using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Serializer
{
    public class RootDescriptor<Type> : IDescriptor<Type>
    {
        string typeName;
        List<IDescriptor<Type>> descriptors;

        public RootDescriptor(string typeName)
        {
            this.typeName = typeName;
            descriptors = new List<IDescriptor<Type>>();
        }

        public void AddNewDescriptor(IDescriptor<Type> newDesc) => descriptors.Add(newDesc);

        public string GenerateStringRepresentation(Type instance)
        {
            StringBuilder result = new StringBuilder();

            foreach (var desc in descriptors)
                result.AppendLine(desc.GenerateStringRepresentation(instance));

            result.Remove(result.Length - 1, 1); //removing last new line

            return result.ToString();
        }

        public void Serialize(TextWriter output, Type instance)
        {
            output.WriteLine($"<{typeName}>");
            output.WriteLine(GenerateStringRepresentation(instance));
            output.WriteLine($"</{typeName}>");
        }
    }
}
