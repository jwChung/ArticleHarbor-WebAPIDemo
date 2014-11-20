namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http.Controllers;
    using Xunit;

    public class CompositeActionValueBinderTest : IdiomaticTest<CompositeActionValueBinder>
    {
        [Test]
        public void SutIsActionValueBinder(CompositeActionValueBinder sut)
        {
            Assert.IsAssignableFrom<IActionValueBinder>(sut);
        }

        [Test]
        public void GetBindingReturnsCorrectBinding(
            CompositeActionValueBinder sut,
            HttpActionDescriptor actionDescriptor,
            HttpActionBinding expected)
        {
            var binders = sut.Binders.ToArray();
            binders[0].ToMock().Setup(x => x.GetBinding(actionDescriptor)).Returns(() => null);
            binders[1].Of(x => x.GetBinding(actionDescriptor) == expected);

            var actual = sut.GetBinding(actionDescriptor);

            Assert.Equal(expected, actual);
        }

        [Test]
        public void GetBindingThrowsWhenAllBindersReturnNullBinding(
            CompositeActionValueBinder sut,
            HttpActionDescriptor actionDescriptor)
        {
            foreach (var binder in sut.Binders)
                binder.ToMock().Setup(x => x.GetBinding(actionDescriptor)).Returns(() => null);
            Assert.Throws<InvalidOperationException>(() => sut.GetBinding(actionDescriptor));
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyGuardClause()
        {
            yield return this.Methods.Select(x => x.GetBinding(null));
        }
    }
}