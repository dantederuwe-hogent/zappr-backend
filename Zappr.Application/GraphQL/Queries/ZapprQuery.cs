﻿using GraphQL.Types;

namespace Zappr.Api.GraphQL.Queries
{
    public class ZapprQuery : ObjectGraphType
    {
        public ZapprQuery()
        {
            Name = "Query";

            Field<SeriesQuery>("seriesQuery", resolve: context => new { });
            Field<UserQuery>("userQuery", resolve: context => new { });
        }

    }
}
