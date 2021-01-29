﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Models.Apis;
using Michaelsoft.ContentManager.Client.Settings;
using Michaelsoft.ContentManager.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Michaelsoft.ContentManager.Client.Services
{
    public class ContentManagerBaseApiService
    {

        private readonly string _serverBasePath;

        protected readonly string ApplicationBasePath;

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IHttpContextAccessor _httpContextAccessor;

        protected ContentManagerBaseApiService(IContentManagerClientSettings settings,
                                               IHttpClientFactory httpClientFactory,
                                               IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _serverBasePath = $"{settings.ServerBasePath}/";
            ApplicationBasePath = $"{settings.ApplicationBasePath}";
        }

        protected async Task<BaseApiResult> GetRequest<T>(string url,
                                                          Dictionary<string, string> queryParams = null) where T : class
        {
            try
            {
                using var client = GetClient();

                if (queryParams != null)
                    url = QueryHelpers.AddQueryString(url, queryParams);

                var uri = new Uri(url, UriKind.Relative);

                var response = await client.GetAsync(uri);

                return await BuildBaseApiResultFromResponse<T>(response);
            }
            catch (Exception ex)
            {
                // TODO: Log exception
                return new BaseApiResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        protected async Task<BaseApiResult> PostRequest<T>(string url,
                                                           dynamic requestObject = null,
                                                           Dictionary<string, string> queryParams = null)
            where T : class
        {
            try
            {
                using var client = GetClient();

                if (queryParams != null)
                    url = QueryHelpers.AddQueryString(url, queryParams);

                var requestContent = requestObject == null
                                         ? null
                                         : new StringContent(JsonConvert.SerializeObject(requestObject),
                                                             Encoding.UTF8,
                                                             "application/json");

                var uri = new Uri(url, UriKind.Relative);

                var response = await client.PostAsync(uri, requestContent);

                return await BuildBaseApiResultFromResponse<T>(response);
            }
            catch (Exception ex)
            {
                // TODO: Log exception
                return new BaseApiResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        protected async Task<BaseApiResult> PutRequest<T>(string url,
                                                          dynamic requestObject,
                                                          Dictionary<string, string> queryParams = null)
            where T : class
        {
            try
            {
                using var client = GetClient();

                if (queryParams != null)
                    url = QueryHelpers.AddQueryString(url, queryParams);

                var requestContent = new StringContent(JsonConvert.SerializeObject(requestObject),
                                                       Encoding.UTF8,
                                                       "application/json");

                var uri = new Uri(url, UriKind.Relative);

                var response = await client.PutAsync(uri, requestContent);

                return await BuildBaseApiResultFromResponse<T>(response);
            }
            catch (Exception ex)
            {
                // TODO: Log exception
                return new BaseApiResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        protected async Task<BaseApiResult> DeleteRequest<T>(string url,
                                                             Dictionary<string, string> queryParams = null)
            where T : class
        {
            try
            {
                using var client = GetClient();

                if (queryParams != null)
                    url = QueryHelpers.AddQueryString(url, queryParams);

                var uri = new Uri(url, UriKind.Relative);

                var response = await client.DeleteAsync(uri);

                return await BuildBaseApiResultFromResponse<T>(response);
            }
            catch (Exception ex)
            {
                // TODO: Log exception
                return new BaseApiResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        private HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri($"{_serverBasePath}");

            string bearer = null;
            _httpContextAccessor.HttpContext?.Request?.Cookies?.TryGetValue("bearer", out bearer);

            if (bearer.IsNullOrEmpty()) return client;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
            return client;
        }

        private async Task<BaseApiResult> BuildBaseApiResultFromResponse<T>(HttpResponseMessage response)
            where T : class
        {
            try
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var json = await response.Content.ReadAsStringAsync();
                var authenticated = response.Headers.TryGetValues("bearer", out var bearers);
                if (authenticated)
                {
                    var bearer = bearers.FirstOrDefault();
                    _httpContextAccessor.HttpContext.Response.Cookies.Append
                        ("bearer", bearer,
                         new CookieOptions {Expires = DateTime.Now.AddDays(1), IsEssential = true});
                }
                else
                {
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete("bearer");
                }

                return new BaseApiResult
                {
                    Success = true,
                    Response = JsonConvert.DeserializeObject<T>(json),
                };
            }
            catch (Exception ex)
            {
                // TODO: Log exception
                return new BaseApiResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

    }
}