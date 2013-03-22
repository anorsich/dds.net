using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace Dds.Contract
{

    [Route("/solve-game")]
    public class SolveGame : IReturn<SolveGameResponse>
    {
        public string PBN { get; set; }
    }

    public class SolveGameResponse
    {
        public List<TrickResult> Tricks { get; set; }

        public SolveGameResponse()
        {
            Tricks = new List<TrickResult>();
        }
    }



    public class TrickResult
    {
        public List<CardResult> West { get; set; }
        public List<CardResult> East { get; set; }
        public List<CardResult> North { get; set; }
        public List<CardResult> South { get; set; }

        public int Number { get; set; }

        public TrickResult()
        {
            West = new List<CardResult>();
            East = new List<CardResult>();
            South = new List<CardResult>();
            North = new List<CardResult>();
        }

        public List<CardResult> this[string player]
        {
            get
            {
                switch (player.ToUpper())
                {
                    case "W":
                        return West;
                    case "N":
                        return North;
                    case "S":
                        return South;
                    case "E":
                        return East;
                }
                return null;
            }
        }
    }
}