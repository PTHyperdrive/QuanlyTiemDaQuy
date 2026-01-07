using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QuanLyTiemDaQuy.Maui.Services;

/// <summary>
/// Interface for caching service - reduces server queries by caching data locally
/// </summary>
public interface ICacheService
{
    T? Get<T>(string key) where T : class;
    void Set<T>(string key, T value, TimeSpan? ttl = null) where T : class;
    void Invalidate(string key);
    void InvalidateAll();
    bool HasValidCache(string key);
}

/// <summary>
/// In-memory cache service with TTL (Time-To-Live) support
/// </summary>
public class CacheService : ICacheService
{
    private readonly Dictionary<string, CacheEntry> _cache = new();
    private readonly SemaphoreSlim _lock = new(1, 1);
    
    // Default TTL: 5 minutes
    private static readonly TimeSpan DefaultTtl = TimeSpan.FromMinutes(5);

    private class CacheEntry
    {
        public object Value { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsExpired => DateTime.UtcNow > ExpiresAt;
    }

    public T? Get<T>(string key) where T : class
    {
        _lock.Wait();
        try
        {
            if (_cache.TryGetValue(key, out var entry))
            {
                if (!entry.IsExpired)
                {
                    return entry.Value as T;
                }
                _cache.Remove(key);
            }
            return null;
        }
        finally
        {
            _lock.Release();
        }
    }

    public void Set<T>(string key, T value, TimeSpan? ttl = null) where T : class
    {
        _lock.Wait();
        try
        {
            _cache[key] = new CacheEntry
            {
                Value = value,
                ExpiresAt = DateTime.UtcNow + (ttl ?? DefaultTtl)
            };
        }
        finally
        {
            _lock.Release();
        }
    }

    public void Invalidate(string key)
    {
        _lock.Wait();
        try
        {
            _cache.Remove(key);
        }
        finally
        {
            _lock.Release();
        }
    }

    public void InvalidateAll()
    {
        _lock.Wait();
        try
        {
            _cache.Clear();
        }
        finally
        {
            _lock.Release();
        }
    }

    public bool HasValidCache(string key)
    {
        _lock.Wait();
        try
        {
            return _cache.TryGetValue(key, out var entry) && !entry.IsExpired;
        }
        finally
        {
            _lock.Release();
        }
    }
}

/// <summary>
/// Cache keys used throughout the application
/// </summary>
public static class CacheKeys
{
    public const string Products = "products_list";
    public const string Customers = "customers_list";
    public const string StoneTypes = "stone_types";
    public const string Suppliers = "suppliers_list";
    public const string DashboardStats = "dashboard_stats";
}
