﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Domain.Models;

namespace Service.Application.ProductFeatures.Commands
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery,IEnumerable<Product>>
        {
            private readonly AestrainingContext _context;
            public GetAllProductsQueryHandler(AestrainingContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
            {
                var productList = await _context.Products.ToListAsync();
                if (productList == null)
                {
                    return null;
                }
                return productList.AsReadOnly();
            }
        }
    }
}
