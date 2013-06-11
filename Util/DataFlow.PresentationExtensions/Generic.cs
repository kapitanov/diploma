using System;
using System.Windows.Markup;

namespace AISTek.DataFlow.PresentationExtensions
{
    public class Generic : MarkupExtension
    {
        public Type BaseType { get; set; }
        public Type[] InnerTypes { get; set; }

        public Generic()
            : base()
        { }

        public Generic(Type baseType, params Type[] innerTypes)
            : base()
        {
            BaseType = baseType;
            InnerTypes = innerTypes;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Type result = BaseType.MakeGenericType(InnerTypes);
            return result;
        }
    }
}


