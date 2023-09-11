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

}
