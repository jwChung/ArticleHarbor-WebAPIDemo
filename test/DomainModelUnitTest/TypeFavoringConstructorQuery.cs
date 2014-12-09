namespace ArticleHarbor.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using Ploeh.AutoFixture.Xunit;

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class FavorTypesAttribute : CustomizeAttribute
    {
        private readonly Type favorType;

        public FavorTypesAttribute(Type favorType)
        {
            this.favorType = favorType;
        }

        public Type FavorType
        {
            get { return this.favorType; }
        }

        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            return new ConstructorCustomization(
                parameter.ParameterType,
                new TypeFavoringConstructorQuery(this.favorType));
        }

        private class TypeFavoringConstructorQuery : IMethodQuery
        {
            private readonly Type favorType;

            public TypeFavoringConstructorQuery(Type favorType)
            {
                this.favorType = favorType;
            }

            public IEnumerable<IMethod> SelectMethods(Type type)
            {
                if (type == null)
                    throw new ArgumentNullException("type");

                return from ci in type.GetConstructors()
                       let score = new FavorTypeParameterScore(this.favorType, ci.GetParameters())
                       orderby score descending
                       select new ConstructorMethod(ci) as IMethod;
            }
        }

        private class FavorTypeParameterScore : IComparable<FavorTypeParameterScore>
        {
            private readonly Type favorType;
            private readonly int score;

            public FavorTypeParameterScore(Type favorType, IEnumerable<ParameterInfo> parameters)
            {
                if (favorType == null)
                    throw new ArgumentNullException("favorType");

                if (parameters == null)
                    throw new ArgumentNullException("parameters");

                this.favorType = favorType;
                this.score = this.CalculateScore(parameters);
            }

            public int CompareTo(FavorTypeParameterScore other)
            {
                if (other == null)
                {
                    return 1;
                }

                return this.score.CompareTo(other.score);
            }

            private int CalculateScore(IEnumerable<ParameterInfo> parameters)
            {
                var arrayScore = parameters.Count(p => p.ParameterType == this.favorType);
                if (arrayScore > 0)
                    return arrayScore;

                return parameters.Count() * -1;
            }
        } 
    }
}