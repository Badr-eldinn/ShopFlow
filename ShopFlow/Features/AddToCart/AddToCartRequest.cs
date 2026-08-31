using System.ComponentModel.DataAnnotations;

namespace ShopFlow.Features.AddToCart
{
    public class AddToCartRequest
    {
        [Required(ErrorMessage ="CartId is Required")]
        public string CartId { get; set; }=string.Empty;
        [Range(1,int.MaxValue,ErrorMessage ="Product must be Positive num")]
        public int ProductId {  get; set; }
        [Range(1,50,ErrorMessage ="Quantity must be between 1 and 50.")]
        public int Quantity { get; set; }
    }
}
