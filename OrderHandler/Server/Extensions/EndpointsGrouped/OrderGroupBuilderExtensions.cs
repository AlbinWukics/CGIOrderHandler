using OrderHandler.Server.Endpoints.Requests.Order;

namespace OrderHandler.Server.Extensions.EndpointsGrouped;

public static class OrderGroupBuilderExtensions
{
    public static RouteGroupBuilder MapOrderGroup(this RouteGroupBuilder builder)
    {
        builder.MediatePost<AddOrderRequest>("/");
        builder.MediateGet<GetOrderByIdRequest>("/{id}");
        builder.MediateGet<GetAllOrdersRequest>("/");
        builder.MediateGet<GetManyByOrderNumberRequest>("/orderNumber/{orderNumber}");
        builder.MediatePut<UpdateOrderRequest>("/");
        builder.MediateDelete<RemoveOrderRequest>("/{id}");

        return builder;
    }
}