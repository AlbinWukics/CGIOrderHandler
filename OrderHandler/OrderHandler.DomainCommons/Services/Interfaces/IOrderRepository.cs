﻿using OrderHandler.DomainCommons.DataTransferObjects;

namespace OrderHandler.DomainCommons.Services.Interfaces;

public interface IOrderRepository : IRepository<OrderDto>
{
    Task<ServiceResponse<IReadOnlyCollection<OrderDto>>> GetManyByOrderNumber(int orderNumber);
}