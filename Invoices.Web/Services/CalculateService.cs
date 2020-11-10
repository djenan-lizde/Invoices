using System.ComponentModel.Composition;

namespace Invoices.Web.Services
{
    public interface ICalculateService
    {
        float CalculatePrice(float price, int quantity, int? tax = null);
    }

    [Export(typeof(ICalculateService))]
    public class CalculateService : ICalculateService
    {
        public float CalculatePrice(float price, int quantity, int? tax)
        {
            if (tax.HasValue)
            {
                return (float)((price * quantity) + (price * quantity * ((float)tax / 100)));
            }
            return price * quantity;
        }
    }
}
