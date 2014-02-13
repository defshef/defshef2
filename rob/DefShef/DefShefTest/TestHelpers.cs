using System;
using System.Linq;

namespace DefShef.Test
{
    internal static class TestHelpers
    {
        internal static bool AssertNestedListsAreEqual(object leftObj, object rightObj)
        {
            if (ReferenceEquals(null, leftObj))
            {
                return false;
            }

            if (ReferenceEquals(null, rightObj))
            {
                return false;
            }

            if (ReferenceEquals(leftObj, rightObj))
            {
                return true;
            }

            Type leftObjType = leftObj.GetType();
            Type rightObjType = rightObj.GetType();

            if (leftObjType != rightObjType)
            {
                return false;
            }

            object[] leftObjAsArray = leftObj as object[];
            object[] rightObjAsArray = rightObj as object[];

            if (leftObjAsArray != null && rightObjAsArray != null)
            {
                foreach (var pair in leftObjAsArray.Select((obj, i) => new {Obj = obj, Index = i})
                                                     .Join(rightObjAsArray.Select((obj, i) => new {Obj = obj, Index = i}),
                                                           left => left.Index,
                                                           right => right.Index,
                                                           (left, right) => new { LeftObj = left.Obj, RightObj = right.Obj }
                                                     ))
                {
                    leftObjType = pair.LeftObj.GetType();
                    rightObjType = pair.RightObj.GetType();

                    if (leftObjType != rightObjType)
                    {
                        return false;
                    }

                    if (leftObjType.IsArray && rightObjType.IsArray && !AssertNestedListsAreEqual(pair.LeftObj, pair.RightObj))
                    {
                        return false;
                    }
                }
            }
            else if (leftObjAsArray != null || rightObjAsArray != null)
            {
                return false;
            }
            else if (!(bool)leftObjType.GetMethod("Equals", new[] { rightObjType }).Invoke(leftObj, new[] { rightObj }))
            {
                return false;
            }

            return true;
        }
    }
}
