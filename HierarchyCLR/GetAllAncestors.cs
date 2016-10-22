using System;
using System.Collections;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Types;

public partial class UserDefinedFunctions
{
    [SqlFunction(
        FillRowMethodName = "FillRow_ListAncestors",
        TableDefinition = "ancestor hierarchyid"
        )]
    public static IEnumerable ListAncestors(SqlHierarchyId h)
    {
        while (!h.IsNull)
        {
            yield return (h);
            h = h.GetAncestor(1);
        }
    }
    public static void FillRow_ListAncestors(Object obj, out SqlHierarchyId ancestor)
    {
        ancestor = (SqlHierarchyId)obj;
    }

    [SqlFunction]
    public static SqlHierarchyId CommonAncestor(SqlHierarchyId h1, SqlHierarchyId h2)
    {
        /*
         * If you want the function to return '/' instead of NULL
         * when the two passed in values don't have a common ancestor,
         * set ReturnRootOnNull to be true 
         */ 
        bool ReturnRootOnNull = false;
        while (!h2.IsDescendantOf(h1))
            h1 = h1.GetAncestor(1);

        if (!ReturnRootOnNull && h1 == SqlHierarchyId.GetRoot())
            h1 = SqlHierarchyId.Null;
        return h1;
    }
}
