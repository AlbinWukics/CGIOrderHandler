
using OrderHandler.Server.Endpoints.Requests.OrderRow;

namespace OrderHandler.Server.Extensions.EndpointsGrouped;

public static class OrderRowGroupBuilderExtensions
{
    public static RouteGroupBuilder MapOrderRowGroup(this RouteGroupBuilder builder)
    {
        builder.MediatePost<AddOrderRowRequest>("/");
        builder.MediateGet<GetOrderRowByIdRequest>("/{id}");
        builder.MediateGet<GetAllOrderRowsRequest>("/");
        builder.MediateGet<GetManyByOrderRowNumberRequest>("/orderRowNumber/{orderRowNumber}");
        builder.MediatePut<UpdateOrderRowRequest>("/");
        builder.MediateDelete<RemoveOrderRowRequest>("/{id}");

        return builder;
    }
}