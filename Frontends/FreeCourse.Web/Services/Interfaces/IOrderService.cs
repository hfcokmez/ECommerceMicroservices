using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using FreeCourse.Web.Models.Inputs.Orders;
using FreeCourse.Web.Models.ViewModels.Orders;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// For Synchronous Communication
        /// </summary>
        /// <param name="checkOutInfoInput"></param>
        /// <returns></returns>
        Task<OrderCreatedViewModel> CreateOrder(CheckOutInfoInput checkOutInfoInput);

        /// <summary>
        /// For Asynchronous Communication, ORDER INFORMATIONS WILL BE SENT TO RABBITMQ
        /// </summary>
        /// <param name="checkOutInfoInput"></param>
        /// <returns></returns>
        Task<SuspendOrderViewModel> SuspendOrder(CheckOutInfoInput checkOutInfoInput);

        Task<List<OrderViewModel>> GetOrders();
    }
}