using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using todoTrelloApi.Dto;

namespace todoTrelloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet("boards")]
        public async Task<IActionResult> GetBoards()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api.trello.com/1/members/me/boards?key=9a9642ad897bdc7f5aed8a16c4fb0bfc&token=1b9cc73d2a0bbaf715d7087b503798232b6f7baaa8aefb926aa26727cf1ff3ab"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    return new JsonResult(apiResponse);

                }
            }
        }
        [HttpGet("lists")]
        public async Task<IActionResult> GetLists()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api.trello.com/1/boards/60b22da2b35305495d3955b9/lists?key=9a9642ad897bdc7f5aed8a16c4fb0bfc&token=1b9cc73d2a0bbaf715d7087b503798232b6f7baaa8aefb926aa26727cf1ff3ab"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    return new JsonResult(apiResponse);

                }
            }
        }

        [HttpGet("listCardDone")]
        public async Task<IActionResult> GetListCardDone()
        {
            List<string> done = new List<string>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api.trello.com/1/lists/610ae0195290df36ee513d15/cards?key=9a9642ad897bdc7f5aed8a16c4fb0bfc&token=1b9cc73d2a0bbaf715d7087b503798232b6f7baaa8aefb926aa26727cf1ff3ab"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(apiResponse);
                    for (int i = 0; i < json.Count; i++)
                    {
                        dynamic x = json[i];
                        string asd = x["name"];
                        done.Add(asd);
                    }
                    return new JsonResult(done);

                }
            }
        }

        [HttpGet("listCardToDo")]
        public async Task<List<string>> GetListCardToDo()
        {
            List<string> toDo = new List<string>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api.trello.com/1/lists/610ae016034205220a9fc046/cards?key=9a9642ad897bdc7f5aed8a16c4fb0bfc&token=1b9cc73d2a0bbaf715d7087b503798232b6f7baaa8aefb926aa26727cf1ff3ab"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(apiResponse);

                    for (int i = 0; i < json.Count; i++)
                    {
                        dynamic x = json[i];
                        string asd = x["name"];
                        asd = asd + "," + x["id"];
                        toDo.Add(asd);
                    }
                    return toDo;

                }
            }
        }
      
        [HttpGet("cardCheckList")]
        public async Task<List<Cards>> GetCardCheckListToDo(string id)
        {
            List<Cards> toDo = new List<Cards>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api.trello.com/1/cards/"+id+"/checklists?key=9a9642ad897bdc7f5aed8a16c4fb0bfc&token=1b9cc73d2a0bbaf715d7087b503798232b6f7baaa8aefb926aa26727cf1ff3ab"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(apiResponse);

                    for (int i = 0; i < json.Count; i++)
                    {
                        dynamic x = json[i];
                        Cards cards = new Cards
                        {
                            Name = x["name"],
                            CheckLists= new List<CheckList>()
                        };
                        dynamic sq = x["checkItems"];
                        for (int j = 0; j < sq.Count; j++)
                        {
                            CheckList checkList = new CheckList();
                            checkList.Name = sq[j].name;
                            checkList.Status = sq[j].state;
                            cards.CheckLists.Add(checkList);
                        }
                        toDo.Add(cards);
                    }
                    return toDo;

                }
            }
        }


        [HttpPut("doneTask")]
        public async Task<string> SetCardinDoneList(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://api.trello.com/1/cards/"+id+"?idList=610ae0195290df36ee513d15&key=9a9642ad897bdc7f5aed8a16c4fb0bfc&token=1b9cc73d2a0bbaf715d7087b503798232b6f7baaa8aefb926aa26727cf1ff3ab", null))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    return apiResponse;

                }


            }
        }

        [HttpPost("addTask")]
        public async Task<string> AddTask(string name)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://api.trello.com/1/cards?key=9a9642ad897bdc7f5aed8a16c4fb0bfc&token=1b9cc73d2a0bbaf715d7087b503798232b6f7baaa8aefb926aa26727cf1ff3ab&idList=610ae016034205220a9fc046&name="+name+"",null)
)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    return apiResponse;

                }
            }
        }

    }
}
