using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Types;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlHierarchyId CommonAncestor(SqlHierarchyId h1, SqlHierarchyId h2, bool ReturnRootOnNull)
    {
        while (!h2.IsDescendantOf(h1))
        {
            h1 = h1.GetAncestor(1);
        }

        if (!ReturnRootOnNull && h1 == SqlHierarchyId.GetRoot())
            h1 = SqlHierarchyId.Null;
        return h1;
    }
}