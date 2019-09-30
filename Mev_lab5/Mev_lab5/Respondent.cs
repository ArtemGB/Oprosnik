using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mev_lab5
{
  [Serializable]
  class Respondent
  {
    public string RespName { get; set; }
    public string RespSecName { get; set; }
    public List<bool[]> Answs { get; set; }                 //Ответы с вариантами.
    public Dictionary<int, string> FreeAnsws { get; set; } //Ответы в свободной форме.

    public Respondent(string RespName, string RespSecName)
    {
      this.RespName = RespName;
      this.RespSecName = RespSecName;
      Answs = new List<bool[]>();
      FreeAnsws = new Dictionary<int, string>();
    }
  }
}
