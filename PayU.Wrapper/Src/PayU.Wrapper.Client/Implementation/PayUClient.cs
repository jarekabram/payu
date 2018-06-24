﻿using System;
using System.Threading.Tasks;
using PayU.Wrapper.Client.Data;
using PayU.Wrapper.Client.Enum;
using PayU.Wrapper.Client.Exception;

namespace PayU.Wrapper.Client
{
    /// <summary>
    /// Pay U Client Class
    /// </summary>
    /// <seealso cref="PayU.Wrapper.Client.IPayUClient" />
    public class PayUClient : IPayUClient
    {
        /// <summary>
        /// The user
        /// </summary>
        private TokenContract _tokenContract;

        /// <summary>
        /// The user request
        /// </summary>
        private UserRequestData _userRequestData;

        /// <summary>
        /// The rest builder
        /// </summary>
        private readonly IRestBuilder _restBuilder;

        /// <summary>
        /// The country code
        /// </summary>
        private readonly CountryCode _countryCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayUClient"/> class.
        /// </summary>
        /// <param name="isProducition">if set to <c>true</c> [is producition].</param>
        /// <param name="baseUrl">base url to connect with API</param>
        public PayUClient(UserRequestData userRequestData, TokenContract tokenContract)
         {
             _userRequestData = userRequestData;
             _restBuilder = new RestBuilder(userRequestData.BaseUrl);
             _tokenContract = tokenContract;
             _countryCode = userRequestData.CountryCode;
         }

        public async Task<T> Request<T>(PayURequestType payURequestType)
        {
            try
            {
            switch (payURequestType)
            {
                case PayURequestType.GetOrderDetails:
                    return (T)Convert.ChangeType(_restBuilder.GetOrderDetails<T>(_userRequestData.DataToRequest
                        .OrderId, _tokenContract) , typeof(T));

                case PayURequestType.PostRefundOrder:
                    return (T)Convert.ChangeType(_restBuilder.PostRefundOrder<T>(_userRequestData.DataToRequest
                        .OrderId, _tokenContract) , typeof(T));

                case PayURequestType.PutUpdateOrder:
                    return (T) Convert.ChangeType(_restBuilder
                        .PutUpdateOrder<T>(_userRequestData.DataToRequest.OrderId, _userRequestData.OrderStatus, _tokenContract),
                        typeof(T));

                //case PayURequestType.DeleteCancelOrder:
                //    return this;

                case PayURequestType.PostCreateNewOrder:
                    return (T)Convert.ChangeType(_restBuilder
                        .PostCreateNewOrder<T>(_userRequestData.DataToRequest.OrderId,
                        _tokenContract, _userRequestData.DataToRequest.OrderContract), typeof(T));

                //case PayURequestType.PostPayOutFromShop:
                //    return this;

                //case PayURequestType.GetRetrevePayout:
                //    return this;

                //case PayURequestType.FinishRequests:
                //    return this;

                default:
                    throw new InvalidRequestType();
            }
        }
            catch (AggregateException innerException)
            {
                Console.WriteLine($"{innerException.Message}");
                throw innerException.InnerException.InnerException;
            }
        }
    }
}