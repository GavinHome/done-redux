using NUnit.Framework;
using Redux.Utils;

namespace ObjectCloneTests;

public class ObjectCloneTest
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test()
    {
        Object obj = null;
        var newObj = obj.Clone();
        Assert.IsNull(newObj);

        try
        {
            (new ObjectCloneTest()).Clone();
        }
        catch (ArgumentException ex)
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo("The type must be serializable. (Parameter 'source')"), delegate { throw ex; });
        }
    }
}