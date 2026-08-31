using ShopFlow.Data;
using System.ComponentModel.DataAnnotations;

namespace ShopFlow.Features.AddToCart;

public static class AddToCartEndpoint
{
    public static void MapAddToCartEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/cart/add", Handle)
           .WithName("AddProductToCart")
           .WithTags("Cart")
           .Produces<AddToCartResponse>(StatusCodes.Status200OK)
           .ProducesValidationProblem()
           .ProducesProblem(StatusCodes.Status404NotFound)
           .ProducesProblem(StatusCodes.Status409Conflict);
    }

    private static IResult Handle(AddToCartRequest request)
    {
        
        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(request, validationContext, validationResults, validateAllProperties: true);

        if (!isValid)
        {
            var errors = validationResults
                .SelectMany(r => r.MemberNames.Select(m => new { Member = m, r.ErrorMessage }))
                .GroupBy(x => x.Member)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage ?? "").ToArray());

            return Results.ValidationProblem(errors!);
        }

       
        var product = ShopFlowDataStore.Products.FirstOrDefault(p => p.Id == request.ProductId);
        if (product is null)
        {
            return Results.Problem(
                title: "Product not found",
                detail: $"No product exists with Id {request.ProductId}.",
                statusCode: StatusCodes.Status404NotFound);
        }

        if (product.StockQuantity < request.Quantity)
        {
            return Results.Problem(
                title: "Insufficient stock",
                detail: $"Only {product.StockQuantity} unit(s) of '{product.Name}' are available.",
                statusCode: StatusCodes.Status409Conflict);
        }

        if (!ShopFlowDataStore.Carts.TryGetValue(request.CartId, out var cartItems))
        {
            cartItems = new List<CartItem>();
            ShopFlowDataStore.Carts[request.CartId] = cartItems;
        }

        var existingItem = cartItems.FirstOrDefault(c => c.ProductId == request.ProductId);
        if (existingItem is not null)
        {
            existingItem.Quantity += request.Quantity;
        }
        else
        {
            cartItems.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = request.Quantity,
                UnitPrice = product.Price
            });
        }

        
        var response = new AddToCartResponse
        {
            CartId = request.CartId,
            ProductId = product.Id,
            ProductName = product.Name,
            Quantity = existingItem?.Quantity ?? request.Quantity,
            UnitPrice = product.Price,
            CartTotal = cartItems.Sum(c => c.Quantity * c.UnitPrice)
        };

        return Results.Ok(response);
    }
}