namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CompositeEnumerableCommand<TValueElement> : CompositeModelCommand<IEnumerable<TValueElement>>
    {
        public CompositeEnumerableCommand(
            params IModelCommand<IEnumerable<TValueElement>>[] commands)
            : this((IEnumerable<IModelCommand<IEnumerable<TValueElement>>>)commands)
        {
        }

        public CompositeEnumerableCommand(
            IEnumerable<IModelCommand<IEnumerable<TValueElement>>> commands)
            : base(new TValueElement[0], (x, y) => x.Concat(y), commands)
        {
        }
    }
}