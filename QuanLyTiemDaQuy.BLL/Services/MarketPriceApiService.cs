using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using QuanLyTiemDaQuy.DAL.Repositories;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.BLL.Services
{
    /// <summary>
    /// Service để lấy giá thị trường đá quý từ các API bên ngoài
    /// Hỗ trợ: ExchangeRate-API (tỷ giá), OpenFacet (kim cương), Static data (colored gems)
    /// </summary>
    public class MarketPriceApiService
    {
        private readonly MarketPriceRepository _marketPriceRepo;
        private readonly StoneTypeRepository _stoneTypeRepo;
        
        // API URLs
        private const string EXCHANGE_RATE_API_URL = "https://open.er-api.com/v6/latest/USD";
        
        // Cached data
        private static decimal _cachedExchangeRate = 25000m; // Default VND rate
        private static DateTime _lastExchangeRateFetch = DateTime.MinValue;
        private static readonly TimeSpan EXCHANGE_RATE_CACHE_DURATION = TimeSpan.FromHours(24);

        public MarketPriceApiService()
        {
            _marketPriceRepo = new MarketPriceRepository();
            _stoneTypeRepo = new StoneTypeRepository();
        }

        #region Exchange Rate API

        /// <summary>
        /// Lấy tỷ giá USD/VND từ ExchangeRate-API (miễn phí)
        /// </summary>
        public async Task<(bool Success, decimal Rate, string Message)> GetUsdVndRateAsync()
        {
            try
            {
                // Check cache
                if (DateTime.Now - _lastExchangeRateFetch < EXCHANGE_RATE_CACHE_DURATION && _cachedExchangeRate > 0)
                {
                    return (true, _cachedExchangeRate, "Sử dụng tỷ giá đã cache");
                }

                var request = WebRequest.Create(EXCHANGE_RATE_API_URL) as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Timeout = 10000; // 10 seconds timeout

                using (var response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return (false, _cachedExchangeRate, $"API trả về status: {response.StatusCode}");
                    }

                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string json = await reader.ReadToEndAsync();
                        
                        // Parse JSON manually (để tránh dependency với Newtonsoft.Json)
                        decimal vndRate = ParseVndRateFromJson(json);
                        
                        if (vndRate > 0)
                        {
                            _cachedExchangeRate = vndRate;
                            _lastExchangeRateFetch = DateTime.Now;
                            return (true, vndRate, "Lấy tỷ giá thành công từ ExchangeRate-API");
                        }
                    }
                }
                
                return (false, _cachedExchangeRate, "Không thể parse tỷ giá từ response");
            }
            catch (WebException ex)
            {
                return (false, _cachedExchangeRate, $"Lỗi kết nối: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, _cachedExchangeRate, $"Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy tỷ giá đồng bộ (blocking)
        /// </summary>
        public (bool Success, decimal Rate, string Message) GetUsdVndRate()
        {
            return Task.Run(() => GetUsdVndRateAsync()).Result;
        }

        private decimal ParseVndRateFromJson(string json)
        {
            try
            {
                // Tìm "VND": trong JSON và lấy giá trị
                int vndIndex = json.IndexOf("\"VND\"");
                if (vndIndex < 0) return 0;

                int colonIndex = json.IndexOf(":", vndIndex);
                if (colonIndex < 0) return 0;

                int startIndex = colonIndex + 1;
                int endIndex = startIndex;
                
                // Bỏ qua whitespace
                while (startIndex < json.Length && char.IsWhiteSpace(json[startIndex]))
                    startIndex++;
                
                endIndex = startIndex;
                // Tìm end của số
                while (endIndex < json.Length && (char.IsDigit(json[endIndex]) || json[endIndex] == '.'))
                    endIndex++;

                string valueStr = json.Substring(startIndex, endIndex - startIndex);
                if (decimal.TryParse(valueStr, System.Globalization.NumberStyles.Any, 
                    System.Globalization.CultureInfo.InvariantCulture, out decimal rate))
                {
                    return rate;
                }
            }
            catch { }
            
            return 0;
        }

        #endregion

        #region Market Price Fetching

        /// <summary>
        /// Lấy tất cả giá thị trường từ các nguồn
        /// </summary>
        public async Task<MarketPriceApiResult> FetchAllPricesAsync()
        {
            var result = new MarketPriceApiResult
            {
                Success = true,
                FetchedAt = DateTime.Now,
                Prices = new List<GemstoneMarketPriceData>()
            };

            try
            {
                // 1. Lấy tỷ giá USD/VND
                var exchangeResult = await GetUsdVndRateAsync();
                result.ExchangeRateUsdVnd = exchangeResult.Rate;
                
                // 2. Lấy danh sách loại đá từ database
                var stoneTypes = _stoneTypeRepo.GetAll();
                
                // 3. Lấy giá tham khảo cho từng loại đá
                foreach (var stoneType in stoneTypes)
                {
                    var refPrice = GemstoneReferencePrices.GetReferencePrice(stoneType.Name);
                    
                    var priceData = new GemstoneMarketPriceData
                    {
                        StoneTypeId = stoneType.StoneTypeId,
                        StoneTypeName = stoneType.Name,
                        PricePerCaratUsd = refPrice.AvgPrice,
                        PricePerCaratVnd = Math.Round(refPrice.AvgPrice * result.ExchangeRateUsdVnd, 0),
                        Source = "Reference Data (GIA/Gemval)",
                        Notes = refPrice.Notes,
                        LastUpdated = DateTime.Now
                    };
                    
                    result.Prices.Add(priceData);
                }

                result.Message = $"Đã lấy giá cho {result.Prices.Count} loại đá. Tỷ giá: {result.ExchangeRateUsdVnd:N0} VND/USD";
                result.Source = exchangeResult.Success ? "API + Reference Data" : "Reference Data (Offline)";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Lỗi khi lấy giá: {ex.Message}";
            }

            return result;
        }

        /// <summary>
        /// Lấy giá đồng bộ
        /// </summary>
        public MarketPriceApiResult FetchAllPrices()
        {
            return Task.Run(() => FetchAllPricesAsync()).Result;
        }

        #endregion

        #region Database Sync

        /// <summary>
        /// Đồng bộ giá từ API vào database
        /// </summary>
        public (bool Success, string Message) SyncToDatabase(MarketPriceApiResult apiResult)
        {
            if (!apiResult.Success || apiResult.Prices.Count == 0)
            {
                return (false, "Không có dữ liệu để đồng bộ");
            }

            try
            {
                int updated = 0;
                foreach (var price in apiResult.Prices)
                {
                    // Cập nhật giá cơ sở trong bảng GemstoneMarketPrices
                    bool success = _marketPriceRepo.UpdateMarketPrice(price.StoneTypeId, price.PricePerCaratVnd);
                    if (success) updated++;
                    
                    // Lưu lịch sử giá
                    _marketPriceRepo.InsertPriceHistory(price.StoneTypeId, price.PricePerCaratVnd, price.Source);
                }

                return (true, $"Đã cập nhật {updated}/{apiResult.Prices.Count} loại đá thành công");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi đồng bộ vào database: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy và đồng bộ giá từ API
        /// </summary>
        public async Task<(bool Success, string Message)> FetchAndSyncAsync()
        {
            var apiResult = await FetchAllPricesAsync();
            if (!apiResult.Success)
            {
                return (false, apiResult.Message);
            }

            var syncResult = SyncToDatabase(apiResult);
            return syncResult;
        }

        public (bool Success, string Message) FetchAndSync()
        {
            return Task.Run(() => FetchAndSyncAsync()).Result;
        }

        #endregion

        #region Exchange Rate History

        /// <summary>
        /// Lấy tỷ giá hiện tại (từ cache hoặc database)
        /// </summary>
        public decimal GetCurrentExchangeRate()
        {
            if (_cachedExchangeRate > 0 && DateTime.Now - _lastExchangeRateFetch < EXCHANGE_RATE_CACHE_DURATION)
            {
                return _cachedExchangeRate;
            }

            // Thử lấy từ API
            var result = GetUsdVndRate();
            return result.Rate;
        }

        /// <summary>
        /// Lấy giá tham khảo cho một loại đá
        /// </summary>
        public GemstoneMarketPriceData GetReferencePrice(string stoneTypeName, int stoneTypeId = 0)
        {
            var refPrice = GemstoneReferencePrices.GetReferencePrice(stoneTypeName);
            decimal exchangeRate = GetCurrentExchangeRate();

            return new GemstoneMarketPriceData
            {
                StoneTypeId = stoneTypeId,
                StoneTypeName = stoneTypeName,
                PricePerCaratUsd = refPrice.AvgPrice,
                PricePerCaratVnd = Math.Round(refPrice.AvgPrice * exchangeRate, 0),
                Source = "Reference Data",
                Notes = refPrice.Notes,
                LastUpdated = DateTime.Now
            };
        }

        /// <summary>
        /// Lấy lịch sử giá của một loại đá
        /// </summary>
        public List<MarketPriceHistory> GetPriceHistory(int stoneTypeId, int days = 30)
        {
            return _marketPriceRepo.GetPriceHistory(stoneTypeId, days);
        }

        #endregion
    }
}
