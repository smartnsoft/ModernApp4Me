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
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernApp4Me.WP8.Sample.Bo;
using ModernApp4Me.WP8.Sample.Log;
using ModernApp4Me.WP8.WebService;
using ModernApp4Me.WP8.WebServiceCache;
using Newtonsoft.Json;
using RestSharp.Portable;

namespace ModernApp4Me.WP8.Sample.WebService
{

    /// <author>Ludovic ROLAND</author>
    /// <since>2015.03.05</since>
    public sealed class Services : M4MWebServiceCaller
    {

        public sealed class PeopleBackedWSUriStreamParser : M4MBackedWSUriStringParser<List<Person>, Object, Services>
        {

            protected override RestRequest ComputeRestRequest(Object parameter)
            {
                var body = "{\"type\":\"object\",\"properties\":{\"id\":{\"type\":\"integer\",\"ipsum\":\"id\"},\"name\":{\"type\":\"string\",\"ipsum\":\"name\"},\"email\":{\"type\":\"string\",\"format\":\"email\"},\"bio\":{\"type\":\"string\",\"ipsum\":\"sentence\"},\"age\":{\"type\":\"integer\"},\"avatar\":{\"type\":\"string\",\"ipsum\":\"small image\"}}}";
                var request = new RestRequest {Method = HttpMethod.Post};
                request.AddHeader("Accept", "application/json");
                request.Parameters.Clear();
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                    
                return request;
            }

            protected override List<Person> Parse(byte[] response)
            {
                return DeserializeObject(response);
            }

            protected override List<Person> DeserializeObject(byte[] response)
            {
                return JsonConvert.DeserializeObject<List<Person>>(Encoding.UTF8.GetString(response, 0, response.Length));
            }
        }

        private const string URL_BASE = "http://schematic-ipsum.herokuapp.com/?n=50";

        private static volatile Services instance;

        private static readonly object InstanceLock = new Object();

        private readonly PeopleBackedWSUriStreamParser peopleParser;

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
            peopleParser = new PeopleBackedWSUriStreamParser() {WebServiceCaller = this};
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

        public async Task<List<Person>> GetPeople()
        {
            return await peopleParser.GetRetentionValue(true, DateTime.Now.AddHours(1), null);
        }
    }
}
