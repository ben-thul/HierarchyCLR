using System;
using System.Collections;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Types;

public partial class UserDefinedFunctions
{
    private class ListAncestorsReturnType
    {
        public SqlHierarchyId Ancestor { get; }
        public Byte RelativeLevel { get; }

        public ListAncestorsReturnType(SqlHierarchyId ancestor, byte relativeLevel)
        {
            Ancestor = ancestor;
            RelativeLevel = relativeLevel;
        }
    }

    [SqlFunction(
        FillRowMethodName = "FillRow_ListAncestors",
        TableDefinition = "ancestor hierarchyid, relativeLevel tinyint"
        )]
    public static IEnumerable ListAncestors(SqlHierarchyId h)
    {
        byte idx = 0;
        while (!h.IsNull)
        {
            ListAncestorsReturnType r = new ListAncestorsReturnType(h, idx);
            yield return (r);

            h = h.GetAncestor(1);
            idx++;
        }
    }
    public static void FillRow_ListAncestors(Object obj, out SqlHierarchyId ancestor, out byte relativeLevel)
    {
        ListAncestorsReturnType r = (ListAncestorsReturnType)obj;

        ancestor = r.Ancestor;
        relativeLevel = r.RelativeLevel;
    }
}
