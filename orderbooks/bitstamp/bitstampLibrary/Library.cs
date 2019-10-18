﻿using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UOB.Exchanges.Bitstamp.Models;

namespace UOB.Exchanges.Bitstamp
{
    /// <summary>
    /// Currency pairs constants
    /// </summary>
    public enum CurrencyPairs { btcusd, btceur, eurusd, xrpusd, xrpeur, xrpbtc, ltcusd, ltceur, ltcbtc, ethusd, etheur, ethbtc, bchusd, bcheur, bchbtc };

    /// <summary>
    /// Library, to make requests to Bitstamp rest api
    /// </summary>
    public class Library
    {

        /// <summary>
        /// Http client, helps to make requests
        /// </summary>
        private readonly HttpClient _httpClient = new HttpClient();


        /// <summary>
        /// Async method to get list of orders by currency pair
        /// </summary>
        /// <param name="currencyPairs">Currency pair</param>
        /// <returns>Order list object, that contains timestamp, asks and bids lists (if there is an error - error info)</returns>
        public async Task<OrderList> GetOrderListByCurrencyPair(CurrencyPairs currencyPairs)
        {
            var _response = await _httpClient.GetStringAsync(Helpers.GetOrderListUrl(currencyPairs));          
            JObject _root = JObject.Parse(_response);
            var orderList = new OrderList
            {
                Asks = Helpers.GetOrders(_root, Constants.ASKS_LIST_NAME),
                Bids = Helpers.GetOrders(_root, Constants.BIDS_LIST_NAME),
                Error = _root[Constants.ERROR_KEY]?.ToString(),
                Reason = _root[Constants.REASON_KEY]?.ToString(),
                Status = _root[Constants.STATUS_KEY]?.ToString(),
                Timestamp = _root[Constants.TIMESTAMP_KEY].ToString()
            };
            return orderList;
        }
    }
}
