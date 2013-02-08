using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dds.Net.Dto;
using Dds.Net.Integration;
using DealPbn = Dds.Net.Dto.DealPbn;
using FutureTricks = Dds.Net.Dto.FutureTricks;

namespace Dds.Net
{
    public class DdsConnect
    {
        public Dto.FutureTricks SolveBoardPbn(DealPbn dealPbn, int target, int solutions, int mode)
        {
            if (dealPbn.CurrentTrickCards.Count > 3)
                throw new ArgumentException("Invalid current trick cards count", "dealPbn.CurrentTrickCards");

            var deal = new Integration.DealPbn();
            deal.trump = (int)dealPbn.Trump;
            deal.first = (int)dealPbn.TrickDealer;

            deal.currentTrickRank = new int[3];
            deal.currentTrickSuit = new int[3];
            for (int i = 0; i < dealPbn.CurrentTrickCards.Count; i++)
            {
                deal.currentTrickRank[i] = (int)dealPbn.CurrentTrickCards[i].Rank;
                deal.currentTrickSuit[i] = (int)dealPbn.CurrentTrickCards[i].Suit;
            }
            deal.remainCards = DdsHelper.PbnStringToChars(dealPbn.RemainCardsPbn);

            var ddsResult = new Integration.FutureTricks();

            //TODO: Support mutiple threads.
            DdsImport.SolveBoardPBN(deal, target, solutions, mode, ref ddsResult, 0);

            var result = new FutureTricks();
            result.Cards = ddsResult.cards;
            result.Nodes = ddsResult.nodes;
            for (int i = 0; i < 13; i++)
            {

                if (ddsResult.rank[i] != 0)
                {
                    Rank rank = (Rank)ddsResult.rank[i];
                    Suit suit = (Suit)ddsResult.suit[i];

                    result.FutureCards.Add(new Card(rank, suit));
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        //TODO: Wrap args and make it public if needed.
        /// <summary>
        /// Custom wrapper for the DDS function "CalcDDtablePBN". Takes a char array containing a PBN code, and you must provide
        /// a writable reference to the results struct.
        /// </summary>
        /// <param name="PBNCode"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private static int CalculateMakeableContracts(string pbn, out DdTableResults results)
        {
            // instantiate deal struct
            DdTableDealPbn tdPBN = new DdTableDealPbn(DdsHelper.PbnStringToChars(pbn));
            // instantiate resTable array
            results.resTable = new int[20];
            // call the external function in DDS.dll, if it fails, it will return something other than 1.
            // The various returns and their values can be found in Bo Haglund's documentation.
            if (DdsImport.CalcDDtablePBN(tdPBN, ref results) == 1)
            {
                int[] resTable = results.resTable;
                // the CalcDDtablePBN function returns the number of TRICKS the player can make, not the contract, so we take 6 from each result.
                for (int ind = 0; ind < resTable.Length; ind++)
                {
                    resTable[ind] = resTable[ind] - 6;
                    if (resTable[ind] < 0)
                        resTable[ind] = 0;
                }
                // map resTable values to proper locations
                /*
                 * This is where you I mapped the values from the array in a way I could easily follow.
                 * I've done this in the Form code, it's long and tedious but it's just to show you how the array is structured.
                 * See "DDS_Data_Structures.cs" for an index map of the resTable array.
                 */
                return 1;
            }
            return 0;
        }
    }
}
