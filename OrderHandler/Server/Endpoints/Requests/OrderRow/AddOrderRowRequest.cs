using OrderHandler.DomainCommons.DataTransferObjects;

namespace OrderHandler.Server.Endpoints.Requests.OrderRow;

public class AddOrderRowRequest : IHttpRequest
{
    public OrderRowDto OrderRow { get; set; } = null!;
}