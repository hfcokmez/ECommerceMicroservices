using System.Threading.Tasks;
using FreeCourse.Web.Models.Inputs.FakePayment;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}