﻿namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler(IDocumentSession documentSession) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IDocumentSession documentSession = documentSession;

        // Business logic to create a product
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Create Product entity from command object
            var product = new Product
            {
                Name = command.Name,
                Categories = command.Categories,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            // Save the entity to the database
            documentSession.Store(product);
            await documentSession.SaveChangesAsync(cancellationToken);

            // Return CreateProductResult with the Id of the newly created product
            return new CreateProductResult(product.Id);
        }
    }
}
