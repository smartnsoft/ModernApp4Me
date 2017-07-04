// The MIT License (MIT)
//
// Copyright (c) 2017 Smart&Soft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Threading.Tasks;
using ModernApp4Me.WP8.Sample.Bo;
using ModernApp4Me.WP8.Sample.Log;
using ModernApp4Me.WP8.WebService;
using ModernApp4Me.WP8.WebServiceCache;
using RestSharp.Portable;

namespace ModernApp4Me.WP8.Sample.WebService
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.05</since>
    public sealed class Services : M4MWebServiceCaller
    {

        public sealed class WeatherBackedWSUriStreamParser : M4MBackedWSUriStringParser<Weather, string, Services>
        {

            protected override RestRequest ComputeRestRequest(string parameter)
            {
                var request = new RestRequest("forecast?q={q}&mode=json", Method.GET);
                request.AddUrlSegment("q", parameter);
                    
                return request;
            }

            protected override Weather Parse(byte[] response)
            {
                return DeserializeObject(response);
            }
        }

        private const string URL_BASE = "http://api.openweathermap.org/data/2.5";

        private static volatile Services instance;

        private static readonly object InstanceLock = new Object();

        private readonly WeatherBackedWSUriStreamParser weatherParser;

        public static Services Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new Services();
                        }
                    }
                }

                return instance;
            }
        }

        private Services()
            : base(Services.URL_BASE)
        {
            weatherParser = new WeatherBackedWSUriStreamParser() { WebServiceCaller = this };
        }

        protected override void Debug(string message)
        {
            if (Logger.Instance.IsDebugEnabled() == true)
            {
                Logger.Instance.Debug(message);
            }
        }

        protected override void Error(string message)
        {
            if (Logger.Instance.IsErrorEnabled() == true)
            {
                Logger.Instance.Error(message);
            }
        }

        public async Task<Weather> GetWeather(string city)
        {
            return await weatherParser.GetValue(city);
        }

        public async Task<Weather> GetRetentionWeather(string city)
        {
            return await weatherParser.GetRetentionValue(true, DateTime.Now.AddMinutes(10), city);
        }

    }
}
