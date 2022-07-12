using BornToMove.DAL;
using Organizer;

namespace BornToMove.OrganizerTest;

using NUnit.Framework;

[TestFixture]
public class RotateSortTest
{
    private RotateSort<int> RotateSort = new RotateSort<int>();

    [Test]
    public void TestSortEmpty()
    {
        var l = new List<int>();
        var result = RotateSort.Sort(l, Comparer<int>.Default);
        Assert.IsEmpty(result);
        Assert.That(l, Is.Not.SameAs(result));
    }

    [Test]
    public void TestOneElement()
    {
        var l = new List<int>() { 1 };
        var result = RotateSort.Sort(l, Comparer<int>.Default);
        Assert.IsNotEmpty(result);
        Assert.AreEqual(result[0], 1);
        Assert.That(l, Is.Not.SameAs(result));
    }

    [Test]
    public void TestTwoElements()
    {
        var l = new List<int>() { 9, 1 };
        var result = RotateSort.Sort(l, Comparer<int>.Default);
        Assert.IsNotEmpty(result);
        CollectionAssert.IsOrdered(result);
        Assert.That(l, Is.Not.SameAs(result));
    }

    [Test]
    public void TestThreeEqual()
    {
        var l = new List<int>() { 3, 3, 3 };
        var result = RotateSort.Sort(l, Comparer<int>.Default);

        var filtered = result.Where(x => x == 3);
        Assert.IsTrue(filtered.Count() == 3);
        Assert.That(l, Is.Not.SameAs(result));
    }

    [Test]
    public void TestSortUnsortedArray()
    {
        var l = new List<int>();
        var rd = new Random();
        for (var i = 0; i < 99; i++)
        {
            l.Add(rd.Next(-99, 99));
        }

        var result = RotateSort.Sort(l, Comparer<int>.Default);
        CollectionAssert.IsOrdered(result);
        Assert.That(l, Is.Not.SameAs(result));
    }

}