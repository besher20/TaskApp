using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todoTrelloApi.Dto
{
    public class Cards
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<CheckList> CheckLists{ get; set; }
    }   
}
