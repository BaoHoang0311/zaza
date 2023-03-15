using MediatR;
using Service.Domain.Models;

namespace Service.Application.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string ProductName { get; set; }
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly AestrainingContext _context;
            public CreateProductCommandHandler(AestrainingContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product();
                product.ProductName = command.ProductName;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }

        }
    }
}
