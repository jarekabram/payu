﻿using System.Threading.Tasks;
using PayU.Wrapper.Client.Data;
using PayU.Wrapper.Client.Enum;
using RestSharp;

namespace PayU.Wrapper.Client
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IRestBuilder
    {
        /// <summary>
        /// Posts a outh token.
        /// </summary>
        /// <param name="userRequestData">The user request data.</param>
        /// <returns></returns>
        Task<TokenContract> PostAOuthToken(UserRequestData userRequestData);

        /// <summary>
        /// Gets the order details.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="tokenContract">The user contract.</param>
        /// <returns>PayU Client (Fluent)</returns>
        Task<T> GetOrderDetails<T>(int orderId, TokenContract tokenContract);

        /// <summary>
        /// Gets the refund order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="tokenContract">The user contract.</param>
        /// <returns></returns>
        Task<T> PostRefundOrder<T>(int orderId, TokenContract tokenContract);

        /// <summary>
        /// Updates the order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatus"></param>
        /// <param name="tokenContract"></param>
        /// <param name="userRequestData"></param>
        /// <returns>PayU Client (Fluent)</returns>
        Task<T> PutUpdateOrder<T>(int orderId, OrderStatus orderStatus, TokenContract tokenContract);

        /// <summary>
        /// Cancels the order.
        /// </summary>
        /// <returns>PayU Client (Fluent)</returns>
        Task<T> DeleteCancelOrderTask<T>(int orderId, TokenContract token);

        /// <summary>
        /// Creates the new order.
        /// </summary>
        /// <returns>PayU Client (Fluent)</returns>
        Task<T> PostCreateNewOrder<T>(int orderId, TokenContract tokenContract, OrderContract orderContract);

        /// <summary>
        /// Pays the out from shop.
        /// </summary>
        /// <returns>PayU Client (Fluent)</returns>
        Task<T> PostPayOutFromShop<T>(TokenContract token);

        /// <summary>
        /// Retreves the payout.
        /// </summary>
        /// <returns>PayU Client (Fluent)</returns>
        Task<T> GetRetrevePayout<T>(TokenContract token);

        /// <summary>
        /// Finishes the request.
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns>Response</returns>
        Task<Response<T>> FinishRequest<T>();
    }
}