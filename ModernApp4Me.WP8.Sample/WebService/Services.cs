// Copyright (C) 2015 Smart&Soft SAS (http://www.smartnsoft.com/) and contributors
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// Contributors:
//   Smart&Soft - initial API and implementation

using System;
using System.Net.Http;
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
                var request = new RestRequest("forecast?q={q}&mode=json", HttpMethod.Get);
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
