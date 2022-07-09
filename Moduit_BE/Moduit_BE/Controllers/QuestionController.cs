using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moduit_BE.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Moduit_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ILogger<QuestionController> _logger;

        public QuestionController(ILogger<QuestionController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("one")]
        public async Task<object> QuestionOne()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync("https://screening.moduit.id/backend/question/one");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Item>(content);
                    return data;
                }
                return null;
            }
        }

        [HttpGet]
        [Route("two")]
        public async Task<object> QuestionTwo()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync("https://screening.moduit.id/backend/question/two");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<Item>>(content);
                    var datas = data
                                .FindAll(x => (x.title.Contains("Ergonomic") || x.description.Contains("Ergonomic")) && (x.tags != null && x.tags.Contains("Sports")))
                                .OrderByDescending(x => x.id)
                                .TakeLast(3)
                                .ToList();
                    return datas;
                }
                return null;
            }
        }

        [HttpGet]
        [Route("three")]
        public async Task<object> QuestionThree()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync("https://screening.moduit.id/backend/question/three");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<CategoryItem>>(content);
                    var flattened = data.SelectMany((x) =>
                    {
                        return x.items == null ? new object[] { } : x.items.Select(item => new Item
                        {
                            id = x.id,
                            category = x.category,
                            title = item.title,
                            description = item.description,
                            footer = item.footer,
                            createdAt = x.createdAt
                        }).ToArray();
                    }).ToList();

                    return flattened;
                }
                return null;
            }
        }
    }
}

