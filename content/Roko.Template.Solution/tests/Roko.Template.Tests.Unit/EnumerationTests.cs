namespace Roko.Template.Tests.Unit
{
    using FluentAssertions;
    using Microsoft.VisualBasic;
    using Roko.Template.Domain.Seedwork;

    [Trait("Category", "Unit")]
    public class EnumerationTests
    {
        public class TestEnum(int id, string name) : Enumeration(id, name)
        {
            public static TestEnum Value1 { get; } = new TestEnum(1, "Value One");
            public static TestEnum Value2 { get; } = new TestEnum(2, "Value Two");
        }

        [Fact]
        public void Enumeration_UseAsEnum()
        {
            var v1 = TestEnum.Value1;

            v1.Id.Should().Be(1);
            v1.Name.Should().Be("Value One");
        }

        [Fact]
        public void Enumeration_GetAll()
        {
            var all = Enumeration.GetAll<TestEnum>();

            all.Should().BeEquivalentTo([TestEnum.Value1, TestEnum.Value2]);
        }

        [Fact]
        public void Enumeration_FromValue()
        {
            var v1 = Enumeration.FromValue<TestEnum>(1);

            v1.Should().BeEquivalentTo(TestEnum.Value1);
        }

        [Fact]
        public void Enumeration_FromDisplayName()
        {
            var v1 = Enumeration.FromDisplayName<TestEnum>("Value One");

            v1.Should().BeEquivalentTo(TestEnum.Value1);
        }

        [Theory]
        [MemberData(nameof(EqualsData))]
        public void Enumeration_Equals((TestEnum first, object second, bool IsEqual) x)
        {
            x.first.Equals(x.second).Should().Be(x.IsEqual);
        }

        public static TheoryData<(TestEnum first, object second, bool IsEqual)> EqualsData =
            new TheoryData<(TestEnum first, object second, bool IsEqual)>()
            {
                (TestEnum.Value1, TestEnum.Value1, true),
                (TestEnum.Value1, TestEnum.Value2, false),
                (TestEnum.Value1, 1, false)
            };
    }
}